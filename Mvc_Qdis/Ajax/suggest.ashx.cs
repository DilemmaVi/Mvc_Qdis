using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace 渠道客服系统.Nhome.Ajax
{
    /// <summary>
    /// suggest 的摘要说明
    /// </summary>
    public class suggest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sql = "select * from suggest";
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