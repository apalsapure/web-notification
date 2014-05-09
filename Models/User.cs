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

        public static async Task<string> CreateUser(string username, string password, string email, string firstname, string lastname)
        {

            if (string.IsNullOrEmpty(await GetUserNameByEmail(email)) == false)
            {
                return "User with same email address already exists.";
            }

            var user = await GetUser(username, false);

            if (user == null)
            {
                //create user on appacitive
                var state = HttpContext.Current;
                try
                {
                    user = new User(username, email, password, firstname, lastname);
                    await user.Save();
                    return null;
                }
                catch
                {
                    return "Unable to connect to server. Please try again.";
                }
            }
            else
            {
                return "User with same user name already exists.";
            }
        }

        public static async Task<string> SaveSMTPSettings(string username, string password, string host, int port, bool enableSSL)
        {
            try
            {
                var user = App.UserContext.LoggedInUser;
                user.SetAttribute("smtp:username", username);
                user.SetAttribute("smtp:password", password);
                user.SetAttribute("smtp:host", host);
                user.SetAttribute("smtp:port", port.ToString());
                user.SetAttribute("smtp:ssl", enableSSL.ToString());
                await user.SaveAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region Private Helper Methods
        private async static Task<string> GetUserNameByEmail(string email)
        {
            var totalRecords = 0;
            var collection = await GetMatchingUsers(Query.Property("email").IsEqualTo(email), 0, 20);
            if (totalRecords == 0) return null;
            else
            {
                return collection[0].Username;
            }
        }

        private async static Task<List<User>> GetMatchingUsers(IQuery query, int pageIndex, int pageSize)
        {
            var users = await APUsers.FindAllAsync(query, page: pageIndex, pageSize: pageSize, orderBy: "__id", sortOrder: SortOrder.Ascending);

            var result = new List<User>();
            users.ForEach((u) =>
            {
                result.Add(u as User);
            });
            return result;
        }

        private async static Task<User> GetUser(string username, bool userIsOnline)
        {
            var totalRecords = 0;
            var collection = await GetMatchingUsers(Query.Property("username").IsEqualTo(username), 0, 20);
            if (totalRecords == 0) return null;
            else
            {
                return collection[0];
            }
        }
        #endregion

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