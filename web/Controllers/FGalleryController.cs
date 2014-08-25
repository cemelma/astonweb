using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Helpers.Enums;

namespace web.Controllers
{
    public class FGalleryController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Photo.Where(d => d.CategoryId == (int)PhotoTypes.Gallery && d.Language == lang).ToList();
                return View(list);
            }
        }

    }
}
