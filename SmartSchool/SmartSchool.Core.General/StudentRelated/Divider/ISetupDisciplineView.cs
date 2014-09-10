using System;
namespace SmartSchool.StudentRelated.Divider
{
    interface ISetupDisciplineView
    {
        bool 檢視一般生 { get; }
        bool 檢視大功 { get; }
        bool 檢視大過 { get; }
        bool 檢視小功 { get; }
        bool 檢視小過 { get; }
        bool 檢視已刪除學生 { get; }
        bool 檢視已銷過紀錄 { get; }
        bool 檢視休學學生 { get; }
        bool 檢視延修生 { get; }
        bool 檢視畢業及離校學生 { get; }
        bool 檢視嘉獎 { get; }
        bool 檢視警告 { get; }
        bool 檢視留校察看 { get;}
    }
}
