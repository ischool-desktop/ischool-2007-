using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.Data
{
    public class FillScoreInfoEventArgs<T> : FillEventArgs<T>
    {
        private bool _FilterRepeat;

        internal FillScoreInfoEventArgs(AccessHelper accessHelper, bool filterRepeat, IEnumerable<T> list)
            : base(accessHelper, list)
        {
            _FilterRepeat = filterRepeat; 
        }
        public bool FilterRepeat
        {
            get { return _FilterRepeat; }
        }
    }
}
