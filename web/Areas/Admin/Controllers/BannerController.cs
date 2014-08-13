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
using BLL.PhotoBL;
using DAL.Entities;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class BannerController : Controller
    {
        //
        // GET: /Admin/Photo/

        public ActionResult Index()
        {
            //string lang=FillLanguagesList();

            var photos = PhotoManager.GetList("tr", 0);
            return View(photos);
        }

        public ActionResult Add()
        {
            ImageHelperNew.DestroyImageCashAndSession(1120,350);
           // var languages = LanguageManager.GetLanguages();
           // var list = new SelectList(languages, "Culture", "Language");
        //    ViewBag.LanguageList = list;
           
            return View();
        }

        [HttpPost]
        public ActionResult Add(Photo newmodel, HttpPostedFileBase uploadfile)
        {
           // var languages = LanguageManager.GetLanguages();
           // var list = new SelectList(languages, "Culture", "Language");
          //  ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    newmodel.Path = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
                else
                {
                    newmodel.Path = "/Content/images/front/noimage.jpeg";
                }
                newmodel.Language = "tr";
                newmodel.Online = true;
                newmodel.SortOrder = 9999;
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = PhotoManager.Save(newmodel);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult Edit()
        {
            ImageHelperNew.DestroyImageCashAndSession(1120, 350);
            //var languages = LanguageManager.GetLanguages();
            //var list = new SelectList(languages, "Culture", "Language");
     //       ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Photo editPhoto = PhotoManager.GetById(nid);
                    return View(editPhoto);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult Edit(Photo Photomodel, HttpPostedFileBase uploadfile)
        {
            //var languages = LanguageManager.GetLanguages();
            //var list = new SelectList(languages, "Culture", "Language");
            //ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
                //if (uploadfile != null && uploadfile.ContentLength > 0)
                //{
                //    using (System.Drawing.Image image = System.Drawing.Image.FromStream(uploadfile.InputStream, true, true))
                //    {
                //        if (image.Width == 1440 && image.Height == 450)
                //        {
                //            Random random = new Random();
                //            int rand = random.Next(1000, 99999999);
                //            new ImageHelper(1440, 1440).SaveThumbnail(uploadfile, "/Content/images/Photos/", Utility.SetPagePlug("banner") + "_" + rand + Path.GetExtension(uploadfile.FileName));
                //            Photomodel.Path = "/Content/images/Photos/" + Utility.SetPagePlug("banner") + "_" + rand + Path.GetExtension(uploadfile.FileName);
                //            TempData["ImageSizeError"] = null;
                //        }
                //        else
                //        {
                //            TempData["ImageSizeError"] = "Eklemiş olduğunuz resmin boyutları 1440x450 olmalıdır...";
                //            return View();
                //        }
                //    }
                //}

                if (Session["ModifiedImageId"] != null)
                {
                    Photomodel.Path = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        Photomodel.PhotoId = nid;
                        ViewBag.ProcessMessage = PhotoManager.Edit(nid, Photomodel.Title, Photomodel.Path,Photomodel.Link);
                        return View(Photomodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(Photomodel);
                    }
                }
                else  return View();
            }
            else
                return View();

        }


        public JsonResult PhotoEditStatus(int id)
        {
            bool isupdate = PhotoManager.UpdateStatus(id);
            return Json(isupdate);
        }

        
        public JsonResult DeletePhoto(int id)
        {
            bool isdelete = PhotoManager.Delete(id);
            return Json(isdelete);
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
            bool issorted = PhotoManager.SortRecords(idsList);
            return Json(issorted);
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
    }
}
