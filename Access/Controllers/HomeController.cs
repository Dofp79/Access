/*
    ---------------------------------------------------------------
    Class: HomeController
    Description:
        This controller handles the core navigation actions for the 
        web application such as Index, About, Contact, and Logout.

        It uses a custom action filter attribute `[ValidateAttributesSection]` 
        to ensure only authenticated users (those with Session["User"] != null) 
        can access these actions. Unauthorized users are redirected to the 
        login page.

    Security:
        - The `[ValidateAttributesSection]` attribute enforces session-based 
          access control across all action methods in this controller.

    Target Framework: ASP.NET MVC
    ---------------------------------------------------------------
*/

using Access.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Access.Controllers
{
    [ValidateAttributesSection] // Custom filter to restrict access to authenticated users
    public class HomeController : Controller
    {
        // GET: Home/Index - Default home page view
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/About - About page with a message
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // GET: Home/Contact - Contact page with a message
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        /*
            Action: Logout
            Description:
                - Clears the current user's session (logs the user out)
                - Redirects the user to the login page

            Security:
                - Protected by the ValidateAttributesSection filter
        */
        public ActionResult Logout()
        {
            Session["User"] = null; // Clear session
            return RedirectToAction("Login", "Access"); // Redirect to login
        }
    }
}
