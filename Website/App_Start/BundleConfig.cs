using System.Web;
using System.Web.Optimization;

namespace JARVIS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*", "~/Scripts/wizard.js", "~/Scripts/wizardEduExp.js", "~/Scripts/validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css", "~/Content/wizard.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.


            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));



            //HR PORTAL **********************************************************
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/toastr.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/sidebar.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                     "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //           "~/Content/bootstrap.css",
            //           "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/toastr.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/sidebar.css"));
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                  "~/Content/jquery-ui.css"));
            bundles.Add(new ScriptBundle("~/bundles/custom")
.Include("~/Scripts/custom/vanta.{name}.min.js", "~/Scripts/custom/login.js"));
            
        }
    }
}
