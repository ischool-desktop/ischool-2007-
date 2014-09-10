using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.StudentRelated.Placing.DataSource
{
    public class DataFormaterFactory
    {
        public static IDataFormater CreateInstance(PlacingArg arg)
        {
            if (arg.Type == PlaceType.SchoolYear)
            {
                return new SchoolYearDataFormater();
            }
            else
            {
                return new SemesterDataFormater();
            }
        }
    }
}
