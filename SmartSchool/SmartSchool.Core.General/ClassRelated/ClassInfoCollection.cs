using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using SmartSchool.Common;

namespace SmartSchool.ClassRelated
{    
    public class ClassInfoCollection : ReadOnlyCollection<string, ClassInfo>
    {
        internal ClassInfoCollection(Dictionary<string, ClassInfo> items) : base(items) { }
    }

    //internal class ClassInfoCollection : IEnumerable<ClassInfo>
    //{
    //    private Dictionary<string, ClassInfo> _Items;
    //    internal ClassInfoCollection(Dictionary<string, ClassInfo> items)
    //    {
    //        _Items = items;
    //    }
    //    public ClassInfo this[string ID]
    //    {
    //        get
    //        {
    //            if (_Items.ContainsKey(ID))
    //                return _Items[ID];
    //            else
    //                return null;
    //        }
    //    }

    //    #region IEnumerable<GraduationPlanInfo> 成員

    //    public IEnumerator<ClassInfo> GetEnumerator()
    //    {
    //        return ((IEnumerable<ClassInfo>)_Items.Values).GetEnumerator();
    //    }

    //    #endregion

    //    #region IEnumerable 成員

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return _Items.Values.GetEnumerator();
    //    }

    //    #endregion
    //}
}
