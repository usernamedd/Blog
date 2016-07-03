using MyMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyMvc.Repository
{
    public class UserRepository:RepositoryBase<User>
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public override bool Add(User user)
        {
            if (user == null) return false;
            dbContext.User.Add(user);
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override bool Update(User user)
        {
            dbContext.User.Attach(user);
            dbContext.Entry<User>(user).State = EntityState.Modified;
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public override User Find(string Id)
        {
            return dbContext.User.SingleOrDefault(u => u.id == Id);
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public User FindByName(string UserName)
        {
            return dbContext.User.SingleOrDefault(u => u.username == UserName);
        }
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool Exists(string UserName)
        {
            if (dbContext.User.Any(u => u.username.ToUpper() == UserName.ToUpper())) return true;
            else return false;
        }
        /// <summary>
        /// 用户验证【0-成功；1-用户名不存在；2-密码错误】
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWrod"></param>
        /// <returns></returns>
        public int Authentication(string UserName, string PassWrod)
        {
            var _user = dbContext.User.SingleOrDefault(u => u.username == UserName);
            if (_user == null) return 1;
            else if (_user.password != PassWrod) return 2;
            else return 0;
        }
    }
}