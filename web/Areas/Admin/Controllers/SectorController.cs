using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.SectorBL;
using DAL.Entities;
using web.Areas.Admin.Filters;
using BLL.SectorGroupBL;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class SectorController : Controller
    {
        //
        // GET: /Admin/Sector/

        public ActionResult Index()
        {
            string id = FillLanguagesList();
            int groupid = Convert.ToInt32(id);

            var list = SectorManager.GetSectorList(groupid);
            return View(list);
        }

        public ActionResult OurSectors()
        {
            FillLanguagesList();

            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var Sectorpage = SectorManager.GetOurSectors(lang);
            return View(Sectorpage);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OurSectors(OurSectors record)
        {
            FillLanguagesList();
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            record.Language = lang;
            ViewBag.ProcessMessage = SectorManager.EditOurSectors(record);
            return View();
        }

        public ActionResult AddSector()
        {
            FillLanguagesList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSector(Sector newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
               
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.Name);
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = SectorManager.AddSector(newmodel);
                ModelState.Clear();

                return View();
            }
            else
                return View();
        }


        public ActionResult EditSector()
        {
            FillLanguagesList();

            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Sector editrecord = SectorManager.GetSectorById(nid);
                    return View(editrecord);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSector(Sector newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
               
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.Name);

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        newmodel.SectorId = nid;
                        ViewBag.ProcessMessage = SectorManager.EditSector(newmodel);
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
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = SectorGroupManager.GetSectorGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().SectorGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "SectorGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;

            return id;
        }


        public JsonResult EditStatus(int id)
        {
            bool isupdate = SectorManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult Delete(int id)
        {
            bool isdelete = SectorManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = SectorGroupManager.GetSectorGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "SectorGroupId", "GroupName"));
            return result;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = SectorManager.SortRecords(idsList);
            return Json(issorted);
        }
        public class JsonList
        {
            public string[] list { get; set; }
        }
       
    }
}
