using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Mvc_Qdis.App_Start
{
    /// <summary>
    /// 过滤器，防止F5刷新提交数据
    /// </summary>
    ///
    public class NoResubmitAttribute : ActionFilterAttribute
    {
        private static readonly string HttpMehotdPost = "POST";
        private static readonly string prefix = "postFlag";
        private string nameWithRoute;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerContext = filterContext.Controller.ControllerContext;
            if (!controllerContext.IsChildAction)
            {
                var request = controllerContext.HttpContext.Request;
                var session = controllerContext.HttpContext.Session;
                nameWithRoute = generateNameWithRoute(controllerContext);
                int sessionFlag = session[nameWithRoute] == null ? 0 : (int)session[nameWithRoute];
                int requestFlag = string.IsNullOrEmpty(request.Form[nameWithRoute]) ? 0 : int.Parse(request.Form[nameWithRoute]);
                // get or normal post: true;    
                bool isValid = !IsPost(filterContext) || sessionFlag == requestFlag;
                if (sessionFlag == int.MaxValue)
                {
                    sessionFlag = -1;
                }
                session[nameWithRoute] = ++sessionFlag;
                if (!isValid)
                {
                    filterContext.Result = new RedirectResult(GenerateUrlWithTimeStamp(request.RawUrl));
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private string GenerateUrlWithTimeStamp(string url)
        {
            return string.Format("{0}{1}timeStamp={2}", url, url.Contains("?") ? "&" : "?", (DateTime.Now - DateTime.Parse("2010/01/01")).Ticks);
        }

        private bool IsPost(ActionExecutingContext filterContext)
        {
            return filterContext.HttpContext.Request.HttpMethod == HttpMehotdPost;
        }

        private string generateNameWithRoute(ControllerContext controllerContext)
        {
            StringBuilder sb = new StringBuilder(prefix);
            foreach (object routeValue in controllerContext.RouteData.Values.Values)
            {
                sb.AppendFormat("_{0}", routeValue);
            }
            return sb.ToString();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            if (!filterContext.IsChildAction && !(filterContext.Result is RedirectResult))
            {
                //string format = "<script type='text/javascript'>$(function () [[ $('form').each(function()[[$('<input type=hidden id={0} name={0} value={1} />').appendTo($(this));]])]]); </script>";
                string format = "<script type='text/javascript'> var forms = document.getElementsByTagName('form'); for(var i = 0; i<forms.length; i++)[[var ele = document.createElement('input'); ele.type='hidden'; ele.id=ele.name='{0}'; ele.value='{1}'; forms[i].appendChild(ele);]] </script>";
                string script = string.Format(format, nameWithRoute, filterContext.HttpContext.Session[nameWithRoute]).Replace("[[", "{").Replace("]]", "}");
                filterContext.HttpContext.Response.Write(script);
            }
        }
    }
}