using System.Web;
using System.Web.Optimization;

namespace web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;
            #region admin
            //admin login page css
            bundles.Add(new StyleBundle("~/admin/logincss").Include("~/Areas/Admin/Content/styles/login.css"));

            //admin paneldeki login sayfasındaki scriptleri yükler.
            bundles.Add(new ScriptBundle("~/admin/loginscripts").Include(
                 "~/Areas/Admin/Content/scripts/plugins/jquery.tipsy.js",
                 "~/Areas/Admin/Content/scripts/plugins/jquery.cookie.js",
                 "~/Areas/Admin/Content/scripts/plugins/jquery.lightbox-0.5.min.js",
                 "~/Areas/Admin/Content/scripts/plugins/jquery.wysiwyg.js",
                 "~/Areas/Admin/Content/scripts/plugins/functions.js"
            ));

            //admin masterpage içindeki stil dosyalarını yükler
            bundles.Add(new StyleBundle("~/admin/layoutcss").Include(
               "~/Areas/Admin/Content/styles/style.css",
               "~/Areas/Admin/Content/styles/grid.css",
               "~/Areas/Admin/Content/styles/base.css",
               "~/Areas/Admin/Content/styles/lists.css",
               "~/Areas/Admin/Content/styles/calendar.css",
               "~/Areas/Admin/Content/styles/extensions.css",
               "~/Areas/Admin/Content/styles/visualize.css",
               "~/Areas/Admin/Content/styles/navigation.css",
               "~/Areas/Admin/Content/styles/notification.css",
               "~/Areas/Admin/Content/styles/leftsidebar.css"
           ));


            /*form css*/
            bundles.Add(new StyleBundle("~/admin/formstylecss").Include("~/Areas/Admin/Content/styles/forms.css"));
            //left sidebar
            bundles.Add(new StyleBundle("~/admin/leftbar").Include("~/Areas/Admin/Content/styles/leftsidebar.css"));

            //listeleme sayfalarında table stillerini yükler.
            bundles.Add(new StyleBundle("~/admin/tablecss").Include("~/Areas/Admin/Content/styles/table.css"));

            /*modernize scripti için*/
            bundles.Add(new ScriptBundle("~/general/modernizr").Include(
                      "~/Areas/Admin/Content/scripts/plugins/modernizr-*"));

            /*layout scripti için*/
           




            //validation için scripts
            bundles.Add(new ScriptBundle("~/admin/validationscripts").Include(
                "~/Areas/Admin/Content/scripts/plugins/validation/jquery.validate.js",
                "~/Areas/Admin/Content/scripts/myscripts/validate.js"
           ));

            //CKEditor için scripts
            bundles.Add(new ScriptBundle("~/admin/editor").Include(
               "~/Areas/Admin/Content/CKEditor/ckeditor/ckeditor.js",
               "~/Areas/Admin/Content/CKEditor/ckfinder/ckfinder.js",
               "~/Areas/Admin/Content/scripts/myscripts/CKEditorCreate.js"
           ));


            bundles.Add(new ScriptBundle("~/admin/tablesorterscripts").Include(
                "~/Areas/Admin/Content/scripts/plugins/jquery-ui-1.8.20.js",
                "~/Areas/Admin/Content/scripts/plugins/tablesorter/tablesorter.js",
                "~/Areas/Admin/Content/scripts/plugins/tablesorter/jquery.tablesorter.pager.js",
                "~/Areas/Admin/Content/scripts/myscripts/TableSorterScript.js",
                "~/Areas/Admin/Content/scripts/myscripts/DeleteRecordTable.js",
                "~/Areas/Admin/Content/scripts/myscripts/SetOnlineStatusRecord.js"

           ));






            #endregion admin






        }
    }
}