using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using PagedList;

namespace Model.DAO
{
    public class ProductDao
    {
        BaloShopDbContext db = null;
        public ProductDao()
        {
            db = new BaloShopDbContext();
        }

        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.Id).Take(top).ToList();
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.Price < 2000000).OrderByDescending(x => x.Id).Take(top).ToList();
        }

        public List<Product> ListRelatedProducts(long prId)
        {
            var product = db.Products.Find(prId);
            return db.Products.Where(x => x.Id != prId && x.Category_Id == product.Category_Id).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }

         public List<Product> ListByCategoryId(long categoryID)
        {
            var model = db.Products.Where(x => x.Category_Id == categoryID).ToList();
            return model;
        }

        public List<Product> ListAllProduct()
        {
            var model = db.Products.ToList();
            return model;
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var pr = db.Products.Find(id);
                db.Products.Remove(pr);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = (from a in db.Products
                         join b in db.Account_Detail
                         on a.Category_Id equals b.Id
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateName = b.Name,
                             ID = a.Id,
                             Images = a.Image_Link,
                             Name = a.Name,
                             Quantity = a.Quantity,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateName = x.Name,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             Quantity = (int)x.Quantity,
                             Price = (double)x.Price
                         });
            model.OrderByDescending(x => x.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}
