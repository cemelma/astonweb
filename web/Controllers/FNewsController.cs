﻿using BLL.NewsBL;
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

        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {

            var news = NewsManager.GetNewsListForFront(lang,false);
            //var photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.News, lang);
       //     var photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.News);
       //     NewsWrapperModel m = new NewsWrapperModel(news, photos);
            return View(news);
        }

        //makine-ve-arac-parkuru
        public ActionResult Machine()
        {
            var machine = NewsManager.GetNewsListForFrontMachine(lang, true);
            return View(machine);
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
