using MVCStartup.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStartup.Models.__User__;
using MVCStartup.Controllers.Attributes;
using System.Web.SessionState;
using System.Web.Security;

namespace MVCStartup.Controllers
{
    public class AppController : Controller
    {
        //
        // GET: /App/
        //[ValidateUserLogin]
        [RequireHttps]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [RequireHttps]
        public ActionResult Login()
        {
            return View();
        }
        [RequireHttps]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            UManager userManager = new UManager();
            if (ModelState.IsValid)
            {
                try
                {

                    userManager.Login(user.EmailAddress, user.Password);
                    return RedirectToAction("index");
                }
                catch (InvalidUserException ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return View(); 
                }
                catch (UnknownErrorException ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return View(); 
                }
            }
            else
            {
                return View();
            }
           
        }
        public ActionResult Logout()
        {
            USession.KillSession();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            UManager userManager = new UManager();
            if (ModelState.IsValid)
            {
                try
                {
                    userManager.CreateUser(user);
                    return RedirectToAction("Login");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return View();
                }
                catch (UnknownErrorException ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
	}
}