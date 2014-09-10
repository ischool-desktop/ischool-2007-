using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using SmartSchool.Customization.PlugIn;
using System.Drawing;

namespace SmartSchool
{
    public enum LayoutMode { None,Auto};
    public class ButtonItemPlugInManager : IManager<ButtonItem>
    {
        private BaseItem _Target;

        private List<ButtonItem> _Items = new List<ButtonItem>();

        private LayoutMode _LayoutMode = LayoutMode.None;

        public ButtonItemPlugInManager(BaseItem target)
        {
            _Target = target;
        }

        public LayoutMode LayoutMode { get { return _LayoutMode; } set { _LayoutMode = value; } }

        public event EventHandler ItemsChanged;
        #region IManager<ButtonItem> 成員

        public void Add(ButtonItem instance)
        {
            instance.GlobalItem = false;
            _Items.Add(instance);
            _Target.SubItems.Add(instance);

            if ( ItemsChanged != null )
                ItemsChanged.Invoke(this, new EventArgs());

            if ( _LayoutMode == LayoutMode.Auto )
            {

                if ( _Target.SubItems.Count == 1 )
                {
                    foreach ( ButtonItem item in _Target.SubItems )
                    {
                        item.ImagePosition = eImagePosition.Top;
                        item.FixedSize = new Size(0, 0);
                        item.ImageFixedSize = new Size(0, 0);
                        item.ImagePaddingHorizontal = 8;
                        item.ImagePaddingVertical = 6;
                    }
                }
                else if ( _Target.SubItems.Count % 3 == 0 )
                {
                    foreach ( ButtonItem item in _Target.SubItems )
                    {
                        item.ImagePosition = eImagePosition.Left;
                        item.FixedSize = new Size(0, 0);
                        item.ImageFixedSize = new Size(16, 16);
                        item.ImagePaddingHorizontal = 3;
                        item.ImagePaddingVertical = 3;
                    }
                }
                else
                {
                    foreach ( ButtonItem item in _Target.SubItems )
                    {
                        item.ImagePosition = eImagePosition.Left;
                        item.FixedSize = new Size(0, 0);
                        item.ImageFixedSize = new Size(24, 24);
                        item.ImagePaddingHorizontal = 3;
                        item.ImagePaddingVertical = 10;
                    }
                }
            }
        }

        public void Remove(ButtonItem instance)
        {
            _Items.Remove(instance);
            _Target.SubItems.Remove(instance);

            if ( ItemsChanged != null )
                ItemsChanged.Invoke(this, new EventArgs());

            if ( _LayoutMode == LayoutMode.Auto )
            {

                if ( _Target.SubItems.Count == 1 )
                {
                    foreach ( ButtonItem item in _Target.SubItems )
                    {
                        item.ImagePosition = eImagePosition.Top;
                        item.FixedSize = new Size(0, 0);
                        item.ImageFixedSize = new Size(0, 0);
                        item.ImagePaddingHorizontal = 8;
                        item.ImagePaddingVertical = 6;
                    }
                }
                else if ( _Target.SubItems.Count % 3 == 0 )
                {
                    foreach ( ButtonItem item in _Target.SubItems )
                    {
                        item.ImagePosition = eImagePosition.Left;
                        item.FixedSize = new Size(0, 0);
                        item.ImageFixedSize = new Size(16, 16);
                        item.ImagePaddingHorizontal = 3;
                        item.ImagePaddingVertical = 3;
                    }
                }
                else
                {
                    foreach ( ButtonItem item in _Target.SubItems )
                    {
                        item.ImagePosition = eImagePosition.Left;
                        item.FixedSize = new Size(0, 0);
                        item.ImageFixedSize = new Size(24, 24);
                        item.ImagePaddingHorizontal = 3;
                        item.ImagePaddingVertical = 10;
                    }
                }
            }
        }

        #endregion
    }
}
