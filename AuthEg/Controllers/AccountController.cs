using AuthEg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthEg.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            organizationEntities oe = new organizationEntities();
            var count = oe.Users.Where(x => x.UserName == u.UserName && x.Password == u.Password).ToList().Count();
            if (count == 0) 
            {
                ViewBag.message = "Invalid user";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}