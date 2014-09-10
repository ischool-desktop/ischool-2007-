using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn.ImportExport;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using IntelliSchool.DSA30.Util;
using System.Xml;
using SmartSchool.AccessControl;

namespace SmartSchool.GovernmentalDocument.ImportExport
{
    [FeatureCode("Button0280")]
    class ImportTransferSchoolStudentsUpdateRecord:AbstractImportUpdateRecord
    {
        protected override string[] Fields
        {
            get { return new string[] { "異動科別", "年級", "異動學號", "異動姓名", "身份證號", "性別", "生日", "異動代碼", "異動日期", "原因及事項", "轉入前學生資料-科別", "轉入前學生資料-年級", "轉入前學生資料-學校", "轉入前學生資料-(備查日期)", "轉入前學生資料-(備查文號)", "轉入前學生資料-學號", "核準日期", "核準文號", "備註" }; }
        }

        protected override string Type
        {
            get { return "轉入異動"; }
        }

        protected override string[] InsertRequiredFields
        {
            get { return new string[] { "異動代碼", "異動日期", "年級" }; }
        }
    }
    //class ImportTransferSchoolStudentsUpdateRecord : ImportProcess
    //{
    //    private AccessHelper _AccessHelper = new AccessHelper();

    //    private Dictionary<string, SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo> _NewStudentUpdateRecordInfoList = new Dictionary<string, SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>();

    //    //取得代碼對照表
    //    Dictionary<string, string> updateCodeMapping = null;

    //    private List<string> _DateFields = new List<string>(new string[] { "核準日期", "異動日期", "生日", "備查日期", "轉入前學生資料-(備查日期)" });

    //    private List<string> _NonNullFields = new List<string>(new string[] { "異動日期", "異動代碼" });

    //    public ImportTransferSchoolStudentsUpdateRecord()
    //    {
    //        this.Image = null;
    //        this.Title = "匯入轉入異動";
    //        this.Group = "學籍基本資料";
    //        this.PackageLimit = 500;
    //        //"異動科別", "年級", "異動學號", "異動姓名", "身份證號", "性別", "生日", "異動代碼", "異動日期", "原因及事項", "新學號", "轉入前學生資料-科別", "轉入前學生資料-年級", "轉入前學生資料-學校", "轉入前學生資料-(備查日期)", "轉入前學生資料-(備查文號)", "轉入前學生資料-學號", "入學資格-畢業國中", "入學資格-畢業國中所在地代碼", "最後異動代碼", "畢(結)業證書字號", "備查日期", "備查文號", "核準日期", "核準文號", "備註"
    //        foreach ( string field in new string[] { "異動科別", "年級", "異動學號", "異動姓名", "身份證號", "性別", "生日", "異動代碼", "異動日期", "原因及事項", "轉入前學生資料-科別", "轉入前學生資料-年級", "轉入前學生資料-學校", "轉入前學生資料-(備查日期)", "轉入前學生資料-(備查文號)", "轉入前學生資料-學號", "核準日期", "核準文號", "備註" } )
    //        {
    //            this.ImportableFields.Add(field);
    //        }
    //        foreach ( string field in new string[] { "異動代碼" } )
    //        {
    //            this.RequiredFields.Add(field);
    //        }
    //        this.PackageLimit = 250;
    //        this.BeginValidate += new EventHandler<BeginValidateEventArgs>(ImportNewStudentsUpdateRecord_BeginValidate);
    //        this.RowDataValidated += new EventHandler<RowDataValidatedEventArgs>(ImportNewStudentsUpdateRecord_RowDataValidated);
    //        this.DataImport += new EventHandler<DataImportEventArgs>(ImportNewStudentsUpdateRecord_DataImport);
    //        this.EndImport += new EventHandler(ImportNewStudentsUpdateRecord_EndImport);
    //    }

