using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_Qdis.Controllers
{
    public class StructureController : Controller
    {
        //
        // GET: /Structure/
         [Authorize(Roles = "渠道客服主管")]
        public ActionResult organization()
        {
            return View();
        }

    }
}
