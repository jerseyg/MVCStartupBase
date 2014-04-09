using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVCStartup.Models;
using System.Web.Mvc;
using MVCStartup.Models.__User__;
using System.Web;

namespace MVCStartup.Controllers.Attributes
{
        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        sealed class ValidateUserLogin : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                var sessionValid = USession.CurrentUser.SessionId;
                if (sessionValid != null) { }
                else { filterContext.Result = new RedirectResult("/App/Login"); }
            }
    }
}
