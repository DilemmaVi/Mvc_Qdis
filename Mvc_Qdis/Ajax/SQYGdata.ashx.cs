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
    /// SQYGdata 的摘要说明
    /// </summary>
    public class SQYGdata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var date = context.Request["date"].ToString();
            var date1 = context.Request["date1"].ToString();
            if (date == "0")
            {
                string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
                SqlConnection Sqlconn = new SqlConnection(connString);
                Sqlconn.Open();
                SqlCommand cmd = Sqlconn.CreateCommand();
                cmd.CommandText = string.Format("select TOP(1) CONVERT(date,日期) as 日期 from SQdata order by CONVERT(date,日期) desc");
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    date = sdr["日期"].ToString();
                    date1 = date;
                }
                sdr.Close();
                Sqlconn.Close();
            }
            string sql = "select convert(varchar(10),日期,111) as 日期,昵称,组别,主管,员工姓名,米聊号,接待人数, (convert(varchar,convert(decimal(10,2),(convert(decimal(10,4),转化率)*100)))+'%') as 转化率,convert(decimal(10,2),CPH) as CPH,首次响应时长,平均响应时长,平均接待时长 from SQdata where 日期 between '" + date + "' and '" + date1 + "'";
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