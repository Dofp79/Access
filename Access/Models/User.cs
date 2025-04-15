/*
    ---------------------------------------------------------------
    Class: User
    Description:
        This model class defines the structure for user-related data 
        used in the ASP.NET MVC application. It represents a user 
        object and is primarily used for handling login and registration 
        processes.

        The class includes properties for:
        - UID: Unique identifier of the user (primary key)
        - UName: Username
        - Password: User's password (hashed before storage)
        - PasswordConfirmation: Used during registration to verify the password

    Usage:
        - Passed between views and controllers
        - Serves as a data carrier for authentication logic

    Namespace: Access.Models
    ---------------------------------------------------------------
*/

using System;

namespace Access.Models
{
    public class User
    {
        // Unique identifier for the user (used as a primary key)
        public int UID { get; set; }

        // Username of the user
        public string UName { get; set; }

        // User's password (stored as SHA256 hash in the database)
        public string Password { get; set; }

        // Password confirmation used during registration to verify password match
        public string PasswordConfirmation { get; set; }
    }
}
