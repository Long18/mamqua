using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectSEM3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed

            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               name: "Phone",
               url: "dien-thoai/{metatile}-{id}",
               defaults: new { controller = "Product", action = "ProductcategoryPhone", id = UrlParameter.Optional },
               namespaces: new string[] { "ProjectSEM3.Controllers" }

           );
            routes.MapRoute(
              name: "feedback",
              url: "feed-back/",
              defaults: new { controller = "Feedback", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "ProjectSEM3.Controllers" }

          );
            routes.MapRoute(
            name: "chi tiết bài viết",
            url: "bai-viet/{metatile}-{id}",
            defaults: new { controller = "Content", action = "Detail", id = UrlParameter.Optional },
            namespaces: new string[] { "ProjectSEM3.Controllers" }

        );
            routes.MapRoute(
        name: "danh mục",
        url: "danh-muc/{metatile}-{id}",
        defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
        namespaces: new string[] { "ProjectSEM3.Controllers" }

    );
            routes.MapRoute(
            name: "Danh mục bài viết",
            url: "bai-viet/danh-muc/{metatile}-{id}",
            defaults: new { controller = "Content", action = "Category", id = UrlParameter.Optional },
            namespaces: new string[] { "ProjectSEM3.Controllers" }

        );
            routes.MapRoute(
           name: " bài viết",
           url: "bai-viet/",
           defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
           namespaces: new string[] { "ProjectSEM3.Controllers" }

       );

            routes.MapRoute(
               name: "Laptop",
               url: "lap-top/{metatile}-{id}",
               defaults: new { controller = "Product", action = "ProductcategoryLaptop", id = UrlParameter.Optional },
               namespaces: new string[] { "ProjectSEM3.Controllers" }

           );

            routes.MapRoute(
              name: "Phukien",
              url: "phu-kien/{metatile}-{id}",
              defaults: new { controller = "Product", action = "Phukiem", id = UrlParameter.Optional },
              namespaces: new string[] { "ProjectSEM3.Controllers" }

          );
            routes.MapRoute(
                name: "DetailProduct",
                url: "chi-tiet/{metatile}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "ProjectSEM3.Controllers" }

            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ProjectSEM3.Controllers" }

            );
        }
    }
}
