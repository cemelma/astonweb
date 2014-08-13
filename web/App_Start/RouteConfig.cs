using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            ControllerBuilder.Current.DefaultNamespaces.Add("web.Controllers");

            //routes.MapRoute("home_default", "/", new { action = "Index", Controller = "FHome" });
            routes.MapRoute("home_default", "anasayfa", new { action = "Index", Controller = "FHome" });
            routes.MapRoute("kurumsal", "kurumsal", new { action = "Index", Controller = "FInstitutional" });
            routes.MapRoute("iletisim", "iletisim", new { action = "Index", Controller = "FContact" });
            routes.MapRoute("haberler", "haberler", new { action = "Index", Controller = "FNews" });
            routes.MapRoute("referanslar", "referanslar", new { action = "Index", Controller = "FReferences" });
            routes.MapRoute("referansdetay", "referansdetay/{id}", new { action = "Detail", Controller = "FReferences" });
            routes.MapRoute("insankaynaklari", "insankaynaklari", new { action = "Index", Controller = "FHumanResources" });
            routes.MapRoute("arama", "arama", new { action = "Index", Controller = "FSearch" });
            routes.MapRoute("sitemap", "siteharita", new { action = "Index", Controller = "FSiteMap" });
            routes.MapRoute("urunler", "urunler/{id}/{pageslug}", new { action = "Index", Controller = "FProduct" });
            routes.MapRoute("fiyatlar", "fiyatlar/{id}/{pageslug}", new { action = "Prices", Controller = "FProduct" });
            routes.MapRoute("giris", "giris", new { action = "UserLogin", Controller = "FProduct" });
            routes.MapRoute("cik", "guvenlicikis", new { action = "UserLogout", Controller = "FProduct" });

            routes.MapRoute("hizmetler", "hizmetler/{id}/{page}", new { action = "Index", Controller = "FServices" });

            routes.MapRoute("siteharita_tr", "tr/siteharitasi", new { action = "SiteHarita", Controller = "FInstitutional" });
            routes.MapRoute("siteharita_en", "en/sitemap", new { action = "SiteHarita", Controller = "FInstitutional" });

          
           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FHome", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}