using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStartup.Models
{
    public class USession
    {
        public static USession CurrentUser
        {
            get
            {
                USession session =
                (USession)HttpContext.Current.Session["__UserSession__"];
                if (session == null)
                {
                    session = new USession();
                    HttpContext.Current.Session["__UserSession__"] = session;
                }
                return session;
            }
        }

        public static void KillSession()
        {
                HttpContext.Current.Session["__UserSession__"] = null;
                HttpContext.Current.Session.Abandon();                         
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}