using System.Web;
using System.Web.Optimization;

namespace ProjectSEM3
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
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Assets/Admin/css").Include(
                          "~/Assets/Admin/vendors/bootstrap/dist/css/bootstrap.min.css",
                          "~/Assets/Admin/vendors/font-awesome/css/font-awesome.min.css",
                          "~/Assets/Admin/vendors/nprogress/nprogress.css",
                          "~/Assets/Admin/vendors/iCheck/skins/flat/green.css",
                          "~/Assets/Admin/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                          "~/Assets/Admin/vendors/jqvmap/dist/jqvmap.min.css",
                          "~/Assets/Admin/vendors/bootstrap-daterangepicker/daterangepicker.css",
                          "~/Assets/Admin/vendors/build/css/custom.min.css",
                          "~/Assets/Plugin/alertifyjs/css/alertify.min.css",
                          "~/Assets/Plugin/alertifyjs/css/themes/default.min.css",
                           "~/Assets/Plugin/lobibox-master/dist/css/lobibox.min.css",
                          "~/Assets/Admin/vendors/build/css/myStyle.css"

                ));

            bundles.Add(new StyleBundle("~/Assets/Admin/cssdatepicker").Include(
                "~/Assets/Admin/vendors/jqueryui/css/themes/base/jquery.ui.all.css"
                ));

            bundles.Add(new StyleBundle("~/Assets/Client/css").Include(
                           "~/Assets/Client/css/reset.css",
                           "~/Assets/Client/css/bootstrap.css",
                           "~/Assets/Client/css/bootstrap-responsive.css",
                           "~/Assets/Client/css//flexslider.css",
                           "~/Assets/Client/css/andepict.css",
                           "~/Assets/Client/css/product-slider.css",
                           "~/Assets/Client/css/jquery.selectbox.css",
                           "~/Assets/Client/css/nouislider.css",
                           "~/Assets/Client/css/fb_style.css",
                           "~/Assets/Client/css/isotope.css",
                           "~/Assets/Client/css/cloudzoom.css",
                           "~/Assets/Client/css/style.css",
                           "~/Assets/Client/css/animate.css",
                           "~/Assets/Client/rs-plugin/css/settings.css",
                           "~/Assets/Client/rs-plugin/css/extralayers-sport.css",
                           "~/Assets/Client/css/style-sport.css"

                  ));

            bundles.Add(new ScriptBundle("~/Assets/Admin/js").Include(
                         "~/Assets/Admin/vendors/jquery/dist/jquery.min.js",
                         "~/Assets/Admin/vendors/bootstrap/dist/js/bootstrap.min.js",
                         "~/Assets/Admin/vendors/fastclick/lib/fastclick.js",
                         "~/Assets/Admin/vendors/nprogress/nprogress.js",
                         "~/Assets/Admin/vendors/Chart.js/dist/Chart.min.js",
                         "~/Assets/Admin/vendors/gauge.js/dist/gauge.min.js",
                         "~/Assets/Admin/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                         "~/Assets/Admin/vendors/iCheck/icheck.min.js",
                         "~/Assets/Admin/vendors/skycons/skycons.js",
                         "~/Assets/Admin/vendors/Flot/jquery.flot.js",
                         "~/Assets/Admin/vendors/Flot/jquery.flot.pie.js",
                         "~/Assets/Admin/vendors/Flot/jquery.flot.time.js",
                         "~/Assets/Admin/vendors/Flot/jquery.flot.stack.js",
                         "~/Assets/Admin/vendors/Flot/jquery.flot.resize.js",
                         "~/Assets/Admin/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                         "~/Assets/Admin/vendors/flot-spline/js/jquery.flot.spline.min.js",
                         "~/Assets/Admin/vendors/flot.curvedlines/curvedLines.js",
                         "~/Assets/Admin/vendors/DateJS/build/date.js",
                         "~/Assets/Admin/vendors/jqvmap/dist/jquery.vmap.js",
                         "~/Assets/Admin/vendors/jqvmap/dist/maps/jquery.vmap.world.js",
                         "~/Assets/Admin/vendors/jqvmap/examples/js/jquery.vmap.sampledata.js",
                         "~/Assets/Admin/vendors/moment/min/moment.min.js",
                         "~/Assets/Admin/vendors/bootstrap-daterangepicker/daterangepicker.js",
                         "~/Assets/Admin/vendors/build/js/custom.min.js",
                         "~/Assets/Plugin/alertifyjs/alertify.min.js",
                         "~/Assets/Plugin/lobibox-master/dist/js/lobibox.min.js",
                         "~/Assets/Plugin/ckfinder/ckfinder.js",
                         "~/Assets/Plugin/ckeditor/ckeditor.js",
                         "~/Assets/Admin/vendors/build/js/myJS.js"


                         ));
            bundles.Add(new ScriptBundle("~/Assets/Admin/datepicker").Include(

                "~/Assets/Admin/vendors/jqueryui/js/jquery-ui-1.8.2.custom.js"
            ));


            bundles.Add(new ScriptBundle("~/Assets/Client/js").Include(

                    "~/Assets/Client/js/jquery-ui.min.js",
                    "~/Assets/Client/js/bootstrap.js",
                    "~/Assets/Client/js/jquery.easing.js",
                    "~/Assets/Client/js/jquery.flexslider.js",
                    "~/Assets/Client/js/jquery.elastislide.js",
                    "~/Assets/Client/js/jquery.selectbox-0.2.js",
                    "~/Assets/Client/js/jquery.nouislider.js",
                    "~/Assets/Client/js/jquery.isotope.min.js",
                    "~/Assets/Client/js/cloudzoom.js",
                    "~/Assets/Client/js/jquery.inview.js",
                    "~/Assets/Client/js/jquery.jcarousel.min.js",
                    "~/Assets/Client/js/jquery.parallax.js",
                    "~/Assets/Client/js/scripts.js",
                    "~/Assets/Client/js/doubletaptogo.js",
                    "~/Assets/Client/js/navigation.js",
                    "~/Assets/Client/rs-plugin/js/jquery.themepunch.plugins.min.js",
                    "~/Assets/Client/rs-plugin/js/jquery.themepunch.revolution.min.js"
                ));
        }
    }
}
