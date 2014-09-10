using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.Configure
{
    public interface IConfigurationManager
    {
        void AddConfigurationItem(IConfigurationItem configurationItem);
    }
}
