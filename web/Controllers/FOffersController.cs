using web.Models;
using BLL.ProductBL;
using BLL.TeklifBL;
using DAL.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FOffersController : Controller
    {
        //
        // GET: /FOffers/

        [HttpGet]
        public ActionResult Index()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
                var products = ProductManager.GetProductByIds(values);
                OfferWrapperModel modelbind = new OfferWrapperModel(products, null, null);
                return View(modelbind);
            }
            else
            {
                return View(new OfferWrapperModel(new List<Product>(), null, null));
            }
        }

        [HttpPost]
        public ActionResult Index(Teklif teklif, TeklifUrun[] teklifurun)
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);

                bool result = TeklifManager.AddTeklif(teklif, teklifurun, values);

                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                TempData["sent"] = true;
                return RedirectToAction("Index");
            }
            return View(new OfferWrapperModel(new List<Product>(), null, null));
        }

        [HttpPost]
        public string GetOfferCount()
        {
            if (!this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                return "0";
            }
            else
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);

                return values.Count().ToString();
            }
        }

        [HttpPost]
        public string DeleteItem(int OfferID)
        {
            if (!this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                return "0";
            }
            else
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);

                return values.Count().ToString();
            }
        }

        public ActionResult _emptyOfferList()
        {
            return View();
        }

        public ActionResult _sentOfferList()
        {
            return View();
        }

    }
}
