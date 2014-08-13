using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Filters;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.ProductBL;
//using BLL.ProductBL;
using DAL.Entities;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ProductSubGroupController : Controller
    {
        public ActionResult Index()
        {
            string id = FillLanguagesListForList(false);

            int groupid = Convert.ToInt32(id);
            var grouplist = ProductManager.GetProductSubGroupList("", groupid);
            if (TempData["message"] != null)
	        {
                ViewBag.ProcessMessage = TempData["message"]; 
	        } 
            return View(grouplist);
        }

        string FillLanguagesListForList(bool saved)
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            
            if (saved) lang = "tr";
            
            var languages = LanguageManager.GetLanguages();
            
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = ProductManager.GetProductGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().ProductGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "ProductGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;

            return id;
        }

        [HttpPost]
        public ActionResult Index(string drplanguage, string txtname, HttpPostedFileBase uploadfile, string drpgroup)
        {
            string id = FillLanguagesListForList(true);
            if (ModelState.IsValid)
            {
                ProductSubGroup model = new ProductSubGroup();
                model.GroupName = txtname;
                model.Language = drplanguage;
                model.ProductGroupId = Convert.ToInt32(drpgroup);
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/productcategory/", Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    model.GroupImage = "/Content/images/productcategory/" + Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    model.GroupImage = "/Content/images/front/noimage.jpeg";
                }

                model.PageSlug = Utility.SetPagePlug(txtname);
                ViewBag.ProcessMessage = ProductManager.AddProductSubGroup(model);

                int groupid = Convert.ToInt32(id);
                var grouplist = ProductManager.GetProductSubGroupList("", groupid);
                TempData["message"] = ViewBag.ProcessMessage;
                return RedirectToAction("Index");
                
            }
            return View();
        }


        public ActionResult EdtiGroup() 
        {
            var lang = FillLanguagesList();

            var grouplist = ProductManager.GetProductGroupList(lang);
            var grp = new SelectList(grouplist, "ProductGroupId", "GroupName");
            ViewBag.MainGroup = grp;
                if(RouteData.Values["id"]!=null)
                {
                    int nid=0;
                    bool isnumber=int.TryParse(RouteData.Values["id"].ToString(),out nid);
                    if (isnumber)
                    {
                        ProductSubGroup editnews = ProductManager.GetSubGroupById(nid);
                        return View(editnews);
                    }
                    else
                        return View();
                }
                else
                    return View();
         }
    
        [HttpPost]
        public ActionResult EdtiGroup(ProductSubGroup model, HttpPostedFileBase uploadfile)
        {
            var lang = FillLanguagesList();

            var grouplist = ProductManager.GetProductGroupList(lang);
            var grp = new SelectList(grouplist, "ProductGroupId", "GroupName");
            ViewBag.MainGroup = grp;
            if (ModelState.IsValid)
            {
                //ProductSubGroup model = new ProductSubGroup();
               // model.GroupName = txtname;
                //model.Language = drplanguage;
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/productcategory/", Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    model.GroupImage = "/Content/images/productcategory/" + Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        model.PageSlug = Utility.SetPagePlug(model.GroupName);
                        model.ProductSubGroupId = nid;
                        ViewBag.ProcessMessage = ProductManager.EditProductSubGroup(model);
                        return View(model);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(model);
                    }
                }

            }
            return View();
        }




        public void UpdateRecord(int id, string name)
        {
            string clearname = name.Replace("%47", "\'");
            string pageslug = Utility.SetPagePlug(clearname);
            ProductManager.EditProductSubGroup(id, clearname, pageslug);
        }


        public JsonResult GroupEditStatus(int id)
        {
            return Json(ProductManager.UpdateSubGroupStatus(id));
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(ProductManager.DeleteSubGroup(id));
        }


        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ProductManager.SortSubRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
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
    }
}
