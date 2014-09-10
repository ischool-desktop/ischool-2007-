using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using System.Drawing;

namespace SmartSchool.Payment.StudentList
{
    //class GridCellHelper
    //{
    //    private static Color NormalBackColor = Color.Empty;
    //    private static Color DirtyBackColor = Color.Yellow;

    //    public GridCellHelper(DataGridViewX grid, int row, int column)
    //    {
    //        _undata_cell = (row < 0 || column < 0);

    //        if (UndataCell) return; //如果是非資料格(Cell)的話就不處理。

    //        _column = grid.Columns[column];
    //        _field = _column.Tag as PaymentDetailField;
    //        _row = grid.Rows[row];
    //        _cell = _row.Cells[column];
    //        _record = _row.Tag as PaymentDetailRecord;
    //    }

    //    public static void ResetCellsDirtyColor(DataGridViewRow row)
    //    {
    //        foreach (DataGridViewCell each in row.Cells)
    //            each.Style.BackColor = NormalBackColor;
    //    }

    //    #region 相關屬性
    //    private bool _undata_cell;
    //    /// <summary>
    //    /// 取得此 Cell 是否為資料 Cell。
    //    /// </summary>
    //    public bool UndataCell
    //    {
    //        get { return _undata_cell; }
    //    }

    //    private DataGridViewColumn _column;
    //    public DataGridViewColumn GridColumn
    //    {
    //        get { return _column; }
    //    }

    //    private DataGridViewRow _row;
    //    public DataGridViewRow GridRow
    //    {
    //        get { return _row; }
    //    }

    //    private DataGridViewCell _cell;
    //    public DataGridViewCell GridCell
    //    {
    //        get { return _cell; }
    //    }

    //    private PaymentDetailField _field;
    //    public PaymentDetailField Field
    //    {
    //        get { return _field; }
    //    }

    //    private PaymentDetailRecord _record;
    //    public PaymentDetailRecord Record
    //    {
    //        get { return _record; }
    //    }

    //    public bool ValueIsEmpty
    //    {
    //        get
    //        {
    //            string value = GridCell.Value + "";
    //            return string.IsNullOrEmpty(value.Trim());
    //        }
    //    }

    //    public string StringValue
    //    {
    //        get
    //        {
    //            return (GridCell.Value + "").Trim();
    //        }
    //    }

    //    public bool ValueIsInteger
    //    {
    //        get
    //        {
    //            if (ValueIsEmpty) return false;

    //            int intValue;
    //            return int.TryParse(StringValue, out intValue);
    //        }
    //    }

    //    public int IntegerValue
    //    {
    //        get
    //        {
    //            return int.Parse(GridCell.Value + "");
    //        }
    //    }
    //    #endregion

    //    public void SetErrorText(string msg)
    //    {
    //        GridCell.ErrorText = msg;
    //    }

    //    public void ClearError()
    //    {
    //        GridCell.ErrorText = string.Empty;
    //    }

    //    public bool HasError
    //    {
    //        get { return !string.IsNullOrEmpty(GridCell.ErrorText); }
    //    }

    //    /// <summary>
    //    /// 同步目前 Cell 內的資料到 PaymentDetailRecord 的 PaymentItems 屬性。
    //    /// </summary>
    //    public void SyncPaymentItem()
    //    {
    //        if (!ValueIsEmpty) //如果資料是空值，就不檢查
    //            ValidAmountValue();

    //        //如果是「費用項目(PaymentItem)」欄位才進行同步。
    //        if (Field.IsPaymentItemField)
    //        {
    //            Record.SetPaymentItem(Field.FieldName, StringValue);

    //            //取得原始值，以判斷是否要標示資料已更改。
    //            string originValue = string.Empty;
    //            if (Record.OriginPaymentItems.ContainsKey(Field.FieldName))
    //                originValue = Record.OriginPaymentItems[Field.FieldName];

    //            if (originValue != StringValue)
    //                MarkCellDirty();
    //            else
    //                ResetCellDirty();

    //            SyncDirtyStatus();
    //        }
    //    }

    //    /// <summary>
    //    /// 同步目前 Cell 內的資料到 TotalAmount  屬性中。
    //    /// </summary>
    //    public void SyncTotalAmount()
    //    {
    //        if (ValueIsEmpty)
    //            throw new ArgumentException("總金額欄位不能空白。");

    //        ValidAmountValue();

    //        Record.Amount = IntegerValue;

    //        if (Record.OriginAmount != IntegerValue)
    //            MarkCellDirty();
    //        else
    //            ResetCellDirty();

    //        SyncDirtyStatus();
    //    }

    //    /// <summary>
    //    /// 用「金額」的角色檢查資值是否合法。
    //    /// </summary>
    //    private void ValidAmountValue()
    //    {
    //        if (!ValueIsInteger)
    //            throw new ArgumentException("金額必需為數字。");

    //        if (IntegerValue > 1000000)
    //            throw new ArgumentException("金額限制於「1000000」以下。");
    //    }

    //    private void MarkCellDirty()
    //    {
    //        GridCell.Style.BackColor = DirtyBackColor;
    //    }

    //    private void ResetCellDirty()
    //    {
    //        GridCell.Style.BackColor = NormalBackColor;
    //    }

    //    private void SyncDirtyStatus()
    //    {
    //        Record.IsDirty = false;
    //        foreach (DataGridViewCell each in GridRow.Cells)
    //        {
    //            if (each.Style.BackColor == DirtyBackColor)
    //            {
    //                Record.IsDirty = true;
    //                break;
    //            }
    //        }
    //    }
    //}
}
