using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Filters;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using DAL.Entities;
using BLL.SolutionPartnerBL;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class SolutionPartnerController : Controller
    {
        //
        // GET: /Admin/SolutionPartner/

        public ActionResult Index()
        {
            string sellang=FillLanguagesList();

            var referncelist = SolutionPartnerManager.GetSolutionPartnerList(sellang);
            return View(referncelist);
        }

        public ActionResult AddSolutionPartner()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
           
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSolutionPartner(SolutionPartner newmodel, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(240, 240).SaveThumbnail(uploadfile, "/Content/images/solutionpartner/", Utility.SetPagePlug(newmodel.SolutionPartnerName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    newmodel.Logo = "/Content/images/solutionpartner/" + Utility.SetPagePlug(newmodel.SolutionPartnerName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    newmodel.Logo = "/Content/images/front/noimage.jpeg";
                }
                newmodel.SortOrder = 9999;
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = SolutionPartnerManager.AddSolutionPartner(newmodel);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult EditSolutionPartner()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    SolutionPartner editSolutionPartner = SolutionPartnerManager.GetSolutionPartnerById(nid);
                    return View(editSolutionPartner);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSolutionPartner(SolutionPartner SolutionPartnermodel, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(240, 240).SaveThumbnail(uploadfile, "/Content/images/solutionpartner/", Utility.SetPagePlug(SolutionPartnermodel.SolutionPartnerName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    SolutionPartnermodel.Logo = "/Content/images/solutionpartner/" + Utility.SetPagePlug(SolutionPartnermodel.SolutionPartnerName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }


                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        SolutionPartnermodel.SolutionPartnerId = nid;
                        ViewBag.ProcessMessage = SolutionPartnerManager.EditSolutionPartner(SolutionPartnermodel);
                        return View(SolutionPartnermodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(SolutionPartnermodel);
                    }
                }
                else  return View();
            }
            else
                return View();

            return View();
        }


        public JsonResult SolutionPartnerEditStatus(int id)
        {
            string NowState;
            bool isupdate = SolutionPartnerManager.UpdateStatus(id);
            return Json(isupdate);
        }

        
        public JsonResult DeleteSolutionPartner(int id)
        {
            bool isdelete = SolutionPartnerManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
          //  else return false;
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

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = SolutionPartnerManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
       

    }
}
