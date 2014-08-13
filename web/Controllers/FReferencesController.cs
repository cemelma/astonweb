using BLL.PhotoBL;
using BLL.ReferenceBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class FReferencesController : Controller
    {
        //
        // GET: /FReferences/

        public ActionResult Index()
        {
            var references = ReferenceManager.GetReferenceListForFront("tr");
            //SubscribeModel obj = new SubscribeModel();
            //ContactWrapperModel m = new ContactWrapperModel(contact, obj);
            return View(references);
        }

        public ActionResult Detail(int id=0)
        {
            var reference = ReferenceManager.GetReferenceById(id);
            var photos = PhotoManager.GetListForFront((int)web.Areas.Admin.Helpers.PhotoType.Reference,id);
            ReferenceWrapperModel m = new ReferenceWrapperModel(reference, photos);
            return View(m);
        }

    }
}
