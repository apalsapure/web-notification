using Appacitive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Notification.Models
{
    public class AppacitiveMembershipProvider : MembershipProvider
    {
        private string _applicationName = "appacitive-push-demo";
        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return CreateUser(username, password, email, null, null, out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMatchingUsers(Query.Property("email").Like(emailToMatch), pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMatchingUsers(Query.Property("username").Like(usernameToMatch), pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var totalRecords = 0;
            var collection = GetMatchingUsers(Query.Property("username").IsEqualTo(username), 0, 20, out totalRecords);
            if (totalRecords == 0) return null;
            else
            {
                var list = new MembershipUser[collection.Count];
                collection.CopyTo(list, 0);
                return list[0];
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            var totalRecords = 0;
            var collection = GetMatchingUsers(Query.Property("email").IsEqualTo(email), 0, 20, out totalRecords);
            if (totalRecords == 0) return null;
            else
            {
                var list = new MembershipUser[collection.Count];
                collection.CopyTo(list, 0);
                return list[0].UserName;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var task = Task.Run<string>(() => User.Authenticate(username, password));
            return string.IsNullOrEmpty(task.Result);
        }

        #region Public Methods
        public MembershipUser CreateUser(string username, string password, string email, string firstname, string lastname, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if ((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser membershipUser = GetUser(username, false);

            if (membershipUser == null)
            {
                //create user on appacitive
                var user = new User(username, email, password, firstname, lastname);
                var task = Task.Run<string>(() => user.Save());
                if (string.IsNullOrEmpty(task.Result))
                {
                    status = MembershipCreateStatus.Success;
                    return GetUser(username, false);
                }
                else
                {
                    status = MembershipCreateStatus.ProviderError;
                    return null;
                }                
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
        }
        #endregion

        #region Private Helper Methods
        private MembershipUserCollection GetMatchingUsers(IQuery query, int pageIndex, int pageSize, out int totalRecords)
        {
            var task = Task.Run<PagedList<APUser>>(() => APUsers.FindAllAsync(query, page: pageIndex, pageSize: pageSize, orderBy: "__id", sortOrder: SortOrder.Ascending));

            totalRecords = task.Result.TotalRecords;
            var result = new MembershipUserCollection();
            task.Result.ForEach((u) =>
            {
                result.Add(u.ToMembershipUser());
            });
            return result;
        }
        #endregion
    }

    public static class MembershipProviderExtensions
    {
        private static readonly string _providerName = "appacitive-sdk";
        public static MembershipUser ToMembershipUser(this APUser user)
        {
            return new MembershipUser(_providerName, user.Username, user.Id, user.Email, null, null, true, false, user.CreatedAt, user.LastUpdatedAt, user.LastUpdatedAt, DateTime.MinValue, DateTime.MinValue);
        }
    }
}