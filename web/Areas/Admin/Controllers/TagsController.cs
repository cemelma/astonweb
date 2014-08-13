using BLL.LanguageBL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Areas.Admin.Controllers
{
    public class TagsController : Controller
    {
 
        public ActionResult Index()
        {
            string lang ="tr";
            if (RouteData.Values["lang"] != null)
            {
                lang = RouteData.Values["lang"].ToString();
            }
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language",lang);
            ViewBag.LanguageList = list;
            TagPages tagPages = new TagPages();
                int id = Convert.ToInt32(RouteData.Values["id"]);

                
                ViewBag.PageName = tagPages.tagList.Where(x => x.Key == id).FirstOrDefault().Value;
                DAL.Context.MainContext db = new DAL.Context.MainContext();
                Tags stag=db.Tags.Where(x => x.PageId == id && x.Lang==lang).FirstOrDefault();
                if (stag != null)
                {
                    return View(stag);
                }
                else
                    return View();
         
        }

        [HttpPost]
        public ActionResult Index(Tags model)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            TagPages tagPAges = new TagPages();
            int id = Convert.ToInt32(RouteData.Values["id"]);
            ViewBag.PageName = tagPAges.tagList.Where(x => x.Key == id).FirstOrDefault().Value;
            DAL.Context.MainContext db = new DAL.Context.MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == id && x.Lang==model.Lang).FirstOrDefault();
            //Tags stag=db.Tags.Find(id);
            if (stag == null)
            {
                model.PageId = id;
               
                db.Tags.Add(model);
                db.SaveChanges();
            }
            else
            {
                stag.Title = model.Title;
                stag.Description = model.Description;
                stag.Keyword = model.Keyword;
                
                db.SaveChanges();
            }
            return View(model);
        }

    }

    public class TagPages
    {
        public Dictionary<int, string> tagList = new Dictionary<int, string>();
        // GET: /Admin/Tags/
        public TagPages()
        {
            tagList.Add(1, "Ana Sayfa");
            tagList.Add(2, "Venue İstanbul Hakkında");
            tagList.Add(3, "Vizyonumuz ve Misyonumuz");
            tagList.Add(4, "Ekibimiz");
            tagList.Add(5, "Diğer Faaliyetlerimiz");
            tagList.Add(6, "Hizmetler");
            tagList.Add(7, "Sektörler");
            tagList.Add(8, "Firmalar ve Markalar");
            tagList.Add(9, "Neden Biz?");
            tagList.Add(10, "Sektörden Haberler");
            tagList.Add(11, "Basında Venue İstanbul");
            tagList.Add(12, "Adres Bilgileri ve Google Map");
            tagList.Add(13, "Kariyer Olanakları");

            tagList.Add(14, "Öne Çıkan Gayimenkuller");
            tagList.Add(15, "Detaylı Arama");
            tagList.Add(16, "Mesaj Bırakın, Sizi Arayalım");
            tagList.Add(17, "Detaylı Mesaj Bırakma");

        }
    }

  
}
