using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
/// <summary>
/// 你看不到我 你看不到我 ㄞ   ㄟㄇ   ㄤ   ㄈㄧㄙㄅㄛ
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class FISCA_Data_ActiveRecords_Extension_Methods
{
    /// <summary>
    /// 儲存全部，將集合內所有的資料自動依RecordStatus屬性進行新增修改與刪除的動作
    /// </summary>
    /// <param name="records"></param>
    /// <returns>所有變更資料(含新增)的UID</returns>
    public static List<string> SaveAll<T>(this IEnumerable<T> records) where T : Private.UDT.ActiveRecord
    {
        return Private.UDT.AccessHelper.SaveAllBehavior<T>(records);
    }
}