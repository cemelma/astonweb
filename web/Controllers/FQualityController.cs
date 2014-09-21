using BLL.NewsBL;
using BLL.PhotoBL;
using BLL.QualityBL;
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
    public class FQualityController : Controller
    {

        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {

            var news = QualityManager.GetQualityListForFront(lang);
            //var photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.News, lang);
       //     var photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.News);
       //     NewsWrapperModel m = new NewsWrapperModel(news, photos);
            return View(news);
        }

    }
}
