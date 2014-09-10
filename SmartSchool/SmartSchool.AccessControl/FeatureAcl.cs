using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace SmartSchool.AccessControl
{
    /// <summary>
    /// 代表一組權限的集合。
    /// </summary>
    public class FeatureAcl : IEnumerable<FeatureAce>
    {
        public FeatureAcl()
        {
            _acl = new Dictionary<string, FeatureAce>();
        }

        private Dictionary<string, FeatureAce> _acl;
        public FeatureAce this[string featureCode]
        {
            get
            {
                if (string.IsNullOrEmpty(featureCode))
                    return FeatureAce.Null;

                if (_acl.ContainsKey(featureCode.Trim()))
                    return _acl[featureCode];
                else
                    return FeatureAce.Null;
            }
        }

        public FeatureAce this[Type feature]
        {
            get
            {
                if (feature == null)
                    throw new ArgumentException("參數不可為 Null。", "feature");

                return this[FeatureCodeAttribute.GetCode(feature)];
            }
        }

        public FeatureAce this[IFeature feature]
        {
            get
            {
                if (feature == null)
                    throw new ArgumentException("參數不可為 Null。", "feature");

                return this[feature.FeatureCode];
            }
        }

        public FeatureAcl UnionAcl(FeatureAcl acl)
        {
            FeatureAcl newSet = new FeatureAcl();

            foreach (FeatureAce eachAce in acl)
                newSet.MergeAce(eachAce);

            foreach (FeatureAce eachPermission in this)
                newSet.MergeAce(eachPermission);

            return newSet;
        }

        #region IEnumerable<FeaturePermission> 成員

        public IEnumerator<FeatureAce> GetEnumerator()
        {
            return _acl.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _acl.Values.GetEnumerator();
        }

        #endregion

        public void MergeAce(FeatureAce ace)
        {
            string featureCode = ace.FeatureCode;

            if (_acl.ContainsKey(featureCode))
            {
                FeatureAce originAce = _acl[featureCode];
                FeatureAce newAce = new FeatureAce(featureCode, originAce.Access | ace.Access);
                _acl[featureCode] = newAce;
            }
            else
                _acl.Add(featureCode, ace);
        }
    }
}
