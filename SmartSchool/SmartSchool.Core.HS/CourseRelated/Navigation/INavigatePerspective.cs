using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.CourseRelated
{
    internal interface INavigatePerspective
    {
        void BindView(TreeView view);

        TreeView CurrentView { get;}

        void SetTemporalHandler(TemporalHandler handler);

        void Show();

        void Hide();
    }
}
