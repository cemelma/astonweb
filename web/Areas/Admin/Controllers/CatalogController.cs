﻿using System;
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
using BLL.Catalog;
using System.IO;
using web.Areas.Admin.Helpers;

namespace web.Areas.Admin.Controllers
{
    public class CatalogController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CatalogList()
        {
            using (MainContext db = new MainContext())
            {
                string lang = FillLanguagesList();
                var list = db.Photo.Where(d => d.CategoryId == (int)PhotoTypes.Catalog && d.Language == lang).OrderBy(d=>d.SortOrder).ToList();
                return View(list);
            }
        }

        public ActionResult AddImage()
        {
            ImageHelperNew.DestroyImageCashAndSession(1536, 2048);
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

            var groups = CatalogManager.GetGalleryGroupList(lang);

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
        public ActionResult AddImage(Photo newmodel, HttpPostedFileBase fileUpload, string language, string Title)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            Photo p = new Photo();

            if (fileUpload != null)
            {
                Random random = new Random();
                int rand = random.Next(1000, 99999999);
                string path = rand + Path.GetFileName(fileUpload.FileName);
                fileUpload.SaveAs(Server.MapPath("~/Content/images/userfiles/news/") + path);
                newmodel.Path = "/Content/images/userfiles/news/" + path;
            }
            else
            {
                newmodel.Path = "/Content/images/front/noimage.jpeg";
            }

            //if (Session["ModifiedImageId"] != null)
            //{
            //    string imagename = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString();// +Session["WorkingImageExtension"].ToString();
            //    newmodel.Path = imagename + ".jpeg";
            //    ImageHelperNew.DestroyImageCashAndSession(0, 0);

            //    Helpers.ImageHelper.WaterMark(imagename, 100);
            //}
            //else
            //{
            //    return View();
            //    newmodel.Path = "/Content/images/front/noimage.jpeg";
            //}

            p.CategoryId = (int)PhotoTypes.Catalog;
            p.Title = Title;
            p.Language = language;
            p.Online = true;
            p.SortOrder = 9999;
            p.TimeCreated = DateTime.Now;
            p.Path = newmodel.Path;

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
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
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
            FillLanguagesList();
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

        public JsonResult DeletePhoto(int id)
        {
            bool isdelete = PhotoManager.Delete(id);
            return Json(isdelete);
        }

    }
}
