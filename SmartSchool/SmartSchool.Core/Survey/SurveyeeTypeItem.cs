using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Survey
{
    public class SurveyeeTypeItem
    {
        public SurveyeeTypeItem()
        {
        }

        public SurveyeeTypeItem(SurveyeeType value, string name, string description)
        {
            Value = value;
            Name = name;
            Description = description;
        }

        private SurveyeeType _value;
        public SurveyeeType Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _desc;
        public string Description
        {
            get { return _desc; }
            set { _desc = value; }
        }

    }
}
