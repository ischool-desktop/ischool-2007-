using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SmartSchool.Broadcaster
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event
    {
        private List<EventHandler<EventArguments>> _BeforeList = new List<EventHandler<EventArguments>>();

        private List<EventHandler<EventArguments>> _AfterList = new List<EventHandler<EventArguments>>();
        /// <summary>
        /// 處裡這個事件
        /// </summary>
        public event EventHandler<EventArguments> Handler;
        /// <summary>
        /// 
        /// </summary>
        public void AddBeforeInvokeHandler(EventHandler<EventArguments> dele)
        {
            _BeforeList.Add(dele);
        }
        /// <summary>
        /// 
        /// </summary>
        public void AddAfterInvokeHandler(EventHandler<EventArguments> dele)
        {
            _AfterList.Add(dele);
        }
        /// <summary>
        /// 發送事件
        /// </summary>
        public void Invoke(EventArguments args)
        {
            foreach ( EventHandler<EventArguments> dele in _BeforeList )
            {
                dele.Invoke(this, args);
            }
            if ( Handler != null )
                Handler(this, args);
            foreach ( EventHandler<EventArguments> dele in _AfterList )
            {
                dele.Invoke(this, args);
            }
        }
        /// <summary>
        /// 發送事件
        /// </summary>
        public void Invoke()
        {
            foreach ( EventHandler<EventArguments> dele in _BeforeList )
            {
                dele.Invoke(this, new EventArguments());
            }
            if ( Handler != null )
                Handler(this, new EventArguments());
            foreach ( EventHandler<EventArguments> dele in _AfterList )
            {
                dele.Invoke(this, new EventArguments());
            }
        }
        /// <summary>
        /// 發送事件
        /// </summary>
        public void Invoke(IEnumerable items)
        {
            foreach ( EventHandler<EventArguments> dele in _BeforeList )
            {
                dele.Invoke(this, new EventArguments(items));
            }
            if ( Handler != null )
                Handler(this, new EventArguments(items));
            foreach ( EventHandler<EventArguments> dele in _AfterList )
            {
                dele.Invoke(this, new EventArguments(items));
            }
        }
        /// <summary>
        /// 發送事件
        /// </summary>
        public void Invoke(params object[] items)
        {
            foreach ( EventHandler<EventArguments> dele in _BeforeList )
            {
                dele.Invoke(this, new EventArguments(items));
            }
            if ( Handler != null )
                Handler(this, new EventArguments(items));
            foreach ( EventHandler<EventArguments> dele in _AfterList )
            {
                dele.Invoke(this, new EventArguments(items));
            }
        }
    }
}