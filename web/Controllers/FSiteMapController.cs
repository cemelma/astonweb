using BLL.ProductBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FSiteMapController : Controller
    {
        public ActionResult Index()
        {
            web.Areas.Admin.Models.VMProductGroupModel grouplist = new web.Areas.Admin.Models.VMProductGroupModel();
            grouplist.ProductGroup = ProductManager.GetProductGroupList("tr");

            var prodgroups = ProductManager.GetProductGroupList("tr").Where(x => x.TopProductId == 1).Take(6);
            int[] pgoupids = prodgroups.Select(d => d.ProductGroupId).ToArray();
            grouplist.Products = ProductManager.GetProductTopListFront(pgoupids).ToList();

            return View(grouplist);
        }

    }
}
