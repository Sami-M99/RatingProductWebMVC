using RatingProductWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RatingProductWebMVC.Controllers
{
    public class AccountController : Controller
    {
        private RatingProductEntities dp = new RatingProductEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string username , string password)
        {
            var count = dp.Accounts.Count(a => a.UserName.Equals(username) && a.Password.Equals(password));
            if(count > 0)
            {
                Session["username"] = username;
                return RedirectToAction("Index","Product");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Login");
            }
            
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(string username, string password, string fullname)
        {
            var account = new Account() { UserName=username, Password=password ,FullName=fullname};
            dp.Accounts.Add(account);
            dp.SaveChanges();

            return RedirectToAction("Login", "Account");
            //if (username==null || password==null || fullname==null)
            //{

            //    ViewBag.error = "Invalid Account";
            //    return View("SignUp");

                
            //}
            //else
            //{
            //    dp.Accounts.Add(account);
            //    dp.SaveChanges();

            //    return RedirectToAction("Login", "Account");
            //}
        }


        //[HttpGet]
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Product");
        }


        //[HttpPost]
        //public ActionResult Logout(Account account)
        //{
            
        //    return View(Session["username"] = null);
            
        //}
    }
}