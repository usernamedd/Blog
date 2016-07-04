using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvc.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NoUserLogin()
        {
            return View();
        }
    }
}