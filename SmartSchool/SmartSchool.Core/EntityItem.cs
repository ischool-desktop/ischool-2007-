using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Windows.Forms;

namespace SmartSchool
{
    class EntityItem
    {
        private IEntity _entity;
        private NavigationPanePanel _navPanPanel;
        private ButtonItem _navButton;
        private RibbonTabItem _ribbonTabItem;
        private Panel _contentPanel;

        public IEntity Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }
        public ButtonItem NavButton
        {
            set
            {
                //if(_navButton!=null)
                //    _navButton.CheckedChanged -= new EventHandler(_navButton_CheckedChanged);
                _navButton = value;
                //_navButton.CheckedChanged += new EventHandler(_navButton_CheckedChanged);
            }
            get
            {
                return _navButton;
            }
        }
        public RibbonTabItem RibbonTabItem
        {
            set
            {
                //if (_ribbonTabItem != null)
                //{
                //    _ribbonTabItem.CheckedChanged -= new EventHandler(_ribbonTabItem_CheckedChanged);
                //}
                _ribbonTabItem = value;
                //_ribbonTabItem.CheckedChanged += new EventHandler(_ribbonTabItem_CheckedChanged);
            }
            get 
            {
                return _ribbonTabItem;
            }
        }
        public NavigationPanePanel NavPanPanel
        {
            get { return _navPanPanel; }
            set { _navPanPanel = value; }
        }
        public Panel ContentPanel
        {
            get { return _contentPanel; }
            set { _contentPanel = value; }
        }
    }
}
