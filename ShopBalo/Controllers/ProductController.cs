using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace ShopBalo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Categories()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);
        }

        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        public ActionResult CategoryInfo(long id)
        {
            var category = new CategoryDao().ViewDetail(id);
            //add more
            var model = new ProductDao().ListByCategoryId(id);
            return View(model);
        }

       
        public ActionResult Category(long cateId)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var model = new ProductDao().ListByCategoryId(cateId);
            return View(model);
        }
        [OutputCache(Duration = int.MaxValue, VaryByParam = "id")]
        public ActionResult Detail(long productId)
        {
            var product = new ProductDao().ViewDetail(productId);
            ViewBag.Category = new CategoryDao().ViewDetail(product.Category_Id.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProducts(productId);
            return View(product);
        }

        public ActionResult AllProduct()
        {
            var product = new ProductDao().ListAllProduct();
            return View(product);
        }
    }
}