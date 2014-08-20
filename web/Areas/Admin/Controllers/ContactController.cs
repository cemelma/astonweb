using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Filters;
using BLL.ContactBL;
using BLL.LanguageBL;
using DAL.Entities;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ContactController : Controller
    {
        //
        // GET: /Admin/Contact/

        public ActionResult Index()
        {
            var contact = ContactManager.GetContact();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Index(Contact record)
        {
             ViewBag.ProcessMessage = ContactManager.EditContact(record);
            return View();
        }

        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }





        public ActionResult Messages()
        {
            var contact = ContactManager.GetListContactHome();
            return View(contact);
        }

        public JsonResult DeleteContactHome(int id)
        {
            bool isdelete = ContactManager.Delete(id);
            return Json(isdelete);
        }

    }
}
