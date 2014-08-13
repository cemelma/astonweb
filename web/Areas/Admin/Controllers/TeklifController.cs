using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Models;
using BLL.TeklifBL;
using DAL.Entities;
using System.IO;
using web.Areas.Admin.Filters;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class TeklifController : Controller
    {
        //
        // GET: /Admin/Teklif/
        public ActionResult Delete(int id, string type)
        {
            TeklifManager.DeleteTeklif(id);
            return RedirectToAction("Index", new { @type = type });
        }

        public bool DeleteTeklifUrun(int id)
        {
            bool retval = TeklifManager.DeleteTeklifUrun(id);
            return retval;
        }

        public ActionResult Index()
        {
            var list = TeklifManager.GetList();
            if (RouteData.Values["type"] != null)
            {
                ViewBag.Tur = "0";
                string type = RouteData.Values["type"].ToString();
                if (type == "tumteklifler")
                {
                    ViewBag.Header = "TÜM TEKLİFLER";
                    ViewBag.Tur = "1";
                    list = TeklifManager.GetList();
                }
                else  if (type == "onaybekleyenler")
                {
                    ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
                    list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
                }
                else if (type == "onaylananlar")
                {
                    ViewBag.Header = "ONAYLANAN TEKLİFLER";
                    list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylandi));
                }
                else if (type == "iptaledilenler")
                {
                    ViewBag.Header = "İPTAL EDİLEN TEKLİFLER";
                    list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Iptal));
                }

                return View(list);
            }
            else
            {
                ViewBag.Header = "YENİ GELEN TEKLİFLER / ONAY BEKLEYEN TEKLİFLER";
                list = TeklifManager.GetList(Convert.ToInt32(EnumTeklifTip.Onaylanmadi));
                return View(list);
            }
        }

        public ActionResult AllList()
        {
            var list = TeklifManager.GetList();
            return View(list);
        }

        [HttpPost]
        public ActionResult Details(Teklif teklif, string teklifid, string txtcevaptarihi, string TeklifDurum)
        {
            if (ModelState.IsValid)
            {
                teklif.TeklifId = Convert.ToInt32(teklifid);
                teklif.CevapTarihi = Convert.ToDateTime(txtcevaptarihi);
                teklif.Durum = Convert.ToInt32(TeklifDurum);
                ViewBag.ProcessMessage = TeklifManager.UpdateTeklif(teklif);
                return RedirectToAction("Details", new { id = teklifid });
            }

            return View();
        }

        public FileStreamResult Proforma(string tekid)
        {
            MemoryStream pdf = TeklifManager.ProformaOnizle(tekid);
            
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", "1"));
            Response.BinaryWrite(pdf.ToArray());

            return new FileStreamResult(pdf, "application/pdf");    
            //return File(pdf, "application/pdf", "DownloadName.pdf");
            //return RedirectToAction("Details", new { id = tekid });
        }

        public ActionResult ProformaGonder(string tekid2)
        {
            ViewBag.ProcessMessage = TeklifManager.ProformaGonder(tekid2);
            return RedirectToAction("Details", new { id = tekid2 });
        }

        public ActionResult Details(int id)
        {
            int teklifid = 0;
            ViewBag.cevaptrh = "";
            if (RouteData.Values["id"] != null)
            {
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out teklifid);
                if (isnumber)
                {
                    var teklif = TeklifManager.GetTeklifById(teklifid);
                    if (teklif.CevapTarihi == null)
                    {
                        ViewBag.cevaptrh = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        ViewBag.cevaptrh = ((DateTime)teklif.CevapTarihi).ToShortDateString();
                    }

                    var TekDurum = new List<SelectListItem>{ 
                                           new SelectListItem { Value = "0", Text = "Onaylandı", Selected = false },
                                           new SelectListItem { Value = "1", Text = "Onaylanmadı", Selected = false },
                                           new SelectListItem { Value = "2", Text = "İptal Edildi", Selected = false }
                                 };
                    TekDurum.FirstOrDefault(d => d.Value == teklif.Durum.ToString()).Selected = true;
                    ViewBag.TeklifDurum = TekDurum;
                    var urunler = TeklifManager.GetUrunList(teklifid);
                    TeklifModel modelbind = new TeklifModel(teklif, urunler);
                    return View(modelbind);
                }
                else
                    return View();

            }
            else
                return View();
        }


        public JsonResult UpdateRecord(int id,string fiyat,int adet, string donanim,int teklifid)
        {
            string [] vals=TeklifManager.HesaplamaYap(id,fiyat,adet, donanim,teklifid);
            return Json(vals);

        }

    }
}

