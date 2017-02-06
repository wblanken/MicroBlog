using System.Web.Optimization;

namespace MicroBlog.Presentation
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/jquery.validate.min.js", 
                "~/Scripts/jquery.validate.unobtrusive.min.js", 
                "~/Scripts/modernizr-*", 
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-local-storage.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/App/app.js")
                .IncludeDirectory("~/App/services","*.js")
                .IncludeDirectory("~/App/directives","*.js")
                .IncludeDirectory("~/App/controllers","*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/thirdparty/flatly.bootstrap.css", 
                "~/Content/site.css"));
        }
    }
}