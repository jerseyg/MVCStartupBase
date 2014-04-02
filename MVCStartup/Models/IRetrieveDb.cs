using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStartup.Models
{
    public interface IRetrieve<T>
    {
        void RetrieveFrom(T item);
    }
    public class RetrieveFromUserWithEmail : UserEntities , IRetrieve<String>
    {

        public void RetrieveFrom(string emailAddress)
        {
            var query = (from user in Users
                         where user.EmailAddress == emailAddress
                         select user).Count();
            if (query == 1) { /*Do nothing*/ }
            else { throw new InvalidUserException(); }
        }
    }
}