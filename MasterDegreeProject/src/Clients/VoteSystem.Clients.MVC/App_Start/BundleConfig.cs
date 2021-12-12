using System.Web.Optimization;

namespace VoteSystem.Clients.MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }  
         
        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            // TODO Uncaught TypeError: Cannot read property 'innerHTML' of null resultController is needed only when i display the results
            bundles.Add(new ScriptBundle("~/bundles/my-scripts").Include(
               "~/Scripts/Custom/home-grid.js",
               "~/Scripts/Custom/calendars.js"));

            bundles.Add(new ScriptBundle("~/bundles/gentallela-js").Include(
                "~/Content/bootstrap-theme/js/nicescroll/jquery.nicescroll.min.js",
                "~/Content/bootstrap-theme/js/moment.min.js",
                "~/Content/bootstrap-theme/js/datepicker/daterangepicker.js",
                "~/Content/bootstrap-theme/js/echart/echarts-all.js",
                "~/Content/bootstrap-theme/js/echart/green.js",
                "~/Content/bootstrap-theme/js/icheck.min.js",
                "~/Scripts/handlebars.min.js",                
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/spin.min.js",
                "~/Content/bootstrap-theme/js/Custom.js"));

            bundles.Add(new StyleBundle("~/bundles/introduction-page-js").Include(
                  "~/Scripts/Custom/introduction-page.js"));

            bundles.Add(new StyleBundle("~/bundles/spiner").Include(
                 "~/Scripts/spin.min.js"));

            bundles.Add(new StyleBundle("~/bundles/spinerController").Include(
                  "~/Scripts/Custom/spinnerController.js"));

            bundles.Add(new StyleBundle("~/bundles/CustomController").Include(
                  "~/Scripts/Custom/CustomController.js"));

            // -------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/bundles/participantController")
                .Include("~/Scripts/VoteSystem/Areas/Administration/participantController.js"));

            bundles.Add(new ScriptBundle("~/bundles/participantPreviewController")
                .Include("~/Scripts/VoteSystem/Areas/Administration/participantPreviewController.js"));

            bundles.Add(new StyleBundle("~/bundles/questionController").Include(
                "~/Scripts/VoteSystem/Areas/Administration/questionController.js"));

            bundles.Add(new StyleBundle("~/bundles/voteSystemController").Include(
                "~/Scripts/VoteSystem/Areas/Administration/voteSystemController.js"));

            bundles.Add(new ScriptBundle("~/bundles/voteSystemResultController").Include("~/Scripts/VoteSystem/Areas/Administration/voteSystemResultController.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-social.css"));

            bundles.Add(new StyleBundle("~/Content/forms-css").Include(
                "~/Content/Custom/forms.css"));

            bundles.Add(new StyleBundle("~/Content/gentelella-css").Include(
                 "~/Content/bootstrap-theme/fonts/css/font-awesome.min.css",                
                "~/Content/bootstrap-theme/css/Custom.css",
                "~/Content/bootstrap-theme/css/daterangepicker/daterangepicker.css",
                "~/Content/bootstrap-theme/css/icheck/green.css",
                "~/Content/bootstrap-theme/css/animate.min.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/introduction-page-css").Include(
                  "~/Content/Custom/introduction-page.css"));
        }
    }
}
