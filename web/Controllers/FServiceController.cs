using web.Models;
using BLL.ProductBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.ServiceBL;
using BLL.ServiceGroupBL;
using DAL.Entities;
using DAL.Context;

namespace web.Controllers
{
    public class FServiceController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Service/

        public ActionResult Index()
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == 6 && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }

            List<Service> services = new List<Service>();
            services = ServiceManager.GetServiceListForFront(lang);
            var ourservices = ServiceManager.GetOurServices(lang);

            ServiceWrapperModel swm = new ServiceWrapperModel(null, services, null, null, ourservices);
            return View(swm);
        }

        public ActionResult Hizmetlerimiz()
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == 6 && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }

            OurServices services = new OurServices();
            services = ServiceManager.GetOurServices(lang);
            ServiceWrapperModel swm = new ServiceWrapperModel(null, null, null, null, services);
            return View(swm);
        }
    }
}
