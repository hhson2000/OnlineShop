using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class CategoryDao
    {
        BaloShopDbContext db = null;

        public CategoryDao()
        {
            db = new BaloShopDbContext();
        }
        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.Id;
        }
        public List<Category> ListAll()
        {
            return db.Categories.OrderBy(x => x.Id).ToList();
        }

        public Category ViewDetail(long id)
        {
            return db.Categories.Find(id);
        }
    }
}
