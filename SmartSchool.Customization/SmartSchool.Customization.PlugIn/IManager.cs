using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchool.Customization.PlugIn
{
    /// <summary>
    /// 通用管理介面
    /// </summary>
    /// <typeparam name="T">管理項目型別</typeparam>
    public interface IManager<T>
    {
        /// <summary>
        /// 新增
        /// </summary>
        void Add(T instance);
        /// <summary>
        /// 移除
        /// </summary>
        void Remove(T instance);
    }
}
