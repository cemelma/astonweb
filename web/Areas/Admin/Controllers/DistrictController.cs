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
    public class DistrictController : Controller
    {
        private MainContext db = new MainContext();

        //
        // GET: /Admin/Country/

        public ActionResult Index()
        {
            List<Country> countries = db.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            int id = countries.FirstOrDefault().Id;

            List<Town> towns = db.Town.Where(x=>x.CountryId==id).ToList();
            ViewBag.Towns = new SelectList(towns, "Id", "Name");

            if (RouteData.Values["cid"] != null & RouteData.Values["tid"] != null)
            {
                int cid = Convert.ToInt32(RouteData.Values["cid"]);
                int tid = Convert.ToInt32(RouteData.Values["tid"]);
                ViewBag.Countries = new SelectList(countries, "Id", "Name",cid);
                towns = db.Town.Where(x => x.CountryId == cid).ToList();
                ViewBag.Towns = new SelectList(towns, "Id", "Name",tid);

                return View(db.District.Where(x => x.TownId == tid).ToList());
            }
            else if (RouteData.Values["cid"] != null & RouteData.Values["tid"] == null)
            {
                int cid = Convert.ToInt32(RouteData.Values["cid"]);
               
                ViewBag.Countries = new SelectList(countries, "Id", "Name", cid);
                towns = db.Town.Where(x => x.CountryId == cid).ToList();
                int tid = towns.FirstOrDefault().Id;
                ViewBag.Towns = new SelectList(towns, "Id", "Name", tid);

                return View(db.District.Where(x => x.TownId == tid).ToList());
            }

            else
            {
              //   int id = countries.FirstOrDefault().Id;
                return View(db.District.Where(x => x.TownId == id).ToList());
            }
          
        }

        //
        // GET: /Admin/Country/Details/5

      

        //
        // GET: /Admin/Country/Create

        public ActionResult Create()
        {
            List<Country> countries = db.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            int id = countries.FirstOrDefault().Id;

            List<Town> towns = db.Town.Where(x => x.CountryId == id).ToList();
            ViewBag.Towns = new SelectList(towns, "Id", "Name");
            return View();
        }

        //
        // POST: /Admin/Country/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(District town)
        {
            if (ModelState.IsValid)
            {
               
                db.District.Add(town);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(town);
        }

        //
        // GET: /Admin/Country/Edit/5

        public ActionResult Edit(int id = 0)
        {
            District dist = db.District.Find(id);
            if (dist == null)
            {
                return HttpNotFound();
            }

            int cid = db.Town.FirstOrDefault(x => x.Id == dist.TownId).CountryId;

           

            List<Town> towns = db.Town.Where(x=>x.CountryId==cid).ToList();
            ViewBag.Towns = new SelectList(towns, "Id", "Name",dist.TownId);

            List<Country> countries = db.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name", cid);

            return View(dist);
        }

        //
        // POST: /Admin/Country/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(District country)
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
            District country = db.District.Find(id);
            try
            {
                db.District.Remove(country);
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