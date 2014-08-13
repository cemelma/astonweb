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
    public class TownController : Controller
    {
        private MainContext db = new MainContext();

        //
        // GET: /Admin/Country/

        public ActionResult Index()
        {
            List<Country> countries = db.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            if (RouteData.Values["id"] != null)
            {
                int id = Convert.ToInt32(RouteData.Values["id"]);
                ViewBag.Countries = new SelectList(countries, "Id", "Name",id);
                return View(db.Town.Where(x => x.CountryId == id).ToList());
            }
            else
            {
                 int id = countries.FirstOrDefault().Id;
                return View(db.Town.Where(x => x.CountryId == id).ToList());
            }
          
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
            List<Country> countries = db.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View();
        }

        //
        // POST: /Admin/Country/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Town town)
        {
            if (ModelState.IsValid)
            {
               
                db.Town.Add(town);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(town);
        }

        //
        // GET: /Admin/Country/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Town town = db.Town.Find(id);
            if (town == null)
            {
                return HttpNotFound();
            }
            List<Country> countries = db.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name",town.CountryId);


            return View(town);
        }

        //
        // POST: /Admin/Country/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Town country)
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
            Town country = db.Town.Find(id);
            try
            {
                db.Town.Remove(country);
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