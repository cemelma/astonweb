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
            routes.MapRoute("home_tr", "tr/anasayfa", new { action = "Index", Controller = "FHome" });
            routes.MapRoute("home_en", "en/homepage", new { action = "Index", Controller = "FHome" });
            routes.MapRoute("home_ar", "ar/home", new { action = "Index", Controller = "FHome" });

            routes.MapRoute("kurumsal_tr", "tr/kurumsal", new { action = "Index", Controller = "FInstitutional" });
            routes.MapRoute("kurumsal_en", "en/institutional", new { action = "Index", Controller = "FInstitutional" });
            routes.MapRoute("kurumsal_ar", "ar/institutional", new { action = "Index", Controller = "FInstitutional" });

            routes.MapRoute("galeri_tr", "tr/galeri", new { action = "Index", Controller = "FGallery" });
            routes.MapRoute("galeri_en", "en/gallery", new { action = "Index", Controller = "FGallery" });
            routes.MapRoute("galeri_ar", "ar/gallery", new { action = "Index", Controller = "FGallery" });

            routes.MapRoute("contact_tr", "tr/iletisim", new { action = "Index", Controller = "FContact" });
            routes.MapRoute("contact_en", "en/contact", new { action = "Index", Controller = "FContact" });
            routes.MapRoute("contact_ar", "ar/contact", new { action = "Index", Controller = "FContact" });


            routes.MapRoute("reference_tr", "tr/referanslar", new { action = "Index", Controller = "FReferences" });
            routes.MapRoute("reference_en", "en/referance", new { action = "Index", Controller = "FReferences" });
            routes.MapRoute("reference_ar", "ar/referance", new { action = "Index", Controller = "FReferences" });

            routes.MapRoute("search_tr", "tr/arama", new { action = "Index", Controller = "FSearch" });
            routes.MapRoute("search_en", "en/search", new { action = "Index", Controller = "FSearch" });
            routes.MapRoute("search_ar", "ar/search", new { action = "Index", Controller = "FSearch" });

            routes.MapRoute("news_tr", "tr/haberler", new { action = "Index", Controller = "FNews" });
            routes.MapRoute("news_en", "en/news", new { action = "Index", Controller = "FNews" });
            routes.MapRoute("news_ar", "ar/news", new { action = "Index", Controller = "FNews" });

            //routes.MapRoute("news_tr", "tr/haberler", new { action = "Index", Controller = "FNews" });
            //routes.MapRoute("news_en", "en/news", new { action = "Index", Controller = "FNews" });
            //routes.MapRoute("news_ar", "ar/news", new { action = "Index", Controller = "FNews" });


            routes.MapRoute("urunler_tr", "tr/urunler/{id}/{pageslug}", new { action = "Index", Controller = "FProduct" });
            routes.MapRoute("urunler_en", "en/products/{id}/{pageslug}", new { action = "Index", Controller = "FProduct" });
            routes.MapRoute("urunler_ar", "ar/products/{id}/{pageslug}", new { action = "Index", Controller = "FProduct" });

            routes.MapRoute("categoriler_tr", "tr/kategori/{cid}/{pageslug}", new { action = "Index", Controller = "FProduct" });
            routes.MapRoute("categoriler_en", "en/category/{cid}/{pageslug}", new { action = "Index", Controller = "FProduct" });
            routes.MapRoute("categoriler_ar", "ar/category/{cid}/{pageslug}", new { action = "Index", Controller = "FProduct" });

            routes.MapRoute("fiyatlar_tr", "tr/fiyatlar/{id}/{pageslug}", new { action = "Prices", Controller = "FProduct" });
            routes.MapRoute("fiyatlar_en", "en/prices/{id}/{pageslug}", new { action = "Prices", Controller = "FProduct" });
            routes.MapRoute("fiyatlar_ar", "ar/prices/{id}/{pageslug}", new { action = "Prices", Controller = "FProduct" });

            routes.MapRoute("fiyatlist_tr", "tr/fiyatlar", new { action = "Prices", Controller = "FProduct" });
            routes.MapRoute("fiyatlist_en", "en/pricelist", new { action = "Prices", Controller = "FProduct" });
            routes.MapRoute("fiyatlist_ar", "ar/pricelist", new { action = "Prices", Controller = "FProduct" });

            routes.MapRoute("giris_tr", "tr/giris", new { action = "UserLogin", Controller = "FProduct" });
            routes.MapRoute("giris_en", "en/login", new { action = "UserLogin", Controller = "FProduct" });
            routes.MapRoute("giris_ar", "ar/login", new { action = "UserLogin", Controller = "FProduct" });


            routes.MapRoute("cik_tr", "tr/guvenlicikis", new { action = "UserLogout", Controller = "FProduct" });
            routes.MapRoute("cik_en", "en/logout", new { action = "UserLogout", Controller = "FProduct" });
            routes.MapRoute("cik_ar", "ar/logout", new { action = "UserLogout", Controller = "FProduct" });

            routes.MapRoute("makine_tr", "tr/makine-ve-arac-parkuru", new { action = "Machine", Controller = "FNews" });
            routes.MapRoute("makine_en", "en/machinery-and-vehicle-trails", new { action = "Machine", Controller = "FNews" });
            routes.MapRoute("makine_ar", "ar/machinery-and-vehicle-trails", new { action = "Machine", Controller = "FNews" });

            /*Default*/
            //routes.MapRoute("home_default", "/", new { action = "Index", Controller = "FHome" });
            //routes.MapRoute("home_default", "anasayfa", new { action = "Index", Controller = "FHome" });
            //routes.MapRoute("kurumsal", "kurumsal", new { action = "Index", Controller = "FInstitutional" });
            //routes.MapRoute("iletisim", "iletisim", new { action = "Index", Controller = "FContact" });
            //routes.MapRoute("haberler", "haberler", new { action = "Index", Controller = "FNews" });
            //routes.MapRoute("referanslar", "referanslar", new { action = "Index", Controller = "FReferences" });
            //routes.MapRoute("referansdetay", "referansdetay/{id}", new { action = "Detail", Controller = "FReferences" });
            routes.MapRoute("insankaynaklari", "insankaynaklari", new { action = "Index", Controller = "FHumanResources" });
            //routes.MapRoute("arama", "arama", new { action = "Index", Controller = "FSearch" });
            routes.MapRoute("sitemap", "siteharita", new { action = "Index", Controller = "FSiteMap" });
            
            //routes.MapRoute("giris", "giris", new { action = "UserLogin", Controller = "FProduct" });
            //routes.MapRoute("cik", "guvenlicikis", new { action = "UserLogout", Controller = "FProduct" });
            routes.MapRoute("hizmetler", "hizmetler/{id}/{page}", new { action = "Index", Controller = "FServices" });
            routes.MapRoute("siteharita_tr", "tr/siteharitasi", new { action = "SiteHarita", Controller = "FInstitutional" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FHome", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}