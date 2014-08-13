using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.HRBL;
using BLL.LanguageBL;
using DAL.Entities;
using web.Areas.Admin.Filters;
using System.IO;
using web.Areas.Admin.Helpers;
using System.Web.Script.Serialization;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class HumanResourceController : Controller
    {
        //
        // GET: /Admin/HumanResource/

        public ActionResult Index()
        {
            var vision_info = HumanResourceManager.GetHRList();
            vision_info=vision_info.OrderBy(x=>x.SortOrder).ToList();
              return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(HumanResource record)
        {
            ViewBag.ProcessMessage = HumanResourceManager.EditHumanResource(record);


            return View();
        }

        public ActionResult HumanResourcePositions()
        {
            var referncelist = HumanResourceManager.GetHumanResourcePositionList("tr");
            return View(referncelist);
        }

        public ActionResult AddHumanResourcePosition()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddHumanResourcePosition(HumanResource newmodel)
        {
          
            if (ModelState.IsValid)
            {
                newmodel.SortOrder = 9999;
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = HumanResourceManager.AddHumanResourcePosition(newmodel);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult EditHumanResourcePosition()
        {
          
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    HumanResource editHumanResourcePosition = HumanResourceManager.GetHumanResourcePositionById(nid);
                    return View(editHumanResourcePosition);
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
        public ActionResult EditHumanResourcePosition(HumanResource HumanResourcePositionmodel)
        {
          
            if (ModelState.IsValid)
            {
                
                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        HumanResourcePositionmodel.Id = nid;
                        
                        ViewBag.ProcessMessage = HumanResourceManager.EditHumanResourcePosition(HumanResourcePositionmodel);
                        return View(HumanResourcePositionmodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(HumanResourcePositionmodel);
                    }
                }
                else return View();
            }
            else
                return View();

            return View();
        }


        public JsonResult HumanResourcePositionEditStatus(int id)
        {
            string NowState;
            bool isupdate = HumanResourceManager.UpdateStatus(id);
            return Json(isupdate);
        }



        public JsonResult EditStatus(int id)
        {
            string NowState;
            bool isupdate = HumanResourceManager.UpdateStatus(id);
            return Json(isupdate);
        }

        public JsonResult DeleteHumanResourcePosition(int id)
        {
            bool isdelete = HumanResourceManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = HumanResourceManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
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



        public ActionResult ListCV()
        {
            if (RouteData.Values["id"] != null)
            {
                int posId = Convert.ToInt32(RouteData.Values["id"].ToString());
                List<HumanResource> positionList = HumanResourceManager.GetHumanResourcePositionList();
                List<SelectListItem> obj = new List<SelectListItem>();
                obj.Add(new SelectListItem { Text = "Tümü", Value = "0" });
                foreach (var item in positionList)
                {
                    if (posId != item.Id)
                        obj.Add(new SelectListItem { Text = item.PositionName, Value = Convert.ToString(item.Id) });
                    else obj.Add(new SelectListItem { Text = item.PositionName, Value = Convert.ToString(item.Id), Selected = true });
                }
                ViewData["position"] = obj;
                return View(HumanResourceManager.GetHumanResourceCV(posId));
            }
            else
            {
                List<HumanResource> positionList = HumanResourceManager.GetHumanResourcePositionList();
                List<SelectListItem> obj = new List<SelectListItem>();
                obj.Add(new SelectListItem { Text = "Tümü", Value = "0" });
                foreach (var item in positionList)
                {
                    obj.Add(new SelectListItem { Text = item.PositionName, Value = Convert.ToString(item.Id) });
                }
                ViewData["position"] = obj;
                return View(HumanResourceManager.GetHumanResourceCV());
            }
        }

        public JsonResult DeleteHumanPositionCv(int id)
        {
            bool isdelete = HumanResourceManager.DeleteCV(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

    }
}
