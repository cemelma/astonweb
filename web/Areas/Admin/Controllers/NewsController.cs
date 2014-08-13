using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.LanguageBL;
using BLL.NewsBL;
using DAL.Entities;
using web.Areas.Admin.Helpers;
using System.IO;
using System.Drawing;
using web.Areas.Admin.Filters;
using System.Web.Script.Serialization;
using System.Drawing.Imaging;
using BLL.PhotoBL;
using System.Web.Helpers;
namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class NewsController : Controller
    {
              
        public ActionResult Index()
        {
           
            var news = NewsManager.GetNewsList("tr");
            return View(news);
        }

        public ActionResult AddNews()
        {
            //var languages = LanguageManager.GetLanguages();
            //var list = new SelectList(languages, "Culture", "Language");
            //ViewBag.LanguageList = list;
            ImageHelperNew.DestroyImageCashAndSession(600, 338);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNews(IEnumerable<HttpPostedFileBase> attachments, News newsmodel, HttpPostedFileBase uploadfile, string txtdate)
        {
           
            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    string imagename = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString();// + Session["WorkingImageExtension"].ToString();
                    newsmodel.NewsImage = imagename + ".jpeg";
                    ImageHelperNew.DestroyImageCashAndSession(0,0);

                    Helpers.ImageHelper.WaterMark(imagename, 100);
                }
                else
                {
                    newsmodel.NewsImage = "/Content/images/front/noimage.jpeg";
                }

                newsmodel.Language = "tr";
                newsmodel.TypeId = 0;
                newsmodel.PageSlug = Utility.SetPagePlug(newsmodel.Header);
                newsmodel.TimeCreated = Utility.ControlDateTime(txtdate);
                ViewBag.ProcessMessage = NewsManager.AddNews(newsmodel);
                Session.Remove("UploadType");
                //foreach (var item in attachments)
                //{
                //    if (item != null && item.ContentLength > 0)
                //    {
                //        Random random = new Random();
                //        int rand = random.Next(1000, 99999999);
                //        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(newsmodel.Header) + "_" + rand + Path.GetExtension(item.FileName));
                //        Photo p = new Photo();
                //        p.CategoryId = (int)PhotoType.News;
                //        p.ItemId = newsmodel.NewsId;
                //        p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(newsmodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                //        p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(newsmodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                //        p.Online = true;
                //        p.SortOrder = 9999;
                //        p.Language = "tr";
                //        p.TimeCreated = DateTime.Now;
                //        p.Title = "Haberler";
                //        PhotoManager.Save(p);
                //    }
                //}
                ModelState.Clear();
               // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else          
                return View();
        }

        public JsonResult NewsEditStatus(int id)
        {
            string NowState;
            bool isupdate = NewsManager.UpdateStatus(id);
            return Json(isupdate);
        }

        [AllowAnonymous]
        public JsonResult DeleteNews(int id)
        {
            bool isdelete = NewsManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public ActionResult EditNews()
        {
     
            if(RouteData.Values["id"]!=null)
            {
                int nid=0;
                bool isnumber=int.TryParse(RouteData.Values["id"].ToString(),out nid);
                if (isnumber)
                {
                    ImageHelperNew.DestroyImageCashAndSession(600, 338);
                    News editnews = NewsManager.GetNewsById(nid);
                    return View(editnews);
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
        public ActionResult EditNews( News newsmodel, HttpPostedFileBase uploadfile,string txtdate)
        {
    
            if (ModelState.IsValid)
            {
                
                if (Session["ModifiedImageId"] != null)
                {
                    string imagename = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString();// +Session["WorkingImageExtension"].ToString();
                    newsmodel.NewsImage = imagename + ".jpeg";
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);

                    Helpers.ImageHelper.WaterMark(imagename,100);
                }
              
                newsmodel.PageSlug = Utility.SetPagePlug(newsmodel.Header);
                newsmodel.TimeCreated = Utility.ControlDateTime(txtdate);
                newsmodel.Language = "tr";
                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        newsmodel.NewsId = nid;
                        ViewBag.ProcessMessage = NewsManager.EditNews(newsmodel);
                        Session.Remove("UploadType");
                        //foreach (var item in attachments)
                        //{
                        //    if (item != null && item.ContentLength > 0)
                        //    {
                        //        Random random = new Random();
                        //        int rand = random.Next(1000, 99999999);
                        //        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(newsmodel.Header) + "_" + rand + Path.GetExtension(item.FileName));
                        //        Photo p = new Photo();
                        //        p.CategoryId = (int)PhotoType.News;
                        //        p.ItemId = newsmodel.NewsId;
                        //        p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(newsmodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                        //        p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(newsmodel.Header) + "_" + rand + Path.GetExtension(item.FileName);
                        //        p.Online = true;
                        //        p.SortOrder = 9999;
                        //        p.Language = lang;
                        //        p.TimeCreated = DateTime.Now;
                        //        p.Title = "Haberler";
                        //        PhotoManager.Save(p);
                        //    }
                        //}
                        return View(newsmodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(newsmodel);
                    }
                }


                
                
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
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
            bool issorted = NewsManager.SortRecords(idsList);
            return Json(issorted);


        }

    }
}
