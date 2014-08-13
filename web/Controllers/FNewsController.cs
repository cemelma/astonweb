using BLL.NewsBL;
using BLL.PhotoBL;
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
    public class FNewsController : Controller
    {
      

        public ActionResult Index()
        {

            var news = NewsManager.GetNewsListForFront("tr");
            //var photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.News, lang);
       //     var photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.News);
       //     NewsWrapperModel m = new NewsWrapperModel(news, photos);
            return View(news);
        }

       
        //public ActionResult NewsContent(int hid)
        //{
        //    var news = NewsManager.GetNewsItem(hid);
        //    var allnews = NewsManager.GetNewsList(lang);
        //    NewsWrapperModel m = new NewsWrapperModel(allnews, news);
        //    return View(m);
        //}
    }
}
