using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedContent;
using SmartSchool.AccessControl;
using SmartSchool.TeacherRelated.Palmerworm;

namespace SmartSchool.TeacherRelated
{
    internal class PalmerwormFactory : IManager<IContentItem>
    {
        private static PalmerwormFactory _Instence;

        public static PalmerwormFactory Instence
        {
            get
            {
                if ( _Instence == null ) _Instence = new PalmerwormFactory();
                return _Instence;
            }
        }

        private PalmerwormFactory()
        {
        }

        private List<IContentItem> plugInItems = new List<IContentItem>();

        public Customization.PlugIn.ExtendedContent.IContentItem[] Load()
        {
            List<Customization.PlugIn.ExtendedContent.IContentItem> _items = new List<Customization.PlugIn.ExtendedContent.IContentItem>();

            List<Type> _type_list = new List<Type>(new Type[]{
                        typeof(BaseInfoItem),
                        typeof(TeachStudentItem),
                        typeof(ElectronicPaperPalmerworm),
            });

            foreach ( Type type in _type_list )
            {
                if ( CurrentUser.Acl[type].Viewable )
                {
                    try
                    {
                        System.Windows.Forms.UserControl uc = type.GetConstructor(Type.EmptyTypes).Invoke(null) as System.Windows.Forms.UserControl;

                        IContentItem item = type.GetConstructor(Type.EmptyTypes).Invoke(null) as IContentItem;
                        _items.Add(item);
                    }
                    catch ( Exception ex ) { ExceptionHandler.BugReporter.ReportException(ex, false); }
                }
            }

            foreach ( IContentItem var in plugInItems )
            {
                try
                {
                    if ( !Attribute.IsDefined(var.GetType(), typeof(FeatureCodeAttribute)) )
                        _items.Add((IContentItem)var.Clone());
                    else
                        if ( CurrentUser.Acl[var.GetType()].Viewable )
                            _items.Add((IContentItem)var.Clone());
                }
                catch ( Exception ex ) { ExceptionHandler.BugReporter.ReportException(ex, false); }
            }

            return _items.ToArray();
        }

        #region IManager<IContentItem> 成員

        public void Add(IContentItem instance)
        {
            plugInItems.Add(instance);
        }

        public void Remove(IContentItem instance)
        {
            plugInItems.Remove(instance);
        }

        #endregion
    }
}
