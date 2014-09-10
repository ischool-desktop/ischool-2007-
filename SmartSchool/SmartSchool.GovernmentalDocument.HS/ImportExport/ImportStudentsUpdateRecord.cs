using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.AccessControl;

namespace SmartSchool.GovernmentalDocument.ImportExport
{
    [FeatureCode("Button0280")]
    class ImportStudentsUpdateRecord:AbstractImportUpdateRecord
    {
        protected override string[] Fields
        {//"異動科別", "年級", "異動學號", "異動姓名", "身份證號", "性別", "生日", "異動種類", "異動代碼", "異動日期", "原因及事項", "新學號", "轉入前學生資料-科別", "轉入前學生資料-年級", "轉入前學生資料-學校", "轉入前學生資料-(備查日期)", "轉入前學生資料-(備查文號)", "轉入前學生資料-學號", "入學資格-畢業國中", "入學資格-畢業國中所在地代碼", "最後異動代碼", "畢(結)業證書字號", "備查日期", "備查文號", "核準日期", "核準文號", "備註"
            get { return new string[] { "異動科別", "年級", "異動學號", "異動姓名", "身份證號", "異動代碼", "異動日期", "原因及事項", "新學號", "備查日期", "備查文號", "核準日期", "核準文號", "備註" }; }
        }

        protected override string Type
        {
            get { return "學籍異動"; }
        }

        protected override string[] InsertRequiredFields
        {
            get { return new string[] { "異動代碼", "異動日期", "年級" }; }
        }
    }
}
