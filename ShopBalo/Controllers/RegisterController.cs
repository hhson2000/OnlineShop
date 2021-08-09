using BotDetect.Web.Mvc;
using Facebook;
using Model.DAO;
using Model.EF;
using ShopBalo.Common;
using ShopBalo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBalo.Controllers
{
    public class RegisterController : Controller
    {

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.ACCOUNT_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDao();
                var result = dao.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new AccountLogin();
                    userSession.Email = user.Email;
                    userSession.AccountID = user.Id;
                    userSession.GroupID = Convert.ToInt32(user.Role_ID);
                    Session.Add(CommonConstants.ACCOUNT_SESSION, userSession);
                    if(user.Role_ID == 2)
                    {
                        return Redirect("/");
                    } else
                    {
                        return Redirect("/Admin/Home/Index");
                    }
                    
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View(model);
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var acc = new Account();
                var user = new Account_Detail();
                acc.Email = email;
                acc.Status = 1;
                acc.Role_ID = 2;
                acc.Create_Date = DateTime.Now;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.Address = "None";
                user.Phone_Number = "None";
                user.Gender = true;
                var resultInsert = new AccountDetailDao().InsertForFacebook(user);
                var rs = new AccountDao().InsertForFacebook(acc);
                if (rs > 0 && resultInsert >0)
                {
                    var userSession = new AccountLogin();
                    userSession.Email = acc.Email;
                    userSession.AccountID = acc.Id;
                    Session.Add(CommonConstants.ACCOUNT_SESSION, userSession);
                }
            }
            return Redirect("/");
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDao();
                var daoDt = new AccountDetailDao();
                if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                } else
                {
                    var user = new Account();
                    var userDetail = new Account_Detail();
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.Create_Date = DateTime.Now;
                    user.Status = 1;
                    user.Role_ID = 2;
                    userDetail.Address = model.Address;
                    userDetail.Name = model.Name;
                    userDetail.Gender = model.Gender;
                    userDetail.Phone_Number = model.Phone_Number;
                    var rsA = dao.Insert(user);
                    var rsADT = daoDt.Insert(userDetail);
                    if(rsA > 0 && rsADT > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    } else
                    {
                        ModelState.AddModelError("", "Đăng kí không thành công");
                    }
                }
            }
            return View(model);
        }
    }
}