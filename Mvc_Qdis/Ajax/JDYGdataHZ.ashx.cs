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
    /// JDYGdataHZ 的摘要说明
    /// </summary>
    public class JDYGdataHZ : IHttpHandler
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
            string sql = "SELECT [姓名],[米聊号],[组别],sum([咨询回复]) as [咨询回复],sum(convert(float,[评价])) as [评价],sum(convert(float,[投诉处理])) as [投诉处理] FROM [dbo].[JDYGdata] where len(姓名)<4 and convert(date,日期) between  '" + date1 + "' and '" + date2 + "' group by 姓名,组别,米聊号";
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