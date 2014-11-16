using BLL.LanguageBL;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Helpers.Enums;

namespace web.Controllers
{
    public class FCatalogController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            using (MainContext db = new MainContext())
            {
                string lang = FillLanguagesList();
                var list = db.Photo.Where(d => d.CategoryId == (int)PhotoTypes.Catalog && d.Language == lang).OrderBy(d => d.SortOrder).ToList();
                return View(list);
            }
        }

        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }


    }
}
