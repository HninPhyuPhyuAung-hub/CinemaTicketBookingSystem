using cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace cinema.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login)
        {
            cinemacontext context = new cinemacontext();
            if(ModelState.IsValid)
            {
                var result = context.AdminUserset.Where(a => a.Email == login.Email && a.Password == login.Password).FirstOrDefault();
                if(result!=null)
                {
                    FormsAuthentication.SetAuthCookie(login.Email, true);
                    SetCookie("cinemaCookie", result.Id, result.Username, result.Email, result.PhoneNo, result.Role);
                    if (result.Role == "Staff")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Unauthorize = "Please enter valid user name or email and password";
                }
                return View();
            }
            return View();
        }
      public void SetCookie(string CookieName,int Id,string Username,string Email,string PhoneNo,string Role)

        {
            HttpCookie myCookie = HttpContext.Request.Cookies[CookieName] ?? new HttpCookie(CookieName);
            myCookie.Values["Id"] = Id.ToString();
            myCookie.Values["Username"] = Username;
            myCookie.Values["Email"] = Email;
            myCookie.Values["PhoneNo"] = PhoneNo;
            myCookie.Values["Role"] = Role;
            myCookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Response.Cookies.Add(myCookie);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            RemoveCookie("cinemaCookie");
            Response.Cookies["cinemaCookie"].Expires = DateTime.Now.AddYears(-30);
            Session.Clear();
            return RedirectToAction("Login", "UserLogin");
        }
        public Boolean RemoveCookie(string Cookiename)
        {
            if(HttpContext.Request.Cookies[Cookiename]!=null)
            {
                HttpCookie myCookie = HttpContext.Request.Cookies[Cookiename];
                myCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(myCookie);
                return true;
            }
            return false;
        }
    }
}