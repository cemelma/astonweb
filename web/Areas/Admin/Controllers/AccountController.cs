using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Models;
using DAL.Entities;
using BLL.SecurityBL;
using BLL.AccountBL;
using System.Web.Security;

namespace web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login_v2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login_v2(LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                //string password = SecurityManager.EncodeMD5(model.Password);
                if (AccountManager.Login(loginmodel.Email, loginmodel.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ProcessMessage = "false";
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı!");
                }

                return View(loginmodel);

            }
            else
            {
                return View();
            }
        }

       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login_v2", "Account");

        }


        public ActionResult New()
        {
            return View();

        }
        [HttpPost]
        public ActionResult New(AdminUser model)
        {

            if (ModelState.IsValid)
            {
                ViewBag.ProcessMessage = AccountManager.AddNewUser(model);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();

        }

        public ActionResult Index()
        {
            var list = AccountManager.GetUserList();
            return View(list);

        }


        [AllowAnonymous]
        public JsonResult DeleteUser(int id)
        {
            bool isdelete = AccountManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public ActionResult Edit()
        {
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    AdminUser bank = AccountManager.GetUserInfoById(nid);
                    return View(bank);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult Edit(AdminUser model, string txtpassword)
        {


            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    model.AdminUserId = nid;
                    if (!string.IsNullOrEmpty(txtpassword))
                    {
                        model.Password = txtpassword;
                    }
                    ViewBag.ProcessMessage = AccountManager.EditUser(model);
                    return View(model);
                }
                else
                {
                    ViewBag.ProcessMessage = false;
                    return View(model);
                }
            }
            else
                return View();




        }

    }
}
