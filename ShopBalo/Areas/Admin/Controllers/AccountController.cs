using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;
using System.Web.Mvc;
using Model.DAO;
using PagedList;

namespace ShopBalo.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Admin/Account
        public ActionResult Table(string searchString,int page = 1,int pageSize = 5)
        {
            var dao = new AccountDao();
            var model = dao.ListAllPaging(searchString,page, pageSize);
;            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Account acc)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDao();
                long id = dao.Insert(acc);
                if (id > 0)
                {
                    return RedirectToAction("Create", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Create account fail!");
                }
            }
            return View("Create");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new AccountDao().Delete(id);

            return RedirectToAction("Table");
        }
    }
}