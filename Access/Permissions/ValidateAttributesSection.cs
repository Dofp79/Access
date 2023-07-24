using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Access.Permissions
{
    /// <summary>
    /// This C# code is defining a custom Action Filter Attribute named "ValidateAttributesSection." Action Filters in ASP.NET MVC are used to 
    /// add pre or post-processing logic to controller actions. This particular filter is used to check whether the user is authenticated 
    /// (logged in) before accessing a specific action method in a controller.
    /// </summary>
    public class ValidateAttributesSection : ActionFilterAttribute
    {
        // This method is executed before an action method is executed.
        // It takes an ActionExecutingContext parameter, which contains information about the current action execution.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if the "User" key in the Session is null, which indicates that the user is not authenticated.
            if (HttpContext.Current.Session["User"] == null)
            {

                filterContext.Result = new RedirectResult("~/Access/Login");
            }
            //If the user is not authenticated, the filterContext.Result is set to a new RedirectResult object that redirects the user to the Login page.

            base.OnActionExecuting(filterContext);
        }
    }
}