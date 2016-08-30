using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IBatis.Model;
using System.Data.SqlClient;
using System.Data;

namespace Mvc_Qdis.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {




            string zw = "天猫客服专员";
            if (System.Web.HttpContext.Current.Request.Cookies["login"] != null)
            {
                var uid = System.Web.HttpContext.Current.Request.Cookies["login"].Values["uid"];
                SqlDataReader obj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select TOP(1) 日期 from SQdata order by 日期 desc");
                if (obj.Read())
                {
                    ViewBag.time = "数据更新时间：" + Convert.ToDateTime(obj["日期"]).ToString("yyyy-MM-dd");
                    obj.Close();
                }
                SqlDataReader zwj = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select 职位 from ISRegister where UserName=@uid", new SqlParameter("uid", uid));
                if (zwj.Read())
                {
                    zw = zwj["职位"].ToString();
                    zwj.Close();
                }
                if (zw == "天猫客服专员")
                {
                    ViewBag.show = false;
                    var data = getGR_data(true);
                    return View(data);
                }
                else if (zw == "阿里售后专员" || zw == "升级投诉专员")
                {
                    return Redirect("index-sh.aspx");
                }
                else if (zw == "京东业务专员" || zw == "京东业务组长")
                {
                    return Redirect("index-jd.aspx");
                }
                else if (zw == "商品评论组长" || zw == "商品评论专员")
                {
                    return Redirect("index-pj.aspx");
                }
                else if (zw == "淘宝客服专员" || zw == "淘宝客服组长")
                {
                    return Redirect("index-tb.aspx");
                }

                else if (zw == "错误")
                {
                    return Redirect("../login.aspx");
                }
                else
                {
                    ViewBag.show = true;
                    var info =  getGR_data(false);
                    return View(info);
                }
            }
            else
            {
                return Redirect("/account/login");
            }
        }

        public  SQdata getGR_data(bool pd)
        {
                var PerData = new SQdata();//实例化模型
                var uid = System.Web.HttpContext.Current.Request.Cookies["login"].Values["uid"];//获取用户id
                #region 时间区域代码
                string year = DateTime.Today.Year.ToString();
                string month = (Convert.ToInt32(DateTime.Today.Month.ToString()) - 1).ToString();
                string month1 = DateTime.Today.Month.ToString();
                var day = Convert.ToInt32(DateTime.Today.Day.ToString());
                string start;
                if (day > 19)
                {
                    start = string.Format(year + "/" + month1 + "/" + "20");
                }
                else
                {
                    start = string.Format(year + "/" + month + "/" + "20");
                }

                string end = DateTime.Today.ToString();
                #endregion
                if (pd)
                {
                    #region 排名代码
                    Dictionary<string, string> PM = new Dictionary<string, string>();
                    String[] ld = new String[] { "接待人数", "CPH", "转化率", "首次响应时长", "平均响应时长", "平均接待时长" };
                    for (int i = 1; i < 3; i++)
                    {
                        SqlDataReader CZ_data = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, string.Format("select * from (select 米聊号,AVG({0}) as CPH,ROW_NUMBER() over(order by AVG({1}) desc) as 排名 from SQdata where 日期 between  '" + start + "' and '" + end + "' group by 米聊号)t1 where 米聊号='{2}'", ld[i], ld[i], uid));
                        if (CZ_data.Read())
                        {
                            PM.Add(ld[i], "(排名:" + CZ_data["排名"].ToString() + ")");
                            CZ_data.Close();
                        }
                    }
                    SqlDataReader JD_data = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, string.Format("select * from (select 米聊号,sum({0}) as CPH,ROW_NUMBER() over(order by sum({1}) desc) as 排名 from SQdata where 日期 between  '" + start + "' and '" + end + "' group by 米聊号)t1 where 米聊号='{2}'", "接待人数", "接待人数", uid));
                    if (JD_data.Read())
                    {
                        PM.Add("接待人数", "(排名:" + JD_data["排名"].ToString() + ")");
                        JD_data.Close();
                    }
                    for (int i = 3; i < ld.Length; i++)
                    {
                        SqlDataReader SL_data = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, string.Format("select * from (select 米聊号,AVG({0}) as CPH,ROW_NUMBER() over(order by AVG({1}) asc) as 排名 from SQdata where 日期 between  '" + start + "' and '" + end + "' group by 米聊号)t1 where 米聊号='{2}'", ld[i], ld[i], uid));
                        if (SL_data.Read())
                        {
                            PM.Add(ld[i], "(排名:" + SL_data["排名"].ToString() + ")");
                            SL_data.Close();
                        }
                    }

                    #endregion

                    #region 个人接待数据获取
                    Dictionary<string, string> sj = new Dictionary<string, string>();
                    SqlDataReader data = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select sum(接待人数) as 接待人数,avg(CPH) as CPH,avg(转化率) as 转化率,avg(首次响应时长) as 首次响应时长,avg(平均响应时长) as 平均响应时长,avg(平均接待时长) as 平均接待时长 from SQdata where 米聊号=@uid and 日期 Between '" + start + "' And '" + end + "'", new SqlParameter("uid", uid));
                    if (data.Read())
                    {
                        sj.Add("接待人数", data["接待人数"].ToString());
                        sj.Add("CPH", data["CPH"].ToString());
                        sj.Add("转化率", data["转化率"].ToString());
                        sj.Add("首次响应时长", data["首次响应时长"].ToString());
                        sj.Add("平均响应时长", data["平均响应时长"].ToString());
                        sj.Add("平均接待时长", data["平均接待时长"].ToString());
                        data.Close();
                    }
                    PerData.Reception = sj[ld[0]] + PM[ld[0]];
                    PerData.CPH = (Convert.ToDecimal(sj[ld[1]])).ToString("0") + "s" + PM[ld[1]];
                    PerData.Conversion = (Convert.ToDecimal(sj[ld[1]]) / 100).ToString("0.00%") + PM[ld[2]];
                    PerData.FrTime = (Convert.ToDecimal(sj[ld[3]])).ToString("0") + "s" + PM[ld[3]];
                    PerData.AvgTime = (Convert.ToDecimal(sj[ld[4]])).ToString("0") + "s" + PM[ld[4]];
                    PerData.ARTime = (Convert.ToDecimal(sj[ld[5]])).ToString("0") + "s" + PM[ld[5]];
                    #endregion
                }
                    #region 团队数据获取
                SqlDataReader TDdata = BookDAL.SqlHelper.ExecuteReader(BookDAL.SqlHelper.GetConnSting(), CommandType.Text, "select sum(接待人数) as 接待人数,avg(CPH) as CPH,avg(转化率) as 转化率,avg(首次响应时长) as 首次响应时长,avg(平均响应时长) as 平均响应时长,avg(平均接待时长) as 平均接待时长 from SQdata where  日期 Between '" + start + "' And '" + end + "'");
                if (TDdata.Read())
                {
                    PerData.TD_Reception = TDdata["接待人数"].ToString();
                    PerData.TD_CPH = (Convert.ToDecimal(TDdata["CPH"])).ToString("0").ToString();
                    PerData.TD_Conversion = (Convert.ToDecimal(TDdata["转化率"])).ToString("0.00%").ToString();
                    PerData.TD_FrTime = (Convert.ToDecimal(TDdata["首次响应时长"])).ToString("0").ToString() + "s";
                    PerData.TD_AvgTime = (Convert.ToDecimal(TDdata["平均响应时长"])).ToString("0").ToString() + "s";
                    PerData.TD_ARTime = (Convert.ToDecimal(TDdata["平均接待时长"])).ToString("0").ToString() + "s";
                }
                #endregion
                return PerData;

        }
 
    }
}
