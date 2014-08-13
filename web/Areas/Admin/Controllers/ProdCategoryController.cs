using BLL.LanguageBL;
using BLL.ProdCategoryBL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace web.Areas.Admin.Controllers
{
    public class ProdCategoryController : Controller
    {
        //
        // GET: /Admin/ProdCategory/

        public ActionResult Add()
        {
            ViewBag.SubCategory = false;
            FillLanguagesList();
            
            //List<ProdCategory> categories = new List<ProdCategory>();
            
            //ProdCategory cat = new ProdCategory();
            //cat.Name = "";
            //cat.ParentId = 0;
            //categories.Add(cat);

            //var cateogoryList = new SelectList(categories, "ParentId", "Name", "0");
            //ViewBag.CategoryList = cateogoryList;
            
            return View();
        }

        [HttpPost]
        public ActionResult Add(string ddlLanguage, int ddlParentCategory, string txtName)
        {
            ViewBag.SubCategory = false;
            ProdCategory category = new ProdCategory();
            category.Lang = ddlLanguage;
            category.ParentId = ddlParentCategory;
            category.Name = txtName;
            ViewBag.ProcessMessage = ProdCategoryManager.Add(category);
            FillLanguagesList();
            return View();
        }

        [HttpPost]
        public ActionResult GetTreeNodeJSON()
        {
            string retval = "";

            using (DAL.Context.MainContext db = new DAL.Context.MainContext())
            {
                try
                {
                    retval = db.ProdCategory.Select(
                        x => new
                        {
                            id = x.ProdCategoryId,
                            text = x.Name
                        }
                        ).ToJSON();
                }
                catch (Exception ex)
                {
                    
                }
            }

            string result = retval;
            return Content(result, "application/json");
        }

        private string GetCategoryTree()
        {
            //string lang = "";
            //if (RouteData.Values["lang"] == null) lang = "tr"; else lang = RouteData.Values["lang"].ToString();

            using (DAL.Context.MainContext db = new DAL.Context.MainContext())
            {
                try
                {
                    return db.ProdCategory.Select(
                        x => new
                        {
                            ProdCategoryId = x.ProdCategoryId,
                            Name = x.Name
                        }
                        ).ToJSON();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            //ProdCategoryManager.GetMainCategories(lang);
        }
        
        

        private string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else 
                lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }
    }

    public static class ObjectExtensions
    {
        public static string ToJSON(this Object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
