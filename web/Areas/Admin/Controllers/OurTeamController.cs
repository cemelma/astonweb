using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.LanguageBL;
using BLL.OurTeamBL;
using DAL.Entities;
using web.Areas.Admin.Helpers;
using System.IO;
using System.Drawing;
using web.Areas.Admin.Filters;
using System.Web.Script.Serialization;
using System.Drawing.Imaging;
using web.Helpers.Enums;
using BLL.InstituionalBL;
namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class OurTeamController : Controller
    {
        
        public ActionResult Index()
        {
            string sellang = FillLanguagesList();
            var OurTeam = OurTeamManager.GetOurTeamList(sellang);
            return View(OurTeam);
        }

        public ActionResult AddOurTeam()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            ImageHelperNew.DestroyImageCashAndSession(175, 175);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOurTeam(OurTeam OurTeammodel,HttpPostedFileBase uploadfile,string txtdate)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    OurTeammodel.Image = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0,0);
                }
                else
                {
                    OurTeammodel.Image = "/Content/images/front/noimage.jpeg";
                }

                OurTeammodel.PageSlug = Utility.SetPagePlug(OurTeammodel.Name);
                OurTeammodel.TimeCreated = Utility.ControlDateTime(txtdate);
                ViewBag.ProcessMessage = OurTeamManager.AddOurTeam(OurTeammodel);
                ModelState.Clear();
                return View();
            }
            else          
                return View();
        }

        public JsonResult OurTeamEditStatus(int id)
        {
            string NowState;
            bool isupdate = OurTeamManager.UpdateStatus(id);
            return Json(isupdate);
        }

        [AllowAnonymous]
        public JsonResult DeleteOurTeam(int id)
        {
            bool isdelete = OurTeamManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }
        public ActionResult Danismanlarimiz()
        {
            string lang = FillLanguagesList();
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Danismanlarimiz));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Danismanlarimiz(Institutional record)
        {
            string lang = FillLanguagesList();
            record.Language = lang;
            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.Danismanlarimiz);
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            return View();
        }

        public ActionResult UzmanlikAlanlarimiz()
        {
            string lang = FillLanguagesList();
            var vision_info = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.UzmanlikAlanlarimiz));
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UzmanlikAlanlarimiz(Institutional record)
        {
            string lang = FillLanguagesList();
            record.Language = lang;
            record.TypeId = Convert.ToInt32(EnumInstituionalTypes.UzmanlikAlanlarimiz);
            ViewBag.ProcessMessage = InstituionalManager.EditInstituional(record);
            return View();
        }


        public ActionResult EditOurTeam()
        {
            ImageHelperNew.DestroyImageCashAndSession(175, 175);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if(RouteData.Values["id"]!=null)
            {
                int nid=0;
                bool isnumber=int.TryParse(RouteData.Values["id"].ToString(),out nid);
                if (isnumber)
                {
                    OurTeam editOurTeam = OurTeamManager.GetOurTeamById(nid);
                    return View(editOurTeam);
                }
                else
                    return View();
            }
            else
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


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditOurTeam(OurTeam OurTeammodel, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
         
                if (Session["ModifiedImageId"] != null)
                {
                    OurTeammodel.Image = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
              
                OurTeammodel.PageSlug = Utility.SetPagePlug(OurTeammodel.Name);

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        OurTeammodel.OurTeamId = nid;
                        ViewBag.ProcessMessage = OurTeamManager.EditOurTeam(OurTeammodel);
                        return View(OurTeammodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(OurTeammodel);
                    }
                }

                return View();
            }
            else
                return View();
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = OurTeamManager.SortRecords(idsList);
            return Json(issorted);


        }

    }
}
