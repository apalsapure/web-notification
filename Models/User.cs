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

        public async Task<string> CreateUser(string username, string password, string email, string firstname, string lastname)
        {

            if (string.IsNullOrEmpty(GetUserNameByEmail(email)) == false)
            {
                return "User with same email address already exists.";
            }

            var user = GetUser(username, false);

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

        #region Private Helper Methods
        private string GetUserNameByEmail(string email)
        {
            var totalRecords = 0;
            var collection = GetMatchingUsers(Query.Property("email").IsEqualTo(email), 0, 20, out totalRecords);
            if (totalRecords == 0) return null;
            else
            {
                return collection[0].Username;
            }
        }

        private List<User> GetMatchingUsers(IQuery query, int pageIndex, int pageSize, out int totalRecords)
        {
            var state = HttpContext.Current;
            try
            {
                var task = Task.Run<PagedList<APUser>>(
                            () =>
                            {
                                HttpContext.Current = state as HttpContext;
                                return APUsers.FindAllAsync(query, page: pageIndex, pageSize: pageSize, orderBy: "__id", sortOrder: SortOrder.Ascending);
                            });

                totalRecords = task.Result.TotalRecords;
                var result = new List<User>();
                task.Result.ForEach((u) =>
                {
                    result.Add(u as User);
                });
                return result;
            }
            finally
            {
                //restore the context
                HttpContext.Current = state;
            }
        }

        private User GetUser(string username, bool userIsOnline)
        {
            var totalRecords = 0;
            var collection = GetMatchingUsers(Query.Property("username").IsEqualTo(username), 0, 20, out totalRecords);
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