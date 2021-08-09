using Model.DAO;
using ShopBalo.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBalo.Common;

namespace ShopBalo.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDao();
                var result = dao.Login(model.Email, model.Password);
                if (result == 1)
                {
                    var accountSession = new AccountLogin();
                    var account = dao.GetById(model.Email);
                    accountSession.Email = account.Email;
                    accountSession.AccountID = account.Id;
                    Session.Add(CommonConstants.ACCOUNT_SESSION, accountSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account dosen't exist!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Account was locked!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Password is incoorect!");
                }
                else
                {
                    ModelState.AddModelError("", "Login fail!");
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.ACCOUNT_SESSION] = null;
            return Redirect("/Admin/Login/Login");
        }
    }
}