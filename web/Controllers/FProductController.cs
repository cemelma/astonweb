using BLL.PhotoBL;
using BLL.ProductBL;
using BLL.ReferenceBL;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            if (submit == "Giriş")
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
                    if (ProductManager.GetMailControl(user.Login.Email))
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
