using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStartup.Models.__Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApplyDb<T>
    {
        void ApplyTo(T item);

    }
}