using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.PlugIn;
using SmartSchool.AccessControl;

namespace SmartSchool.Evaluation
{
    class SecureButtonAdapter : ButtonAdapter, IFeature
    {
        public SecureButtonAdapter(string featureCode)
        {
            _feature_code = featureCode;
        }

        #region IFeature 成員

        private string _feature_code;
        public string FeatureCode
        {
            get { return _feature_code; }
        }

        #endregion
    }
}
