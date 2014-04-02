using MVCStartup.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStartup.Controllers
{
    public class AppController : Controller
    {
        //
        // GET: /App/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
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
                catch(Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return View(); 
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            UManager userManager = new UManager();

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
	}
}