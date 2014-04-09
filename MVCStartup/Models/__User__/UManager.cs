using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;
using MVCStartup.Models.__Interfaces;

namespace MVCStartup.Models.__User__
{
    public class UManager : UserEntities
    {
        
        //public Boolean IsUserValid(string emailAddress)
        //{
        //    IRetrieveUser<String> RetrieveDb = new RetrieveFromUserWithEmail();
        //    try
        //    {
        //        RetrieveDb.RetrieveFrom(emailAddress);
        //        return true;
        //    }
        //    catch(InvalidUserException)
        //    {
        //        throw;
        //    }
        //    catch (UnknownErrorException)
        //    {
        //        throw;
        //    }
        //}
        //public Boolean IsUserValid(Guid UserId)
        //{
        //    IRetrieveUser<Guid> RetrieveDb = new RetrieveFromUserWithGuid();
        //    try
        //    {
        //        RetrieveDb.RetrieveFrom(UserId);
        //        return true;
        //    }
        //    catch (InvalidUserException)
        //    {
        //        throw;
        //    }
        //    catch (UnknownErrorException)
        //    {
        //        throw;
        //    }
        //}
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="userModel">Model of the User Entity</param>
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
        /// <summary>
        /// Logs in the user and creates a session.
        /// </summary>
        /// <param name="emailAddress">emailddress of the user</param>
        /// <param name="password">password of the user</param>
        public void Login(string emailAddress, string password)
        {
            IRetrieveDb<User, String> RetrieveDb = new RetrieveFromUserWithEmail();
            HashPassword hash = new HashPassword();
            string email = emailAddress;
            string nonHashedPassword = password;

            try
            {
                var user = RetrieveDb.RetrieveFrom(email);
                var hashPassword = hash.HashString(password);
                var hashDbPassword = hash.HashString(user.Password, user.Salt);
                var isValidPassword = HashPassword.ValidatePassword(password, user.Password);
                if (isValidPassword)
                {
                    USession.CurrentUser.valid = 1;
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
            catch (Exception ex)
            {
                throw new UnknownErrorException(ex);
            }
            



        }
      
    }
}
