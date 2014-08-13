using BLL.InstituionalBL;
using BLL.OurTeamBL;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Helpers.Enums;
using web.Models;

namespace web.Controllers
{
    public class FOurTeamController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

         

        public ActionResult Index()
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == 4 && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }

            var list = OurTeamManager.GetOurTeamListForFront(lang);
            var dan = InstituionalManager.GetInstationalByLanguage(lang, (int)EnumInstituionalTypes.Danismanlarimiz); 
            var uzm = InstituionalManager.GetInstationalByLanguage(lang, (int)EnumInstituionalTypes.UzmanlikAlanlarimiz); 
            OurTeamWrapperModel m = new OurTeamWrapperModel(list, dan, uzm);
            return View(m);
        }
    }
}
