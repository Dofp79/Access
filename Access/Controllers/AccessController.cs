using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Access.Models;
using System.Web.Services.Description;
using System.Diagnostics;
using System.Web.Helpers;
using System.Web.UI.WebControls;

namespace Access.Controllers
{
    /// <summary>
    /// In this C# code, we have a controller class named "AccessController" within the namespace "Access.Controllers." This controller handles login and registration actions for users.
    /// The Login method returns the "Login" view when accessed with a GET request.
    /// The Registration method returns the "Registration" view when accessed with a GET request, and handles the user registration process when accessed with a POST request.
    /// The Login method handles the user login process when accessed with a POST request.
    /// The controller uses a static connection string to connect to the database and executes stored procedures to perform user registration and validation.The User model class is used to pass user information between the views and controller.
    /// The ConvertToSha256 method is a utility method used to convert the user's password to a SHA256 hash for secure storage in the database.
    /// The controller utilizes the ASP.NET MVC framework to handle the user interactions and data flow between the views and the database.
    /// </summary>
    public class AccessController : Controller
    {
        // Connection string to the database
        static string bind = "Data Source=(local);Initial Catalog=AccessDB;Integrated Security=true";

        // GET: Access/Login
        public ActionResult Login()
        {
            // Return the Login view
            return View();
        }

        // GET: Access/Registration
        public ActionResult Registration()
        {
            // Return the Registration view
            return View();
        }

        // POST: Access/Registration
        [HttpPost]
        public ActionResult Registration(User oUser)
        {
            // Initialize variables to store registration status and message
            bool registered;
            string message;

            // Check if the entered password matches the confirmation password
            if (oUser.Password == oUser.PasswordConfirmation)
            {
                // Convert the password to SHA256 hash
                oUser.Password = ConvertToSha256(oUser.Password);
            }
            else
            {
                // If passwords do not match, set an error message and return the view
                ViewData["Message"] = "Passwords do not match";
                return View();
            }

            // Create a new SqlConnection to the database
            using (SqlConnection cn = new SqlConnection(bind))
            {
                // Create a new SqlCommand to call the "sp_RegisterUser" stored procedure
                SqlCommand cmd = new SqlCommand("sp_RegisterUser", cn);

                // Set the parameters for the stored procedure
                cmd.Parameters.AddWithValue("UName", oUser.UName);
                cmd.Parameters.AddWithValue("Password", oUser.Password);
                cmd.Parameters.Add("RegisteredUser", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                // Open the database connection
                cn.Open();

                // Execute the stored procedure
                cmd.ExecuteNonQuery();

                // Get the output parameters from the stored procedure
                registered = Convert.ToBoolean(cmd.Parameters["RegisteredUser"].Value);
                message = cmd.Parameters["Message"].Value.ToString();
            }

            // Set the registration message to be displayed in the view
            ViewData["Message"] = message;

            // If registration was successful, redirect to the Login page; otherwise, stay on the Registration page
            if (registered)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {
                return View();
            }
        }

        // POST: Access/Login
        [HttpPost]
        public ActionResult Login(User oUser)
        {
            // Convert the entered password to SHA256 hash
            oUser.Password = ConvertToSha256(oUser.Password);

            // Create a new SqlConnection to the database
            using (SqlConnection cn = new SqlConnection(bind))
            {
                // Create a new SqlCommand to call the "sp_ValidateUser" stored procedure
                SqlCommand cmd = new SqlCommand("sp_ValidateUser", cn);

                // Set the parameters for the stored procedure
                cmd.Parameters.AddWithValue("UName", oUser.UName);
                cmd.Parameters.AddWithValue("Password", oUser.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                // Open the database connection
                cn.Open();

                // Execute the stored procedure and get the result (user ID)
                oUser.UID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            // If a valid user ID is returned, set the user object in the session and redirect to the Home page
            // Otherwise, set an error message and return the view
            if (oUser.UID != 0)
            {
                Session["User"] = oUser;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "User not found";
                return View();
            }
        }

        // Method to convert the input text to SHA256 hash
        private static string ConvertToSha256(string text)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
