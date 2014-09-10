using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;

namespace SmartSchool.Common
{
    public class ItemSelector<T> :DevComponents.DotNetBar.ExpandablePanel where T : IDenominated
    {
        private RaiseEventCollection<T> _Items = new RaiseEventCollection<T>();
        private List<RadioButton> _Buttons = new List<RadioButton>();
        private void Reflash(object sender, EventArgs e)
        {
            this.SuspendLayout();
            T selectedItem = this.SelectedItem;
            if (!_Items.Contains(selectedItem))
                selectedItem = default(T);
            int Y = this.TitleHeight + 6;
            int speace = 6;
            #region 補按鈕到足夠數量
            for (int i = _Buttons.Count; i < _Items.Count; i++)
            {
                RadioButton newButton = new RadioButton();
                newButton.TabIndex = 0;
                newButton.TabStop = true;
                newButton.AutoSize = true;
                newButton.CheckedChanged += new EventHandler(newButton_CheckedChanged);
                this.Controls.Add(newButton);
                _Buttons.Add(newButton);
            } 
            #endregion
            #region 隱藏多餘的按鈕
            for (int i = _Buttons.Count - 1; i >= _Items.Count; i--)
            {
                _Buttons[i].Visible = false;
            } 
            #endregion
            for (int i = 0; i < _Items.Count; i++)
            {
                RadioButton newButton = _Buttons[i];
                newButton.Tag = _Items[i];
                newButton.Text = _Items[i].Name;
                if (selectedItem == null && i == 0)
                    newButton.Checked = true;
                else
                {
                    if (_Items[i].Equals(selectedItem))
                        newButton.Checked = true;
                    else
                        newButton.Checked = false;
                }
                newButton.Location = new Point(12, Y);
                Y += newButton.Height + speace;
                newButton.Visible = true;
            }
            this.Size=new Size(this.Width,Y);
            SetForeColor(this);
            this.ResumeLayout();
        }

        void ScoreCalcRuleEditor_ColorTableChanged(object sender, EventArgs e)
        {
            SetForeColor(this);
        }

        private void newButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, new EventArgs());
        }

        private void SetForeColor(Control parent)
        {
            foreach (Control var in parent.Controls)
            {
                if (var is RadioButton)
                    var.ForeColor = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.CheckBoxItem.Default.Text;
                SetForeColor(var);
            }
        }

        public event EventHandler SelectionChanged;

        public ItemSelector()
        {
            _Items.ItemChaged += new EventHandler(Reflash);
            #region 如果系統的Renderer是Office2007Renderer，同化_ClassTeacherView,_CategoryView的顏色
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                ((Office2007Renderer)GlobalManager.Renderer).ColorTableChanged += new EventHandler(ScoreCalcRuleEditor_ColorTableChanged);
            }
            #endregion
        }

        public T SelectedItem
        {
            get
            {
                int itemcount = _Buttons.Count < _Items.Count ? _Buttons.Count : _Items.Count;
                for (int i = 0; i < itemcount; i++)
                {
                    if (_Buttons[i].Checked)
                        return (T)_Buttons[i].Tag;
                }
                return default(T);
            }
        }

        public RaiseEventCollection<T> Items
        {
            get { return _Items; }
        }

        public class RaiseEventCollection<T> : Collection<T> 
        {
            public event EventHandler ItemChaged;
            protected override void ClearItems()
            {
                base.ClearItems();
                if (ItemChaged != null) ItemChaged.Invoke(this,new EventArgs());
            }
            protected override void InsertItem(int index, T item)
            {
                base.InsertItem(index, item);
                if (ItemChaged != null) ItemChaged.Invoke(this, new EventArgs());
            }
            protected override void SetItem(int index, T item)
            {
                base.SetItem(index, item);
                if (ItemChaged != null) ItemChaged.Invoke(this, new EventArgs());
            }
            protected override void RemoveItem(int index)
            {
                base.RemoveItem(index);
                if (ItemChaged != null) ItemChaged.Invoke(this, new EventArgs());
            }
        }
    }
}
