using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Filters;
using web.Areas.Admin.Helpers;
using BLL.DocumentsBL;
using BLL.LanguageBL;
using DAL.Entities;
using System.Web.Script.Serialization;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class DocumentsController : Controller
    {
        //
        // GET: /Admin/Documents/

        public ActionResult Index()
        {
            string id=FillLanguagesListForDocumentList();

            int groupid = Convert.ToInt32(id);

            var list = DocumentManager.GetDocumentList(groupid);
            return View(list);
        }

        public ActionResult AddDocument()
        {
            FillLanguagesList();
            return View();
        }

        [HttpPost]
        public ActionResult AddDocument(Document model,HttpPostedFileBase uploadfile)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadfile.SaveAs(Server.MapPath("/Content/images/documents/"+ Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName)));
                    model.DocumentFile = "/Content/images/documents/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    model.DocumentFile = "Dosya Yok";
                }
                ModelState.Clear();
                ViewBag.ProcessMessage = DocumentManager.AddDocument(model);
            }
            return View();
        }


        public ActionResult EditDocument()
        {
            
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Document record = DocumentManager.GetDocumentById(nid);
                    var languages = LanguageManager.GetLanguages();
                    var list = new SelectList(languages, "Culture", "Language", record.Language);
                    ViewBag.LanguageList = list;
                    var groups = DocumentManager.GetDocumentGroupList(record.Language);
                    var grouplist = new SelectList(groups, "DocumentGroupId", "GroupName", record.DocumentGroupId);
                    ViewBag.GroupList = grouplist;
                    return View(record);
                }
                else
                    return View();
            }
            else
                return View();

        }



        [HttpPost]
        public ActionResult EditDocument(Document model, HttpPostedFileBase uploadfile)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {

                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadfile.SaveAs(Server.MapPath("/Content/images/documents/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName)));
                    model.DocumentFile = "/Content/images/documents/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }


                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        model.DocumentId = nid;
                        ViewBag.ProcessMessage = DocumentManager.EditDocument(model);
                        return View(model);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(model);
                    }
                }
                else return View();

              
            }
            else
                return View();
        }





        public JsonResult EditStatus(int id)
        {
            return Json(DocumentManager.UpdateStatus(id));
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(DocumentManager.DeleteDocument(id));
        }


        string FillLanguagesListForDocumentList()
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = DocumentManager.GetDocumentGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count!=0)
                    id = groups.First().DocumentGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "DocumentGroupId", "GroupName",id);
            ViewBag.GroupList = grouplist;

            return id;
        }


        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            //var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = DocumentManager.GetDocumentGroupList(lang);
            var grouplist = new SelectList(groups, "DocumentGroupId", "GroupName");
            ViewBag.GroupList = grouplist;

            return lang;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = DocumentManager.GetDocumentGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "DocumentGroupId", "GroupName"));
            return result;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = DocumentManager.SortDocuments(idsList);
            return Json(issorted);
        }
        public class JsonList
        {
            public string[] list { get; set; }
        }
    }
}
