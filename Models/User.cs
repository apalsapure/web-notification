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

        public User(string username, string email, string password, string firstname, string lastname)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        //authenticate user
        public async static Task<string> Authenticate(string userName, string password)
        {
            try
            {
                //authenticate user on Appacitive
                var credentials = new UsernamePasswordCredentials(userName, password)
                {
                    TimeoutInSeconds = int.MaxValue,
                    MaxAttempts = int.MaxValue
                };

                await Appacitive.Sdk.AppContext.LoginAsync(credentials);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }

        //signup user
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

        //save SMTP settings in user attributes
        public static async Task<string> SaveSMTPSettings(string username, string password, string host, int port, bool enableSSL)
        {
            try
            {
                var user = AppContext.UserContext.LoggedInUser;
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
                if (ExceptionPolicy.HandleException(ex))
                    throw;
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
            var users = await APUsers.FindAllAsync(query, pageNumber: pageIndex, pageSize: pageSize, orderBy: "__id", sortOrder: SortOrder.Ascending);

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

        //save user
        public async Task<string> Save()
        {
            try
            {
                await this.SaveAsync();
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex))
                    throw;
                return ex.Message;
            }
        }

        //logs out user
        public static string Logout()
        {
            try
            {
                //Logout user
                Appacitive.Sdk.AppContext.LogoutAsync();
            }
            catch (Exception ex) { return ex.Message; }
            return null;
        }
    }
}