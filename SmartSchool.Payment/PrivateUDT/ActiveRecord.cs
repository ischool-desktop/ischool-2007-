using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Private.UDT
{
    /// <summary>
    /// 資料變更狀態
    /// </summary>
    public enum RecordStatus { Insert, Update, Delete, NoChange }
    /// <summary>
    /// 可編輯的資料
    /// </summary>
    public abstract class ActiveRecord
    {
        private Dictionary<string, object> _FieldValues = new Dictionary<string, object>();
        internal void ResetFieldValues()
        {
            _FieldValues.Clear();
            foreach ( var item in fields )
            {
                if ( !_FieldValues.ContainsKey(item.Name) )
                {
                    _FieldValues.Add(item.Name, item.Target.GetValue(this, null));
                }
            }
        }
        internal List<FieldInfo> fields = new List<FieldInfo>();
        /// <summary>
        /// 傳回資料表名稱，預設是TypeName。如資料表名稱與TypeName不同時可覆寫此方法，或利用TableNameAttribute
        /// </summary>
        public virtual string TableName()
        {
            foreach ( var item in this.GetType().GetCustomAttributes(typeof(TableNameAttribute), true) )
            {
                return ( (TableNameAttribute)item ).TableName;
            }
            return this.GetType().Name;
        }
        /// <summary>
        /// 取得資料編號(PrimaryKey)
        /// </summary>
        public string UID { get; internal set; }
        /// <summary>
        /// 取得或設定，指出是否刪除這筆資料(Deleted為true後呼叫Save()才會真的從資料庫刪除)
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 取得資料變更狀態
        /// </summary>
        public RecordStatus RecordStatus
        {
            get
            {
                if ( UID == "" )
                {
                    if ( Deleted ) return RecordStatus.NoChange;
                    else return RecordStatus.Insert;
                }
                else
                {
                    if ( Deleted )
                        return RecordStatus.Delete;
                    else
                    {
                        foreach ( var item in fields )
                        {
                            if ( _FieldValues[item.Name] != null )
                            {
                                if ( !_FieldValues[item.Name].Equals(item.Target.GetValue(this, null)) )
                                    return RecordStatus.Update;
                            }
                            else
                                if ( item.Target.GetValue(this, null) != null )
                                    return RecordStatus.Update;
                        }
                        return RecordStatus.NoChange;
                    }
                }
            }
        }
        /// <summary>
        /// 建構子
        /// </summary>
        public ActiveRecord()
        {
            UID = "";
            Deleted = false;
            foreach ( var properites in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) )
            {
                if ( properites.CanRead && properites.CanWrite )//可讀寫
                {
                    if (//是可支援的型別
                        //Boolean
                        properites.PropertyType == typeof(bool?) ||
                        properites.PropertyType == typeof(bool) ||
                        //String
                        properites.PropertyType == typeof(string) ||
                        //Number
                        properites.PropertyType == typeof(int?) ||
                        properites.PropertyType == typeof(int) ||
                        properites.PropertyType == typeof(float?) ||
                        properites.PropertyType == typeof(float) ||
                        properites.PropertyType == typeof(double?) ||
                        properites.PropertyType == typeof(double) ||
                        properites.PropertyType == typeof(decimal?) ||
                        properites.PropertyType == typeof(decimal) ||
                        //DataTime
                        properites.PropertyType == typeof(DateTime?) ||
                        properites.PropertyType == typeof(DateTime)
                        )
                    {
                        foreach ( var item in properites.GetCustomAttributes(typeof(FieldAttribute), true) )//有貼上Field標籤
                        {
                            var fieldName = ( (FieldAttribute)item ).Field;
                            if ( fieldName == null )
                                fieldName = properites.Name;
                            //fields.Add(new FieldInfo() {  });
                            DataType type = DataType.String;
                            if ( properites.PropertyType == typeof(bool?) ||
                                properites.PropertyType == typeof(bool) )
                                type = DataType.Boolean;
                            if ( properites.PropertyType == typeof(string) )
                                type = DataType.String;
                            if ( properites.PropertyType == typeof(int?) ||
                                properites.PropertyType == typeof(int) ||
                                properites.PropertyType == typeof(float?) ||
                                properites.PropertyType == typeof(float) ||
                                properites.PropertyType == typeof(double?) ||
                                properites.PropertyType == typeof(double) ||
                                properites.PropertyType == typeof(decimal?) ||
                                properites.PropertyType == typeof(decimal) )
                                type = DataType.Number;
                            if ( properites.PropertyType == typeof(DateTime?) ||
                                properites.PropertyType == typeof(DateTime) )
                                type = DataType.DateTime;
                            fields.Add(new FieldInfo() { Type = type, Name = fieldName, Target = properites, Indexed = ( (FieldAttribute)item ).Indexed, Instance = this });
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 儲存變更，如果是新增資料，新資料的UID將自動填入
        /// </summary>
        public void Save()
        {
            if ( UID == "" )
                UID = AccessHelper.SaveAllBehavior(new ActiveRecord[] { this })[0];
            else
                AccessHelper.SaveAllBehavior(new ActiveRecord[] { this });
        }
    }

}
