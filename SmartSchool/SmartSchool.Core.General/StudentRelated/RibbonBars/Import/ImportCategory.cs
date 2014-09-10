using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Customization.Data;
using SmartSchool.API.PlugIn;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated.RibbonBars.Import
{
    [FeatureCode("Button0285")]
    class ImportCategory:API.PlugIn.Import.Importer
    {
        #region Importer 成員

        public override  string Description
        {
            get { return ""; }
        }

        public override System.Drawing.Image Image
        {
            get { return null; }
        }

        public override void InitializeImport(SmartSchool.API.PlugIn.Import.ImportWizard wizard)
        {
            wizard.PackageLimit = 200;

            SortedList<string, SortedList<string, int>> _CategoryMapping = new SortedList<string, SortedList<string, int>>();
            List<string> changedList = new List<string>();
            #region 抓現有的學生相關類別
            foreach ( XmlElement tagElement in SmartSchool.Feature.Tag.QueryTag.GetDetailList(SmartSchool.Feature.Tag.TagCategory.Student).SelectNodes("Tag") )
            {
                int id = int.Parse(tagElement.GetAttribute("ID"));
                string name = "";
                string prefix = "";
                if ( tagElement.SelectSingleNode("Prefix") != null )
                    prefix = tagElement.SelectSingleNode("Prefix").InnerText;
                if ( tagElement.SelectSingleNode("Name") != null )
                    name = tagElement.SelectSingleNode("Name").InnerText;
                if ( prefix == "" )
                {
                    if ( !_CategoryMapping.ContainsKey(name) )
                        _CategoryMapping.Add(name, new SortedList<string, int>());
                    _CategoryMapping[name].Add(name, id);
                }
                else
                {
                    if ( !_CategoryMapping.ContainsKey(prefix) )
                        _CategoryMapping.Add(prefix, new SortedList<string, int>());
                    _CategoryMapping[prefix].Add(name, id);
                }
            }
            #endregion
            //依載入的資料篩選匯入欄位
            #region 依載入的資料篩選匯入欄位
            wizard.LoadSource += delegate(object sender, SmartSchool.API.PlugIn.Import.LoadSourceEventArgs e)
                {
                    #region 設訂匯入欄位
                    wizard.ImportableFields.Clear();
                    foreach ( string field in e.Fields )
                    {
                        if ( _CategoryMapping.ContainsKey(field) )
                            wizard.ImportableFields.Add(field);
                    }
                    #endregion
                }; 
            #endregion
            //驗證匯入資料
            #region 驗證匯入資料
            wizard.ValidateRow += delegate(object sender, SmartSchool.API.PlugIn.Import.ValidateRowEventArgs e)
                {
                    #region 驗證匯入的資料都是已存在的類別
                    foreach ( string field in e.SelectFields )
                    {
                        if ( e.Data[field] != "" )
                        {
                            string missingMapping = "";
                            foreach ( string value in e.Data[field].Split('、') )
                            {
                                if ( !_CategoryMapping[field].ContainsKey(value) )
                                    missingMapping = ( missingMapping == "" ? "" : missingMapping + "、" ) + value;
                            }
                            if ( missingMapping != "" )
                            {
                                missingMapping += "不是一個已存在的類別";
                                e.ErrorFields.Add(field, missingMapping);
                            }
                        }
                    }
                    #endregion
                }; 
            #endregion
            //匯入資料
            #region 匯入資料
            AccessHelper helper = new AccessHelper();
            wizard.ImportPackage += delegate(object sender, SmartSchool.API.PlugIn.Import.ImportPackageEventArgs e)
            {
                SortedList<int, List<int>> removeTags = new SortedList<int, List<int>>();//TagID ---* StudentID
                SortedList<int, List<int>> insertTags = new SortedList<int, List<int>>();//TagID ---* StudentID
                #region 整理要新增及刪除的清單
                foreach ( RowData row in e.Items )
                {
                    StudentRecord studentRec = helper.StudentHelper.GetStudent(row.ID);
                    foreach ( string field in e.ImportFields )
                    {
                        #region 整理這學生在這個類別中貼的標簽
                        List<string> removeCategorys = new List<string>();
                        foreach ( CategoryInfo categoryInfo in studentRec.StudentCategorys )
                        {
                            if ( categoryInfo.Name == field )
                                removeCategorys.Add(categoryInfo.SubCategory == "" ? categoryInfo.Name : categoryInfo.SubCategory);
                        }
                        #endregion
                        if ( row[field] != "" )
                        {
                            #region 掃描匯入資料
                            foreach ( string value in row[field].Split('、') )
                            {
                                if ( removeCategorys.Contains(value) )
                                    removeCategorys.Remove(value);//本來就有的就不處理也不用刪除
                                else
                                {
                                    //新增標簽
                                    if ( !insertTags.ContainsKey(_CategoryMapping[field][value]) )
                                        insertTags.Add(_CategoryMapping[field][value], new List<int>());
                                    insertTags[_CategoryMapping[field][value]].Add(int.Parse(row.ID));
                                    if ( !changedList.Contains(row.ID) ) changedList.Add(row.ID);
                                }
                            }
                            #endregion
                        }
                        #region 移除這學生在這個類別中要刪除的標簽
                        foreach ( string remove in removeCategorys )
                        {
                            if ( !removeTags.ContainsKey(_CategoryMapping[field][remove]) )
                                removeTags.Add(_CategoryMapping[field][remove], new List<int>());
                            removeTags[_CategoryMapping[field][remove]].Add(int.Parse(row.ID));
                            if ( !changedList.Contains(row.ID) ) changedList.Add(row.ID);
                        }
                        #endregion
                    }
                }
                #endregion
                foreach ( int tagid in removeTags.Keys )
                {
                    SmartSchool.Feature.Tag.EditStudentTag.Remove(removeTags[tagid], tagid);
                }
                foreach ( int tagid in insertTags.Keys )
                {
                    SmartSchool.Feature.Tag.EditStudentTag.Add(insertTags[tagid], tagid);
                }
            }; 
            #endregion
            //匯入完成通報資料變更
            wizard.ImportComplete += delegate
            {
                SmartSchool.Broadcaster.Events.Items["學生/資料變更"].Invoke(changedList.ToArray());
            };
        }

        public override string Path
        {
            get { return ""; }
        }

        public override string Text
        {
            get { return "匯入學生類別"; }
        }

        #endregion
    }
}
