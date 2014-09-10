using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using IntelliSchool.DSA30.Util;

namespace SmartSchool
{
    public  class DataValueManager
    {
        private Dictionary<string, string> _displayTexts;
        private Dictionary<string, string> _nowValues;
        private Dictionary<string, string> _oldValues;

        public DataValueManager()
        {
            Initialize();
        }

        public void AddValue(string name, string value)
        {
            AddValue(name, value, name);
        }

        /// <summary>
        /// �[�J����
        /// </summary>
        /// <param name="name">���د���</param>
        /// <param name="value">���ح�</param>
        public void AddValue(string name, string value,string displayText)
        {
            if (_nowValues.ContainsKey(name))
                _nowValues[name] = value;
            else
                _nowValues.Add(name, value);

            if (_oldValues.ContainsKey(name))
                _oldValues[name] = value;
            else
                _oldValues.Add(name, value);

            if (_displayTexts.ContainsKey(name))
                _displayTexts[name] = displayText;
            else
                _displayTexts.Add(name, displayText);
        }

        /// <summary>
        /// �ܧ󶵥�
        /// </summary>
        /// <param name="name">���د���</param>
        /// <param name="value">�s��</param>
        public void SetValue(string name, string value)
        {
            if (_nowValues.ContainsKey(name))
                _nowValues[name] = value;
        }

        /// <summary>
        /// ���X�ثe�Ҧ�����
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetValues()
        {
            return _nowValues;
        }

        /// <summary>
        /// ���X���w�W�٪���l��ơC
        /// </summary>
        public string GetOldValue(string name)
        {
            return _oldValues[name];
        }

        public string GetDisplayText(string name)
        {
            return _displayTexts[name];
        }

        /// <summary>
        /// �N�Ҧ����زM�ŭ��]
        /// </summary>
        public void ResetValues()
        {
            Initialize();
        }

        /// <summary>
        /// �N�ܧ󶵥س]���w�]����
        /// </summary>
        public void MakeDirtyToClean()
        {
            foreach (string key in _nowValues.Keys)
            {
                _oldValues[key] = _nowValues[key];
            }
        }

        /// <summary>
        /// �P�_�O�_�w���ȳQ�ܧ�
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (string key in _oldValues.Keys)
                {
                    if (IsDirtyItem(key))
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// ��l��
        /// </summary>
        private void Initialize()
        {
            _nowValues = new Dictionary<string, string>();
            _oldValues = new Dictionary<string, string>();
            _displayTexts = new Dictionary<string, string>();
        }

        public IntelliSchool.DSA30.Util.DSRequest GetRequest(string rootName, string dataElementName, string fieldElementName, string conditionElementName, string conditionName, string id)
        {
            DSRequest dsreq = new DSRequest();
            DSXmlHelper helper = new DSXmlHelper(rootName);
            if (!string.IsNullOrEmpty(dataElementName))
            {
                helper.AddElement(dataElementName);
                helper.AddElement(dataElementName, fieldElementName);
                helper.AddElement(dataElementName, conditionElementName);
                fieldElementName = dataElementName + "/" + fieldElementName;
                conditionElementName = dataElementName + "/" + conditionElementName;
            }
            else
            {
                helper.AddElement(fieldElementName);
                helper.AddElement(conditionElementName);
            }

            foreach (string key in _nowValues.Keys)
            {
                if (_nowValues[key] != _oldValues[key])
                {
                    helper.AddElement(fieldElementName, key, _nowValues[key]);
                }
            }

            helper.AddElement(conditionElementName, conditionName, id);
            dsreq.SetContent(helper);
            //Console.WriteLine(helper.GetRawXml());
            return dsreq;
        }

        /// <summary>
        /// ���X�ܧ󶵥زM��
        /// </summary>
        /// <returns>�ܧ󶵥زM��Akey�Ȭ�����,value���ܧ�᪺��</returns>
        public  Dictionary<string, string> GetDirtyItems()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (string key in _oldValues.Keys)
            {
                if (IsDirtyItem(key))
                    dic.Add(key, _nowValues[key]);
            }
            return dic;
        }

        /// <summary>
        /// �P�_key�ȬO�_�w�ܧ�
        /// </summary>
        /// <param name="key">����</param>
        /// <returns>�Y�w�ܧ�h�Ǧ^ true�A�Ϥ��Ǧ^ false</returns>
        public bool IsDirtyItem(string key)
        {
            return _oldValues[key] != _nowValues[key];
        }
    }
}
