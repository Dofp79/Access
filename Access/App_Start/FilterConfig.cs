/*
    ---------------------------------------------------------------
    Class: FilterConfig
    Description:
        This configuration class is used to register global filters 
        in an ASP.NET MVC application. Global filters are applied 
        across all controller actions in the application.

        In this setup, the 'HandleErrorAttribute' is added as a 
        global filter. This ensures that any unhandled exceptions 
        in MVC controllers are caught and routed to an error view.

    Target Framework: ASP.NET MVC
    ---------------------------------------------------------------
*/

using System.Web;
using System.Web.Mvc;

namespace Access
{
    public class FilterConfig
    {
        // Registers global filters to be applied to all MVC actions
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Add the HandleErrorAttribute to globally handle exceptions
            filters.Add(new HandleErrorAttribute());
        }
    }
}
