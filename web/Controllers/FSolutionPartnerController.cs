using BLL.SolutionPartnerBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FSolutionPartnerController : Controller
    {
        //
        // GET: /FSolutionPartner/

        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            var list = SolutionPartnerManager.GetSolutionPartnerListForFront(lang);
            return View(list);
        }
    }
}
