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
    }
}