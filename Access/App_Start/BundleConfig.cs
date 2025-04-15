/*
    ---------------------------------------------------------------
    Class: BundleConfig
    Description:
        This configuration class is part of an ASP.NET application. 
        It registers bundles for scripts and stylesheets, which helps 
        reduce the number of HTTP requests and improve page load time 
        by combining and minifying JavaScript and CSS files.

        The bundles include:
        - jQuery
        - jQuery validation
        - Modernizr
        - Bootstrap JavaScript
        - Site-wide CSS

    Target Framework: ASP.NET MVC
    Reference: https://go.microsoft.com/fwlink/?LinkId=301862 for more bundling info
    ---------------------------------------------------------------
*/

using System.Web;
using System.Web.Optimization;

namespace Access
{
    public class BundleConfig
    {
        // Method to register all script and style bundles
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Register jQuery bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Register jQuery validation bundle
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Register Modernizr for feature detection (use development version for learning)
            // For production, use the Modernizr build tool to include only needed tests
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Register Bootstrap JavaScript bundle
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // Register CSS bundle including Bootstrap and site-specific styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
