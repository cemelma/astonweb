using web.Models;
using BLL.ProductBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.SectorBL;
using BLL.SectorGroupBL;
using DAL.Entities;
using BLL.ServiceGroupBL;
using DAL.Context;

namespace web.Controllers
{
    public class FSectorController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Sector/

        public ActionResult Index()
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == 7 && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }


            var Sector_group_list = SectorGroupManager.GetSectorGroupListForFront(lang);
            //Sector Sector = new Sector();
            //OurSectors ourSectors = new OurSectors();
            //SectorGroup sectorgrp = new SectorGroup();
            //int index = 0;
            //if (RouteData.Values["sid"] != null)
            //{
            //    Sector = SectorManager.GetSectorById(Convert.ToInt32(RouteData.Values["sid"].ToString()));
            //    ViewBag.grpname = SectorGroupManager.GetSectorGroupById(Sector.SectorGroupId).GroupName;
            //    index = Sector_group_list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.SectorGroupId == Sector.SectorGroupId).index;
            //}
            //else if (RouteData.Values["gid"] != null)
            //{
            //    sectorgrp = SectorGroupManager.GetSectorGroupById(Convert.ToInt32(RouteData.Values["gid"].ToString()));
            //    index = Sector_group_list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.SectorGroupId == sectorgrp.SectorGroupId).index;
            //}
            //else
            //{
            //    ourSectors = SectorManager.GetOurSectors(lang);
            //}

            //Sector_group_list = ServiceGroupManager.Swap(Sector_group_list, 0, index);

            SectorWrapperModel swm = new SectorWrapperModel(null, Sector_group_list, null, null, null);
            return View(swm);
        }

        public ActionResult subSectors(int id)
        {
            var sl = SectorManager.GetSectorList(id);

            var sg = SectorGroupManager.GetSectorGroupById(id);
            SectorSubWrapperModel pswm = new SectorSubWrapperModel(sl, sg);
            return PartialView("_Sectors", pswm);
        }

        //public ActionResult ProductDetail(int pid)
        //{
        //    var product_group_list = ProductManager.GetProductGroupListForFront(lang);
            
        //    var product = ProductManager.GetProductById(pid);
        //    var psg = ProductManager.GetProductSubGroupById(product.ProductSubGroupId);
        //    var pg = ProductManager.GetProductGroupById(product.ProductGroupId);

        //    ViewBag.grpid = pg.ProductGroupId;
        //    ViewBag.sgid = psg.ProductSubGroupId;
        //    ViewBag.subgrpname = psg.GroupName;
        //    ViewBag.grpname = pg.GroupName;

        //    ProductDetailWrapperModel model = new ProductDetailWrapperModel(product, product_group_list);
            
        //    return View(model);
        //}
    }
}
