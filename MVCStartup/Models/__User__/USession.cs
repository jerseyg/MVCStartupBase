using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStartup.Models.__User__
{
    /// <summary>
    /// A session wrapper for the user
    /// </summary>
    public class USession
    {
        /// <summary>
        /// Creats or retrieves the current Session
        /// </summary>
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

        /// <summary>
        /// Destroys the current session
        /// </summary>
        public static void KillSession()
        {

                USession.CurrentUser.SessionId = null;
                HttpContext.Current.Session["__UserSession__"] = null;
                HttpContext.Current.Session.Abandon();                         
        }

        public string SessionId { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}