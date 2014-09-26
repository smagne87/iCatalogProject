using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace iCatalogSite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Javascript Files

            bundles.Add(new ScriptBundle("~/Scripts/jQuery").Include(
                "~/js/jquery-{version}.js",
                "~/js/jquery.*",
                "~/js/jquery-ui-{version}.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap").Include(
                "~/js/bootstrap.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/AutoComplete").Include(
                "~/js/autocomplete/jquery.mockjax.js",
                "~/js/autocomplete/jquery.autocomplete.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/MainScripts").Include(
                "~/js/jQueryRotateCompressed.2.2.js",
                "~/js/icatalog/main.js",
                "~/js/icatalog/ajax-helper.js",
                "~/js/icatalog/misc-functions.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/Validate").Include(
                "~/js/jquery.validate.js",
                "~/js/jquery.validate.unobtrusive.js")
            );


            bundles.Add(new ScriptBundle("~/Scripts/AjaxHelper").Include(
                "~/js/icatalog/ajax-helper.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/TabHelper").Include(
                "~/js/icatalog/tabs.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/DataTableHelpers").Include(
                "~/js/DataTables/jquery.dataTables.js",
                "~/js/DataTables/jquery.dataTables.custom.js")
            );

            #endregion

            bundles.Add(new StyleBundle("~/css/Bootstrap").Include(
                "~/css/bootstrap.css",
                "~/css/bootstrap-responsive.css"
                ));

            bundles.Add(new StyleBundle("~/css/MainStyles").Include(
                /*"~/css/main.css",
                "~/css/main-custom.css",*/
                "~/css/sb-admin.css"
                ));

            bundles.Add(new StyleBundle("~/css/FontsAwesome").Include(
                "~/font-awesome-4.1.0/css/font-awesome.min.css"
                ));

            bundles.Add(new StyleBundle("~/css/DataTablesCSS").Include(
                "~/css/DataTables/jquery.dataTables.css",
                "~/css/DataTables/jquery.dataTables_themeroller.css"
                ));
        }
    }
}