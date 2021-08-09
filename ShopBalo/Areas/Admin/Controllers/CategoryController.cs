using Model.DAO;
using Model.EF;
using ShopBalo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShopBalo.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Category()
        {
            return View();
        }
    
     
        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstants.CurrentCulture];
                var id = new CategoryDao().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("Category");
                }
                else
                {
                    return RedirectToAction("404");
                }
            }
            return View(model);
        }
    }
}