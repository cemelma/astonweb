using BLL.ContactBL;
using BLL.InstituionalBL;
using BLL.ProductBL;
using BLL.SectorGroupBL;
using BLL.ServiceGroupBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FSharedController : Controller
    {
        //
        // GET: /FShared/
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public PartialViewResult topproducts()
        {
            var prodgroups = ProductManager.GetProductGroupListForFront(lang).Take(16);
            return PartialView("_topproducts", prodgroups);
        }

        public PartialViewResult topsectors()
        {
            var sectors = SectorGroupManager.GetSectorGroupListForFront(lang).Take(16);
            return PartialView("_topsectors", sectors);
        }

        public PartialViewResult topservices()
        {
            var services = ServiceGroupManager.GetServiceGroupListForFront(0, lang).Take(16);
            return PartialView("_topservices", services);
        }

        public PartialViewResult footer()
        {
            var contact = ContactManager.GetContact();
            return PartialView("_footer", contact);
        }

        public PartialViewResult main2left()
        {
            var pages = PageManager.List(lang);
            return PartialView("_main2left", pages);
        }

        public PartialViewResult topaboutus()
        {
            var pages = PageManager.List(lang);
            return PartialView("_topaboutus", pages);
        }
    }
}
