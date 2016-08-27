using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IBatis.Model;
using System.Data.SqlClient;
using System.Data;

namespace Mvc_Qdis.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            string zw = "天猫客服专员";
            if (System.Web.HttpContext.Current.Request.Cookies["login"] != null)
            {
                var uid = System.Web.HttpContext.Current.Request.Cookies["login"].Values["uid"];
                SqlDataReader obj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select TOP(1) 日期 from SQdata order by 日期 desc");
                if (obj.Read())
                {
                    ViewBag.time = "数据更新时间：" + Convert.ToDateTime(obj["日期"]).ToString("yyyy-MM-dd");
                    obj.Close();
                }
                SqlDataReader zwj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select 职位 from ISRegister where UserName=@uid", new SqlParameter("uid", uid));
                if (zwj.Read())
                {
                    zw = zwj["职位"].ToString();
                    zwj.Close();
                }
                if (zw == "天猫客服专员")
                {
                    ViewBag.show = false;
                    return View();
                }
                else if (zw == "阿里售后专员" || zw == "升级投诉专员")
                {
                    return Redirect("index-sh.aspx");
                }
                else if (zw == "京东业务专员" || zw == "京东业务组长")
                {
                    return Redirect("index-jd.aspx");
                }
                else if (zw == "商品评论组长" || zw == "商品评论专员")
                {
                    return Redirect("index-pj.aspx");
                }
                else if (zw == "淘宝客服专员" || zw == "淘宝客服组长")
                {
                    return Redirect("index-tb.aspx");
                }

                else if (zw == "错误")
                {
                    return Redirect("../login.aspx");
                }
                else
                {
                    ViewBag.show = true;
                    return View();
                }
            }
            ViewBag.show = true;
            return View();
        }


    }
}
