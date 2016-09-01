using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace Mvc_Qdis.Controllers
{
    public class DataShowController : Controller
    {
        //
        // GET: /DataShow/
         [Authorize]
        public ActionResult SHdata_Show()
        {
            return View();
        }
         [Authorize]
        public ActionResult JDdata_Show()
        {
            return View();
        }
         [Authorize]
        public ActionResult PJdata_Show()
        {
            return View();
        }

        [Authorize(Roles = "渠道客服组长,渠道客服主管,京东业务组长,淘宝客服组长,阿里现场调控,商品评论组长,渠道客服经理")]
        public ActionResult KPI_Show()
         {
             List<string> list = new List<string>();
             SqlDataReader obj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select  distinct 姓名 from KPI");
             while (obj.Read())
             {
                 list.Add(obj["姓名"].ToString());
             }
             obj.Close();
            ViewBag.ls = list;
            return View();
        }

        [HttpPost]
        public string rank()
        {
            string rank;
            string staff = Request["staff"];
            string month = Request["month"];
            if (month == "全年平均")
            {
                SqlDataReader obj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text,  "select * from (select 姓名, AVG(绩效) as  平均绩效,ROW_NUMBER() over(order by AVG(绩效) desc) as 排名 from KPI group by 姓名)t1 where 姓名='"+staff+"'");
                if (obj.Read())
                {
                    rank = (Convert.ToDecimal(obj["排名"].ToString()) / 167).ToString("0.00%");
                    return rank;
                }
                else
                {
                    return "error";
                }
               
            }
            else
            {
                month = month.Replace("月", "/");
                month = month.Replace("年", "/");
                month = month + "1";
                string month_end = month + "2";
                SqlDataReader obj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select 绩效排名占比 from KPI where 绩效月份 between '" + month + "' and '" + month_end + "' and 姓名='" + staff + "'");
                if (obj.Read())
                {

                    if (obj["绩效排名占比"].ToString() != "")
                    {
                        rank = (Convert.ToDecimal(obj["绩效排名占比"].ToString())).ToString("0.00%");
                        return rank;
                    }
                    else
                    {
                        return "null";
                    }

                }
                else
                {
                    return "error";
                }

            }
        }
    }
}
