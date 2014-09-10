using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSchool.Survey;

namespace SmartSchool.ClassRelated.RibbonBars.Survey
{
    public partial class ClassSurveyManager : SurveyManager
    {
        public ClassSurveyManager()
        {
            InitializeComponent();
            InitializeForm();
        }

        protected override SurveyeeType[] GetTargetType()
        {
            return new SurveyeeType[] { SurveyeeType.Class, SurveyeeType.ClassStudent };
        }
    }
}