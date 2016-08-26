using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace 渠道客服系统.Nhome.Ajax.taobao
{
    /// <summary>
    /// tbtd 的摘要说明
    /// </summary>
    public class tbtd : IHttpHandler
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
                    da = tbTDstr(data[i], tp);
                    bd.Append(da);
                    if (!(data.Length - 1 == i))
                    {
                        bd.Append(",");
                    }
                }
            }
            else if (TD == "1")
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
                    da = tbZstr(tp);
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

        public string tbTDstr(string zuzhang, string type)//团队数据获取
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
            cmd.CommandText = string.Format("select cast(AVG({0}) as decimal(18,2)) as {1} from SQdatatb where 员工姓名='{2}' and 日期 Between '" + start + "' And '" + end + "'", type, type, zuzhang);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
            sdr.Fill(dr);
            string str; ;
            str = dr.Tables[0].Rows[0][type].ToString();
            Sqlconn.Close();
            return str;
        }//团队数据获取

        public String[] Gleader()
        {
            String[] ld = new String[] { "赵文辉", "刘垚", "崔鹏宇", "张艳芳", "魏嘉颐", "李昂" };
            return ld;
        }

        public string tbZstr(string type)//团队数据获取
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
            cmd.CommandText = string.Format("select cast(AVG({0}) as decimal(18,2)) as {1} from SQdatatb where 日期 Between '" + start + "' And '" + end + "'", type, type);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
            sdr.Fill(dr);
            string str; ;
            str = dr.Tables[0].Rows[0][type].ToString();
            Sqlconn.Close();
            return str;
        }//总数据获取



    }
}