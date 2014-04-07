using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCStartup.Models.__Interfaces;

namespace MVCStartup.Models
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception inner) : base(message, inner) { }
        protected CustomException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    [Serializable]
    public class InvalidUserException : CustomException
    {
            public InvalidUserException() :base("Wrong username and password"){ }
            public InvalidUserException(string message) : base(message) { }
            public InvalidUserException(string message, Exception inner) : base(message, inner) { }
            protected InvalidUserException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        
    }
    [Serializable]
    public class UnknownErrorException : CustomException
    {
        public UnknownErrorException() : base("An Unknown error occured, See database table TrackException for more info") { }
        public UnknownErrorException(Exception ex) : base("An Unknown error occured, See database table TrackException for more info") { UploadException.Upload(ex); }
        public UnknownErrorException(string message) : base(message) { }
        public UnknownErrorException(string message, Exception inner) : base(message, inner) { }
        protected UnknownErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }

    public class UploadException : TrackExceptionEntities
    {
        
        public static void Upload(Exception ex)
        {
            IApplyDb<TrackException> ApplyDb = new ApplyToTrackException();
            TrackException tException = new TrackException();
            tException.HelpLink = ex.HelpLink;
            tException.HResult = ex.HResult.ToString();
            tException.InnerException = GetInnerException(ex);
            tException.Message = ex.Message;
            tException.Source = ex.Source;
            tException.StackTrace = ex.StackTrace;
            tException.TargetSite = ex.TargetSite.Name;
            ApplyDb.ApplyTo(tException);

        }
        public static string GetInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return string.Format("{0}", GetInnerException(ex.InnerException));
            }
            return string.Empty;
        }
    }
}
