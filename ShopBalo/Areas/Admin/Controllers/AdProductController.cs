using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBalo.Areas.Admin.Controllers
{
    public class AdProductController : BaseController
    {
        // GET: Admin/AdProduct
        public ActionResult Table(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product pr)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                long id = dao.Insert(pr);
                if (id > 0)
                {
                    return RedirectToAction("Create", "AdProduct");
                }
                else
                {
                    ModelState.AddModelError("", "Create Product fail!");
                }
            }
            return View("Create");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Table");
        }
    }
}