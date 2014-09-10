using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using SmartSchool.ClassRelated.Palmerworm;

namespace SmartSchool.ClassRelated
{
    class ClassPalmerwormFactory
    {
        //private static List<IClassPalmerwormItem> _items;

        public static List<SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem> GetItems()
        {
            List<SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem> _items = new List<SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem>();

            List<Type> _type_list = new List<Type>(new Type[]{
                typeof(ClassBaseInfoItem),
                typeof(ClassStudentItem),
                typeof(ElectronicPaperPalmerworm)
            });

            foreach (Type type in _type_list)
            {
                if (CurrentUser.Acl[type].Viewable)
                    _items.Add(type.GetConstructor(Type.EmptyTypes).Invoke(null) as SmartSchool.Customization.PlugIn.ExtendedContent.IContentItem);
            }

            //if (_items == null)
            //{
            //    _items = new List<IClassPalmerwormItem>();
            //    _items.Add(new ClassBaseInfoItem());
            //    _items.Add(new ClassStudentItem());
            //}

            return _items;
        }
    }

}
