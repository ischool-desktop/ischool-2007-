using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn.Report
{
    /// <summary>
    /// �ǥͬ�������
    /// </summary>
    public static class StudentReport
    {
        private static IReportManager _Manager;
        private static List<ButtonAdapter> catchItems = new List<ButtonAdapter>();
        public static void SetManager(IReportManager manager)
        {
            _Manager = manager;
            foreach (ButtonAdapter var in catchItems)
            {
                _Manager.AddButton(var);
            }
            catchItems.Clear();
        }
        /// <summary>
        /// �s�W�ǥͬ���������s
        /// </summary>
        /// <param name="report"></param>
        public static void AddReport(ButtonAdapter report)
        {
            if (_Manager == null)
            {
                catchItems.Add(report);
            }
            _Manager.AddButton(report);
        }

        /// <summary>
        /// �����ǥͬ���������s
        /// </summary>
        /// <param name="report"></param>
        public static void RemoveReport(ButtonAdapter report)
        {
            if (_Manager == null)
            {
                if (catchItems.Contains(report))
                    catchItems.Remove(report);
            }
            else
                _Manager.RemoveButton(report);
        }
    }
}
