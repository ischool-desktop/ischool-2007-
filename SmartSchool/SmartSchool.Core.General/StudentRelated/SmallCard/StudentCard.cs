using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace SmartSchool.StudentRelated.SmallCard
{
    internal class StudentCard
    {
        private const int CardWidth=145;
        
        #region static
        protected static Dictionary<Control, StudentCardCollection> CardCollections;

        static StudentCard()
        {
            CardCollections = new Dictionary<Control, StudentCardCollection>();
        }

        protected static void _container_SizeChanged(object sender, EventArgs e)
        {
            Control _container = (Control)sender;
            int contentwidth = (_container.DisplayRectangle.Width);
            int newWidth = (contentwidth % CardWidth) / (contentwidth / CardWidth + 1);
            if ((contentwidth / CardWidth + 1) == 1)
                newWidth = 2;
            //CardCollections[_container].MarginLeft = newWidth;
        }

        protected static void container_Disposed(object sender, EventArgs e)
        {
            Control _container = (Control)sender;
            CardCollections.Remove(_container);
        } 
        #endregion

        private StudentCardCollection _CardCollection;

        private Control _Container;

        private string _Name;

        private string _Id;

        private string _Class;

        private string _SeatNo;

        private bool _Show;

        private UCStudentCard _DisplayCard;

        public StudentCard(Control container)
        {
            _Container = container;
            if (!CardCollections.ContainsKey(container))
            {
                _Container.Disposed += new EventHandler(container_Disposed);
                //_Container.SizeChanged += new EventHandler(_container_SizeChanged);
                _CardCollection = new StudentCardCollection();
                _CardCollection.Container = container;
                CardCollections.Add(container, _CardCollection);
                _container_SizeChanged(container, null);
            }
            else
            {
                _CardCollection = CardCollections[container];
            }
            _Show = false;
        }

        public string StudentName
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string StudentID
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string StudentClass
        {
            get { return _Class; }
            set { _Class = value; }
        }
        public string StudentSeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value; }
        }
        public void Show()
        {
            if (!_Show)
            {
                //_Show = true;
                //ShowList[_Container].Add(this);
                //if (!BkwShow[_Container].IsBusy)
                //{
                //    _Container.SuspendLayout();
                //    BkwShow[_Container].RunWorkerAsync(_Container);
                //}
                if (_CardCollection.HideList.Count > 0)
                {
                    _DisplayCard = _CardCollection.HideList[0];
                    _CardCollection.HideList.RemoveAt(0);
                    _CardCollection.ShowList.Add(_DisplayCard);
                }
                else
                {
                    _DisplayCard = new UCStudentCard();
                    _DisplayCard.Visible = false;
                    _Container.Controls.Add(_DisplayCard);
                    //_DisplayCard.Margin = new Padding(_CardCollection.MarginLeft, _DisplayCard.Margin.Top, 0, _DisplayCard.Margin.Bottom);
                    _CardCollection.ShowList.Add(_DisplayCard);
                }
                _DisplayCard.StudentClass = _Class;
                _DisplayCard.StudentID = _Id;
                _DisplayCard.StudentName = _Name;
                _DisplayCard.StudentSeatNo = _SeatNo;
                _DisplayCard.Visible = true;
                _Show = true;
            }
        }
        public void Hide()
        {
            if (_Show)
            {               
                _DisplayCard.StudentClass = _Class;
                _DisplayCard.StudentID = _Id;
                _DisplayCard.StudentName = _Name;
                _DisplayCard.StudentSeatNo = _SeatNo;
                _DisplayCard.Visible = false;
                _CardCollection.ShowList.Remove(_DisplayCard);
                _CardCollection.HideList.Add(_DisplayCard);
                _Container.Controls.SetChildIndex(_DisplayCard, _Container.Controls.Count - 1);
                _DisplayCard = null;
                _Show = false;
            }
        }
    }
}
