using BLL.AccountBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            var list = AccountManager.GetCustomerList();
            return View(list);
        }

        [AllowAnonymous]
        public JsonResult DeleteUser(int id)
        {
            bool isdelete = AccountManager.DeleteUsers(id);
            return Json(isdelete);
        }

        public JsonResult UpdateStatus(int id)
        {
            return Json(AccountManager.UpdateStatus(id));
        }

    }
}
