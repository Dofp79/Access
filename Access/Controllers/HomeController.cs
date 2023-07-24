using Access.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Access.Controllers
{
    /// <summary>
    /// The HomeController class provides various actions related to the home page and uses the [ValidateAttributesSection] 
    /// attribute to ensure that only authorized users can access these actions. This way, it helps protect sensitive 
    /// areas of the application from unauthorized access.
    /// </summary>
    [ValidateAttributesSection]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        /// <summary>
        /// The [ValidateAttributesSection] attribute applied to the controller indicates that the custom action filter 
        /// ValidateAttributesSection should be executed for every action method in this controller. The ValidateAttributesSection 
        /// action filter checks if a user is logged in (Session["User"] != null) and redirects them to the login page if not 
        /// authorized.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Access");
        }

    }
}