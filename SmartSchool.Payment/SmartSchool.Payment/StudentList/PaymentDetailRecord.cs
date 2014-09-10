using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Payment.StudentList
{
    /// <summary>
    /// 包含每一筆 PaymentDetail 的資料。
    /// 包含一個指向 DataGridViewRow 的參考。
    /// </summary>
    //class PaymentDetailRecord
    //{
    //    public PaymentDetailRecord()
    //        : this("")
    //    {
    //    }

    //    public PaymentDetailRecord(string identity)
    //    {
    //        _identity = identity;
    //        _items = new Dictionary<string, string>();
    //        _origin_items = new Dictionary<string, string>();
    //    }

    //    private DataGridViewRow _grid_row;
    //    public DataGridViewRow GridRow
    //    {
    //        get { return _grid_row; }
    //        set { _grid_row = value; }
    //    }

    //    private string _identity;
    //    public string Identity
    //    {
    //        get { return _identity; }
    //    }

    //    private Dictionary<string, string> _items;
    //    public Dictionary<string, string> PaymentItems
    //    {
    //        get { return _items; }
    //    }

    //    private Dictionary<string, string> _origin_items;
    //    public Dictionary<string, string> OriginPaymentItems
    //    {
    //        get { return _origin_items; }
    //    }

    //    /// <summary>
    //    /// 新增 PaymentItem 資料。
    //    /// </summary>
    //    /// <param name="name">費用項目名稱。</param>
    //    /// <param name="amount">費用金額。</param>
    //    public void AddPaymentItem(string name, string amount)
    //    {
    //        SetPaymentItem(PaymentItems, name, amount);
    //        SetPaymentItem(OriginPaymentItems, name, amount);
    //    }

    //    /// <summary>
    //    /// 設定 PaymentItem 資料。
    //    /// </summary>
    //    /// <param name="name">費用項目名稱。</param>
    //    /// <param name="value">費用金額。</param>
    //    public void SetPaymentItem(string name, string amount)
    //    {
    //        SetPaymentItem(PaymentItems, name, amount);
    //    }

    //    private string _class_name;
    //    public string ClassName
    //    {
    //        get { return _class_name; }
    //        set { _class_name = value; }
    //    }

    //    private string _student_name;
    //    public string StudentName
    //    {
    //        get { return _student_name; }
    //        set { _student_name = value; }
    //    }

    //    private string student_number;
    //    public string StudentNumber
    //    {
    //        get { return student_number; }
    //        set { student_number = value; }
    //    }

    //    private int _amount;
    //    public int Amount
    //    {
    //        get { return _amount; }
    //        set
    //        {
    //            _amount = value;
    //        }
    //    }

    //    private int _origin_amount;
    //    public int OriginAmount
    //    {
    //        get { return _origin_amount; }
    //        set
    //        {
    //            _origin_amount = value;
    //            Amount = value;
    //        }
    //    }

    //    private int _paid_amount;
    //    public int PaidAmount
    //    {
    //        get { return _paid_amount; }
    //        set { _paid_amount = value; }
    //    }

    //    /// <summary>
    //    /// 計算 PaymentItems 的總合。
    //    /// </summary>
    //    /// <returns></returns>
    //    public int CalculateAmount()
    //    {
    //        int total = 0;
    //        foreach (string each in PaymentItems.Values)
    //        {
    //            int temp;
    //            if (int.TryParse(each, out temp))
    //                total += temp;
    //        }

    //        return total;
    //    }

    //    private bool _isDirty = false;
    //    public bool IsDirty
    //    {
    //        get { return _isDirty; }
    //        set { _isDirty = value; }
    //    }

    //    private bool _isdirty_server = false;
    //    public bool IsDirtyServer
    //    {
    //        get { return _isdirty_server; }
    //        set { _isdirty_server = value; }
    //    }

    //    /// <summary>
    //    /// 將目前的資料設定成原始資料。
    //    /// </summary>
    //    public void SetCurrentDataToOrigin()
    //    {
    //        IsDirty = false;
    //        _origin_amount = _amount;
    //        _origin_items = new Dictionary<string, string>(_items);
    //    }

    //    #region Private Methods
    //    private void SetPaymentItem(Dictionary<string, string> items, string name, string amount)
    //    {
    //        //目的：保證有項目就有金額。
    //        if (string.IsNullOrEmpty(amount))
    //        {
    //            if (items.ContainsKey(name))
    //                items.Remove(name);

    //            return;
    //        }

    //        if (items.ContainsKey(name))
    //            items[name] = amount;
    //        else
    //            items.Add(name, amount);
    //    }
    //    #endregion

    //}

    //class PaymentDetailRecordCollection : Dictionary<string, PaymentDetailRecord>
    //{
    //    public void AddRecord(PaymentDetailRecord detail)
    //    {
    //        Add(detail.Identity, detail);
    //    }
    //}
}
