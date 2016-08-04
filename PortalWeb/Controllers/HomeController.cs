using Entity_Logic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalWeb.Controllers
{
    public class HomeController : Controller
    {
        //private readonly string strSessionMensaje = string.Empty;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpLoad()
        {
            return View();
        }
        
        public ActionResult Dowload()
        {
            if (!clsSessionHelper.SessionExpirada)
            {
                 return View();
            }
            
            return RedirectToAction("/");
        }
    }
}