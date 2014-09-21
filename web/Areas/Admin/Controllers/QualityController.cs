using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.LanguageBL;
using BLL.QualityBL;
using DAL.Entities;
using web.Areas.Admin.Helpers;
using System.IO;
using System.Drawing;
using web.Areas.Admin.Filters;
using System.Web.Script.Serialization;
using System.Drawing.Imaging;
using BLL.PhotoBL;
using System.Web.Helpers;
using BLL.QualityBL;
namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class QualityController : Controller
    {
              
        public ActionResult Index()
        {
            string lang = FillLanguagesList();
            var Quality = QualityManager.GetQualityList(lang, false);
            return View(Quality);
        }

        public ActionResult AddQuality()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            ImageHelperNew.DestroyImageCashAndSession(600, 338);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddQuality(IEnumerable<HttpPostedFileBase> attachments, Quality Qualitymodel, HttpPostedFileBase uploadfile, string txtdate, string Language)
        {
           
            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    string imagename = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString();// + Session["WorkingImageExtension"].ToString();
                    Qualitymodel.NewsImage = imagename + ".jpeg";
                    ImageHelperNew.DestroyImageCashAndSession(0,0);

                    Helpers.ImageHelper.WaterMark(imagename, 100);
                }
                else
                {
                    Qualitymodel.NewsImage = "/Content/images/front/noimage.jpeg";
                }

                Qualitymodel.Language = Language;
                Qualitymodel.TypeId = 0;
                Qualitymodel.IsMachine = false;
                Qualitymodel.PageSlug = Utility.SetPagePlug(Qualitymodel.Header);
                Qualitymodel.TimeCreated = Utility.ControlDateTime(txtdate);
                ViewBag.ProcessMessage = QualityManager.AddQuality(Qualitymodel);
                Session.Remove("UploadType");
                //foreach (var item in attachments)
                //{
                //    if (item != null && item.ContentLength > 0)
                //    {
                //        Random random = new Random();
                //        int rand = random.Next(1000, 99999999);
                //        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(Qualitymodel.Header) + "_" + rand + Path.GetExtension(item.FileName));
                //        Photo p = new Photo();
                //        p.CategoryId = (int)PhotoType.Quality;
                //        p.ItemId = Qualitymodel.QualityId;
                //        p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(Qualitymodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                //        p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(Qualitymodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                //        p.Online = true;
                //        p.SortOrder = 9999;
                //        p.Language = "tr";
                //        p.TimeCreated = DateTime.Now;
                //        p.Title = "Haberler";
                //        PhotoManager.Save(p);
                //    }
                //}
                ModelState.Clear();
               // Response.Redirect("/yonetim/haberduzenle/" + Qualitymodel.QualityId);
                var languages = LanguageManager.GetLanguages();
                var list = new SelectList(languages, "Culture", "Language");
                ViewBag.LanguageList = list;
                return View();
            }
            else          
                return View();
        }

        public JsonResult QualityEditStatus(int id)
        {
            string NowState;
            bool isupdate = QualityManager.UpdateStatus(id);
            return Json(isupdate);
        }

        [AllowAnonymous]
        public JsonResult DeleteQuality(int id)
        {
            bool isdelete = QualityManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public ActionResult EditQuality()
        {
     
            if(RouteData.Values["id"]!=null)
            {
                int nid=0;
                bool isnumber=int.TryParse(RouteData.Values["id"].ToString(),out nid);
                if (isnumber)
                {
                    ImageHelperNew.DestroyImageCashAndSession(600, 338);
                    Quality editQuality = QualityManager.GetQualityById(nid);
                    var languages = LanguageManager.GetLanguages();
                    var list = new SelectList(languages, "Culture", "Language");
                    ViewBag.LanguageList = list;
                    return View(editQuality);
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
        [SaveImageAltTags]
        public ActionResult EditQuality(Quality Qualitymodel, HttpPostedFileBase uploadfile, string txtdate, string Language)
        {
    
            if (ModelState.IsValid)
            {
                
                if (Session["ModifiedImageId"] != null)
                {
                    string imagename = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString();// +Session["WorkingImageExtension"].ToString();
                    Qualitymodel.NewsImage = imagename + ".jpeg";
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);

                    Helpers.ImageHelper.WaterMark(imagename,100);
                }
              
                Qualitymodel.PageSlug = Utility.SetPagePlug(Qualitymodel.Header);
                Qualitymodel.TimeCreated = Utility.ControlDateTime(txtdate);
                Qualitymodel.Language = Language;
                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        Qualitymodel.QualityId = nid;
                        ViewBag.ProcessMessage = QualityManager.EditQuality(Qualitymodel);
                        Session.Remove("UploadType");
                        //foreach (var item in attachments)
                        //{
                        //    if (item != null && item.ContentLength > 0)
                        //    {
                        //        Random random = new Random();
                        //        int rand = random.Next(1000, 99999999);
                        //        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(Qualitymodel.Header) + "_" + rand + Path.GetExtension(item.FileName));
                        //        Photo p = new Photo();
                        //        p.CategoryId = (int)PhotoType.Quality;
                        //        p.ItemId = Qualitymodel.QualityId;
                        //        p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(Qualitymodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                        //        p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(Qualitymodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                        //        p.Online = true;
                        //        p.SortOrder = 9999;
                        //        p.Language = lang;
                        //        p.TimeCreated = DateTime.Now;
                        //        p.Title = "Haberler";
                        //        PhotoManager.Save(p);
                        //    }
                        //}
                        var languages = LanguageManager.GetLanguages();
                        var list = new SelectList(languages, "Culture", "Language");
                        ViewBag.LanguageList = list;
                        return View(Qualitymodel);
                    }
                    else
                    {
                        var languages = LanguageManager.GetLanguages();
                        var list = new SelectList(languages, "Culture", "Language");
                        ViewBag.LanguageList = list;
                        ViewBag.ProcessMessage = false;
                        return View(Qualitymodel);
                    }
                }


                
                
                // Response.Redirect("/yonetim/haberduzenle/" + Qualitymodel.QualityId);
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
            bool issorted = QualityManager.SortRecords(idsList);
            return Json(issorted);


        }

    }
}
