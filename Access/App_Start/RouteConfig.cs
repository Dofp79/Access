/*
    ---------------------------------------------------------------
    Class: RouteConfig
    Description:
        This class defines the URL routing configuration for the 
        ASP.NET MVC application. It specifies how incoming URLs are 
        matched to controllers and actions.

        The configuration includes:
        - Ignoring requests for resource files like .axd (used for trace, WebResource, etc.)
        - Defining a default route pattern that maps URLs in the form:
          /{controller}/{action}/{id}
        - If not specified, it defaults to the "Home" controller and "Index" action

    Target Framework: ASP.NET MVC
    ---------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Access
{
    public class RouteConfig
    {
        // Method to register all route definitions
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore routing for .axd resource requests (commonly used by ASP.NET for system resources)
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Define the default route pattern
            routes.MapRoute(
                name: "Default", // Name of the route
                url: "{controller}/{action}/{id}", // URL pattern
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Default route values
            );
        }
    }
}
