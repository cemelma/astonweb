using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.ServiceGroupBL;
using DAL.Entities;
using web.Areas.Admin.Filters;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class ServiceGroupController : Controller
    {
        //
        // GET: /Admin/ServiceGroup/

        public ActionResult Index()
        {
            string sellang = FillLanguagesList();

            var list = ServiceGroupManager.GetServiceGroupList(sellang);
            return View(list);
        }

        public ActionResult AddServiceGroup()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddServiceGroup(ServiceGroup newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
               
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.GroupName);
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = ServiceGroupManager.AddServiceGroup(newmodel);
                ModelState.Clear();

                return View();
            }
            else
                return View();
        }


        public ActionResult EditServiceGroup()
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
                    ServiceGroup editrecord = ServiceGroupManager.GetServiceGroupById(nid);
                    return View(editrecord);
                }
                else
                    return View();
            }
            else
                return View();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditServiceGroup(ServiceGroup newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
               
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.GroupName);

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        newmodel.ServiceGroupId = nid;
                        ViewBag.ProcessMessage = ServiceGroupManager.EditServiceGroup(newmodel);
                        return View(newmodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(newmodel);
                    }
                }
                else return View();
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


        public JsonResult EditStatus(int id)
        {
            string NowState;
            bool isupdate = ServiceGroupManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult Delete(int id)
        {
            bool isdelete = ServiceGroupManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ServiceGroupManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
    }
}
