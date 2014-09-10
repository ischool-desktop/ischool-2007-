using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.Palmerworm;
using SmartSchool.Common;
using SmartSchool.Customization.PlugIn;
using SmartSchool.Customization.PlugIn.ExtendedContent;
using SmartSchool.AccessControl;

namespace SmartSchool.StudentRelated
{
    internal class PalmerwormFactory : IManager<IContentItem>
    {
        private static PalmerwormFactory _Instence;

        public static PalmerwormFactory Instence
        {
            get
            {
                if (_Instence == null) _Instence = new PalmerwormFactory();
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
                typeof(BaseInfoPalmerwormItem),
                typeof(ClassInfoPalmerwormItem),   
                typeof(ParentInfoPalmerwormItem),
                typeof(PhonePalmerwormItem),
                typeof(AddressPalmerwormItem),
                typeof(AbsencePalmerwormItem),
                typeof(MeritPalmerwormItem),
                typeof(DemeritPalmerwormItem),
                //typeof(CourseScorePalmerwormItem),
                typeof(TeacherBiasItem),
                typeof(SemesterHistoryPalmerworm),
                typeof(DiplomaInfoPalmerworm),
                typeof(ExtensionValuesPalmerwormItem),
                typeof(ElectronicPaperPalmerworm),
                typeof(WordCommentPalmerworm),
            });

            foreach (Type type in _type_list)
            {
                if (!Attribute.IsDefined(type, typeof(FeatureCodeAttribute)) || CurrentUser.Acl[type].Viewable)
                {
                    try
                    {
                        IContentItem item = type.GetConstructor(Type.EmptyTypes).Invoke(null) as IContentItem;
                        _items.Add(item);
                    }
                    catch (Exception ex) { ExceptionHandler.BugReporter.ReportException(ex, false); }
                }
            }

            #region 先註解起來
            //_items.Add(new BaseInfoPalmerwormItem());
            //_items.Add(new ClassInfoPalmerwormItem());
            //_items.Add(new ParentInfoPalmerwormItem());
            //_items.Add(new PhonePalmerwormItem());
            //_items.Add(new AddressPalmerwormItem());
            ////_items.Add(new UpdatePalmerwormItem());   //換成Plugin了
            //_items.Add(new AbsencePalmerwormItem());
            //_items.Add(new MeritPalmerwormItem());
            //_items.Add(new DemeritPalmerwormItem());
            ////_items.Add(new SchoolYearScorePalmerworm());
            ////_items.Add(new SemesterScorePalmerworm());
            //_items.Add(new CourseScorePalmerwormItem());
            //_items.Add(new TeacherBiasItem());
            //_items.Add(new SemesterHistoryPalmerworm());
            ////_items.Add(new GraduateInfoPalmerworm());
            #endregion

            foreach (IContentItem var in plugInItems)
            {
                try
                {
                    if (!Attribute.IsDefined(var.GetType(), typeof(FeatureCodeAttribute)))
                        _items.Add((IContentItem)var.Clone());
                    else
                        if (CurrentUser.Acl[var.GetType()].Viewable)
                            _items.Add((IContentItem)var.Clone());
                }
                catch (Exception ex) { ExceptionHandler.BugReporter.ReportException(ex, false); }
            }

            return _items.ToArray();
        }

        #region IManager<IContentItem> 成員

        public void Add(IContentItem instance)
        {
            plugInItems.Add(instance);
            if ( ItemsChanged != null ) ItemsChanged(this, new EventArgs());
        }

        public void Remove(IContentItem instance)
        {
            plugInItems.Remove(instance);
            if ( ItemsChanged != null ) ItemsChanged(this, new EventArgs());
        }

        public event EventHandler ItemsChanged;
        #endregion
    }
}
