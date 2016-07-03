using MyMvc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvc.Controllers
{
    public class HomeController : Controller
    {
        private UserRepository userRsy;
        
        public HomeController()
        {
            userRsy = new UserRepository();
        }

        // GET: Home
        public ActionResult Index()
        {
            //ViewBag.Register = "注册";

            var _user = userRsy.FindByName(Global.GlobalInfo.UserName);
            return View(_user);
            //return View();
        }
        //public string Index()
        //{
        //    return "home";
        //}

    }
}