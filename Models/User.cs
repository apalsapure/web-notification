using Appacitive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notification.Models
{
    public class User : APUser
    {
        public User() : base() { }

        public User(APUser existing) : base(existing) { }

        public const string APPACITIVE_TYPE = "user";

        public User(string username, string email, string password, string firstname, string lastname)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public async static Task<string> Authenticate(string userName, string password)
        {
            try
            {
                await App.LoginAsync(new UsernamePasswordCredentials(userName, password)); ;
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }

        public async Task<string> Save()
        {
            try
            {
                await this.SaveAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Logout()
        {
            return null;
        }
    }
}