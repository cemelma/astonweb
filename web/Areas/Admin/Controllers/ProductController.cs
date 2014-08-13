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
using System.Drawing;
using BLL.PhotoBL;
using DAL.Context;
using web.Areas.Admin.Models;
using System.Web.Helpers;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ProductController : Controller
    {
        public ActionResult AddProduct(int id=0)
        {
            if (RouteData.Values["id"] != null)
            {
                ViewBag.SaveResult = true;
                ViewBag.ProductId = id;
            }
            else
            {
                ViewBag.SaveResult = false;
            }
            
            web.Areas.Admin.Models.VMProductGroupModel grouplist = new Models.VMProductGroupModel();
            grouplist.ProductGroup = ProductManager.GetProductGroupList("tr");
            ProductAddModel model = new ProductAddModel();
            model.VMProductGroupModel = grouplist;

           //      ViewBag.Groups = grouplist;
            return View(model);
        }

        public ActionResult EditProduct(int id = 0)
        {
            web.Areas.Admin.Models.VMProductGroupModel grouplist = new Models.VMProductGroupModel();
            grouplist.ProductGroup = ProductManager.GetProductGroupList("tr");
            ProductAddModel model = new ProductAddModel();
            model.VMProductGroupModel = grouplist;

            if (RouteData.Values["id"] != null)
            {
                ViewBag.SaveResult = true;
                ViewBag.ProductId = id;
               
                Product prt = ProductManager.GetProductById(id);
                ViewBag.CategoryId = prt.ProductGroupId;
                model.Product = prt;
            }
            else
            {
                ViewBag.SaveResult = false;
            }

            var photos = PhotoManager.GetList(11, id);
            ViewBag.Photos = photos;
            model.VMProductGroupModel = grouplist;
            //      ViewBag.Groups = grouplist;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(ProductAddModel model, IEnumerable<HttpPostedFileBase> attachments, HttpPostedFileBase prd1, HttpPostedFileBase prd2, string ddl_group)
        {
            try
            {
                model.Product.PageSlug = Utility.SetPagePlug(model.Product.Name);

                model.Product.ProductGroupId = Convert.ToInt32(ddl_group);

                model.Product.TopProductGroupId = GetTopCategoryId(Convert.ToInt32(ddl_group));

                if (prd1 != null)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    string path = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(prd1.FileName);
                    prd1.SaveAs(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                    new ImageHelper(210, 125).SaveThumbnail(prd1, "/Content/images/userfiles/productthumb/", path);
                    model.Product.Image1 = "/Content/images/userfiles/productthumb/" + path + ".jpeg"; ;

                    Helpers.ImageHelper.WaterMark("/Content/images/userfiles/productbig/" + path, 100);
                    Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/productthumb/" + path);
                }
                else
                {
                    model.Product.Image1 = "/Content/images/front/noimage.jpeg";
                }

                if (prd2 != null)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    string path = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(prd2.FileName);

                    prd2.SaveAs(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                    new ImageHelper(210, 125).SaveThumbnail(prd2, "/Content/images/userfiles/productthumb/", path);
                    model.Product.Image2 = "/Content/images/userfiles/productthumb/" + path + ".jpeg"; ;

                    Helpers.ImageHelper.WaterMark("/Content/images/userfiles/productbig/" + path, 40);
                    Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/productthumb/" + path);
                }
                else
                {
                    model.Product.Image2 = "/Content/images/front/noimage.jpeg";
                }

                ProductManager.AddProduct(model.Product);

                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {

                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        string path = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                        item.SaveAs(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                        new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/productthumb/", path);

                        string thumbnail = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                        Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                        Bitmap bmp2 = new Bitmap(bmp);
                        using (Bitmap Orgbmp = bmp2)
                        {

                            int sabit = 90;
                            Size Boyut = new Size(210, 125);
                            Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                            ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/productthumb/") + thumbnail);
                            bmp.Dispose();
                            bmp2.Dispose();
                            Orgbmp.Dispose();
                            GC.Collect();
                        }

                        Helpers.ImageHelper.WaterMark("/Content/images/userfiles/productbig/" + path, 60);
                        Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/productthumb/" + thumbnail);
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.ProductUygulama;
                        p.ItemId = model.Product.ProductId;
                        p.Path = "/Content/images/userfiles/productbig/" + path + ".jpeg"; ;
                        p.Thumbnail = "/Content/images/userfiles/productthumb/" + thumbnail + ".jpeg";
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = "tr";
                        p.TimeCreated = DateTime.Now;
                        p.Title = model.Product.Name;
                        PhotoManager.Save(p);
                    }
                }

             
                ViewBag.SaveResult = true;
                ViewBag.ProcessMessage = true;
            }
            catch (Exception ex)
            {
                ViewBag.SaveResult = false;
                ViewBag.ProcessMessage = false;
            }

        

            return Redirect("/yonetim/urunduzenle/" + model.Product.ProductId);
            //return View();
        }

        public ActionResult EditImg()
        {

            using (MainContext db = new MainContext())
            {
                var plist = db.Product.Where(x => x.Deleted == false && x.ProductId == 86).ToList();
                foreach (var item in plist)
                {
                    string item1 = item.Image1.Replace("Content/images/", "Content/EditImg/");

                    string filePath = Server.MapPath(item1);
                    if (System.IO.File.Exists(filePath))
                    {

                        string filePathb = Server.MapPath(item1.Replace("userfiles/", "userfiles/productbig/"));
                        if (System.IO.File.Exists(filePathb))
                        {
                            Helpers.ImageHelper.WaterMark(item1.Replace("userfiles/", "userfiles/productbig/"), 100);
                        }

                        Helpers.ImageHelper.WaterMarkThumb(item1);
                    }
                }
                

            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(ProductAddModel model, IEnumerable<HttpPostedFileBase> attachments, HttpPostedFileBase prd1, HttpPostedFileBase prd2,int hdProductId)
        {
            try
            {
                model.Product.PageSlug = Utility.SetPagePlug(model.Product.Name);
                model.Product.ProductId = hdProductId;
                if (prd1 != null)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    string path = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(prd1.FileName);
                    prd1.SaveAs(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                    new ImageHelper(210, 125).SaveThumbnail(prd1, "/Content/images/userfiles/productthumb/", path);
                    model.Product.Image1 = "/Content/images/userfiles/productthumb/" + path + ".jpeg"; ;

                    Helpers.ImageHelper.WaterMark("/Content/images/userfiles/productbig/" + path, 100);
                    Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/productthumb/" + path);
                }

                if (prd2 != null)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    string path = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(prd2.FileName);

                    prd2.SaveAs(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                    new ImageHelper(210, 125).SaveThumbnail(prd2, "/Content/images/userfiles/productthumb/", path);
                    model.Product.Image2 = "/Content/images/userfiles/productthumb/" + path + ".jpeg"; ;

                    Helpers.ImageHelper.WaterMark("/Content/images/userfiles/productbig/" + path, 40);
                    Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/productthumb/" + path);
                }

                ProductManager.EditProduct(model.Product);

                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        string path = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                        item.SaveAs(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                        new ImageHelper(1020, 768).SaveThumbnail(item, "/Content/images/userfiles/productthumb/", path);

                        string thumbnail = Utility.SetPagePlug(model.Product.Name) + "_" + rand + Path.GetFileNameWithoutExtension(item.FileName);
                        Bitmap bmp = new Bitmap(Server.MapPath("/Content/images/userfiles/productbig/") + path);
                        Bitmap bmp2 = new Bitmap(bmp);
                        using (Bitmap Orgbmp = bmp2)
                        {

                            int sabit = 90;
                            Size Boyut = new Size(210, 125);
                            Bitmap ReSizedThmb = new Bitmap(Orgbmp, Boyut);
                            ReSizedThmb.Save(Server.MapPath("/Content/images/userfiles/productthumb/") + thumbnail);
                            bmp.Dispose();
                            bmp2.Dispose();
                            Orgbmp.Dispose();
                            GC.Collect();
                        }

                        Helpers.ImageHelper.WaterMark("/Content/images/userfiles/productbig/" + path, 60);
                        Helpers.ImageHelper.WaterMarkThumb("/Content/images/userfiles/productthumb/" + thumbnail);
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.ProductUygulama;
                        p.ItemId = Convert.ToInt32(RouteData.Values["id"]);
                        p.Path = "/Content/images/userfiles/productbig/" + path + ".jpeg"; ;
                        p.Thumbnail = "/Content/images/userfiles/productthumb/" + thumbnail + ".jpeg";
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = "tr";
                        p.TimeCreated = DateTime.Now;
                        p.Title = model.Product.Name;
                        PhotoManager.Save(p);

                    }
                }


                ViewBag.SaveResult = true;
            }
            catch (Exception ex)
            {
                ViewBag.SaveResult = false;
            }



            var photos = PhotoManager.GetList(11, Convert.ToInt32(RouteData.Values["id"]));
            ViewBag.Photos = photos;

            web.Areas.Admin.Models.VMProductGroupModel grouplist = new Models.VMProductGroupModel();
            grouplist.ProductGroup = ProductManager.GetProductGroupList("tr");
            model.VMProductGroupModel = grouplist;

            return View(model);
        }



        public ActionResult SaveDetail(string code, string malzeme, string birim, string ebat, string agirlik, string ton, string fiyat, string renk, string prId)
        {
            using (MainContext db = new MainContext())
            {
                if (!string.IsNullOrEmpty(prId))
                {
                    int id = Convert.ToInt32(prId);

                    ProductDetail pd = new ProductDetail();
                    pd.ProductId = id;
                    pd.Code = code;
                    pd.Material = malzeme;
                    pd.Unit = birim;
                    pd.VehicleTon = ton;
                    pd.Weight = agirlik;
                    pd.Dimension = ebat;
                    pd.Price = fiyat;
                    pd.ColorWhite = renk;
                    //pd.ColorName = renkadi;

                    db.ProductDetail.Add(pd);
                    db.SaveChanges();

                    ViewBag.Details = db.ProductDetail.Where(x => x.ProductId == id).ToList();
                    return PartialView("~/Areas/Admin/Views/Product/_detailtable1.cshtml", ViewBag.Details);
                }
                else
                {
                    ViewBag.Details = null;
                    return PartialView("~/Areas/Admin/Views/Product/_detailtable1.cshtml", ViewBag.Details);
                }
            }
        }


        public PartialViewResult SaveProductDetail(string prid,string catid, string input1, string input2, string input3, string input4, string input5, string input6, string input7, string input8)
        {
            using (MainContext db = new MainContext())
            {
                ProductInformation pinfo = new ProductInformation();
                if (input1 != "undefined") pinfo.Field1 = input1;
                if (input2 != "undefined") pinfo.Field2 = input2;
                if (input3 != "undefined") pinfo.Field3 = input3;
                if (input4 != "undefined") pinfo.Field4 = input4;
                if (input5 != "undefined") pinfo.Field5 = input5;
                if (input6 != "undefined") pinfo.Field6 = input6;
                if (input7 != "undefined") pinfo.Field7 = input7;
                if (input8 != "undefined") pinfo.Field8 = input8;

                pinfo.ProductId = Convert.ToInt32(prid);

                db.ProductInformation.Add(pinfo);
                db.SaveChanges();

                int cid = Convert.ToInt32(catid);
                int pid = Convert.ToInt32(prid);
                PropertyModel model = new PropertyModel();
                model.header = db.ProductHeaders.FirstOrDefault(x => x.CategoryId == cid);

                model.ProductInfo = db.ProductInformation.Where(x => x.ProductId == pid).ToList();

                ViewBag.PrId = pid;
                ViewBag.Details = db.ProductDetail.Where(x => x.ProductId == pid).ToList();
                return PartialView("~/Areas/Admin/Views/Product/_detailproptable.cshtml", model);
            }
        
        }




        public ActionResult GetDetail(int id)
        {
            using (MainContext db = new MainContext())
            {
                   ViewBag.Details = db.ProductDetail.Where(x => x.ProductId == id).ToList();
                   return PartialView("~/Areas/Admin/Views/Product/_detailtable1.cshtml", ViewBag.Details);
             }
        }

        public ActionResult GetDetailPage(int id,int cid)
        {
            using (MainContext db = new MainContext())
            {
                PropertyModel model = new PropertyModel();
                model.header = db.ProductHeaders.FirstOrDefault(x => x.CategoryId == cid);
                model.ProductInfo = db.ProductInformation.Where(x => x.ProductId == id).ToList();
                ViewBag.PrId = id;
                ViewBag.CatId = cid;
                ViewBag.Details = db.ProductDetail.Where(x => x.ProductId == id).ToList();
                return PartialView("~/Areas/Admin/Views/Product/_detailproptable.cshtml",model);
            }
        }

      
        public ActionResult Index(int? groupId)
        {
            using (MainContext db = new MainContext())
            {
                VMProductGroupModel vm = new VMProductGroupModel();
                vm.ProductGroup = ProductManager.GetProductGroupList("tr");
                if (groupId==null)
                    vm.Products = db.Product.Where(x => x.Deleted==false).ToList();
                else vm.Products = db.Product.Where(x => x.Deleted == false && x.TopProductGroupId == groupId).OrderBy(d=>d.SortNumber).ToList();
                return View(vm);
            }
          
        }

        public void SaveCategory(int id, int prdId)
        {
            using (MainContext db = new MainContext())
            {
                int topid = id;
                ProductGroup prodgroup;
                prodgroup = ProductManager.GetTopGroupById(id);
                while (prodgroup.TopProductId != 1)
                {
                    prodgroup = ProductManager.GetTopGroupById(prodgroup.TopProductId);
                    topid = prodgroup.ProductGroupId;
                }

                Product prd = db.Product.Where(x => x.ProductId == prdId).SingleOrDefault();
                if (prd != null)
                {
                    prd.TopProductGroupId = topid;
                    prd.ProductGroupId = id;
                    db.SaveChanges();
                }
            }
        }

        public int GetTopCategoryId(int id)
        {
            using (MainContext db = new MainContext())
            {
                int topid = id;
                ProductGroup prodgroup;
                prodgroup = ProductManager.GetTopGroupById(id);
                while (prodgroup.TopProductId != 1)
                {
                    prodgroup = ProductManager.GetTopGroupById(prodgroup.TopProductId);
                    topid = prodgroup.ProductGroupId;
                }
                return topid;
            }
        }

        [AllowAnonymous]
        public JsonResult DeleteProduct(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Product.FirstOrDefault(d => d.ProductId == id);
                    db.Product.Remove(record);
                    db.SaveChanges();

                    if (record.Image1 != "/Content/images/front/noimage.jpeg")
                    {
                        string filePath = Server.MapPath(record.Image1);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    if (record.Image2 != "/Content/images/front/noimage.jpeg")
                    {
                        string filePath2 = Server.MapPath(record.Image2);
                        if (System.IO.File.Exists(filePath2))
                        {
                            System.IO.File.Delete(filePath2);
                        }
                    }
                    var recordphoto = db.Photo.Where(d => d.CategoryId == (int)PhotoType.ProductUygulama && d.ItemId == record.ProductId).ToList();
                    foreach (var item in recordphoto)
                    {
                        string filePathfullimg = Server.MapPath(item.Path);
                        if (System.IO.File.Exists(filePathfullimg))
                        {
                            System.IO.File.Delete(filePathfullimg);
                        }

                        string filePathfullimgt = Server.MapPath(item.Thumbnail);
                        if (System.IO.File.Exists(filePathfullimgt))
                        {
                            System.IO.File.Delete(filePathfullimgt);
                        }

                    }

                    return Json(true);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
        }

        [AllowAnonymous]
        public JsonResult DeleteDetail(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ProductInformation.FirstOrDefault(d => d.ProductInformationId == id);
                    db.ProductInformation.Remove(record);
                    //var record = db.ProductDetail.FirstOrDefault(d => d.DetailId == id);
                    //db.ProductDetail.Remove(record);
                    db.SaveChanges();
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            using (MainContext db = new MainContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Product sortingrecord = db.Product.SingleOrDefault(d => d.ProductId == mid);
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



        }

        public class JsonList
        {
            public string[] list { get; set; }
        }

        public JsonResult DeletePhoto(int id)
        {
            bool isdelete = PhotoManager.Delete(id);
            return Json(isdelete);
        }


        public JsonResult EditStatus(int id)
        {
            string NowState;
            using (MainContext db = new MainContext())
            {
                var list = db.Product.SingleOrDefault(d => d.ProductId == id);
                try
                {

                    if (list != null)
                    {
                        list.Online = list.Online == true ? false : true;
                        db.SaveChanges();

                    }
                    return Json(list.Online);

                }
                catch (Exception)
                {
                    return Json(list.Online);
                }
            }
        }

        public ActionResult GetColors(int id,int cid)
        {
            using (MainContext db = new MainContext())
            {
                ViewBag.PrId = id;
                ViewBag.Colors = db.ProductColors.Where(x => x.ProductId == id).ToList();
                return PartialView("~/Areas/Admin/Views/Product/_colorss.cshtml", ViewBag.Colors);
            }
        }

        public ActionResult SaveColor(int id, string ad,string code)
        {
            using (MainContext db = new MainContext())
            {
                ProductColors pinfo = new ProductColors();
                pinfo.Adi = ad;
                pinfo.RenkKodu = code;
                pinfo.ProductId = id;
                

                //pinfo.ProductId = Convert.ToInt32(prid);

                db.ProductColors.Add(pinfo);
                db.SaveChanges();

                ViewBag.PrId = id;
                ViewBag.Colors = db.ProductColors.Where(x => x.ProductId == id).ToList();
                return PartialView("~/Areas/Admin/Views/Product/_colorss.cshtml", ViewBag.Colors);
            }
        }

        [AllowAnonymous]
        public JsonResult DeleteColor(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ProductColors.FirstOrDefault(d => d.ColorId == id);
                    db.ProductColors.Remove(record);
                    //var record = db.ProductDetail.FirstOrDefault(d => d.DetailId == id);
                    //db.ProductDetail.Remove(record);
                    db.SaveChanges();
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
        }
    }
}
