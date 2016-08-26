using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 渠道客服系统.Nhome.Ajax
{
    /// <summary>
    /// miliao 的摘要说明
    /// </summary>
    public class miliao : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var name = context.Request["name"].ToString();
            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
            string yuangong;
            SqlConnection Sqlconn = new SqlConnection(connString);
            Sqlconn.Open();
            SqlCommand cmd = Sqlconn.CreateCommand();
            cmd.CommandText = string.Format("select UserName from ISRegister where 姓名= '{0}'", name);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                yuangong = sdr["UserName"].ToString();
                context.Response.ContentType = "text/plain";
                context.Response.Write(yuangong);
            }
            sdr.Close();
            Sqlconn.Close();   
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