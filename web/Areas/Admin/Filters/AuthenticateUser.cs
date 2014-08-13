using BLL.PhotoBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace web.Areas.Admin.Filters
{
    public class AuthenticateUser:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Redirect("/yonetim/login");
                HttpContext.Current.Response.End();
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SaveImageAltTags : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var tags = filterContext.Controller.ValueProvider.GetValue("alttag").AttemptedValue.Split(',');
                var photoid = filterContext.Controller.ValueProvider.GetValue("photoid").AttemptedValue.Split(',');
                for (int i = 0; i < tags.Count(); i++)
                {
                    PhotoManager.Edit(Convert.ToInt32(photoid[i]), tags[i], null, "");
                }
            
            }
            catch (Exception)
            {

            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}