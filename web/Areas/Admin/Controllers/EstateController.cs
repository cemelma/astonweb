using BLL.EstateBL;
using BLL.LanguageBL;
using BLL.PhotoBL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Filters;
using web.Areas.Admin.Helpers;

namespace web.Areas.Admin.Controllers
{
    public class EstateController : Controller
    {
        //
        // GET: /Admin/State/

        public ActionResult Index()
        {
            string lang = FillLanguagesList();
            var estates =EstateManager.GetEstateList(lang);
            

            return View(estates);
        }

        public ActionResult Add()
        {
            var languages = LanguageManager.GetLanguages();
            var countries = EstateManager.GetCountryList();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            var countrylist = new SelectList(countries, "Id", "Name");
            ViewBag.Country = countrylist;
            //ImageHelperNew.DestroyImageCashAndSession(577, 296);
            ImageHelperNew.DestroyImageCashAndSession(750, 483);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Estate record, HttpPostedFileBase uploadfile, IEnumerable<HttpPostedFileBase> attachments,HttpPostedFileBase fileDosya)
        {
            var languages = LanguageManager.GetLanguages();
            //string lang = "";
            //if (RouteData.Values["lang"] == null)
            //    lang = "tr";
            //else lang = RouteData.Values["lang"].ToString();
            //lang =
            var countries = EstateManager.GetCountryList();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            var countrylist = new SelectList(countries, "Id", "Name");
            ViewBag.Country = countrylist;
            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    record.Photo = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
                else
                {
                    record.Photo = "/Content/images/front/noimage.jpeg";
                }

                if (fileDosya != null)
                {
                    fileDosya.SaveAs(Server.MapPath("/Content/images/estates/")+fileDosya.FileName);
                    record.EmlakDosyasi = "/Content/images/estates/" + fileDosya.FileName;
                }

                ViewBag.ProcessMessage = EstateManager.AddEstate(record);
                Session.Remove("UploadType");
                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(item.FileName));
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.Estate;
                        p.ItemId = record.Id;
                        p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Online = true;
                        p.SortOrder = 9999;
                       
                        p.Language = record.Language;
                        p.TimeCreated = DateTime.Now;
                        p.Title = record.Header;
                        PhotoManager.Save(p);
                    }
                }

                ModelState.Clear();
                return View();
            }
            else
                return View();
        }

        public ActionResult Edit()
        {
            ImageHelperNew.DestroyImageCashAndSession(750, 483, 250, 161);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            var countries = EstateManager.GetCountryList();
 
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Estate record = EstateManager.GetEstateById(nid);

                    var countrylist = new SelectList(countries, "Id", "Name",record.CountryId);
                    ViewBag.Country = countrylist;

                    var towns = EstateManager.GetTownList(record.CountryId);
                    var townList = new SelectList(towns, "Id", "Name",record.TownId);
                    ViewBag.Town = townList;

                    var districts = EstateManager.GetDistrictList(record.TownId);
                    var districtsList = new SelectList(districts, "Id", "Name",record.DistrictId);
                    ViewBag.District = districtsList;

                    return View(record);
                }
                else
                    return View();
            }
            else
                return View();
 
        }

        [HttpPost]
        [ValidateInput(false)]
        [SaveImageAltTags]
        public ActionResult Edit(IEnumerable<HttpPostedFileBase> attachments, Estate record, HttpPostedFileBase uploadfile,HttpPostedFileBase fileDosya)
        {
            var languages = LanguageManager.GetLanguages();
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                //if (uploadfile != null && uploadfile.ContentLength > 0)
                //{
                //    Random random = new Random();
                //    int rand = random.Next(1000, 99999999);
                //    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/Photos/", Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                //    record.Photo = "/Content/images/Photos/" + Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                //}

                if (Session["ModifiedImageId"] != null)
                {
                    record.Photo = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {

                        if (fileDosya != null)
                        {
                            fileDosya.SaveAs(Server.MapPath("/Content/images/estates/") + fileDosya.FileName);
                            record.EmlakDosyasi = "/Content/images/estates/" + fileDosya.FileName;
                        }


                        record.Id = nid;
                        ViewBag.ProcessMessage = EstateManager.EditEstate(record);
                        //return View(record);
                        foreach (var item in attachments)
                        {
                            if (item != null && item.ContentLength > 0)
                            {
                                Random random = new Random();
                                int rand = random.Next(1000, 99999999);
                                new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(item.FileName));
                                Photo p = new Photo();
                                p.CategoryId = (int)PhotoType.Estate;
                                p.ItemId = record.Id;
                                p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(item.FileName);
                                p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(item.FileName);
                                p.Online = true;
                                p.SortOrder = 9999;
                                p.Language = lang;
                                p.TimeCreated = DateTime.Now;
                                p.Title = "Emlak";
                                PhotoManager.Save(p);
                            }
                        }

                        return RedirectToAction("Index", "Estate");
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(record);
                    }
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

        public JsonResult GetTowns(int id)
        {
            //return Json(EstateManager.GetTownList(id));
            var towns = EstateManager.GetTownList(id);
            return Json(new SelectList(towns, "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistricts(int id)
        {
            //return Json(EstateManager.GetTownList(id));
            var towns = EstateManager.GetDistrictList(id);
            return Json(new SelectList(towns, "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult DeleteEstate(int id)
        {
            bool isdelete = EstateManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }


    }
}
