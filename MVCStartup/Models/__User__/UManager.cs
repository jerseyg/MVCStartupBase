using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCStartup.Models
{
    public class UManager : UserEntities
    {
        
        public Boolean IsUserValid(string emailAddress)
        {
            IRetrieve<String> RetrieveDb = new RetrieveFromUserWithEmail();
            try
            {
                RetrieveDb.RetrieveFrom(emailAddress);
                return true;
            }
            catch (UnknownErrorException)
            {
                throw;
            }
        }
        public Boolean IsUserValid(Guid UserId)
        {
           var query = (from user in Users
                        where user.UserId == UserId
                        select user).Count();
           var isValid = query;
           return (isValid != 0) ? true : false;
        }
        public void CreateUser(User userModel)
        {
            IApplyDb<User> ApplyDb = new ApplyToNewUser();
            try
            {
                ApplyDb.ApplyTo(userModel);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (UnknownErrorException)
            {
                throw;
            }
        }
        public User GetUserRow(string emailAddress)
        {
            var query = (from user in Users
                         where user.EmailAddress == emailAddress
                         select user).First();

            return query;
        }
        public void Login(string emailAddress, string password)
        {
            HashPassword hash = new HashPassword();
            string email = emailAddress;
            string nonHashedPassword = password;

            var isValid = IsUserValid(email);
            if (isValid)
            {
                var user = GetUserRow(email);
                var salt = user.Salt;
                var hashedPassword = hash.HashString(nonHashedPassword, salt);
                var userDbPassword = user.Password;

                if (hashedPassword == userDbPassword)
                {
                    USession.CurrentUser.UserId = user.UserId;
                    USession.CurrentUser.Username = email;
                    USession.CurrentUser.FirstName = user.FirstName;
                    USession.CurrentUser.LastName = user.LastName;
                  
                }
                else
                {
                    //password not match
                    throw new InvalidUserException();
                }

            }
            else
            {
                //user not found
                throw new InvalidUserException();
            }
        }
      
    }
}