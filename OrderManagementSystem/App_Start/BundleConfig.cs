namespace OrderManagementSystem
{
    using System.Collections.Generic;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptsBundle = new ScriptBundle("~/bundles/all") { Orderer = new AsIsBundleOrderer() };
            scriptsBundle
                        .Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/respond.js",
                            "~/Scripts/ekko-lightbox.js",
                            "~/Scripts/star-rating.min.js",
                            "~/Scripts/modernizr-*",
                            "~/Scripts/jquery.validate.js",
                            "~/Scripts/jquery.validate.unobtrusive.js")
                        .IncludeDirectory("~/Scripts/Application", "*.js", searchSubdirectories: false);

            bundles.Add(scriptsBundle);

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                    "~/Content/bootstrap.css",
                    "~/Content/star-rating.min.css",
                    "~/Content/ekko-lightbox.css",
                    "~/Content/Site.css"));
        }

        public class AsIsBundleOrderer : IBundleOrderer
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }
    }
}
