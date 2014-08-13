using BLL.EstateBL;
using BLL.MailBL;
using BLL.PhotoBL;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{

    public class FEstatesController : Controller
    {
        //
        // GET: /FEstates/
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            if (RouteData.Values["id"] != null)
            {
                int estateId = Convert.ToInt32(RouteData.Values["id"]);
                Estate model = EstateManager.GetEstateById(estateId);
                
                var photos = PhotoManager.GetList((int)web.Areas.Admin.Helpers.PhotoType.Estate, estateId);
                EstateWrapperModel m = new EstateWrapperModel(photos, model);
                
                ViewBag.Lang = lang;
                return View(m);
            }
            else
            {
                return View();
            }
        }

        public ActionResult PopularEstates()
        {
            using (MainContext db = new MainContext())
            {
                Tags stag = db.Tags.Where(x => x.PageId == 14 && x.Lang == lang).FirstOrDefault();

                if (stag != null)
                {
                    ViewBag.Title = stag.Title;
                    ViewBag.Description = stag.Description;
                    ViewBag.Keywords = stag.Keyword;
                }
                //var list = db.Estate.Where(d => d.Popular == true).ToList();
                var list = db.Estate.Include("Country").Include("Town").Include("District")
                                .Where(d => d.Language == lang && d.Popular == true)
                                .OrderByDescending(d=>d.Id)
                                .ToList();

                return View(list);
            }
        }

        public ActionResult SearchEstates(int TypeId = 3)
        {
            using (MainContext db = new MainContext())
            {
                Tags stag = db.Tags.Where(x => x.PageId == 15 && x.Lang == lang).FirstOrDefault();

                if (stag != null)
                {
                    ViewBag.Title = stag.Title;
                    ViewBag.Description = stag.Description;
                    ViewBag.Keywords = stag.Keyword;
                }


                var list = db.Estate.Include("Country").Include("Town").Include("District").Where(d => d.Language == lang).ToList();
                if (TypeId == 3)
                {
                    ViewBag.result = null;
                }
                else
                {
                    ViewBag.result = TypeId;
                    list = list.Where(d=>d.TypeId == TypeId).ToList();
                }
                ViewBag.BriefResultante = false;
                AdvancedSearchModel model = new AdvancedSearchModel(list, new SearchEstateModel());
                IEnumerable<SelectListItem> basetypes = db.Country.ToList().Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name });
                ViewData["search.sehir"] =  basetypes;
                return View(model);
            }
        }

        public JsonResult GetTowns(int? listid)
        {

            //todo: preparing the data source filter
            using (MainContext db = new MainContext())
            {
                if (listid == null)
                {
                    listid = 0;
                }
                var towns = db.Town.ToList().Where(d => d.CountryId == listid).Select(d=> new { d.Id, d.Name } );

                return Json(towns, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDistricts(int? listid)
        {

            //todo: preparing the data source filter
            using (MainContext db = new MainContext())
            {
                if (listid == null)
                {
                    listid = 0;
                }
                var list = db.District.ToList().Where(d => d.TownId == listid).Select(d => new { d.Id, d.Name });

                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SearchEstates(AdvancedSearchModel model)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Estate.Include("Country").Include("Town").Include("District").Where(d=>d.Language == lang).ToList();
                if (!string.IsNullOrEmpty(model.search.referansno))
                {
                    try
                    {
                        //int refno = Convert.ToInt32(model.search.referansno);
                        list = list.Where(d => d.ReferenceNo == model.search.referansno).ToList();
                    }
                    catch (Exception)
                    {
                    }
                }
                if (!string.IsNullOrEmpty(model.search.keyword))
                {
                    list = list.Where(d => d.Header.Contains(model.search.keyword)).ToList();
                }
                if (model.search.emlaktipi != 3)
                {
                    list = list.Where(d => d.TypeId == model.search.emlaktipi).ToList();
                    ViewBag.emlaktipi = model.search.emlaktipi;
                }
                if (model.search.islemtipi != 2)
                {
                    list = list.Where(d => d.TransactionId == model.search.islemtipi).ToList();
                    ViewBag.islemtipi = model.search.islemtipi;
                }

                if (!string.IsNullOrEmpty(model.search.sehir))
                {
                    int sehir = Convert.ToInt32(model.search.sehir);
                    list = list.Where(d => d.CountryId == sehir).ToList();
                }

                if (model.search.ilce != 0)
                {
                    list = list.Where(d => d.TownId == model.search.ilce).ToList();
                }

                if (model.search.semt != 0)
                {
                    list = list.Where(d => d.DistrictId == model.search.semt).ToList();
                }

                //if (!string.IsNullOrEmpty(model.fiyataraligialt))
                //{
                //    list = list.Where(d => d.Price >= model.fiyataraligialt).ToList();
                //}

                if (!string.IsNullOrEmpty(model.search.metrekarealt))
                {
                    int metrekare = Convert.ToInt32(model.search.metrekarealt);
                    list = list.Where(d => Convert.ToInt32(d.RoomNumber) > metrekare).ToList();
                }

                if (!string.IsNullOrEmpty(model.search.metrekareust))
                {
                    int metrekare = Convert.ToInt32(model.search.metrekareust);
                    list = list.Where(d => Convert.ToInt32(d.RoomNumber) < metrekare).ToList();
                }

                if (!string.IsNullOrEmpty(model.search.odasayisialt))
                {
                    int roomcount = Convert.ToInt32(model.search.odasayisialt);
                    list = list.Where(d => Convert.ToInt32(d.RoomNumber) > roomcount).ToList();
                }

                if (!string.IsNullOrEmpty(model.search.odasayisiust))
                {
                    int roomcount = Convert.ToInt32(model.search.odasayisiust);
                    list = list.Where(d => Convert.ToInt32(d.RoomNumber) < roomcount).ToList();
                }

                if (!string.IsNullOrEmpty(model.search.binayasialt))
                {
                    int binayasialt = Convert.ToInt32(model.search.binayasialt);
                    list = list.Where(d => int.Parse(d.Age) >= binayasialt).ToList();
                }

                if (!string.IsNullOrEmpty(model.search.binayasiust))
                {
                    int binayasiust = Convert.ToInt32(model.search.binayasiust);
                    list = list.Where(d => int.Parse(d.Age) < binayasiust).ToList();
                }

                if (!string.IsNullOrEmpty(model.search.fiyataraligialt))
                {
                    int fiyat = Convert.ToInt32(model.search.fiyataraligialt);
                    list = list.Where(d => d.Price > fiyat).ToList();
                }

                if (!string.IsNullOrEmpty(model.search.fiyataraligiust))
                {
                    int fiyat = Convert.ToInt32(model.search.fiyataraligiust);
                    list = list.Where(d => d.Price < fiyat).ToList();
                }
                ViewBag.result = model.search.emlaktipi;
                ViewBag.BriefResultante = false;
                ViewBag.Lang = lang;

                AdvancedSearchModel searchmodel = new AdvancedSearchModel(list, model.search);
                ViewBag.BriefResultante = true;
                
                IEnumerable<SelectListItem> basetypes = db.Country.ToList().Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name });
                List<SelectListItem> ilce = new List<SelectListItem>();
                ilce.Add(new SelectListItem { Value = "", Text = "" });
                ViewData["search.sehir"] = basetypes;
                ViewData["search.ilce"] = ilce;

                return View(searchmodel);
            }
        }

        public string SendMail(string dialogName, string contactSurnameDialog, string contactNameFriend, string contactSurFriend, string contactEmailDialog, string contactEmailFriend, string contactMessagefriend, string ilanadi,string ilanUrl)
        {
            //try
            //{
                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(0);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = mset.Security;//burası düzeltilecek
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    var mail = new MailMessage();
                    mail.From = new MailAddress(contactEmailDialog);
                    mail.To.Add(contactEmailFriend);
                    if(lang=="tr")
                    {
                        mail.Subject = "Venueİstanbul İlan Tavsiyesi";
                        string maiilContent = "İLAN ADI: " + ilanadi;
                        maiilContent += "<br/> İLAN LİNKİ :" + ilanUrl;
                        maiilContent += "<br/><br/>" + contactMessagefriend;
                        mail.Body = maiilContent;
                    }
                    else
                    {
                        mail.Subject = "Venueİstanbul Advice Annoucement ";
                        string maiilContent = "Annoucement Name: " + ilanadi;
                        maiilContent += "<br/> Annoucement Url :" + ilanUrl;
                        maiilContent += "<br/><br/>" + contactMessagefriend;
                        mail.Body = maiilContent;
                    }
                    mail.IsBodyHtml = true;

                   
                   
                  
                    if (mail.To.Count > 0) client.Send(mail);
                }
                TempData["sent"] = "true";
                if (lang == "tr") return "Mail başarıyla Gönderildi";
                else return "Mail has sendt succesfully";
            //}
            //catch (Exception ex)
            //{
            //    if (lang == "tr") return "Hata Oluştu";
            //    else return "An error has been occured";
            //}
        }
    }

    public class SearchEstateModel
    {
        public string referansno { get; set; }
        public string keyword { get; set; }
        public int islemtipi { get; set; }
        public int emlaktipi { get; set; }
        public string sehir { get; set; }
        public int ilce { get; set; }
        public int semt { get; set; }
        public string fiyataraligialt { get; set; }
        public string fiyataraligiust { get; set; }
        public string parabirimi { get; set; }
        public string metrekarealt { get; set; }
        public string metrekareust { get; set; }
        public string odasayisialt { get; set; }
        public string odasayisiust { get; set; }
        public string binayasialt { get; set; }
        public string binayasiust { get; set; }
        public string emlakdanismani { get; set; }

    }

}
