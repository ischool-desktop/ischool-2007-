using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace SmartSchool.TagManage
{
    /// <summary>
    /// �㦳�T�ت��A�� DataGridView�C
    /// </summary>
    public partial class StateDataGridView : DataGridViewX
    {
        static StateDataGridView()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(StateDataGridView));
            StateImage = new ImageList();
            StateImage.ImageStream = ((ImageListStreamer)(resources.GetObject("StateImage.ImageStream")));
            StateImage.TransparentColor = System.Drawing.Color.White;
            StateImage.Images.SetKeyName(0, "Unchecked");
            StateImage.Images.SetKeyName(1, "Checked");
            StateImage.Images.SetKeyName(2, "Indeterminate");
        }

        public event RowCheckedEventHandler RowCheckStateChanged;

        private int _state_column_index = -1;

        public StateDataGridView()
        {
            InitializeComponent();
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            ReadOnly = true;
            RowHeadersVisible = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);

            if (e.RowIndex < 0) return;

            //���o�ϥΪ��I�쪺�uCell�v
            DataGridViewCell cell = Rows[e.RowIndex].Cells[e.ColumnIndex];

            //�p�G�ӡuCell�v�� ColumnIndex �O StateColumnIndex�A�N���� Row �� CheckState�C
            if (e.ColumnIndex == StateColumnIndex)
                ChangeRowState(Rows[e.RowIndex]);
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnCellDoubleClick(e);

            if (e.RowIndex < 0) return;

            DataGridViewCell cell = Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (!(cell is DataGridViewImageCell))
                ChangeRowState(Rows[e.RowIndex]);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Space)
            {
                foreach (DataGridViewRow row in SelectedRows)
                    ChangeRowState(row);
            }
        }
        /// <summary>
        /// ���A�檺 ColumnIndex�C
        /// </summary>
        public int StateColumnIndex
        {
            get { return _state_column_index; }
            set { _state_column_index = value; }
        }

        private void ChangeRowState(DataGridViewRow row)
        {
            if (row is StateDataGridViewRow)
            {
                StateDataGridViewRow three = row as StateDataGridViewRow;

                CheckState originState = three.CurrentState;
                CheckState newState = GetNewState(three); ;

                if (RowCheckStateChanged != null)
                {
                    RowCheckedEventArgs arg = new RowCheckedEventArgs(three, newState);
                    RowCheckStateChanged(this, arg);

                    if (arg.Cancel) //�p�G�����A�N��쪬�A��@�s���A�C
                        newState = originState;
                    else
                        newState = arg.NewState;
                }

                three.CurrentState = newState;
            }
        }

        private static CheckState GetNewState(StateDataGridViewRow three)
        {
            CheckState newState;
            if (three.CurrentState == CheckState.Unchecked)
                newState = CheckState.Checked;
            else if (three.CurrentState == CheckState.Checked)
                newState = CheckState.Indeterminate;
            else
                newState = CheckState.Unchecked;
            return newState;
        }

        #region Images Setting

        private const string UncheckedKey = "Unchecked";
        private const string CheckedKey = "Checked";
        private const string IndeterminateKey = "Indeterminate";

        private static ImageList StateImage;

        public static Image CheckedImage
        {
            get { return StateImage.Images[CheckedKey]; }
        }

        public static Image UncheckedImage
        {
            get { return StateImage.Images[UncheckedKey]; }
        }

        public static Image IndeterminateImage
        {
            get { return StateImage.Images[IndeterminateKey]; }
        }

        #endregion

    }

    public delegate void RowCheckedEventHandler(object sender, RowCheckedEventArgs e);

    #region RowCheckedEventArgs
    public class RowCheckedEventArgs : EventArgs
    {
        private StateDataGridViewRow _row;
        private CheckState _new_state;
        private bool _cancel;

        public RowCheckedEventArgs(StateDataGridViewRow row, CheckState newState)
        {
            _row = row;
            _new_state = newState;
            _cancel = false;
        }

        public StateDataGridViewRow Row
        {
            get { return _row; }
        }

        /// <summary>
        /// �O�_�����������A�ܧ�C�p�G�������A�ܧ� NewState �ݩʪ��]�w�N�|�Q���L�C
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        /// <summary>
        /// ���o�γ]�w�s���A�C
        /// </summary>
        public CheckState NewState
        {
            get { return _new_state; }
            set { _new_state = value; }
        }
    }
    #endregion

    #region StateDataGridViewRow
    /// <summary>
    /// �إ߷s�� Row �ɡACurrentState �w�]�� Unchecked�C
    /// </summary>
    public class StateDataGridViewRow : DataGridViewRow
    {
        private CheckState _current_state;

        public StateDataGridViewRow()
        {
            CurrentState = CheckState.Unchecked;
        }

        /// <summary>
        /// ���ܦ��ݩʭn�b�s�W��@�� StateDataGridView ������A�~�|���T��ܡC
        /// </summary>
        public CheckState CurrentState
        {
            get { return _current_state; }
            set
            {
                _current_state = value;

                if (StateGridView != null)
                {
                    SyncStateColumn();
                }
            }
        }

        protected void SyncStateColumn()
        {
            if (StateGridView.StateColumnIndex != -1)
                Cells[StateGridView.StateColumnIndex].Value = GetStateImage(CurrentState);
        }

        private StateDataGridView StateGridView
        {
            get { return DataGridView as StateDataGridView; }
        }

        private object GetStateImage(CheckState value)
        {
            if (value == CheckState.Checked)
                return StateDataGridView.CheckedImage;
            else if (value == CheckState.Unchecked)
                return StateDataGridView.UncheckedImage;
            else
                return StateDataGridView.IndeterminateImage;
        }
    }
    #endregion

}
