using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.ApplicationLog
{
    public enum EntityType
    {
        Student,
        Class,
        Teacher,
        Course,
        Undefine
    }

    public class EntityTypeName
    {
        public static string Get(EntityType entity)
        {
            switch(entity)
            {
                case EntityType.Student:
                    return "Student";
                case EntityType.Class:
                    return "Class";
                case EntityType.Teacher:
                    return "Teacher";
                case EntityType.Course:
                    return "Course";
                default:
                    return "Undefine";
            }
        }
    }
}
