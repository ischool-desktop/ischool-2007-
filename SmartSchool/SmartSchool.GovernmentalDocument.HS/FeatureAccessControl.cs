using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using DevComponents.DotNetBar;

namespace SmartSchool.GovernmentalDocument
{
    public class FeatureAccessControl
    {
        private string _featureCode;

        public FeatureAccessControl(string featureCode)
        {
            _featureCode = featureCode;
        }

        public bool Executable()
        {
            return CurrentUser.Acl[_featureCode].Executable;
        }

        public void Inspect(ButtonItem button)
        {
            if (!CurrentUser.Acl[_featureCode].Executable)
                button.Enabled = false;
        }
    }
}
