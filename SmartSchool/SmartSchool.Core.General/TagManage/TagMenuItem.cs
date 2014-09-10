using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using System.Windows.Forms;

namespace SmartSchool.TagManage
{
    public class TagMenuItem : ButtonItem, ITagCheckState
    {
        private TagInfo _tag;
        private int _state_count;

        public TagMenuItem(TagInfo tag)
        {
            _tag = tag;
            _state_count = 0;
            _origin_state = CheckState.Unchecked;
        }

        #region ITagCheckState 成員

        public int TagIdentity
        {
            get { return _tag.Identity; }
        }

        public TagInfo StateOwner
        {
            get { return _tag; }
        }

        public CheckState CurrentState
        {
            get
            {
                if (Checked)
                    return CheckState.Checked;
                else
                    return CheckState.Unchecked;
            }
        }

        private CheckState _origin_state;
        public CheckState OriginState
        {
            get { return _origin_state; }
        }

        public void AddStateCount()
        {
            _state_count++;
        }

        public void CalcuateState(int totalCount)
        {
            if (_state_count == 0)
                _origin_state = CheckState.Unchecked;

            else if (_state_count == totalCount)
                _origin_state = CheckState.Checked;

            else if (_state_count != totalCount)
                _origin_state = CheckState.Unchecked;

            if (_origin_state == CheckState.Checked)
                Checked = true;
            else
                Checked = false;

            _state_count = 0;
        }

        #endregion
    }
}
