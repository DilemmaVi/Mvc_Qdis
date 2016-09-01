using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Qdis.Controllers
{
    public class DataExportController : Controller
    {
        //
        // GET: /DataExport/

        [Authorize(Roles = "渠道客服组长,渠道客服主管,京东业务组长,淘宝客服组长,阿里现场调控,商品评论组长,渠道客服经理")]
        public ActionResult SQ_Export()
        {
            return View();
        }

    }
}
