using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Mvc_Qdis.Controllers
{
    public class SuggestController : Controller
    {
        //
        // GET: /Suggest/
        [Authorize]
        [HttpGet]
        public ActionResult Suggestion(bool? pd)
        {
            ViewBag.pd = "false";
            return View();
        }

        [Authorize]
        [Mvc_Qdis.App_Start.NoResubmit]
        [HttpPost]
        public ActionResult Suggestion()
        {
            string time = DateTime.Now.ToString();
            string type = Request["SelectVal"];
            string text = Request["textbox"];
            string jj = "未解决";
            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
            try
            {
                SqlConnection Sqlconn = new SqlConnection(connString);
                Sqlconn.Open();
                string sql = "insert into suggest values('" + time + "','" + type + "','" + text + "','" + jj + "')";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.ExecuteNonQuery();
                Sqlconn.Close();
                ViewBag.pd = "true";
                return View();

            }
            catch 
            {
                ViewBag.pd = "error";
                return View();
            }

        }

    }
}
