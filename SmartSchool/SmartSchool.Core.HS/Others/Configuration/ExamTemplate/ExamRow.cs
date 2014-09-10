using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using IntelliSchool.DSA30.Util;

namespace SmartSchool.Others.Configuration.ExamTemplate
{
    class ExamRow : DataGridViewRow
    {
        private ExamItem _exam;
        private ExamItemUpdateAction _update_action;
        private bool _new_row_template = false;

        public ExamRow()
        {
            _new_row_template = true;
            _exam = ExamItem.NewExamItem();
            UpdateAction = ExamItemUpdateAction.Add;
        }

        public ExamRow(ExamItem exam)
        {
            _exam = exam;
            UpdateAction = ExamItemUpdateAction.NoAction;
        }

        public ExamItemUpdateAction UpdateAction
        {
            get { return _update_action; }
            private set
            {
                _update_action = value;
            }
        }

        public ExamItem ExamInformation
        {
            get { return _exam; }
        }

        public void ShowItemData()
        {
            ExamNameCell.Value = ExamInformation.RefExamID;
            UseScoreCell.Value = ExamInformation.UseScoreBoolean;
            UseTextCell.Value = ExamInformation.UseTextBoolean;
            WeightCell.Value = ExamInformation.Weight;
            OpenTeacherAccessColumn.Value = ExamInformation.OpenTeacherAccessBoolean;
            StartTimeColumn.Value = ExamInformation.StartTime;
            EndTimeColumn.Value = ExamInformation.EndTime;
        }

        public void SaveDataToItem()
        {
            DataGridViewCell exam = ExamNameCell;

            _exam.UseScore = ((bool)UseScoreCell.Value) ? "是" : "否";
            _exam.UseText = ((bool)UseTextCell.Value) ? "是" : "否";
            _exam.Weight = "" + WeightCell.Value;
            _exam.SetRefExamID("" + exam.Value);
            _exam.ExamName = "" + exam.FormattedValue;
            _exam.OpenTeacherAccess = ((bool)OpenTeacherAccessColumn.Value ? "是" : "否");
            _exam.StartTime = "" + StartTimeColumn.Value;
            _exam.EndTime = "" + EndTimeColumn.Value;

            if (UpdateAction != ExamItemUpdateAction.Add && _exam.IsDirty)
                UpdateAction = ExamItemUpdateAction.Update;
        }

        public void RefreshAction()
        {
            if (ExamInformation.IsDirty)
            {
                if (UpdateAction == ExamItemUpdateAction.NoAction)
                    UpdateAction = ExamItemUpdateAction.Update;
            }
        }

        public void Delete()
        {
            Visible = false;

            if (UpdateAction == ExamItemUpdateAction.Add)
                UpdateAction = ExamItemUpdateAction.NoAction;
            else
                UpdateAction = ExamItemUpdateAction.Delete;
        }

        public DataGridViewCell ExamNameCell
        {
            get { return Cells["ExamNameColumn"]; }
        }

        public DataGridViewCell UseScoreCell
        {
            get { return Cells["UseScoreColumn"]; }
        }

        public DataGridViewCell UseTextCell
        {
            get { return Cells["UseTextColumn"]; }
        }

        public DataGridViewCell WeightCell
        {
            get
            {
                return Cells["WeightColumn"];
            }
        }

        public DataGridViewCell OpenTeacherAccessColumn
        {
            get
            {
                return Cells["OpenTeacherAccess"];
            }
        }

        public DataGridViewCell StartTimeColumn
        {
            get
            {
                return Cells["StartTime"];
            }
        }

        public DataGridViewCell EndTimeColumn
        {
            get
            {
                return Cells["EndTime"];
            }
        }

        public bool IsDirty
        {
            get
            {
                return ExamInformation.IsDirty ||
                    UpdateAction == ExamItemUpdateAction.Delete ||
                    UpdateAction == ExamItemUpdateAction.Add;
            }
        }

        public void ValidWeightValue()
        {
            if (IsWeightValueValid)
                WeightCell.ErrorText = string.Empty;
            else
                WeightCell.ErrorText = "資料不正確，請輸入數字。";
        }

        public bool IsWeightValueValid
        {
            get
            {
                if (WeightCell.Value == null)
                    return false;

                string weight = WeightCell.Value.ToString();
                float iweight;

                if (!float.TryParse(weight, out iweight))
                    return false;

                return true;
            }
        }

        public override object Clone()
        {
            ExamRow row = base.Clone() as ExamRow;
            row.UpdateAction = this.UpdateAction;

            if (_new_row_template)
                row._exam = ExamItem.NewExamItem();
            else
                row._exam = this.ExamInformation;

            row._new_row_template = false;

            return row;
        }
    }
}
