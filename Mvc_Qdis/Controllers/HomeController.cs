using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc_Qdis.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.show = true;
            return View();
        }


    }
}
