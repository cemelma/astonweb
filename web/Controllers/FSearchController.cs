using BLL.SearchBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FSearchController : Controller
    {
        //
        // GET: /Shared/

        public ActionResult _search()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string SearchText)
        {
            SearchText = SearchText.TrimEnd().TrimStart();
            var result = SearchManager.Search(SearchText);
            ViewBag.SearchTextLower = SearchText.ToLower();
            ViewBag.SearchTextUpper = SearchText.ToUpper();
            return View(result);
        }
    }
}