    //    void ImportNewStudentsUpdateRecord_BeginValidate(object sender, BeginValidateEventArgs e)
    //    {
    //        #region 整理異動代號及類別對照
    //        XmlElement updateCodeMappingElement = SmartSchool.Feature.Basic.Config.GetUpdateCodeSynopsis().GetContent().BaseElement;
    //        updateCodeMapping = new Dictionary<string, string>();
    //        foreach ( XmlNode var in updateCodeMappingElement.SelectNodes("異動") )
    //        {
    //            string UpdateCode, UpdateType;
    //            UpdateCode = var.SelectSingleNode("代號").InnerText;
    //            UpdateType = var.SelectSingleNode("分類").InnerText;
    //            if ( !updateCodeMapping.ContainsKey(UpdateCode) )
    //            {
    //                updateCodeMapping.Add(UpdateCode, UpdateType);
    //            }
    //        }
    //        #endregion



    //        _AccessHelper = new AccessHelper();
    //        _NewStudentUpdateRecordInfoList = new Dictionary<string, SmartSchool.Customization.Data.StudentExtension.UpdateRecordInfo>();

    //        List<StudentRecord> students = _AccessHelper.StudentHelper.GetStudents(e.List);

    //        Common.MultiThreadWorker<StudentRecord> mworker = new SmartSchool.Common.MultiThreadWorker<StudentRecord>();
    //        mworker.PackageSize = 250;
    //        mworker.MaxThreads = 3;
    //        mworker.PackageWorker += delegate(object s, SmartSchool.Common.PackageWorkEventArgs<StudentRecord> e1)
    //        {
    //            _AccessHelper.StudentHelper.FillUpdateRecord(e1.List);
    //        };
    //        mworker.Run(students);

    //        foreach ( StudentRecord stu in students )
    //        {
    //            if ( !_NewStudentUpdateRecordInfoList.ContainsKey(stu.StudentID) )
    //                _NewStudentUpdateRecordInfoList.Add(stu.StudentID, null);
    //            else
    //                _NewStudentUpdateRecordInfoList[stu.StudentID] = null;
    //            foreach ( UpdateRecordInfo uinfo in stu.UpdateRecordList )
    //            {
    //                if ( uinfo.UpdateRecordType == "轉入異動" )
    //                {
    //                    _NewStudentUpdateRecordInfoList[stu.StudentID] = uinfo;
    //                    break;
    //                }
    //            }
    //        }
    //    }

    //    void ImportNewStudentsUpdateRecord_RowDataValidated(object sender, RowDataValidatedEventArgs e)
    //    {
    //        foreach ( string field in e.SelectFields )
    //        {
    //            #region 如果是異動代號則檢查輸入代號是否在清單中
    //            if ( field == "異動代碼" )
    //            {
    //                if ( updateCodeMapping.ContainsKey(e.Data[field]) == false || updateCodeMapping[e.Data[field]] != "轉入異動" )
    //                {
    //                    e.ErrorFields.Add(field, "輸入的代號不在指定的轉入異動代號清單中。");
    //                    continue;
    //                }
    //            }
    //            #endregion
    //            #region 如果是日期欄位檢查輸入值
    //            if ( _DateFields.Contains(field) )
    //            {
    //                if ( e.Data[field] == "" && _NonNullFields.Contains(e.Data[field]) )
    //                {
    //                    e.ErrorFields.Add(field, "此欄為必填欄位，請輸入西元年/月/日。");
    //                    continue;
    //                }
    //                else
    //                {
    //                    if ( e.Data[field] != "" )
    //                    {
    //                        //檢查欄位值
    //                        if ( !CheckIsDate(e.Data[field]) )
    //                        {
    //                            if ( _NonNullFields.Contains(field) )
    //                            {
    //                                e.ErrorFields.Add(field, "此欄為必填欄位，\n請依照\"西元年/月/日\"格式輸入。");
    //                            }
    //                            else
    //                            {
    //                                e.ErrorFields.Add(field, "輸入格式錯誤，請輸入西元年/月/日。\n此筆錯誤資料將不會被儲存");
    //                            }
    //                            continue;
    //                        }
    //                    }
    //                }
    //            }
    //            #endregion
    //            #region 如果是必填欄位檢查非空值
    //            if ( _NonNullFields.Contains(field) && e.Data[field] == "" )
    //            {
    //                e.ErrorFields.Add(field, "此欄位必須填寫，不允許空值");
    //                continue;
    //            }
    //            #endregion
    //            #region 如果是年級則檢查輸入資料
    //            if ( field == "年級" )
    //            {
    //                int i = 0;
    //                if ( e.Data[field] != "延修生" && ( !int.TryParse(e.Data[field], out i) || i <= 0 ) )
    //                {
    //                    e.ErrorFields.Add(field, "年級欄必需依以下格式填寫：\n\t1.若為一般學生請填入學生年級。\n\t2.若為延修生請填入\"延修生\"");
    //                    continue;
    //                }
    //            }
    //            #endregion
    //        }
    //    }

