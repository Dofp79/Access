/*
    ---------------------------------------------------------------
    Class: AccessController
    Description:
        This controller manages user authentication functionalities 
        (login and registration) in an ASP.NET MVC application.

        Features:
        - Displays the login and registration pages (GET requests)
        - Handles user registration by calling the 'sp_RegisterUser' stored procedure (POST)
        - Handles user login by validating credentials via the 'sp_ValidateUser' stored procedure (POST)
        - Stores user session upon successful login
        - Passwords are hashed using SHA256 before being stored or validated
        - Uses a static SQL Server connection string for database operations

    Dependencies:
        - User model (Access.Models.User)
        - SQL Server stored procedures: sp_RegisterUser, sp_ValidateUser
        - System.Security.Cryptography for password hashing

    Target Framework: ASP.NET MVC
    ---------------------------------------------------------------
*/

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

namespace Access.Controllers
{
    public class AccessController : Controller
    {
        // Connection string for SQL Server database
        static string bind = "Data Source=(local);Initial Catalog=AccessDB;Integrated Security=true";

        // GET: Access/Login
        public ActionResult Login()
        {
            return View(); // Show login page
        }

        // GET: Access/Registration
        public ActionResult Registration()
        {
            return View(); // Show registration page
        }

        // POST: Access/Registration - Handle new user registration
        [HttpPost]
        public ActionResult Registration(User oUser)
        {
            bool registered;
            string message;

            // Check password confirmation
            if (oUser.Password == oUser.PasswordConfirmation)
            {
                oUser.Password = ConvertToSha256(oUser.Password); // Hash password
            }
            else
            {
                ViewData["Message"] = "Passwords do not match";
                return View();
            }

            // Use ADO.NET to call stored procedure
            using (SqlConnection cn = new SqlConnection(bind))
            {
                SqlCommand cmd = new SqlCommand("sp_RegisterUser", cn);
                cmd.Parameters.AddWithValue("UName", oUser.UName);
                cmd.Parameters.AddWithValue("Password", oUser.Password);
                cmd.Parameters.Add("RegisteredUser", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();

                // Read results from stored procedure
                registered = Convert.ToBoolean(cmd.Parameters["RegisteredUser"].Value);
                message = cmd.Parameters["Message"].Value.ToString();
            }

            ViewData["Message"] = message;

            return registered ? RedirectToAction("Login", "Access") : View();
        }

        // POST: Access/Login - Validate user login
        [HttpPost]
        public ActionResult Login(User oUser)
        {
            oUser.Password = ConvertToSha256(oUser.Password); // Hash password

            using (SqlConnection cn = new SqlConnection(bind))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidateUser", cn);
                cmd.Parameters.AddWithValue("UName", oUser.UName);
                cmd.Parameters.AddWithValue("Password", oUser.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                oUser.UID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if (oUser.UID != 0)
            {
                Session["User"] = oUser; // Store user in session
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "User not found";
                return View();
            }
        }

        // Helper: Convert string to SHA256 hash
        private static string ConvertToSha256(string text)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2")); // Convert byte to hex
            }

            return Sb.ToString();
        }
    }
}
