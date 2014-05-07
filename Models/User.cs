using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notification.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

        public User(string username, string email, string password, string firstname, string lastname)
        {
            this.UserName = username;
            this.Email = email;
            this.Password = password;
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public static string Authenticate(string userName, string password)
        {
            return null;
        }

        public string Save()
        {
            return null;
        }

        public static string Logout()
        {
            return null;
        }
    }
}