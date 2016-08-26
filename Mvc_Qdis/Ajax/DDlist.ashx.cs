using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace 渠道客服系统.Nhome.Ajax
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var zuzhang = context.Request["zuzhang"].ToString();
            var tp = context.Request["tp"].ToString();
            StringBuilder bd = new StringBuilder();
            String[] data;
            if (tp == "0")
            {
                data = str(zuzhang);
            }
            else
            {
                data = dtr(zuzhang);
            }
            for (int i = 0; i < data.Length; i++)
            {
                bd.Append(data[i]);
                if (!(data.Length - 1 == i))
                {
                    bd.Append(",");
                }
            }
            string ds = bd.ToString();
            context.Response.ContentType = "text/plain";
            context.Response.Write(ds);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public String[] str(string zuzhang)
        {
            string start = DateTime.Today.AddDays(-14).ToString();
            string end = DateTime.Today.ToString();
            DataSet dr = new DataSet();
            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
            SqlConnection Sqlconn = new SqlConnection(connString);
            //打开连接
            Sqlconn.Open();
            SqlCommand cmd = Sqlconn.CreateCommand();
            cmd.CommandText = string.Format("select DISTINCT 组别 from SQdata where 主管= '{0}' and 日期 between '"+start+"' and '"+end+"'", zuzhang);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
            sdr.Fill(dr);
            String[] str = new String[dr.Tables[0].Rows.Count];
            for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
            {
                str[i] = dr.Tables[0].Rows[i]["组别"].ToString();
            }
            Sqlconn.Close();
            return str;
        }//组长信息获取

        public String[] dtr(string yuangong)
        {
            string start = DateTime.Today.AddDays(-14).ToString();
            string end = DateTime.Today.ToString();
            DataSet dr = new DataSet();
            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
            SqlConnection Sqlconn = new SqlConnection(connString);
            //打开连接
            Sqlconn.Open();
            SqlCommand cmd = Sqlconn.CreateCommand();
            cmd.CommandText = string.Format("select DISTINCT 员工姓名 from SQdata where 组别= '{0}' and 日期 between '" + start + "' and '" + end + "'", yuangong);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
            sdr.Fill(dr);
            String[] str = new String[dr.Tables[0].Rows.Count];
            for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
            {
                str[i] = dr.Tables[0].Rows[i]["员工姓名"].ToString();
            }
            Sqlconn.Close();
            return str;
        }//员工信息获取

    }
}