using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data.StudentExtension
{
    /// <summary>
    /// 延伸欄位資料
    /// </summary>
    public class ExtensionFieldCollection
    {
        private SortedList<string, SortedList<string, string>> _FieldValues = new SortedList<string, SortedList<string, string>>();
        
        /// <summary>
        /// 取得延伸欄位值
        /// </summary>
        /// <param name="nameSpace">命名空間</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns>值</returns>
        public string this[string nameSpace, string fieldName]
        {
            get
            {
                lock ( _FieldValues )
                {
                    if ( _FieldValues.ContainsKey(nameSpace) && _FieldValues[nameSpace].ContainsKey(fieldName) )
                    {
                        return _FieldValues[nameSpace][fieldName];
                    }
                }
                return "";
            }
            internal set
            {
                lock ( _FieldValues )
                {
                    if ( !_FieldValues.ContainsKey(nameSpace) )
                        _FieldValues.Add(nameSpace, new SortedList<string, string>());
                    if ( _FieldValues[nameSpace].ContainsKey(fieldName) )
                    {
                        if ( !string.IsNullOrEmpty(value) )
                            _FieldValues[nameSpace][fieldName] = value;
                        else
                        {
                            _FieldValues[nameSpace].Remove(fieldName);
                            if ( _FieldValues[nameSpace].Count == 0 )
                                _FieldValues.Remove(nameSpace);
                        }
                    }
                    else
                        _FieldValues[nameSpace].Add(fieldName, value);
                }
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        internal void Clear()
        {
            lock ( _FieldValues )
            {
                foreach ( SortedList<string, string> var in _FieldValues.Values )
                {
                    var.Clear();
                }
                _FieldValues.Clear();
            }
        }
        /// <summary>
        /// 清空特定NameSpace內的值
        /// </summary>
        internal void Clear(string nameSpace)
        {
            lock ( _FieldValues )
            {
                if ( _FieldValues.ContainsKey(nameSpace) )
                    _FieldValues[nameSpace].Clear();
            }
        }
        ///// <summary>
        ///// 填入延伸欄位
        ///// </summary>
        ///// <param name="nameSpace">命名空間</param>
        ///// <param name="fields">欄位名稱</param>
        //void Fill(string nameSpace, params string[] fields);
    }
}
