using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated;
using SmartSchool.ClassRelated;
using SmartSchool.TeacherRelated;
using SmartSchool.Others.Configuration;
using SmartSchool.Others.Configuration.DisciplineMapping;
using SmartSchool.Others.Configuration.MoralityMapping;
using SmartSchool.Others.Configuration.MDReduceMapping;
using SmartSchool.Others.Configuration.PeriodMapping;
using SmartSchool.Others.Configuration.DegreeMapping;
using SmartSchool.Others.Configuration.AbsenceMapping;
using SmartSchool.Others.Configuration.Setup;
using SmartSchool.Others.Configuration.IdentityMapping;
using SmartSchool.Others.Configuration.WordCommentMapping;

namespace SmartSchool
{
    public static class Core_General_Program
    {
        public static void Init_Student_Class_Teacher()
        {

            List<IEntity> _Entities = new List<IEntity>();

            //CreateInstance 要先做
            Class.CreateInstance();
            Student.CreateInstance();
            Teacher.CreateInstance();

            _Entities.Add(Student.Instance);
            _Entities.Add(Class.Instance);
            _Entities.Add(Teacher.Instance);

            //設定同步更新事件
            Student.Instance.SetupSynchronization();
            Class.Instance.SetupSynchronization();
            Teacher.Instance.SetupSynchronization();
            foreach (IEntity var in _Entities)
            {
                MotherForm.Instance.AddEntity(var);
            }

            //學生相關 Ribbon
            MotherForm.Instance.AddProcess(SmartSchool.StudentRelated.Process.StudentIUD.StudentIDUProcess.Instance, 0);//<!--學生/編輯-->
            MotherForm.Instance.AddProcess(new StudentRelated.RibbonBars.Doctrine(), 1);//<!--學生/學務作業-->
            MotherForm.Instance.AddProcess(new StudentRelated.RibbonBars.EducationalAdministration(), 2);//<!--學生/教務作業-->
            MotherForm.Instance.AddProcess(new StudentRelated.RibbonBars.Assign(), 3);//<!--學生/教務作業-->
            MotherForm.Instance.AddProcess(new StudentRelated.RibbonBars.Report(), 4);//<!--學生/統計報表/報表 (ButtonAdapter)--><!--學生/統計報表/統計 (ButtonAdapter)-->
            MotherForm.Instance.AddProcess(new SmartSchool.StudentRelated.RibbonBars.Import.ImportExport(), 5);
            MotherForm.Instance.AddProcess(new StudentRelated.RibbonBars.TemporaStudentManager(), 6);
            MotherForm.Instance.AddProcess(new StudentRelated.RibbonBars.Others(), 7);

            SmartSchool.API.PlugIn.PlugInManager.Student.Exporters.Add(new SmartSchool.StudentRelated.RibbonBars.Export.ExportLeaveInfo());
            SmartSchool.API.PlugIn.PlugInManager.Student.Exporters.Add(new SmartSchool.StudentRelated.RibbonBars.Export.ExportExtandField());
            SmartSchool.API.PlugIn.PlugInManager.Student.Exporters.Add(new SmartSchool.StudentRelated.RibbonBars.Export.ExportCategory());
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new SmartSchool.StudentRelated.RibbonBars.Export.ExportAbsence());
            SmartSchool.Customization.PlugIn.ImportExport.ExportStudent.AddProcess(new SmartSchool.StudentRelated.RibbonBars.Export.ExportDiscipline());
            SmartSchool.API.PlugIn.PlugInManager.Student.Importers.Add(new SmartSchool.StudentRelated.RibbonBars.Import.ImportExtandField());
            SmartSchool.API.PlugIn.PlugInManager.Student.Importers.Add(new SmartSchool.StudentRelated.RibbonBars.Import.ImportCategory());
            SmartSchool.Customization.PlugIn.ImportExport.ImportStudent.AddProcess(new SmartSchool.StudentRelated.RibbonBars.Import.ImportAbsence());
            SmartSchool.API.PlugIn.PlugInManager.Student.Importers.Add(new SmartSchool.StudentRelated.RibbonBars.Import.ImportDiscipline());

            ChangeStatusBatch.Init();//右鍵變更學生狀態

