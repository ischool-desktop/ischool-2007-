using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Aspose.Words;

namespace SmartSchool.Payment.BillTemplate
{
    internal class TemplateManager
    {
        public static Document DefaultTemplate;
        static TemplateManager()
        {
            Stream samdoc = new MemoryStream(Properties.Resources.BillTemplate);
            DefaultTemplate = new Document(samdoc);
            samdoc.Close();
        }
    }
}
