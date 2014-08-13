using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.InstituionalBL;
using BLL.LanguageBL;
using DAL.Context;
using DAL.Entities;
using web.Helpers.Enums;
using web.Areas.Admin.Filters;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class InstitutionalController : Controller
    {
        public ActionResult Index()
        {
         
            return View();
        }

        //public ActionResult Create()
        //{
        //    string lang = FillLanguagesList();
        //    return View();
        //}

        public ActionResult Create(int id = 0)
        {
            string lang = FillLanguagesList();
            if (id == 0)
            {
                return View();
            }
            var rec = PageManager.Get(id);
            return View(rec);    
        }
        
        public JsonResult DeletePage(int id)
        {
            bool isdelete = PageManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Page record, string ddlLanguage)
        {
            record.Language = ddlLanguage;
            record.PageSlug = Utility.SetPagePlug(record.Header);
            if (record.PageId == 0)
            {
                ViewBag.ProcessMessage = PageManager.Create(record);
            }
            else
            {
                ViewBag.ProcessMessage = PageManager.Edit(record);
            }
            return RedirectToAction("Index");
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = PageManager.Sort(idsList);
            return Json(issorted);
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }

        public ActionResult WhyUs()
        {
            string lang = FillLanguagesList();
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.NedenBiz));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult WhyUs(Institutional record)
        {
            string lang = FillLanguagesList();
            record.Language = lang;
            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.NedenBiz);
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            return View();
        }

        public ActionResult Hakkimizda()
        {
            var vision_info = InstituionalManager.GetInstationalByLanguage("tr", Convert.ToInt32(EnumInstituionalTypes.Hakkimizda));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Hakkimizda(Institutional record, HttpPostedFileBase fileUpload)
        {
          
            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.Hakkimizda);
            record.Language = "tr";
            //if (fileUpload != null)
            //{
            //    Random random = new Random();
            //    int rand = random.Next(1000, 99999999);

            //    fileUpload.SaveAs(Server.MapPath("/Content/images/" + fileUpload.FileName));
            //    record.Resim = "/Content/images/" + Utility.SetPagePlug(fileUpload.FileName) + "_" + rand + System.IO.Path.GetExtension(fileUpload.FileName);

            //}
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            return View();
        }

        public ActionResult Ekibimiz()
        {
            string lang = FillLanguagesList();
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Ekibimiz));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekibimiz(Institutional record)
        {
            string lang = FillLanguagesList();
            record.Language = lang;
            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.Ekibimiz);
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            return View();
        }

        
        public ActionResult Vizyon()
        {
            string lang = FillLanguagesList();
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Vizyon));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Vizyon(Institutional record, HttpPostedFileBase fileUpload)
        {
            string lang = FillLanguagesList();
            record.Language = lang;
            record.TypeId=Convert.ToInt32(EnumInstituionalTypes.Vizyon);
            if (fileUpload != null)
            {
                Random random = new Random();
                int rand = random.Next(1000, 99999999);

                fileUpload.SaveAs(Server.MapPath("/Content/images/" + fileUpload.FileName));
                record.Resim = "/Content/images/" + Utility.SetPagePlug(fileUpload.FileName) + "_" + rand + System.IO.Path.GetExtension(fileUpload.FileName);

            }
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            return View();
        }

        public ActionResult Misyon()
        {
            string lang = FillLanguagesList();
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Misyon));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Misyon(Institutional record, HttpPostedFileBase fileUpload)
        {
            string lang = FillLanguagesList();
            record.Language = lang;
            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.Misyon);
            if (fileUpload != null)
            {
                Random random = new Random();
                int rand = random.Next(1000, 99999999);

                fileUpload.SaveAs(Server.MapPath("/Content/images/" + fileUpload.FileName));
                record.Resim = "/Content/images/" + Utility.SetPagePlug(fileUpload.FileName) + "_" + rand + System.IO.Path.GetExtension(fileUpload.FileName);

            }
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            return View();
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
