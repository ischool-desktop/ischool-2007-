using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API
{
    internal class CategoryInfo:Customization.Data.CategoryInfo
    {
        private string _Name;

        private string _SubCategory;

        public CategoryInfo(TagManage.TagInfo tag)
        {
            if (tag.Prefix == "")
            {
                _Name = tag.Name;
                _SubCategory = "";
            }
            else
            {
                _Name = tag.Prefix;
                _SubCategory = tag.Name;
            }
        }

        #region CategoryInfo 成員

        public string FullName
        {
            get
            {
                return _Name + (_SubCategory == "" ? "" : (":" + _SubCategory));
            }
        }

        public string Name
        {
            get { return _Name; }
        }

        public string SubCategory
        {
            get { return _SubCategory; }
        }

        #endregion
    }
}
