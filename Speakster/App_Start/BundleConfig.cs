using System.Web;
using System.Web.Optimization;

namespace Speakster
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/home/jquery").Include(
                      "~/Content/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                      "~/Content/js/jquery.scrollex.min.js",
                      "~/Content/js/jquery.scrolly.min.js",
                      "~/Content/js/skel.min.js",
                      "~/Content/js/util.js",
                      "~/Content/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                      "~/Content/assets_dashboard/js/bootstrap.min.js",
                      "~/Content/assets_dashboard/js/jquery-1.8.3.min.js",
                      "~/Content/assets_dashboard/js/jquery.dcjqaccordion.2.7.js",
                      "~/Content/assets_dashboard/js/jquery.scrollTo.min.js",
                      "~/Content/assets_dashboard/js/jquery.nicescroll.js",
                      "~/Content/assets_dashboard/js/jquery.sparkline.js",
                      "~/Content/assets_dashboard/js/common-scripts.js",
                      "~/Content/assets_dashboard/js/gritter/js/jquery.gritter.js",
                      "~/Content/assets_dashboard/js/gritter-conf.js",
                      "~/Content/assets_dashboard/js/sparkline-chart.js",
                      "~/Content/assets_dashboard/js/zabuto_calendar.js",
                      "~/Scripts/modal.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/dashboard").Include(
                      "~/Content/assets_dashboard/css/bootstrap.css",
                      "~/Content/assets_dashboard/font-awesome/css/font-awesome.css",
                      "~/Content/assets_dashboard/lineicons/style.css",
                      "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Content/dashboard/style").Include(
                      "~/Content/assets_dashboard/css/style.css",
                      "~/Content/assets_dashboard/css/style-responsive.css"));



        }
    }
}
