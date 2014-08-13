using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.SectorGroupBL;
using DAL.Entities;
using web.Areas.Admin.Filters;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class SectorGroupController : Controller
    {
        //
        // GET: /Admin/SectorGroup/

        public ActionResult Index()
        {
            string sellang = FillLanguagesList();

            var list = SectorGroupManager.GetSectorGroupList(sellang);
            return View(list);
        }

        public ActionResult AddSectorGroup()
        {
            ImageHelperNew.DestroyImageCashAndSession(175, 127);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSectorGroup(SectorGroup newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                //if (uploadimage != null && uploadimage.ContentLength > 0)
                //{
                //    using (System.Drawing.Image image = System.Drawing.Image.FromStream(uploadimage.InputStream, true, true))
                //    {
                //        if (image.Width == 48 && image.Height == 48)
                //        {
                //            Random random = new Random();
                //            int rand = random.Next(1000, 99999999);
                //            uploadimage.SaveAs(Server.MapPath("/Content/images/projects/" + Utility.SetPagePlug(newmodel.GroupName) + "_" + rand + Path.GetExtension(uploadimage.FileName)));
                //            newmodel.SectorGroupLogo = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.GroupName) + "_" + rand + Path.GetExtension(uploadimage.FileName);
                //        }
                //        else
                //        {
                //            TempData["ImageSizeError"] = "Eklemiş olduğunuz ikonun boyutları 48x48 olmalıdır...";
                //            return View();
                //        }
                //    }
                //}
                if (Session["ModifiedImageId"] != null)
                {
                    newmodel.SectorGroupLogo = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
                else
                {
                    newmodel.SectorGroupLogo = "/Content/images/front/noimage.jpeg";
                }
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.GroupName);
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = SectorGroupManager.AddSectorGroup(newmodel);
                ModelState.Clear();

                return View();
            }
            else
                return View();
        }


        public ActionResult EditSectorGroup()
        {
            ImageHelperNew.DestroyImageCashAndSession(175, 127);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    SectorGroup editrecord = SectorGroupManager.GetSectorGroupById(nid);
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
        public ActionResult EditSectorGroup(SectorGroup newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
                //if (uploadimage != null && uploadimage.ContentLength > 0)
                //{
                //    using (System.Drawing.Image image = System.Drawing.Image.FromStream(uploadimage.InputStream, true, true))
                //    {
                //        if (image.Width == 48 && image.Height == 48)
                //        {
                //            Random random = new Random();
                //            int rand = random.Next(1000, 99999999);
                //            uploadimage.SaveAs(Server.MapPath("/Content/images/sectors/" + Utility.SetPagePlug(newmodel.GroupName) + "_" + rand + Path.GetExtension(uploadimage.FileName)));
                //            newmodel.SectorGroupLogo = "/Content/images/sectors/" + Utility.SetPagePlug(newmodel.GroupName) + "_" + rand + Path.GetExtension(uploadimage.FileName);
                //        }
                //        else
                //        {
                //            TempData["ImageSizeError"] = "Eklemiş olduğunuz ikonun boyutları 48x48 olmalıdır...";
                //            return View();
                //        }
                //    }
                //}
                if (Session["ModifiedImageId"] != null)
                {
                    newmodel.SectorGroupLogo = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
               
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.GroupName);

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        newmodel.SectorGroupId = nid;
                        ViewBag.ProcessMessage = SectorGroupManager.EditSectorGroup(newmodel);
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
            bool isupdate = SectorGroupManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult Delete(int id)
        {
            bool isdelete = SectorGroupManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = SectorGroupManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
    }
}
