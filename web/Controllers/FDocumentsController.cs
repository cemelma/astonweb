using BLL.DocumentsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FDocumentsController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        //
        // GET: /Dokuman/

        public ActionResult Index()
        {
            var group = DocumentManager.GetDocumentGroupListForFront(lang);
            return View(group);
        }

        public ActionResult DocumentList(int gid)
        {
            var doclist = DocumentManager.GetDocumentListForFront(gid);
            ViewBag.title = DocumentManager.GetDocumentGroupById(gid).GroupName;
            return View(doclist);
        }
    }
}
