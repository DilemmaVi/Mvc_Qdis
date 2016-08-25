using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using Mvc_Qdis.Models;

namespace Mvc_Qdis.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return PartialView();
        }

        public string test()
        {
            return "1";
        }

        [HttpPost]
        public string Login(string username, string password, bool cbox)
        {
            object obj = BookDAL.SqlHelper.ExecuteScalar(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select * from ISRegister where UserName=" + username.Trim() + " and PassWord = " + password.Trim() + "");
            string data;
            if (obj!=null)
            {
                FormsAuthenticationTicket authTicket;
                if (cbox)
                {
                     authTicket = new FormsAuthenticationTicket(
                                      1,
                                      username,
                                      DateTime.Now,
                                      DateTime.Now.AddDays(14),
                                      false,
                                      "admin"
                                      );
                     HttpCookie cookie = new HttpCookie("login");
                     cookie.Values.Add("uid", username.Trim());
                     cookie.Expires = DateTime.Now.AddDays(14);
                     Response.Cookies.Add(cookie);
                }
                else
                {
                    {
                      authTicket = new FormsAuthenticationTicket(
                                          1,
                                          username,
                                          DateTime.Now,
                                          DateTime.Now.AddMinutes(30),
                                          false,
                                          "admin"
                      );
                    }
                    HttpCookie cookie = new HttpCookie("login");
                    cookie.Values.Add("uid", username.Trim());
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(cookie);

                }
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                data = "1";
                return data;
            }
            else
            {
                data = "0";
                return data;
            }

        }

        [HttpPost]
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/account/login");
        }
    }
}