    //    void ImportNewStudentsUpdateRecord_DataImport(object sender, DataImportEventArgs e)
    //    {
    //        Dictionary<string, string> fieldNameMapping = new Dictionary<string, string>();
    //        #region 建立匯入欄位名稱跟Xml內欄位對照表
    //        fieldNameMapping.Add("Department", "異動科別");
    //        fieldNameMapping.Add("GradeYear", "年級");
    //        fieldNameMapping.Add("StudentNumber", "異動學號");
    //        fieldNameMapping.Add("Name", "異動姓名");
    //        fieldNameMapping.Add("IDNumber", "身份證號");
    //        fieldNameMapping.Add("Gender", "性別");
    //        fieldNameMapping.Add("Birthdate", "生日");
    //        fieldNameMapping.Add("UpdateRecordType", "異動種類");
    //        fieldNameMapping.Add("UpdateCode", "異動代碼");
    //        fieldNameMapping.Add("UpdateDate", "異動日期");
    //        fieldNameMapping.Add("UpdateDescription", "原因及事項");
    //        fieldNameMapping.Add("NewStudentNumber", "新學號");
    //        fieldNameMapping.Add("PreviousDepartment", "轉入前學生資料-科別");
    //        fieldNameMapping.Add("PreviousGradeYear", "轉入前學生資料-年級");
    //        fieldNameMapping.Add("PreviousSchool", "轉入前學生資料-學校");
    //        fieldNameMapping.Add("PreviousSchoolLastADDate", "轉入前學生資料-(備查日期)");
    //        fieldNameMapping.Add("PreviousSchoolLastADNumber", "轉入前學生資料-(備查文號)");
    //        fieldNameMapping.Add("PreviousStudentNumber", "轉入前學生資料-學號");
    //        fieldNameMapping.Add("GraduateSchool", "入學資格-畢業國中");
    //        fieldNameMapping.Add("GraduateSchoolLocationCode", "入學資格-畢業國中所在地代碼");
    //        fieldNameMapping.Add("LastUpdateCode", "最後異動代碼");
    //        fieldNameMapping.Add("GraduateCertificateNumber", "畢(結)業證書字號");
    //        fieldNameMapping.Add("LastADDate", "備查日期");
    //        fieldNameMapping.Add("LastADNumber", "備查文號");
    //        fieldNameMapping.Add("ADDate", "核準日期");
    //        fieldNameMapping.Add("ADNumber", "核準文號");
    //        fieldNameMapping.Add("Comment", "備註");
    //        #endregion
    //        DSXmlHelper inserthelper = new DSXmlHelper("InsertRequest");
    //        DSXmlHelper updatehelper = new DSXmlHelper("UpdateRequest");
    //        bool insert = false, update = false;
    //        foreach ( RowData row in e.Items )
    //        {
    //            if ( _NewStudentUpdateRecordInfoList.ContainsKey(row.ID) )
    //            {
    //                if ( _NewStudentUpdateRecordInfoList[row.ID] == null )
    //                {
    //                    insert = true;
    //                    #region 新增
    //                    inserthelper.AddElement("UpdateRecord");
    //                    inserthelper.AddElement("UpdateRecord", "Field");
    //                    inserthelper.AddElement("UpdateRecord/Field", "RefStudentID", row.ID);
    //                    inserthelper.AddElement("UpdateRecord/Field", "ContextInfo");

