using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;
using DevComponents.DotNetBar;
using SmartSchool.Common;
using SmartSchool.AccessControl;

namespace SmartSchool
{
    public class ButtonAdapterPlugInManager : IManager<ButtonAdapter>
    {
        private ButtonItem _TemplateButton = null;

        private BaseItem _Target;

        private Dictionary<ButtonItem, ButtonAdapter> _Adapters = new Dictionary<ButtonItem, ButtonAdapter>();

        private void newButton_Click(object sender, EventArgs e)
        {
            _Adapters[(DevComponents.DotNetBar.ButtonItem)sender].Click();
        }

        private void button_OnSetBarMessage(object sender, SmartSchool.Customization.PlugIn.ButtonAdapter.SetBarMessageEventArgs e)
        {
            if (e.HasProgress)
                SmartSchool.MotherForm.Instance.SetBarMessage(e.Message, e.Progress);
            else
                SmartSchool.MotherForm.Instance.SetBarMessage(e.Message);
        }

        private void TracePath(BaseItem buttonItem, ButtonItem newButton, List<string> paths)
        {
            string path = "";
            if (paths.Count > 0)
            {
                path = paths[0];
                paths.RemoveAt(0);
            }
            else
            {
                buttonItem.SubItems.Add(newButton);
                return;
            }

            bool found = false;
            int index = 0;
            foreach (DevComponents.DotNetBar.ButtonItem item in buttonItem.SubItems)
            {
                if (item.Text == path)
                {
                    index = buttonItem.SubItems.IndexOf(item);
                    found = true;
                    break;
                }
            }

            if (found)
            {
                TracePath((DevComponents.DotNetBar.ButtonItem)buttonItem.SubItems[index], newButton, paths);
            }
            else
            {
                DevComponents.DotNetBar.ButtonItem pathButtom;

                if ( _TemplateButton != null )
                    pathButtom = (ButtonItem)_TemplateButton.Clone();
                else
                    pathButtom = new DevComponents.DotNetBar.ButtonItem();
                pathButtom.Text = path;
                buttonItem.SubItems.Add(pathButtom);
                TracePath(pathButtom, newButton, paths);
            }
        }

        public ButtonAdapterPlugInManager(BaseItem target)
        {
            _Target = target;
        }

        public event EventHandler ItemsChanged;

        public ButtonItem TemplateButton
        {
            get { return _TemplateButton; }
            set { _TemplateButton = value; }
        }

        #region IManager<ButtonAdapter> 成員

        public void Add(SmartSchool.Customization.PlugIn.ButtonAdapter button)
        {
            DevComponents.DotNetBar.ButtonItem newButton;
            if ( _TemplateButton == null )
            {
                newButton = new DevComponents.DotNetBar.ButtonItem();
                newButton.ImagePaddingHorizontal = 8;
            }
            else
            {
                newButton = (ButtonItem)_TemplateButton.Clone();
                //newButton.ButtonStyle = _TemplateButton.ButtonStyle;
                //newButton.Image = _TemplateButton.Image.Clone() as System.Drawing.Image;
            }
            newButton.Text = button.Text;
            newButton.Click += new EventHandler(newButton_Click);
            if ( button.Image != null )
                newButton.Image = button.Image;
            if (button is IFeature)
                newButton.Enabled = CurrentUser.Acl[button as IFeature].Executable;

            button.OnSetBarMessage += new EventHandler<SmartSchool.Customization.PlugIn.ButtonAdapter.SetBarMessageEventArgs>(button_OnSetBarMessage);
            _Adapters.Add(newButton, button);

            if (string.IsNullOrEmpty(button.Path))
                this._Target.SubItems.Add(newButton);
            else
            {
                List<string> paths = new List<string>();
                paths.AddRange(button.Path.Split('/'));
                TracePath(this._Target, newButton, paths);
            }
            if (ItemsChanged != null)
                ItemsChanged.Invoke(this, new EventArgs());
        }

        public void Remove(SmartSchool.Customization.PlugIn.ButtonAdapter button)
        {
            foreach (DevComponents.DotNetBar.ButtonItem var in _Adapters.Keys)
            {
                if (_Adapters[var] == button)
                {
                    var.Click -= new EventHandler(newButton_Click);
                    button.OnSetBarMessage -= new EventHandler<SmartSchool.Customization.PlugIn.ButtonAdapter.SetBarMessageEventArgs>(button_OnSetBarMessage);
                    this._Target.SubItems.Remove(var);
                    _Adapters.Remove(var);
                    break;
                }
            }
            if (ItemsChanged != null)
                ItemsChanged.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