            //班級相關 Ribbon
            MotherForm.Instance.AddProcess(SmartSchool.ClassRelated.RibbonBars.Manage.Instance, 0);
            MotherForm.Instance.AddProcess(new ClassRelated.RibbonBars.Upgrade(), 1);
            MotherForm.Instance.AddProcess(new ClassRelated.RibbonBars.TeacherBiasRibbon(), 2);
            MotherForm.Instance.AddProcess(new ClassRelated.RibbonBars.Assign(), 3);
            MotherForm.Instance.AddProcess(new ClassRelated.RibbonBars.Report(), 4);
            MotherForm.Instance.AddProcess(new ClassRelated.RibbonBars.ImportExport(), 5);
            MotherForm.Instance.AddProcess(new ClassRelated.RibbonBars.History(), 6);

            //教師相關 Ribbon
            MotherForm.Instance.AddProcess(SmartSchool.TeacherRelated.RibbonBars.Manage.Instance);
            MotherForm.Instance.AddProcess(new TeacherRelated.RibbonBars.Report(), 2);
            MotherForm.Instance.AddProcess(new TeacherRelated.RibbonBars.ImportExport(), 3);
            MotherForm.Instance.AddProcess(new TeacherRelated.RibbonBars.History(), 4);

            //設定Customization資料介面使用的的InformationProvider
            Customization.Data.AccessHelper.SetStudentProvider(new API.Provider.StudentProvider());
            Customization.Data.AccessHelper.SetClassProvider(new API.Provider.ClassProvider());
            Customization.Data.AccessHelper.SetTeacherProvider(new API.Provider.TeacherProvider());

            Customization.PlugIn.ExtendedContent.ExtendStudentContent.SetManager(StudentRelated.PalmerwormFactory.Instence);
            Customization.PlugIn.ExtendedContent.ExtendTeacherContent.SetManager(TeacherRelated.PalmerwormFactory.Instence);
        }

