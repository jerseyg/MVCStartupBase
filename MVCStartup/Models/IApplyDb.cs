using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MVCStartup.Models
{
    public interface IApplyDb<T>
    {
        void ApplyTo(T item);

    }
    public class ApplyToNewUser : IApplyDb<User>
    {
        UserEntities db = new UserEntities();
        HashPassword hash = new HashPassword();
        public void ApplyTo(User userModel)
        {
           
            userModel.UserId = Guid.NewGuid();
            userModel.Password = hash.HashString(userModel.Password);
            userModel.Salt = hash.Salt;

            try
            {
                db.Users.Add(userModel);
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException("A duplicate email exists within the database");
            }
            catch (Exception)
            {
                throw new UnknownErrorException();
            }
        }
    }
    public class ApplyToTrackException : IApplyDb<TrackException>
    {
        TrackExceptionEntities db = new TrackExceptionEntities();
        public void ApplyTo(TrackException trackExceptionModel)
        {
            db.TrackExceptions.Add(trackExceptionModel);
            db.SaveChanges();
        }
    }

    public class HashPassword : PasswordHash
    {
        public string Salt { get; set; }
        public string HashString(string password)
        {
            var salt = CreateSalt();
            Salt = salt;
            byte[] byteArraySalt = System.Text.Encoding.UTF8.GetBytes(salt);
            var hash = CreateHash(password, byteArraySalt);
            return hash;
        }
        public string HashString(string password, string salt)
        {
            byte[] byteArraySalt = System.Text.Encoding.UTF8.GetBytes(salt);
            var hash = CreateHash(password, byteArraySalt);
            return hash;
        }
    }
}