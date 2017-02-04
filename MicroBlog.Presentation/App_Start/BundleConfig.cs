using System.Web.Optimization;

namespace MicroBlog.Presentation.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*", 
                "~/Scripts/modernizr-*", 
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css", 
                "~/Content/site.css"));
        }
    }
}