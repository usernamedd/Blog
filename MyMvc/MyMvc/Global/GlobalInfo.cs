using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvc.Global
{
    public class GlobalInfo
    {
        /// <summary>
        /// 获取当前用户名
        /// </summary>
        public static string UserName
        {
            get
            {
                HttpCookie _cookie = HttpContext.Current.Request.Cookies["User"];
                if (_cookie == null) return "";
                else return _cookie["UserName"];
            }
        }
    }
}