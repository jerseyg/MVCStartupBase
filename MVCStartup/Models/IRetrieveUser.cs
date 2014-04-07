using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCStartup.Models.__Interfaces;
namespace MVCStartup.Models
{
    public class RetrieveFromUserWithEmail : UserEntities , IRetrieveDb<User, String>
    {
        /// <summary>
        /// Gets the first row from the user table with an emailaddress
        /// </summary>
        /// <param name="emailAddress">emailaddress of the user</param>
        /// <returns>User Model</returns>
        public User RetrieveFrom(string emailAddress)
        {
            try
            {
                var query = from user in Users
                             where user.EmailAddress == emailAddress
                             select user;
                var resultCount = query.Count();
                if (resultCount == 1)
                {
                    return query.First();
                }
                else
                {
                    throw new InvalidUserException();
                }
            }
            catch (Exception ex)
            {
                throw new UnknownErrorException(ex);
            }

        }
    }
    /// <summary>
    /// Gets the first row from the user table with a guid
    /// </summary>
    public class RetrieveFromUserWithGuid : UserEntities, IRetrieveDb<User, Guid>
    {
        /// <summary>
        /// Gets the first row from the user table with a guid
        /// </summary>
        /// <param name="emailAddress">guid of the user</param>
        /// <returns>User Model</returns>
        public User RetrieveFrom(Guid guid)
        {
            try
            {
                var query = from user in Users
                             where user.UserId == guid
                             select user;
                var resultCount = query.Count();
                if (resultCount == 1)
                {
                    return query.First();
                }
                else
                {
                    throw new InvalidUserException();
                }
            }
            catch (Exception ex)
            {
                throw new UnknownErrorException(ex);
            }
        }
    }
}