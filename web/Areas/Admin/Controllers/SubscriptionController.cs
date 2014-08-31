using BLL.AccountBL;
using BLL.SubscriptionBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Areas.Admin.Controllers
{
    public class SubscriptionController : Controller
    {
        public ActionResult Index()
        {
            var list = SubscriptionManager.GetSubsciption();
            return View(list);
        }

        [AllowAnonymous]
        public JsonResult DeleteUser(int id)
        {
            bool isdelete = SubscriptionManager.DeleteUsers(id);
            return Json(isdelete);
        }
    }
}
