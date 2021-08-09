using System.Web.Mvc;
using System.Web.Routing;

namespace ShopBalo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}",
  new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               name: "Product Category",
               url: "Product/{Name}-{id}",
               defaults: new { controller = "Product", action = "CategoryInfo", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
           );

            routes.MapRoute(
              name: "Detail",
              url: "Product/Detail/{Name}-{productId}",
              defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
          );

            routes.MapRoute(
            name: "Category List",
            url: "Product/{Name}-{cateId}",
            defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
        );

            routes.MapRoute(
            name: "Add to cart",
            url: "AddToCart",
            defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
            namespaces: new[] { "OnlineShop.Controllers" }
        );
            routes.MapRoute(
           name: "Cart",
           url: "Cart",
           defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "OnlineShop.Controllers" }
       );
            routes.MapRoute(
         name: "Payment",
         url: "Payment",
         defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
         namespaces: new[] { "OnlineShop.Controllers" }
     );

         routes.MapRoute( name: "Payment Sucsess",
         url: "Success",
         defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
         namespaces: new[] { "OnlineShop.Controllers" }
     );
         routes.MapRoute(
         name: "ContactShop",
         url: "Contact",
         defaults: new { controller = "Contact", action = "Contact", id = UrlParameter.Optional },
         namespaces: new[] { "OnlineShop.Controllers" }
     );

      routes.MapRoute(
      name: "ContactFeedback",
      url: "Contact/Feedback",
      defaults: new { controller = "Contact", action = "Send", id = UrlParameter.Optional },
      namespaces: new[] { "OnlineShop.Controllers" }
  );

      routes.MapRoute(
      name: "Register",
      url: "Register",
      defaults: new { controller = "Register", action = "Register", id = UrlParameter.Optional },
      namespaces: new[] { "OnlineShop.Controllers" }
  );

      routes.MapRoute(
      name: "LoginUser",
      url: "Login",
      defaults: new { controller = "Register", action = "Login", id = UrlParameter.Optional },
      namespaces: new[] { "OnlineShop.Controllers" }
  );
            routes.MapRoute(
     name: "AllProduct",
     url: "Product",
     defaults: new { controller = "Product", action = "AllProduct", id = UrlParameter.Optional },
     namespaces: new[] { "OnlineShop.Controllers" }
 );

            routes.MapRoute(
     name: "SearchProduct",
     url: "Search",
     defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
     namespaces: new[] { "OnlineShop.Controllers" }
 );

            routes.MapRoute( name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "ShopBalo.Controllers" }
           );
        }
    }
}