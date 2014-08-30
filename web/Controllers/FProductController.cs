using BLL.PhotoBL;
using BLL.ProductBL;
using BLL.ReferenceBL;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using web.Models;

namespace web.Controllers
{
    public class FProductController : Controller
    {
        public ActionResult Index(int id)
        {
            //web.Areas.Admin.Models.VMProductGroupModel grouplist = new web.Areas.Admin.Models.VMProductGroupModel();
            //grouplist.ProductGroup = ProductManager.GetProductGroupList("tr");
            int pId = Convert.ToInt32(RouteData.Values["id"]);
         //   ViewBag.ProductGroup = ProductManager.GetProductGroupItem(pId);
            ViewData["referanslar"] = ReferenceManager.GetReferenceListForFront("tr");
            
            
            
            using(MainContext db = new MainContext())
            {
                ProductFrontModel model = new ProductFrontModel();
                model.product = ProductManager.GetProductById(pId);
                int catId = model.product.ProductGroupId;
                model.headers = db.ProductHeaders.FirstOrDefault(x=>x.CategoryId==catId);
                model.ProductInfo = db.ProductInformation.Where(x => x.ProductId == pId).ToList();

                model.Colors = db.ProductColors.Where(x => x.ProductId == pId).ToList();

                ViewBag.ProductGroup = model.product.ProductGroup.GroupName;
                ViewBag.Photos = PhotoManager.GetListForFront(11, pId);

                ViewBag.ProductInfo = db.ProductDetail.Where(x => x.ProductId == pId).ToList();
                return View(model);
            }
               


          
        }

        public ActionResult ExportData(int id)
        {
            using (MainContext db = new MainContext())
            {
                GridView gv = new GridView();
                var ss = db.ProductInformation.Where(d=>d.ProductId == id).ToList();
                int catid = db.Product.FirstOrDefault(d=>d.ProductId == id).ProductGroupId;

                var headers = db.ProductHeaders.FirstOrDefault(d => d.CategoryId == catid);

                DataTable dtDetails = new DataTable();
                int colcount = 0;

                if (headers.Header1 != null) { dtDetails.Columns.Add(headers.Header1, typeof(string)); colcount = 1; } 
                if (headers.Header2 != null) {dtDetails.Columns.Add(headers.Header2, typeof(string)); colcount = 2; }
                if (headers.Header3 != null) {dtDetails.Columns.Add(headers.Header3, typeof(string)); colcount = 3; }
                if (headers.Header4 != null) {dtDetails.Columns.Add(headers.Header4, typeof(string)); colcount = 4; }
                if (headers.Header5 != null) {dtDetails.Columns.Add(headers.Header5, typeof(string)); colcount = 5; }
                if (headers.Header6 != null) {dtDetails.Columns.Add(headers.Header6, typeof(string)); colcount = 6; }
                if (headers.Header7 != null) {dtDetails.Columns.Add(headers.Header7, typeof(string)); colcount = 7; }
                if (headers.Header8 != null) {dtDetails.Columns.Add(headers.Header8, typeof(string)); colcount = 8; }
                if (headers.Header9 != null) {dtDetails.Columns.Add(headers.Header9, typeof(string)); colcount = 9; }
                if (headers.Header10 != null) { dtDetails.Columns.Add(headers.Header10, typeof(string)); colcount = 10; }
                if (headers.Header11 != null) {dtDetails.Columns.Add(headers.Header11, typeof(string)); colcount = 11; }
                if (headers.Header12 != null) {dtDetails.Columns.Add(headers.Header12, typeof(string)); colcount = 12; }

                int bobo = 0;

                foreach (var item in ss)
                {
                    DataRow dr = null;
                    dr = dtDetails.NewRow();

                    for (int i = 0; i < colcount; i++)
                    {
                        dr[i] = item.GetType().GetProperty("Field" + (i + 1)).GetValue(item, null);
                    }
                    bobo++;

                    dtDetails.Rows.Add(dr);
                    //dtDetails.Rows.Add(item.Field1, item.Field2, item.Field3, item.Field4, item.Field5, item.Field6, item.Field7, item.Field8, item.Field9, item.Field10, item.Field11, item.Field11);
                }

                gv.DataSource = dtDetails;
                gv.DataBind();
                
                Response.ClearContent();
                Response.Buffer = true;
                Response.Charset = Encoding.UTF8.EncodingName;
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

                Response.AddHeader("content-disposition", "attachment; filename=UrunListesi.xls");
                Response.ContentType = "application/ms-excel";
                
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);


                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

                return RedirectToAction("Prices");
            }
        }

        public ActionResult Prices()
        {
            ProductPriceModel model = new ProductPriceModel();
            if (RouteData.Values["id"] != null)
            {
                int pId = Convert.ToInt32(RouteData.Values["id"]);

                using(MainContext db = new MainContext())
                {
                    var groups = db.ProductGroup.Where(x => x.TopProductId == pId && x.Deleted==false).ToList();
                    int[] groupIds = groups.Select(x => x.ProductGroupId).ToArray();

                    List<ProductHeaders> productHeaders = db.ProductHeaders.Where(x => groupIds.Contains(x.CategoryId)).ToList();
                    List<Product> products = db.Product.Where(x => x.TopProductGroupId == pId && x.Deleted == false && x.Online == true).OrderBy(x => x.SortNumber).ToList();
                    int[] prIds = db.Product.Select(x => x.ProductId).ToArray();
                    List<ProductInformation> procudtDetails = db.ProductInformation.Where(x => prIds.Contains(x.ProductId)).ToList();

                    foreach (var item in products)
                    {
                        ProductPriceDetail detail = new ProductPriceDetail();
                        detail.ProductName = item.Name;
                        detail.CategoryId = item.ProductGroupId;
                        detail.ProductId = item.ProductId;
                        detail.PageSlug = item.PageSlug;
                        detail.headers = productHeaders.FirstOrDefault(x => x.CategoryId == detail.CategoryId);
                        detail.productsinfo = procudtDetails.Where(x => x.ProductId == detail.ProductId).ToList();

                        model.Info.Add(detail);
                    }



                    ViewBag.ProductGroup = ProductManager.GetProductGroupItem(pId);


                    if (Session["userlogin"] != null)
                        ViewBag.userloginemail = Session["userlogin"];
                    else ViewBag.userloginemail = "";

                    return View(model);
                }
               


                //List<ProductHeaders> headerlist = d





              

                //return View(ProductManager.GetProductListFront(pId).ToList());
            }
            return View();
        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(UserModel user,string submit)
        {
            if (submit == "Giriş" || submit == "Login")
            {
                if (ProductManager.GetLoginControl(user.Login.Email, user.Login.Password))
                {
                    Session["userlogin"] = user.Login.Email;
                    return RedirectToAction("Index", "FHome");
                }
                else TempData["login"] = "false";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (ProductManager.GetMailControl(user.NewUser.Email))
                    {
                        if (user.NewUser.Password == user.RePassword)
                        {
                            if (ProductManager.AddUser(user.NewUser))
                            {
                                Session["userlogin"] = user.NewUser.Email;
                                return RedirectToAction("Index", "FHome");
                            }
                            else TempData["usernew"] = "false";
                        }
                        else TempData["pass"] = "false";
                    }
                    else TempData["mailisuse"] = "false";
                }
                else TempData["usernewvalid"] = "false"; 
            }


            return View();
        }

        public ActionResult UserLogout()
        {
            if (Session["userlogin"] != null) Session.Remove("userlogin");
            return RedirectToAction("Index", "FHome");
        }
        

    }
}
