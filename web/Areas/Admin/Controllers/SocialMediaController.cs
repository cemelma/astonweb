using BLL.SocialMediaBL;
using DAL.Entities;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;

using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace web.Areas.Admin.Controllers
{
    public class SocialMediaController : Controller
    {
        //
        // GET: /Admin/SocialMedia/

        public ActionResult Index()
        {
            var list = SocialMediaManager.GetList();
            return View(list);
        }

        public ActionResult Add()
        {

           // ImageHelperNew.DestroyImageCashAndSession(1920, 1080);
            return View();
        }
        [HttpPost]
        public ActionResult Add(SocialMedia model, HttpPostedFileBase uploadfile)
        {
            //if (ModelState.IsValid)
            //{
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadfile.SaveAs(Server.MapPath("/Content/images/documents/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName)));
                    model.Logo = "/Content/images/socialmedia/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    model.Logo = "Dosya Yok";
                }
                ModelState.Clear();
                ViewBag.ProcessMessage = SocialMediaManager.AddSocialMedia(model);
            //}
            return View();
        }

        public ActionResult Edit()
        {
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    SocialMedia media = SocialMediaManager.GetSocialMediaById(nid);
                    return View(media);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult Edit(SocialMedia media, HttpPostedFileBase uploadfile)
        {

            //if (ModelState.IsValid)
            //{
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/mediainfo/", Utility.SetPagePlug(media.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    media.Logo = "/Content/images/socialmedia/" + Utility.SetPagePlug(media.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        media.Id = nid;
                        ViewBag.ProcessMessage = SocialMediaManager.EditMedia(media);
                        return View(media);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(media);
                    }
                }
                else
                    return View();
            //}
            //else
            //    return View();



        }



        public JsonResult DeleteLink(int id)
        {
            bool isdelete = SocialMediaManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }


        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = SocialMediaManager.SortRecords(idsList);
            return Json(issorted);
        }

        public class JsonList
        {
            public string[] list { get; set; }

          
        }


      

    }
}
