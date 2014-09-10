using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.AccessControl
{
    public struct FeatureAce
    {
        public FeatureAce(string code, string accessString)
        {
            _code = code;

            try
            {
                _permAccess = Parse(accessString);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("某些權限設定值不支援。(" + accessString + ")", ex.Message);
            }
        }

        internal FeatureAce(string code, AccessOptions access)
        {
            _code = code.Trim();
            _permAccess = access;
        }

        private string _code;
        /// <summary>
        /// 功能代碼。
        /// </summary>
        public string FeatureCode
        {
            get { return _code; }
        }

        private AccessOptions _permAccess;
        public AccessOptions Access
        {
            get { return _permAccess; }
        }

        /// <summary>
        /// 是否有檢視權限。
        /// </summary>
        public bool Viewable
        {
            get { return IsSupersetOf(AccessOptions.View); }
        }

        /// <summary>
        /// 是否有編輯權限(包含了檢視)。
        /// </summary>
        public bool Editable
        {
            get { return IsSupersetOf(AccessOptions.Edit); }
        }

        /// <summary>
        /// 是否有執行權限。
        /// </summary>
        public bool Executable
        {
            get { return IsSupersetOf(AccessOptions.Execute); }
        }

        internal bool IsSupersetOf(AccessOptions permission)
        {
            return ((_permAccess & permission) == permission);
        }

        /// <summary>
        /// 代表沒有任何資訊的物件。
        /// </summary>
        public static FeatureAce Null
        {
            get { return new FeatureAce(string.Empty, AccessOptions.None); }
        }

        public static AccessOptions Parse(string permString)
        {
            return (AccessOptions)Enum.Parse(typeof(AccessOptions), permString, true);
        }
    }
}