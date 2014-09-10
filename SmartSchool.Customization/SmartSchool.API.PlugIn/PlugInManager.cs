using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API.PlugIn
{
    public class PlugInManager
    {
        private static PlugInManager _Class = null, _Student = null, _Teacher = null, _Course = null;
        private PlugInManager() { }

        /// <summary>
        /// 班級相關外掛
        /// </summary>
        public static PlugInManager Class { get { if ( _Class == null )_Class = new PlugInManager(); return _Class; } }
        /// <summary>
        /// 學生相關外掛
        /// </summary>
        public static PlugInManager Student { get { if ( _Student == null )_Student = new PlugInManager(); return _Student; } }
        /// <summary>
        /// 教師相關外掛
        /// </summary>
        public static PlugInManager Teacher { get { if ( _Teacher == null )_Teacher = new PlugInManager(); return _Teacher; } }
        /// <summary>
        /// 課程相關外掛
        /// </summary>
        public static PlugInManager Course { get { if ( _Course == null )_Course = new PlugInManager(); return _Course; } }

        /// <summary>
        /// 外掛匯入功能
        /// </summary>
        private Collection<Import.Importer> _Importers = new Collection<SmartSchool.API.PlugIn.Import.Importer>();
        public Collection<Import.Importer> Importers { get { return _Importers; } }
        /// <summary>
        /// 外掛匯出功能
        /// </summary>
        private Collection<Export.Exporter> _Exporters = new Collection<SmartSchool.API.PlugIn.Export.Exporter>();
        public Collection<Export.Exporter> Exporters { get { return _Exporters; } }
    }
}
