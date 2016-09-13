using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using Mvc_Qdis.Models;
using System.Data.SqlClient;
using IBatis.Model;

namespace Mvc_Qdis.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Login/

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var msg = new LoginInfo();
            msg.Errormsg = false;
            ViewBag.returnUrl = returnUrl;
            return PartialView(msg);
        }

         [HttpPost]
        public ActionResult Login(string returnUrl, LoginInfo info)
        {
            ViewBag.returnUrl = returnUrl;
            info.username = Request["username"];
            info.password = Request["password"];
            info.cbox =Request["cbox"];
            string data = Login(info.username, info.password, info.cbox).ToString();
            if (data == "1")
            {
                try
                {
                    return Redirect(returnUrl);
                }
                catch
                {
                    return Redirect("/home/index");
                }
            }
            else
            {
                info.Errormsg = true;
                return PartialView(info);
            }
        }



        public string Login(string username, string password, string cbox)
        {
            //身份验证
            SqlDataReader obj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select * from ISRegister where UserName=@UserName and PassWord =@PassWord", new SqlParameter("UserName", username.Trim()), new SqlParameter("PassWord", password.Trim()));
            string data;

            if (obj.Read())
            {
                string role = obj["职位"].ToString();
                string name = obj["姓名"].ToString();
                obj.Close();
                FormsAuthenticationTicket authTicket;
                if (cbox=="true")
                {
                    //MVC票据生成
                     authTicket = new FormsAuthenticationTicket(
                                      1,
                                      username,
                                      DateTime.Now,
                                      DateTime.Now.AddDays(14),
                                      false,
                                      role
                                      );
                     HttpCookie cookie = new HttpCookie("login");
                     cookie.Values.Add("uid", username.Trim());
                     cookie.Values.Add("name",HttpUtility.UrlEncode(name.Trim()));
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
                                          role
                      );
                    }
                    HttpCookie cookie = new HttpCookie("login");
                    cookie.Values.Add("uid", username.Trim());
                    cookie.Values.Add("name", HttpUtility.UrlEncode(name.Trim()));
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
                obj.Close();
                data = "0";
                return data;
            }
           
        }

        [HttpPost]
        public void SignOut()//登出系统
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/account/login");
        }
    }
}
