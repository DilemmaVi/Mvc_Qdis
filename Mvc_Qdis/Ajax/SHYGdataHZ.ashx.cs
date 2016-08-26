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
    /// SHYGdataHZ 的摘要说明
    /// </summary>
    public class SHYGdataHZ : IHttpHandler
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
            string sql = "select 员工姓名,组别,SUM(convert(float,售前审单)) as 售前审单, SUM(convert(float,退款管理))as 退款管理, SUM(convert(float,法政先锋)) as 法政先锋,SUM(convert(float,AACC工单))as AACC工单,SUM(convert(float,[物流异常（事务/客建）]))as [物流异常（事务/客建）],SUM(convert(float,[物流异常（承运商）]))as [物流异常（承运商）],SUM(convert(float,[大家电（事务）]))as [大家电（事务）],SUM(convert(float,[大家电（异常）]))as [大家电（异常）],SUM(convert(float,[大家电（退款）]))as [大家电（退款）],SUM(convert(float,外呼确认))as 外呼确认,SUM(convert(float,收货交接))as 收货交接,SUM(convert(float,特例))as 特例 from SHYGdata where len(组别)<4 and convert(date,日期) between '" + date1 + "' and '" + date2 + "' group by 员工姓名,组别";
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