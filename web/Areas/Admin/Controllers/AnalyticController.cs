using BLL.AccountBL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Areas.Admin.Controllers
{
    public class AnalyticController : Controller
    {
        //
        // GET: /Admin/Analytic/

        public ActionResult Index()
        {
            Analytic anl = AnalyticManager.GetAnalytic();
            if (anl != null)
                return View(anl);
            else
                return View();
        }
        [ValidateInput(false)] 
        [HttpPost]
        public ActionResult Index(Analytic record)
        {
            ViewBag.Process=AnalyticManager.AddAnalytic(record.Code);
            return View(record);
        }

    }
}
