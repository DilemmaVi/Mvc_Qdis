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
    /// TDchart 的摘要说明
    /// </summary>
    public class TDchart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            var tp = context.Request["type"].ToString();
           var TD = context.Request["TD"].ToString();
            StringBuilder bd = new StringBuilder();
            String[] data;
            string da;
            data = Gleader();
            if (TD == "0")
            {
                for (int i = 0; i < data.Length; i++)
                {
                    da = TDstr(data[i], tp);
                    bd.Append(da);
                    if (!(data.Length - 1 == i))
                    {
                        bd.Append(",");
                    }
                }
            }
            else if(TD=="1")
            {
                for (int i = 0; i < data.Length; i++)
                {
                    da = data[i];
                    bd.Append(da);
                    if (!(data.Length - 1 == i))
                    {
                        bd.Append(",");
                    }
                }
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    da = Zstr(tp);
                    bd.Append(da);
                    if (!(data.Length - 1 == i))
                    {
                        bd.Append(",");
                    }
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

        public string TDstr(string zuzhang, string type)//团队数据获取
        {

            DataSet dr = new DataSet();
            string year = DateTime.Today.Year.ToString();
            string month = DateTime.Today.Month.ToString();
            string day = DateTime.Today.Day.ToString();
            string end = DateTime.Today.ToString();
            string start;
            if (Convert.ToInt32(day) > 19)
            {
                 start = year + "/" + month + "/" + "20";
            }
            else
            {
                 start = year + "/" +(Convert.ToInt32(month)-1).ToString() + "/" + "20";
            }
                      
            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
            SqlConnection Sqlconn = new SqlConnection(connString);
            Sqlconn.Open();
            SqlCommand cmd = Sqlconn.CreateCommand();
            cmd.CommandText = string.Format("select cast(AVG({0}) as decimal(18,2)) as {1} from SQdata where 组别='{2}' and 日期 Between '" + start + "' And '" + end + "'", type, type,zuzhang);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
            sdr.Fill(dr);
            string str; ;
            str = dr.Tables[0].Rows[0][type].ToString();
            Sqlconn.Close();
            return str;
        }//团队数据获取

        public String[] Gleader()
        {
            String[] ld = new String[] {"李孟宇","王刚","张理想","孟凡峥","刘奕","周岳"};
            return ld;
        }

        public string Zstr(string type)//团队数据获取
        {

            DataSet dr = new DataSet();
            string year = DateTime.Today.Year.ToString();
            string month = DateTime.Today.Month.ToString();
            string day = DateTime.Today.Day.ToString();
            string end = DateTime.Today.ToString();
            string start;
            if (Convert.ToInt32(day) > 19)
            {
                start = year + "/" + month + "/" + "20";
            }
            else
            {
                start = year + "/" + (Convert.ToInt32(month) - 1).ToString() + "/" + "20";
            }

            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";
            SqlConnection Sqlconn = new SqlConnection(connString);
            Sqlconn.Open();
            SqlCommand cmd = Sqlconn.CreateCommand();
            cmd.CommandText = string.Format("select cast(AVG({0}) as decimal(18,2)) as {1} from SQdata where 日期 Between '" + start + "' And '" + end + "'", type, type);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
            sdr.Fill(dr);
            string str; ;
            str = dr.Tables[0].Rows[0][type].ToString();
            Sqlconn.Close();
            return str;
        }//总数据获取

 

    }
}