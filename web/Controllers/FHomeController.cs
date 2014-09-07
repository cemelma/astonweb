using System.Linq;
using System.Web.Mvc;
using web.Models;
using BLL.NewsBL;
using BLL.ReferenceBL;
using BLL.DocumentsBL;
using BLL.PhotoBL;
using BLL.SectorGroupBL;
using BLL.ProductBL;
using BLL.ServiceGroupBL;
using BLL.ContactBL;
using System.Collections.Generic;
using DAL.Context;
using DAL.Entities;
using BLL.ServiceBL;
using BLL.SubscriptionBL;
using System.Xml;
using System;

namespace web.Controllers
{
    public class slider
    {
        public string image { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumb { get; set; }
    }

    public class FHomeController : Controller
    {

        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            HomePageWrapperModel model = new HomePageWrapperModel();
            model.photos = PhotoManager.GetListForFront(lang,0);
            model.news = NewsManager.GetNewsListForFront(lang,false);
            model.servicegroups = ServiceManager.GetServiceList();
            model.references = ReferenceManager.GetReferenceListForFront(lang);

            model.prodgroups = ProductManager.GetProductGroupList(lang).Where(x=>x.TopProductId==1).Take(6);
            int[] pgoupids = model.prodgroups.Select(d => d.ProductGroupId).ToArray();
            model.Products = ProductManager.GetProductTopListFrontMainPage(pgoupids).ToList();
            model.servicesphotos = PhotoManager.GetListForFrontServices((int)web.Areas.Admin.Helpers.PhotoType.Service);

            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult GetAddress()
        {
            web.Areas.Admin.Models.VMProductGroupModel grouplist = new web.Areas.Admin.Models.VMProductGroupModel();
            List<ProductGroup> pgList = ProductManager.GetProductGroupList(lang);
            ViewData["pgList"] = pgList;

            Contact cont=ContactManager.GetContact(lang);
            ViewBag.Services = ServiceManager.GetServiceListForFront(lang).Take(3);
            return PartialView("Partial/_footeraddress",cont);
        }

        [ChildActionOnly]
        public PartialViewResult GetTopMenu()
        {
            VMProductGroupModel grouplist = new VMProductGroupModel();
            grouplist.ProductGroup = ProductManager.GetProductGroupList(lang);

            int[] ids = grouplist.ProductGroup.Select(x => x.ProductGroupId).ToArray();
            grouplist.Products = ProductManager.GetProductList(ids);

            Kur();

            ViewBag.Services = ServiceManager.GetServiceListForFront(lang);
            return PartialView("Partial/_topmenu", grouplist);
        }

        public void Kur()
        {
            XmlTextReader oku = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml");
            XmlDocument dok = new XmlDocument();
            dok.Load(oku);
            XmlNode xdollar = dok.SelectSingleNode("/Tarih_Date/Currency[CurrencyName='US DOLLAR']");
            XmlNode xeuro = dok.SelectSingleNode("/Tarih_Date/Currency[CurrencyName='EURO']");
            XmlNode xsterling = dok.SelectSingleNode("/Tarih_Date/Currency[CurrencyName='POUND STERLING']");

            double dolar = double.Parse(xdollar.ChildNodes[4].InnerText);
            double euro = double.Parse(xeuro.ChildNodes[4].InnerText);
            double sterling = double.Parse(xsterling.ChildNodes[4].InnerText);

            ViewData["dolar"] = dolar;
            ViewData["euro"] = euro;
            ViewData["sterling"] = sterling;

            //ya da

            Func<XmlNode, double> fnc = delegate(XmlNode x)
            {
                return double.Parse(x.ChildNodes[4].InnerText);
            };
            ViewData["dolar"] =  String.Format("{0:0.00}", fnc(xdollar));
            ViewData["euro"] = fnc(xeuro);
            //ViewData["sterling"] = fnc(xsterling);
        }


        [ChildActionOnly]
        public PartialViewResult GetProductsMenu()
        {
            VMProductGroupModel grouplist = new VMProductGroupModel();
            grouplist.ProductGroup = ProductManager.GetProductGroupList(lang);

            int[] ids = grouplist.ProductGroup.Select(x => x.ProductGroupId).ToArray();
            grouplist.Products = ProductManager.GetProductList(ids);

            ViewBag.Services = ServiceManager.GetServiceListForFront(lang);
            return PartialView("Partial/_productsmenu", grouplist);
        }



        public JsonResult GetImages()
        {
            var photos = PhotoManager.GetListForFront(lang, 0);
            
            var slider = new List<slider>();    

            foreach (var item in photos)
            {
                slider s = new slider();
                s.image = item.Path;
                s.title = item.Title;
                s.url = item.Link;
                s.thumb = "";
                slider.Add(s);
            }

            return Json(slider
                ,
                JsonRequestBehavior.AllowGet
                );
        }

        public ActionResult ChangeCulture(string lang,string returnUrl)
        {
            Session["culture"] = lang;
            if(lang=="en") return Redirect("/en/homepage");
            else if (lang == "ar")  return Redirect("/ar/home");
            else return Redirect("/tr/anasayfa");

        }

        public JsonResult Subscription(string mail)
        {
            string proc = SubscriptionManager.AddSubscription(mail);
            return Json(proc, JsonRequestBehavior.AllowGet);
        }

    }
}
