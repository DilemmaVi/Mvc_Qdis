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
         [Authorize(Roles = "admin")]
        public ActionResult organization()
        {
            return View();
        }

    }
}
