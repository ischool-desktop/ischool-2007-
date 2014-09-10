using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.GovernmentalDocument
{
    public class ReportBuilderManager
    {
        static private ReportBuilderManager _Items;
        static public ReportBuilderManager Items
        {
            get
            {
                if (_Items == null)
                    _Items = new ReportBuilderManager();
                return _Items;
            }
        }

        private Dictionary<string, List<IReportBuilder>> _items;
        private ReportBuilderManager()
        {
            _items = new Dictionary<string, List<IReportBuilder>>();
        }
        public List<IReportBuilder> this[string Key]
        {
            get 
            {
                if (!_items.ContainsKey(Key))
                {
                    _items.Add(Key, new List<IReportBuilder>());
                }
                return _items[Key];
            }
        }
    }
}
