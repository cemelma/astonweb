using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Entities;
using DAL.Context;
using System.Web.Script.Serialization;

namespace web.Areas.Admin.Controllers
{
    public class CountryController : Controller
    {
        private MainContext db = new MainContext();

        //
        // GET: /Admin/Country/

        public ActionResult Index()
        {
            return View(db.Country.OrderBy(d=>d.SortNumber).ToList());
        }

        //
        // GET: /Admin/Country/Details/5

        public ActionResult Details(int id = 0)
        {
            Country country = db.Country.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // GET: /Admin/Country/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Country/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                country.SortNumber = 9999;
                db.Country.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        //
        // GET: /Admin/Country/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Country country = db.Country.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        //
        // POST: /Admin/Country/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        //
        // POST: /Admin/Country/Delete/5

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            Country country = db.Country.Find(id);
            try
            {
                db.Country.Remove(country);
                db.SaveChanges();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            try
            {
                int row = 0;
                foreach (string id in idsList)
                {
                    int mid = Convert.ToInt32(id);
                    Country sortingrecord = db.Country.SingleOrDefault(d => d.Id == mid);
                    sortingrecord.SortNumber = Convert.ToInt32(row);
                    db.SaveChanges();
                    row++;
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}