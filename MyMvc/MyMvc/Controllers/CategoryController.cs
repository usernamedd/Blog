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
            //添加数据库操作
            categoryRep.AddFromCreateModel(category);
            return RedirectToAction("CategoryMgr");
        }

        public ActionResult CategoryMgr()
        {
            //没有用户登录的话则不向视图传递数据
            if(string.IsNullOrEmpty(Global.GlobalInfo.UserName))
            {
                return RedirectToAction("NoUserLogin", "Error");
                //return View();
            }
            //如果登录了的话，则向视图传递当前用户下的分类信息
            List<category> categorys = new List<category>();
            categorys = categoryRep.FindAllByUser(new UserRepository().FindByName(Global.GlobalInfo.UserName));
            ViewBag.Categorys = categorys??new List<category>();
            return View(ViewBag);
        }

        public ActionResult EditCategory(string id,string title,string desc)
        {
            category c = new category { id = id, c_name = title, c_desc = desc };
            string errorMessage = string.Empty;
            try
            {
                categoryRep.Update(c);
            }
            catch(Exception ex)
            {//优化时需要详细判断异常类型，给出对应的友好提示
                errorMessage = ex.Message;
            }
            //客户端在处理时判断开头的字符以此来判断是否保存成功
            if(string.IsNullOrEmpty(errorMessage))
            {
                Response.Output.WriteLine("OK:");//发送响应
            }
            else
            {
                Response.Output.WriteLine("error;"+errorMessage);//发送响应
            }
            return null;
        }
    }
}