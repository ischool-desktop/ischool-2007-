using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Drawing;

namespace SmartSchool
{
    public interface IEntity
    {
        string Title { get;}
        NavigationPanePanel NavPanPanel{get;}
        Panel ContentPanel { get;}
        Image Picture { get;}
        void Actived();
    }
}
