using BLL.PhotoBL;
using BLL.ServiceBL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class FServicesController : Controller
    {
        //
        // GET: /FServices/

        public ActionResult Index()
        {
            ServiceModel model = new ServiceModel();
            if (RouteData.Values["id"] != null)
            {
                int id = Convert.ToInt32(RouteData.Values["id"]);
                Service srv = ServiceManager.GetServiceById(id);

                model.services = srv;
                model.Photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.Service, id); 
                return View(model);
            }
            else
                return View();
        }

    }
}
