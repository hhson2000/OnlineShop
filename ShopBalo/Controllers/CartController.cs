using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using ShopBalo.Models;
using System.Web.Script.Serialization;
using System.Configuration;
using Common;

namespace ShopBalo.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.Id == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult AddItem(long productId,int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.Id == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
       [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address)
        {
            var order = new Order();
            var orderDetail = new Other_Address();
            order.Create_Date = DateTime.Now;
            orderDetail.Address = address;
            orderDetail.Phone_Number = mobile;
            orderDetail.Name = shipName;
            var id = new OrderDao().Insert(order);
            var cart = (List<CartItem>)Session[CartSession];
            var detailDao = new OrderDetailDao();
            try
            {
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDT = new Order_Detail();
                    orderDT.Product_Id = item.Product.Id;
                    orderDT.Order_Id = (int?)id;
                    orderDT.Product_Name = item.Product.Name;
                    orderDT.Product_Price = (decimal?)item.Product.Price;
                    orderDT.Product_Quantity = item.Product.Quantity;
                    detailDao.Insert(orderDT);

                    //total += ((decimal)(item.Product.Price.GetValueOrDefault(0) * item.Quantity));
                }
            } catch(Exception ex)
            {
                return Redirect("/Error");
            }
            return Redirect("/Success");
        }

        public ActionResult Success()
        {
            Session[CartSession] = null;
            var list = new List<CartItem>();
            list = null;
            return View();
        }
    }
}