using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMvc.Models;
using MyMvc.Repository;

namespace MyMvc.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository  categoryRep = new CategoryRepository();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(CreateCategoryModel category)
        {
            return RedirectToAction("CategoryMgr");
        }

        public ActionResult CategoryMgr()
        {
            //没有用户登录的话则不向视图传递数据
            if(string.IsNullOrEmpty(Global.GlobalInfo.UserName))
            {
                return View();
            }
            //如果登录了的话，则向视图传递当前用户下的分类信息
            List<category> categorys = new List<category>();
            categorys = categoryRep.FindAllByUser(new UserRepository().FindByName(Global.GlobalInfo.UserName));
            ViewBag.Categorys = categorys??new List<category>();
            return View(ViewBag);
        }
    }
}