using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;


namespace 渠道客服系统.Nhome.Ajax
{
    /// <summary>
    /// PJYGdataHZ 的摘要说明
    /// </summary>
    public class PJYGdataHZ : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var date1 = context.Request["date1"].ToString();
            var date2 = context.Request["date2"].ToString();
            if (date1 == "0")
            {
                string year = DateTime.Today.Year.ToString();
                string month = (Convert.ToInt32(DateTime.Today.Month.ToString()) - 1).ToString();
                string month1 = DateTime.Today.Month.ToString();
                var day = Convert.ToInt32(DateTime.Today.Day.ToString());
                if (day > 19)
                {
                    date1 = string.Format(year + "/" + month1 + "/" + "20");
                }
                else
                {
                    date1 = string.Format(year + "/" + month + "/" + "20");
                }

                date2 = DateTime.Today.ToString();
            }
            string sql = "SELECT [姓名],sum([天猫追评]) as 天猫追评,sum([天猫首页]) as 天猫首页,sum([天猫负评]) as 天猫负评,sum([天猫分类处理量]) as [天猫分类处理量],sum(convert(float,[天猫评价审核量])) as [天猫评价审核量],sum([淘宝追评]) as [淘宝追评],sum([淘宝中评]) as [淘宝中评],sum([淘宝差评]) as [淘宝差评],sum([淘宝退款评价]) as [淘宝退款评价] FROM [dbo].[PJYGdata] where len(店铺)<3 and convert(date,日期) between '" + date1 + "' and '" + date2 + "' group by 姓名";
            DataTable table = Mvc_Qdis.Helper.SqlHelper.ExecuteDataTable(Mvc_Qdis.Helper.SqlHelper.connstr, CommandType.Text, sql, null);
            context.Response.ContentType = "text/plain";
            context.Response.Write(Mvc_Qdis.Helper.JSONHelper.DataTableToJSON(table));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}