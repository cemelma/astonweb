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
using BLL.ReferenceBL;
using DAL.Entities;
using BLL.PhotoBL;
using System.Drawing;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ReferenceController : Controller
    {
        //
        // GET: /Admin/Reference/

        public ActionResult Index()
        {
          

            var referncelist = ReferenceManager.GetReferenceList("tr");
            return View(referncelist);
        }

        public ActionResult AddReference()
        {
            ImageHelperNew.DestroyImageCashAndSession(169, 62);
             return View();
        }

        [HttpPost]
        public ActionResult AddReference(References newmodel, HttpPostedFileBase uploadfile, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    newmodel.Logo = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
                else
                {
                    newmodel.Logo = "/Content/images/front/noimage.jpeg";
                }
                //if (uploadfile != null && uploadfile.ContentLength > 0)
                //{
                //    Random random = new Random();
                //    int rand = random.Next(1000, 99999999);
                //    new ImageHelper(240, 240).SaveThumbnail(uploadfile,"/Content/images/references/", Utility.SetPagePlug(newmodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                //    newmodel.Logo = "/Content/images/references/" + Utility.SetPagePlug(newmodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                //}
                //else
                //{
                //    newmodel.Logo = "/Content/images/front/noimage.jpeg";
                //}

                newmodel.Language = "tr";
                newmodel.SortOrder = 9999;
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = ReferenceManager.AddReference(newmodel);

                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        item.SaveAs(Server.MapPath("/Content/images/userfiles/")+item.FileName);
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        string path = Utility.SetPagePlug(newmodel.ReferenceName) + "_" + rand + Path.GetExtension(item.FileName);
                        new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/", path);

                        rand = random.Next(1000, 99999999);
                        string thumbnail = Utility.SetPagePlug(newmodel.ReferenceName) + "_" + rand + Path.GetExtension(item.FileName);

                       // Image img = Image.FromFile(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                        Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                    Bitmap bmp2 = new Bitmap(bmp);

                  using (Bitmap Orgbmp = bmp2)
                   {

                       int sabit = 90;
                       Size Boyut = new Size(210, 125);
                       Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                        ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/")+thumbnail);
                       bmp.Dispose();
                         bmp2.Dispose();
                       Orgbmp.Dispose();
                       GC.Collect();
                    }

                       //new ImageHelper(300, 280).ResizeFromStream("/Content/images/userfiles/",thumbnail,img);
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.Reference;
                        p.ItemId = newmodel.ReferenceId;
                        p.Path = "/Content/images/userfiles/" + path;
                        p.Thumbnail = "/Content/images/userfiles/" + thumbnail;
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = "tr";
                        p.TimeCreated = DateTime.Now;
                        p.Title = newmodel.ReferenceName;
                        PhotoManager.Save(p);
                    }
                }


             
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult EditReference()
        {
         
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    ImageHelperNew.DestroyImageCashAndSession(169, 62);
                    References editreference = ReferenceManager.GetReferenceById(nid);
                    var photos = PhotoManager.GetList(4,nid);
                    ViewBag.Photos = photos;
                    return View(editreference);
                }
                else
                    return View();
            }
            else
                return View();
            return View();
        }

        [HttpPost]
        public ActionResult EditReference(References referencemodel, HttpPostedFileBase uploadfile,IEnumerable<HttpPostedFileBase> attachments)
        {
             int ID = Convert.ToInt32(RouteData.Values["id"]);
            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    referencemodel.Logo = "/Content/images/userfiles/news/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
                //if (uploadfile != null && uploadfile.ContentLength > 0)
                //{
                //    Random random = new Random();
                //    int rand = random.Next(1000, 99999999);
                //    new ImageHelper(240, 240).SaveThumbnail(uploadfile, "/Content/images/references/", Utility.SetPagePlug(referencemodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                //    referencemodel.Logo = "/Content/images/references/" + Utility.SetPagePlug(referencemodel.ReferenceName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                //}
              
                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        item.SaveAs(Server.MapPath("/Content/images/userfiles/") + item.FileName);
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        string path = Utility.SetPagePlug(referencemodel.ReferenceName) + "_" + rand + Path.GetExtension(item.FileName);
                        new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/", path);

                        rand = random.Next(1000, 99999999);
                        string thumbnail = Utility.SetPagePlug(referencemodel.ReferenceName) + "_" + rand + Path.GetExtension(item.FileName);

                        // Image img = Image.FromFile(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                        Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                        Bitmap bmp2 = new Bitmap(bmp);

                        using (Bitmap Orgbmp = bmp2)
                        {

                            int sabit = 90;
                            Size Boyut = new Size(210, 125);
                            Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                            ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/") + thumbnail);
                            bmp.Dispose();
                            bmp2.Dispose();
                            Orgbmp.Dispose();
                            GC.Collect();
                        }

                        //new ImageHelper(300, 280).ResizeFromStream("/Content/images/userfiles/",thumbnail,img);
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.Reference;
                        p.ItemId = ID;
                        p.Path = "/Content/images/userfiles/" + path;
                        p.Thumbnail = "/Content/images/userfiles/" + thumbnail;
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = "tr";
                        p.TimeCreated = DateTime.Now;
                        p.Title = "Haberler";
                        PhotoManager.Save(p);
                    }
                }




                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        referencemodel.Language = "tr";
                        referencemodel.ReferenceId = nid;
                        ViewBag.ProcessMessage = ReferenceManager.EditReference(referencemodel);
                        var photos = PhotoManager.GetList(4,ID);
                        ViewBag.Photos = photos;
                        return View(referencemodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(referencemodel);
                    }
                }
                else  return View();
            }
            else
                return View();

            return View();
        }


        public JsonResult ReferenceEditStatus(int id)
        {
            string NowState;
            bool isupdate = ReferenceManager.UpdateStatus(id);
            return Json(isupdate);
        }

        
        public JsonResult DeleteReference(int id)
        {
            bool isdelete = ReferenceManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
          //  else return false;
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
            bool issorted = ReferenceManager.SortRecords(idsList);
            return Json(issorted);


        }

        public JsonResult SortPhotos(string list)
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

        public JsonResult DeletePhoto(int id)
        {
            bool isdelete = PhotoManager.Delete(id);
            return Json(isdelete);
        }
    }
}
