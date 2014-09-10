using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.API.PlugIn.Import
{
    /// <summary>
    /// 匯入功能
    /// </summary>
    public abstract class Importer : VirtualButton
    {
        /// <summary>
        /// 開始一個新的匯入作業
        /// </summary>
        /// <param name="wizard">處理這次匯入的精靈</param>
        public abstract void InitializeImport(ImportWizard wizard);
    }
}
