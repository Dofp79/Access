using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Access.Models
{
    /// <summary>
    /// In this C# code, we have a model class named "User" within the namespace "Access.Models." A model class is used to 
    /// represent data and define the structure of objects used in an application.
    /// </summary>
    public class User
    {
        // Property representing the unique identifier of the user
        public int UID { get; set; }

        // Property representing the username of the user
        public string UName { get; set; }

        // Property representing the password of the user
        public string Password { get; set; }

        // Property representing the user's entered password confirmation during registration
        public string PasswordConfirmation { get; set; }
    }
}
