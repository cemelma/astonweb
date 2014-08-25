using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Gallery;
using BLL.LanguageBL;
using DAL.Context;
using web.Helpers.Enums;
using System.Web.Script.Serialization;
using BLL.PhotoBL;
using DAL.Entities;

namespace web.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GalleryList()
        {
            using (MainContext db = new MainContext())
            {
                FillLanguagesList();
                var list = db.Photo.Where(d => d.CategoryId == (int)PhotoTypes.Gallery).ToList();
                return View(list);
            }
        }

        public ActionResult AddImage()
        {
            ImageHelperNew.DestroyImageCashAndSession(1024, 768);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            return View();
        }


        string FillLanguagesListForGalleryList()
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = GalleryManager.GetGalleryGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().GalleryGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "GalleryGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;

            return id;
        }
        [HttpPost]
        public ActionResult AddImage(Photo newmodel, HttpPostedFileBase uploadfile, string language, string Title)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            Photo p = new Photo();
            
            if (Session["ModifiedImageId"] != null)
            {
                newmodel.Path = "/userfiles/images/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                ImageHelperNew.DestroyImageCashAndSession(0, 0);
            }
            else
            {
                newmodel.Path = "/Content/images/front/noimage.jpeg";
            }

            p.CategoryId = (int)PhotoTypes.Gallery;
            p.Title = Title;
            p.Language = language;
            p.Online = true;
            p.SortOrder = 9999;
            p.TimeCreated = DateTime.Now;

            ViewBag.ProcessMessage = PhotoManager.Save(p);

            return View();
        }



        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            //var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = GalleryManager.GetGalleryGroupList(lang);
            var grouplist = new SelectList(groups, "GalleryGroupId", "GroupName");
            ViewBag.GroupList = grouplist;

            return lang;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = GalleryManager.GetGalleryGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "GalleryGroupId", "GroupName"));
            return result;
        }


        [HttpPost]
        public string UploadFile(HttpPostedFileBase fileData,
            FormCollection form)
        {
            //var guid = form.Get("thisGuid");
            //var fileName = Server.MapPath("~/Content/upload/" +
            //     System.IO.Path.GetFileName(guid + fileData.FileName));
         
            //person.Picture = guid + fileData.FileName;
            //_repo.Update(person);
            //fileData.SaveAs(fileName);
            return "ok";
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = PhotoManager.SortRecords(idsList);
            return Json(issorted);
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }

        public void Upload(HttpPostedFileBase files)
        {

        }

    }
}
