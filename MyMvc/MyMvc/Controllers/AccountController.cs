using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMvc.Models;
using System.Drawing;

namespace MyMvc.Controllers
{
    public class AccountController : Controller
    {
        DBBlogEntities dbBlogContext = null;
        public AccountController()
        {
            dbBlogContext = new DBBlogEntities();//EF和MVC的结合; Controller维护一个上下文；在运行时出现 “未将对象引用设置到对象的实例”错误 
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string password2)
        {
            /*
            如何进入这里呢？
            */
            /*
            1、注册，向数据库添加一条纪录；使用EF的添加功能，参考 http://www.cnblogs.com/bomo/p/3331602.html 
            2、跳转到主页; 页面跳转，参考：http://blog.csdn.net/lonestar555/article/details/7046717
            */
            dbBlogContext.User.Add(new User
            {
                id = Guid.NewGuid().ToString(),//使用 new Guid()的写法是不对的
                username = username,
                password = password,
                reg_date = DateTime.Now,
                lastlogin_date = DateTime.Now
            });
            dbBlogContext.SaveChanges();

            Response.Redirect("~/Home/Index");//Home/Index 写法错误，页面会看到请求的地址是Account/Home/Index
            return View();
        }
        /// <summary>
        /// 这里返回void的话不会打开对应的View页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLoginModel loginModel)
        {
            //验证验证码
            if (Session["VerificationCode"] == null || Session["VerificationCode"].ToString() == "")
            {
                ViewBag.VerificationErrorMessage = "服务器未保存验证码，可能是不是通过登录页面登录的，可能使用了登录模拟器";
                return View();
            }
            string verificationCode = Session["VerificationCode"].ToString();
            if (!verificationCode.Equals(loginModel.VerificationCode,StringComparison.CurrentCultureIgnoreCase))//比较时忽略大小写
            {
                ViewBag.VerificationErrorMessage = "验证码填写错误";
                return View();
            }
            //验证用户名与密码
            var users = dbBlogContext.User.Where(u => u.username == loginModel.UserName);
            if(users.Count()==0)
            {
                ViewBag.VerificationErrorMessage = "用户名或者密码错误";
                return View();
            }
            var user = users.First();
            if (user==null)
            {
                ViewBag.VerificationErrorMessage = "用户名或者密码错误";
                return View();
            }
            if(!user.password.Trim().Equals(loginModel.Password))
            {
                ViewBag.VerificationErrorMessage = "用户名或者密码错误";
                return View();
            }
            //使客户端保存Cookie；成功进入主页或者原始页面（从原始页面跳转到登录页面）；
            HttpCookie _cookie = new HttpCookie("User");
            _cookie.Values.Add("UserName", loginModel.UserName);
            _cookie.Values.Add("Password", loginModel.Password);
            _cookie.Expires = DateTime.Now.AddMinutes(10);//设置Cookie的过期时间,这里可能需要获取客户端的时间而不应该用服务器的时间
            Response.Cookies.Add(_cookie);

            if (Request.QueryString["ReturnUrl"] != null) return Redirect(Request.QueryString["ReturnUrl"]);
            else return RedirectToAction("Index", "Home");
        }

        
        public ActionResult LoginOut()
        {
            if (Request.Cookies["User"] != null)
            {
                HttpCookie _cookie = Request.Cookies["User"];
                _cookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(_cookie);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CheckCode()
        {
            int _verificationLength = 6;
            int _width = 100, _height = 20;
            SizeF _verificationTextSize;
            //Bitmap _bitmap = new Bitmap(_width,_height);
            //TextureBrush _brush = new TextureBrush(_bitmap);
            Brush _brush = Brushes.Olive;
            //获取验证码
            string _verificationText = Common.CheckCodeGenerator.VerificationText(_verificationLength);
            //存储验证码
            Session["VerificationCode"] = _verificationText.ToUpper();
            Font _font = new Font("Arial", 14, FontStyle.Bold);
            Bitmap _image = new Bitmap(_width, _height);
            Graphics _g = Graphics.FromImage(_image);
            //清空背景色
            _g.Clear(Color.OrangeRed);
            //绘制验证码
            _verificationTextSize = _g.MeasureString(_verificationText, _font);
            _g.DrawString(_verificationText, _font, _brush, (_width - _verificationTextSize.Width) / 2, (_height - _verificationTextSize.Height) / 2);
            _image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return null;
        }
    }
}