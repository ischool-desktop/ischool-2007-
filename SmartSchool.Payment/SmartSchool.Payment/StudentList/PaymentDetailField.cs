using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.Payment.StudentList
{
    /// <summary>
    /// 用於保存畫面上 PaymentDetail 的欄位資訊。
    /// 包含指向 DataGridView  的 Column 參考。
    /// </summary>
    //class PaymentDetailField
    //{
    //    public PaymentDetailField(string fieldName)
    //    {
    //        _field_name = fieldName;
    //    }

    //    private float _order = 0;
    //    public float Order
    //    {
    //        get { return _order; }
    //        set { _order = value; }
    //    }

    //    private bool _is_payment_item = false;
    //    /// <summary>
    //    /// 指示此欄位是否為「金額細項」欄位。
    //    /// </summary>
    //    public bool IsPaymentItemField
    //    {
    //        get { return _is_payment_item; }
    //        set { _is_payment_item = value; }
    //    }

    //    private string _field_name;
    //    public string FieldName
    //    {
    //        get { return _field_name; }
    //    }

    //    private DataGridViewColumn _grid_column;
    //    public DataGridViewColumn GridColumn
    //    {
    //        get { return _grid_column; }
    //        set { _grid_column = value; }
    //    }

    //    private static PaymentDetailField _total_amount;
    //    /// <summary>
    //    /// 總金額欄位，用於程式中的部份判斷。
    //    /// </summary>
    //    public static PaymentDetailField TotalAmountField
    //    {
    //        get { return _total_amount; }
    //        set { _total_amount = value; }
    //    }

    //    private static PaymentDetailField _name_field;
    //    /// <summary>
    //    /// 學生姓名欄位。
    //    /// </summary>
    //    public static PaymentDetailField StudentNameField
    //    {
    //        get { return _name_field; }
    //        set { _name_field = value; }
    //    }
    //}

    ///// <summary>
    ///// PaymentDetailField 的集合類別。
    ///// </summary>
    //class PaymentDetailFieldCollection : Dictionary<string, PaymentDetailField>
    //{
    //    public void AddField(PaymentDetailField field)
    //    {
    //        Add(field.FieldName, field);
    //    }

    //    public PaymentDetailFieldCollection GetMoneyFields()
    //    {
    //        PaymentDetailFieldCollection fields = new PaymentDetailFieldCollection();
    //        foreach (PaymentDetailField each in this.Values)
    //        {
    //            if (each.IsPaymentItemField)
    //                fields.AddField(each);
    //        }

    //        return fields;
    //    }
    //}

    /// <summary>
    /// PaymentDetailField 的比較物件，用於排序。
    /// 用於將欄位排成特定順序。
    /// 例：班級、姓名、學號、(校車費)、(輔導費)、總金額、已繳金額。
    /// 「( )」內的為動態欄位，數量跟據「收費」設定而不同。
    /// </summary>
    //class PaymentDetailFieldComparer : IComparer<PaymentDetailField>
    //{
    //    #region IComparer<PaymentDetailField> 成員

    //    public int Compare(PaymentDetailField x, PaymentDetailField y)
    //    {
    //        return x.Order.CompareTo(y.Order);
    //    }

    //    #endregion
    //}
}
