using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace 渠道客服系统.Nhome
{
    /// <summary>
    /// ajax 的摘要说明
    /// </summary>
    public class ajax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var miliao = context.Request["miliao"].ToString();
            var type = context.Request["type"].ToString();
            var TD = context.Request["TD"].ToString();
            StringBuilder bd = new StringBuilder();
            String[] data;
            if (TD == "0")
            {
                data = str(miliao, type);
            }
            else
            {
                data = time(miliao,type);
            }
            for (int i = 0; i < data.Length; i++)
            {
                bd.Append(data[i]);
                if (!(data.Length-1 == i))
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

        public String[] str(string miliao,string type)
        {

            DataSet dr = new DataSet();
            string name = miliao;
            string year = DateTime.Today.Year.ToString();
            string start = DateTime.Today.AddDays(-30).ToString();
            string end = DateTime.Today.ToString();
            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";

                SqlConnection Sqlconn = new SqlConnection(connString);
                Sqlconn.Open();
                SqlCommand cmd = Sqlconn.CreateCommand();
                cmd.CommandText = string.Format("select * from SQdata where 米聊号= '{0}' and 日期 Between '" + start + "' And '" + end + "' order by 日期 asc", name);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
                sdr.Fill(dr);
                String[] str = new String[dr.Tables[0].Rows.Count];
                if (type == "日期")
                {
                    for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                    {
                        str[i] = DateTime.Parse(dr.Tables[0].Rows[i][type].ToString()).ToShortDateString();
                    }
                }
                else
                {
                    for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                    {
                        str[i] = dr.Tables[0].Rows[i][type].ToString();
                    }
                }
                Sqlconn.Close();
                return str;
        }//个人数据获取
        public string TDstr(string time, string type)//团队数据获取
        {

            DataSet dr = new DataSet();
            string year = DateTime.Today.Year.ToString();
            string start = DateTime.Today.AddDays(-30).ToString();
            string end = DateTime.Today.ToString();
            string connString = "server=10.233.15.36;database =Qdis;uid =sa;pwd=123";

            SqlConnection Sqlconn = new SqlConnection(connString);
            Sqlconn.Open();
            SqlCommand cmd = Sqlconn.CreateCommand();
            cmd.CommandText = string.Format("select cast(AVG({0}) as decimal(18,2)) as {1} from SQdata where 日期 Between '" + time + "' And '" + time + "' group by 日期 order by 日期 asc", type, type);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd.CommandText, Sqlconn);
            sdr.Fill(dr);
            string str; ;
            str = dr.Tables[0].Rows[0][type].ToString();
            Sqlconn.Close();
            return str;
        }//团队数据获取

        public String[] time(string miliao,string type)
        {
          var date=str(miliao,"日期");
          String[] data=new String[date.Length];
          for (var i = 0; i < date.Length; i++)
          {
             data[i]= TDstr(date[i], type);
          }
          return data;
        }
    }
}