        public static void Init_Core_Others()
        {
            SimplyConfigure departmentSetting = new SimplyConfigure();
            departmentSetting.Caption = "科別對照管理";
            departmentSetting.Category = "學籍作業";
            departmentSetting.TabGroup = "教務作業";
            departmentSetting.OnShown += new EventHandler(departmentSetting_OnShown);
            if (CurrentUser.Acl["Button0790"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(departmentSetting);

            SimplyConfigure identityMappingTable = new SimplyConfigure();
            identityMappingTable.Caption = "學籍身分對照表";
            identityMappingTable.Category = "學籍作業";
            identityMappingTable.TabGroup = "教務作業";
            identityMappingTable.OnShown += new EventHandler(identityMappingTable_OnShown);
            if (CurrentUser.Acl["Button0795"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(identityMappingTable);

            //SimplyConfigure scoreMappingTable = new SimplyConfigure();
            //scoreMappingTable.Caption = "評量表";
            //scoreMappingTable.Category = "成績作業";
            //scoreMappingTable.TabGroup = "教務作業";
            //scoreMappingTable.OnShown += new EventHandler(scoreMappingTable_OnShown);
            //SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(scoreMappingTable);

            //SimplyConfigure templateManager = new SimplyConfigure();
            //templateManager.Caption = "評分樣版";
            //templateManager.Category = "成績作業";
            //templateManager.TabGroup = "教務作業";
            //templateManager.OnShown += new EventHandler(templateManager_OnShown);
            //SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(templateManager);

            SimplyConfigure subjectChineseToEnglishForm = new SimplyConfigure();
            subjectChineseToEnglishForm.Caption = "科目中英文對照表";
            subjectChineseToEnglishForm.Category = "成績作業";
            subjectChineseToEnglishForm.TabGroup = "教務作業";
            subjectChineseToEnglishForm.OnShown += new EventHandler(subjectChineseToEnglishForm_OnShown);
            //if (CurrentUser.Acl["Button0820"].Executable)
            //    SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(subjectChineseToEnglishForm);

            SimplyConfigure degreeForm = new SimplyConfigure();
            degreeForm.Caption = "等第對照管理";
            degreeForm.Category = "學務作業";
            degreeForm.TabGroup = "學務作業";
            degreeForm.OnShown += new EventHandler(degreeForm_OnShown);
            if (CurrentUser.Acl["Button0720"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(degreeForm);

            SimplyConfigure absenceForm = new SimplyConfigure();
            absenceForm.Caption = "缺曠類別管理";
            absenceForm.Category = "學務作業";
            absenceForm.TabGroup = "學務作業";
            absenceForm.OnShown += new EventHandler(absenceForm_OnShown);
            if (CurrentUser.Acl["Button0730"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(absenceForm);

            SimplyConfigure reduceForm = new SimplyConfigure();
            reduceForm.Caption = "功過換算管理";
            reduceForm.Category = "學務作業";
            reduceForm.TabGroup = "學務作業";
            reduceForm.OnShown += new EventHandler(reduceForm_OnShown);
            if (CurrentUser.Acl["Button0740"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(reduceForm);

            SimplyConfigure periodForm = new SimplyConfigure();
            periodForm.Caption = "每日節次管理";
            periodForm.Category = "學務作業";
            periodForm.TabGroup = "學務作業";
            periodForm.OnShown += new EventHandler(periodForm_OnShown);
            if (CurrentUser.Acl["Button0750"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(periodForm);

            SimplyConfigure moralityForm = new SimplyConfigure();
            moralityForm.Caption = "德行評語代碼表";
            moralityForm.Category = "學務作業";
            moralityForm.TabGroup = "學務作業";
            moralityForm.OnShown += new EventHandler(moralityForm_OnShown);
            if (CurrentUser.Acl["Button0760"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(moralityForm);

            SimplyConfigure wordCommentForm = new SimplyConfigure();
            wordCommentForm.Caption = "文字評量代碼表";
            wordCommentForm.Category = "學務作業";
            wordCommentForm.TabGroup = "學務作業";
            wordCommentForm.OnShown += new EventHandler(wordCommentForm_OnShown);
            if (CurrentUser.Acl["Button0765"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(wordCommentForm);

            SimplyConfigure disciplineForm = new SimplyConfigure();
            disciplineForm.Caption = "獎懲事由代碼表";
            disciplineForm.Category = "學務作業";
            disciplineForm.TabGroup = "學務作業";
            disciplineForm.OnShown += new EventHandler(disciplineForm_OnShown);
            if (CurrentUser.Acl["Button0770"].Executable)
                SmartSchool.Customization.PlugIn.Configure.SystemConfiguration.AddConfigurationItem(disciplineForm);

            MotherForm.Instance.AddProcess(new Others.RibbonBars.NameList(), 1);
            MotherForm.Instance.AddProcess(new Others.RibbonBars.AwardScore(), 3);
            MotherForm.Instance.AddProcess(new Others.RibbonBars.ScoreOpenTime(), 9);
        }

        private static void wordCommentForm_OnShown(object sender, EventArgs e)
        {
            //WordCommentForm form = new WordCommentForm();
            //form.ShowDialog();
            TextCommentForm form = new TextCommentForm();
            form.ShowDialog();
        }

        private static void identityMappingTable_OnShown(object sender, EventArgs e)
        {
            IdentityForm form = new IdentityForm();
            form.ShowDialog();
        }
        static void disciplineForm_OnShown(object sender, EventArgs e)
        {
            DisciplineForm form = new DisciplineForm();
            form.ShowDialog();
        }

        static void moralityForm_OnShown(object sender, EventArgs e)
        {
            MoralityForm form = new MoralityForm();
            form.ShowDialog();
        }

        static void periodForm_OnShown(object sender, EventArgs e)
        {
            PeriodForm form = new PeriodForm();
            form.ShowDialog();
        }

        static void reduceForm_OnShown(object sender, EventArgs e)
        {
            ReduceForm form = new ReduceForm();
            form.ShowDialog();
        }

        static void absenceForm_OnShown(object sender, EventArgs e)
        {
            AbsenceForm form = new AbsenceForm();
            form.ShowDialog();
        }

        static void degreeForm_OnShown(object sender, EventArgs e)
        {
            DegreeForm form = new DegreeForm();
            form.ShowDialog();
        }

        static void subjectChineseToEnglishForm_OnShown(object sender, EventArgs e)
        {
            SubjectChineseToEnglishForm form = new SubjectChineseToEnglishForm();
            form.ShowDialog();
        }

        //static void templateManager_OnShown(object sender, EventArgs e)
        //{
        //    SmartSchool.SmartPlugIn.Course.ScoresTemplate.TemplateManager obj = new SmartSchool.SmartPlugIn.Course.ScoresTemplate.TemplateManager();
        //    obj.ShowDialog();
        //}

        //static void scoreMappingTable_OnShown(object sender, EventArgs e)
        //{
        //    ExamManager em = new ExamManager();
        //    em.ShowDialog();
        //}

        static void departmentSetting_OnShown(object sender, EventArgs e)
        {
            DeptSetup form = new DeptSetup();
            form.ShowDialog();
        }
    }
}