    //                    XmlDocument contextInfo = new XmlDocument();
    //                    XmlElement root = contextInfo.CreateElement("ContextInfo");
    //                    contextInfo.AppendChild(root);
    //                    foreach ( string field in fieldNameMapping.Keys )
    //                    {
    //                        string fieldname = fieldNameMapping[field];
    //                        // 如果是 Previous 開頭的全丟到 ContextInfo 中
    //                        if ( field.StartsWith("Previous") || field.StartsWith("Graduate") || field == "NewStudentNumber" || field == "LastUpdateCode" )
    //                        {
    //                            XmlNode importNode = contextInfo.CreateElement(field);
    //                            importNode.InnerText = row.ContainsKey(fieldname) ? row[fieldname] : "";
    //                            root.AppendChild(importNode);
    //                        }
    //                        else
    //                        {
    //                            inserthelper.AddElement("UpdateRecord/Field", field, row.ContainsKey(fieldname) ? row[fieldname] : "");
    //                        }
    //                    }
    //                    // 將 contextInfo 這個 document 的資料塞進 ContextInfo 的 CdataSection 裡
    //                    inserthelper.AddXmlString("UpdateRecord/Field/ContextInfo", root.OuterXml);
    //                    #endregion
    //                }
    //                else
    //                {
    //                    update = true;
    //                    #region 修改
    //                    XmlElement oldElement = _NewStudentUpdateRecordInfoList[row.ID].Detail;

    //                    updatehelper.AddElement("UpdateRecord");
    //                    updatehelper.AddElement("UpdateRecord", "Field");
    //                    updatehelper.AddElement("UpdateRecord/Field", "ContextInfo");
    //                    updatehelper.AddElement("UpdateRecord", "Condition");
    //                    updatehelper.AddElement("UpdateRecord/Condition", "ID", oldElement.GetAttribute("ID"));

    //                    XmlDocument contextInfo = new XmlDocument();
    //                    XmlElement root = contextInfo.CreateElement("ContextInfo");
    //                    contextInfo.AppendChild(root);
    //                    foreach ( string field in fieldNameMapping.Keys )
    //                    {
    //                        string fieldname = fieldNameMapping[field];
    //                        // 如果是 Previous 開頭的全丟到 ContextInfo 中
    //                        if ( field.StartsWith("Previous") || field.StartsWith("Graduate") || field == "NewStudentNumber" || field == "LastUpdateCode" )
    //                        {
    //                            XmlNode importNode = contextInfo.CreateElement(field);
    //                            importNode.InnerText = row.ContainsKey(fieldname) ? row[fieldname] : ( oldElement.SelectSingleNode("ContextInfo/" + field) == null ? "" : oldElement.SelectSingleNode("ContextInfo/" + field).InnerText );
    //                            root.AppendChild(importNode);
    //                        }
    //                        else
    //                        {
    //                            updatehelper.AddElement("UpdateRecord/Field", field, row.ContainsKey(fieldname) ? row[fieldname] : ( oldElement.SelectSingleNode(field) == null ? "" : oldElement.SelectSingleNode(field).InnerText ));
    //                        }
    //                    }
    //                    // 將 contextInfo 這個 document 的資料塞進 ContextInfo 的 CdataSection 裡
    //                    updatehelper.AddXmlString("UpdateRecord/Field/ContextInfo", root.OuterXml);
    //                    #endregion
    //                }
    //            }
    //        }
    //        if ( insert )
    //            SmartSchool.Feature.EditStudent.InsertUpdateRecord(new DSRequest(inserthelper));
    //        if ( update )
    //            SmartSchool.Feature.EditStudent.ModifyUpdateRecord(new DSRequest(updatehelper));
    //    }

    //    void ImportNewStudentsUpdateRecord_EndImport(object sender, EventArgs e)
    //    {
    //        SmartSchool.Customization.PlugIn.Global.SetStatusBarMessage("轉入異動匯入完成。");
    //    }


    //    private bool CheckIsDate(string text)
    //    {
    //        if ( text.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).Length != 3 )
    //            return false;
    //        DateTime d = DateTime.Now;
    //        return DateTime.TryParse(text, out  d);
    //    }
    //}
}
