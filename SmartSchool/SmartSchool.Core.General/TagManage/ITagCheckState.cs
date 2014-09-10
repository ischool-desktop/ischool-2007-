using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartSchool.TagManage
{
    public interface ITagCheckState
    {
        int TagIdentity { get;}

        TagInfo StateOwner { get;}

        CheckState CurrentState { get;}

        CheckState OriginState { get;}

        void AddStateCount();

        void CalcuateState(int totalCount);
    }
}
