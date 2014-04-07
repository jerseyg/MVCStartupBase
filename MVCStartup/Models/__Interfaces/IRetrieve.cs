using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStartup.Models.__Interfaces
{
    /// <summary>
    /// Generic interface with an enitity model
    /// </summary>
    /// <typeparam name="Model">The Entity Model</typeparam>
    /// <typeparam name="Type">Value Type</typeparam>
    public interface IRetrieveDb<Model, Type>
    {       
        /// <summary>
        /// Generic void method for retrieving from an enitity model
        /// </summary>
        /// <param name="value">Generic arugment type specified by the interface</param>
        /// <returns>Entity Model</returns>
        Model RetrieveFrom(Type value);
    }
}