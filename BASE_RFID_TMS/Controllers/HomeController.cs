using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BASE_RFID_TMS.Controllers
{
    public class HomeController : Controller
    {
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        
      

    }
}
