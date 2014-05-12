using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notification.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

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
                //TODO
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
                //get the user from the context
                //and set the SMTP settings in the attribute
                //TODO
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
        //get username by email
        private async static Task<string> GetUserNameByEmail(string email)
        {
            //TODO
            throw new NotImplementedException();
        }

        //get user by username
        private async static Task<User> GetUser(string username, bool userIsOnline)
        {
            //TODO
            throw new NotImplementedException();
        }

        //helper function which executes the given query and returns matching users
        //private async static Task<List<User>> GetMatchingUsers(IQuery query, int pageIndex, int pageSize)
        //{
        //    //TODO
        //    throw new NotImplementedException();
        //}
        #endregion

        //save user
        public async Task<string> Save()
        {
            try
            {
                //save in the backend
                //TODO
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
                //TODO
            }
            catch (Exception ex) { return ex.Message; }
            return null;
        }
    }
}