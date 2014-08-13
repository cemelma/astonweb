using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.ServiceBL;
using DAL.Entities;
using web.Areas.Admin.Filters;
using BLL.ServiceGroupBL;
using System.Drawing;
using BLL.PhotoBL;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class ServiceController : Controller
    {
        //
        // GET: /Admin/Service/

        public ActionResult Index()
        {
            string id = FillLanguagesList();
            int groupid = Convert.ToInt32(id);

            var list = ServiceManager.GetServiceList(groupid);
            return View(list);
        }

        public ActionResult OurServices()
        {
            var servicepage = ServiceManager.GetOurServices("tr");
            return View(servicepage);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OurServices(OurServices record)
        {
            FillLanguagesList();
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            record.Language = lang;
            ViewBag.ProcessMessage = ServiceManager.EditOurServices(record);
            return View();
        }

        public ActionResult AddService()
        {
        
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddService(Service newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage,IEnumerable<HttpPostedFileBase> attachments)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.Name);
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = ServiceManager.AddService(newmodel);

                 foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        string path = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                        item.SaveAs(Server.MapPath("/Content/images/userfiles/servicebig/") + path);
                        new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/servicethumb/", path);

                        string thumbnail = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                        Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/servicebig/") + path);
                        Bitmap bmp2 = new Bitmap(bmp);
                        using (Bitmap Orgbmp = bmp2)
                        {

                            int sabit = 90;
                            Size Boyut = new Size(210, 125);
                            Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                            ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/servicethumb/") + thumbnail);
                            bmp.Dispose();
                            bmp2.Dispose();
                            Orgbmp.Dispose();
                            GC.Collect();
                        }

                  //      item.SaveAs(Server.MapPath("/Content/images/userfiles/")+item.FileName);
                  //      Random random = new Random();
                  //      int rand = random.Next(1000, 99999999);
                  //      string path = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                  //      new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/", path);

                  //      rand = random.Next(1000, 99999999);
                  //      string thumbnail = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);

                  //     // Image img = Image.FromFile(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                  //      Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                  //  Bitmap bmp2 = new Bitmap(bmp);

                  //using (Bitmap Orgbmp = bmp2)
                  // {

                  //     int sabit = 90;
                  //     Size Boyut = new Size(210, 125);
                  //     Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                  //      ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/")+thumbnail);
                  //     bmp.Dispose();
                  //       bmp2.Dispose();
                  //     Orgbmp.Dispose();
                  //     GC.Collect();
                  //  }

                        Helpers.ImageHelper.WaterMark("/Content/images/userfiles/servicebig/" + path, 60);
                        Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/servicethumb/" + thumbnail);
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.Service;
                        p.ItemId = newmodel.ServiceId;
                        p.Path = "/Content/images/userfiles/servicebig/" + path + ".jpeg"; ;
                        p.Thumbnail = "/Content/images/userfiles/servicethumb/" + thumbnail + ".jpeg";
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = "tr";
                        p.TimeCreated = DateTime.Now;
                        p.Title = newmodel.Name;
                        PhotoManager.Save(p);
                    }
                 }
                ModelState.Clear();
                return View();
            }
            else
                return View();
        }


        public ActionResult EditService()
        {
            FillLanguagesList();

            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Service editrecord = ServiceManager.GetServiceById(nid);
                    var photos = PhotoManager.GetList(5, nid);
                    ViewBag.Photos = photos;
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
        public ActionResult EditService(Service newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage, IEnumerable<HttpPostedFileBase> attachments)
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
                        newmodel.ServiceId = nid;
                        ViewBag.ProcessMessage = ServiceManager.EditService(newmodel);

                        Service editrecord = ServiceManager.GetServiceById(nid);
                        var photos = PhotoManager.GetList(5, nid);
                        ViewBag.Photos = photos;

                        foreach (var item in attachments)
                        {
                            if (item != null && item.ContentLength > 0)
                            {
                                Random random = new Random();
                                int rand = random.Next(1000, 99999999);
                                string path = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                                item.SaveAs(Server.MapPath("/Content/images/userfiles/servicebig/") + path);
                                new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/servicethumb/", path);

                                string thumbnail = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                                Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/servicebig/") + path);
                                Bitmap bmp2 = new Bitmap(bmp);
                                using (Bitmap Orgbmp = bmp2)
                                {

                                    int sabit = 90;
                                    Size Boyut = new Size(210, 125);
                                    Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                                    ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/servicethumb/") + thumbnail);
                                    bmp.Dispose();
                                    bmp2.Dispose();
                                    Orgbmp.Dispose();
                                    GC.Collect();
                                }
                                //item.SaveAs(Server.MapPath("/Content/images/userfiles/") + item.FileName);
                                //Random random = new Random();
                                //int rand = random.Next(1000, 99999999);
                                //string path = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                                //new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/", path);

                                //rand = random.Next(1000, 99999999);
                                //string thumbnail = Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);

                                //// Image img = Image.FromFile(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                                //Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/") + item.FileName);

                                //Bitmap bmp2 = new Bitmap(bmp);

                                //using (Bitmap Orgbmp = bmp2)
                                //{

                                //    int sabit = 90;
                                //    Size Boyut = new Size(210, 125);
                                //    Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                                //    ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/") + thumbnail);
                                //    bmp.Dispose();
                                //    bmp2.Dispose();
                                //    Orgbmp.Dispose();
                                //    GC.Collect();
                                //}

                                Helpers.ImageHelper.WaterMark("/Content/images/userfiles/servicebig/" + path, 60);
                                Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/servicethumb/" + thumbnail);
                                Photo p = new Photo();
                                p.CategoryId = (int)PhotoType.Service;
                                p.ItemId = newmodel.ServiceId;
                                p.Path = "/Content/images/userfiles/servicebig/" + path + ".jpeg"; ;
                                p.Thumbnail = "/Content/images/userfiles/servicethumb/" + thumbnail + ".jpeg";
                                p.Online = true;
                                p.SortOrder = 9999;
                                p.Language = "tr";
                                p.TimeCreated = DateTime.Now;
                                p.Title = newmodel.Name;
                                PhotoManager.Save(p);
                            }
                        }












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

            var groups = ServiceGroupManager.GetServiceGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().ServiceGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "ServiceGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;

            return id;
        }


        public JsonResult EditStatus(int id)
        {
            string NowState;
            bool isupdate = ServiceManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult Delete(int id)
        {
            bool isdelete = ServiceManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = ServiceGroupManager.GetServiceGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "ServiceGroupId", "GroupName"));
            return result;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ServiceManager.SortRecords(idsList);
            return Json(issorted);
        }

        public JsonResult SortPhotos(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = PhotoManager.SortRecords(idsList);
            return Json(issorted);
        }


        public JsonResult DeletePhoto(int id)
        {
            bool isdelete = PhotoManager.Delete(id);
            return Json(isdelete);
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
       
    }
}
