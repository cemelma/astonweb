﻿using BLL.InstituionalBL;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Helpers.Enums;

namespace web.Controllers
{
    public class FInstitutionalController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        public ActionResult Index()
        {
            Institutional model = InstituionalManager.GetInstationalByLanguage(lang, 0);
            return View(model);
        }

      
    }
}
