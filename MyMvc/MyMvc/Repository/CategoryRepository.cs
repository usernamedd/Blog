using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMvc.Models;

namespace MyMvc.Repository
{
    public class CategoryRepository:RepositoryBase<category>
    {
        public override category Find(string Id)
        {
            return base.Find(Id);
        }

        public List<category> FindAllByUser(User user)
        {
            List<category> categorys = dbContext.categories.Where(c => c.userid == user.id).ToList();

            return categorys;
        }

        public bool AddFromCreateModel(CreateCategoryModel cModel)
        {
            UserRepository user = new UserRepository();
            User u = user.FindByName(Global.GlobalInfo.UserName);
            dbContext.categories.Add(new category { id = Guid.NewGuid().ToString(), userid = u.id, c_name = cModel.CategoryName,c_desc=cModel.CategoryDesc });
            if (dbContext.SaveChanges() > 0) return true;
            else return false;
        }
    }
}