using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCStartup.Models
{
    public class UserManager : UserEntities
    {
        public Boolean IsUserValid(string emailAddress)
        {
            var query = (from user in Users
                         where user.EmailAddress == emailAddress
                         select user).Count();
            var isValid = query;
            return (isValid != 0) ? true : false;
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
            userModel.UserId = Guid.NewGuid();
            userModel.Password = HashPassword(userModel.Password);
            userModel.Salt = Salt;

            try
            {
                Users.Add(userModel);
                SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
        public User GetUserRow(string emailAddress)
        {
            var query = (from user in Users
                         where user.EmailAddress == emailAddress
                         select user).First();
            var userRow = query;

            return userRow;
        }
        public Boolean Login(string emailAddress, string password)
        {
            string email = emailAddress;
            string nonHashedPassword = password;

            var isValid = IsUserValid(email);
            if (isValid)
            {
                var user = GetUserRow(email);
                var salt = user.Salt;
                var hashedPassword = HashPassword(nonHashedPassword, salt);
                var userDbPassword = user.Password;

                if (hashedPassword == userDbPassword)
                {
                    UserSession.CurrentUser.UserId = user.UserId;
                    UserSession.CurrentUser.Username = email;
                    UserSession.CurrentUser.FirstName = user.FirstName;
                    UserSession.CurrentUser.LastName = user.LastName;

                    return true;
                }
                else
                {
                    //password not match
                    return false;
                }

            }
            else
            {
                //user not found
                return false;
            }
        }


        public string Salt { get; set; }
        public string HashPassword(string password)
        {
            var salt = PasswordHash.CreateSalt();
            Salt = salt;
            byte[] byteArraySalt = Encoding.UTF8.GetBytes(salt);
            var hash = PasswordHash.CreateHash(password, byteArraySalt);
            return hash;
        }
        public string HashPassword(string password, string salt)
        {
            byte[] byteArraySalt = Encoding.UTF8.GetBytes(salt);
            var hash = PasswordHash.CreateHash(password, byteArraySalt);
            return hash;
        }
    }
}