using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Customization.Data;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using System.Diagnostics;
using SmartSchool.Common;

namespace SmartSchool.StudentRelated.RibbonBars.AcademicAffairs
{
    class LevelOfEducation
    {
        public LevelOfEducation(string code)
        {
            //學校代碼
            string school_code = SmartSchool.Customization.Data.SystemInformation.SchoolCode;

            while (school_code.Length < 6)
                school_code = "9" + school_code;

            AccessHelper helper = new AccessHelper();
            List<StudentRecord> students = helper.StudentHelper.GetSelectedStudent();

            Encoding Big5 = Encoding.GetEncoding("big5");
            string name_space = "            ";
            string id_number_space = "          ";
            string birthdate_space = "       ";

            StringBuilder builder = new StringBuilder("");

            foreach (StudentRecord each in students)
            {
                //姓名
                byte[] name = Big5.GetBytes(each.StudentName + name_space);

                //身分證字號
                byte[] id_number = Big5.GetBytes(each.IDNumber + id_number_space);

                //生日
                byte[] birthdate = Big5.GetBytes(CDATE(each.Birthday) + birthdate_space);

                builder.Append(Big5.GetString(name, 0, name_space.Length));
                builder.Append(Big5.GetString(id_number, 0, id_number_space.Length));
                builder.Append(Big5.GetString(birthdate, 0, birthdate_space.Length));
                builder.AppendLine(code + school_code);
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "儲存教育程度資料檔";
            sfd.Filter = "純文字檔案 (*.txt)|*.txt";
            sfd.ShowDialog();

            try
            {
                FileStream stream = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(stream, Big5);
                writer.Write(builder.ToString());
                writer.Close();
                stream.Close();
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private string CDATE(string p)
        {
            DateTime d = DateTime.Now;
            if (p != "" && DateTime.TryParse(p, out d))
            {
                string year = Convert.ToString(d.Year - 1911);
                string month = d.Month.ToString();
                string day = d.Day.ToString();

                if (year.Length < 3)
                    year = "0" + year;
                if (month.Length < 2)
                    month = "0" + month;
                if (day.Length < 2)
                    day = "0" + day;

                return year + month + day;
            }
            else
                return "";
        }
    }
}
