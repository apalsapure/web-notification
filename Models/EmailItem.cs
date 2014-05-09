using Appacitive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notification.Models
{
    public class EmailItem : APObject
    {
        public const string APPACITIVE_TYPE = "email";

        public EmailItem() : base(APPACITIVE_TYPE) { }

        public EmailItem(APObject existing) : base(existing) { }

        public string To
        {
            get { return base.Get<string>("to"); }
            set { base.Set<string>("to", value); }
        }
        public string CC
        {
            get { return base.Get<string>("cc"); }
            set { base.Set<string>("cc", value); }
        }
        public string From
        {
            get { return base.Get<string>("from"); }
            set { base.Set<string>("from", value); }
        }
        public string Subject
        {
            get { return base.Get<string>("subject"); }
            set { base.Set<string>("subject", value); }
        }
        public string Body
        {
            get { return base.Get<string>("body"); }
            set { base.Set<string>("body", value); }
        }
        public string TemplateName
        {
            get { return base.Get<string>("templatename"); }
            set { base.Set<string>("templatename", value); }
        }
        public bool WithCustomSettings
        {
            get { return base.Get<bool>("withcustomsettings"); }
            set { base.Set<bool>("withcustomsettings", value); }
        }
        public Dictionary<string, string> PlaceHolders { get; set; }
        public DateTime Created { get { return base.CreatedAt; } }
        public string CreatedShortStr
        {
            get
            {
                if (Created.Date == DateTime.Now.Date) return Created.ToString("hh:mm tt");
                else return Created.ToString("ddd dd/MM/yy hh:mm tt");
            }
        }
        public string CreatedStr { get { return Created.ToString("ddd dd/MM/yy hh:mm tt"); } }

        public async static Task<List<EmailItem>> LoadData(int pageCount, int pageSize)
        {
            try
            {
                var list = new List<EmailItem>();

                //get the items from appacitive
                var result = await App.UserContext.LoggedInUser.GetConnectedObjectsAsync("user_email",
                                                                                        pageNumber: pageCount + 1,
                                                                                        pageSize: pageSize,
                                                                                        orderBy: "__id",
                                                                                        sortOrder: SortOrder.Descending);
                result.ForEach((r) => { list.Add(r as EmailItem); });

                //populate empty list for paging
                var dummyList = FillDataSet(result.TotalRecords);
                var itemsToPopulate = 0;
                var counter = pageCount * pageSize;

                while (itemsToPopulate < pageSize)
                {
                    if (counter >= result.TotalRecords) break;
                    dummyList[counter] = list[itemsToPopulate];
                    counter++;
                    itemsToPopulate++;
                }
                return dummyList;
            }
            catch { }
            return null;
        }

        public async static Task<EmailItem> Fetch(string id)
        {
            try
            {
                return await APObjects.GetAsync(APPACITIVE_TYPE, id) as EmailItem;
            }
            catch { return null; }
        }

        public async Task<string> Save()
        {
            try
            {
                var result = await SendEmail();
                if (string.IsNullOrEmpty(result) == false) return result;
                if (PlaceHolders != null && PlaceHolders.Count > 0)
                {
                    foreach (var key in PlaceHolders.Keys)
                        this.SetAttribute(key, PlaceHolders[key]);
                }

                //as we need to store this email object in context of user
                //we will create a connection between user and the email object
                //when connection is saved, email object is automatically created
                await Appacitive.Sdk.APConnection
                                .New("user_email")
                                .FromExistingObject("user", App.UserContext.LoggedInUser.Id)
                                .ToNewObject("email", this)
                                .SaveAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        #region Private Helper Methods
        private static List<EmailItem> FillDataSet(int maxRecords)
        {
            var list = new List<EmailItem>();
            while (maxRecords-- > 0)
            {
                list.Add(new EmailItem());
            }
            return list;
        }
        private async Task<string> SendEmail()
        {
            try
            {
                var to = new List<string>();
                var cc = new List<string>(); ;
                to.AddRange(this.To.Split(','));
                if (string.IsNullOrEmpty(this.CC) == false) cc.AddRange(this.CC.Split(','));
                var email = NewEmail
                                .Create(this.Subject)
                                .To(to, cc)
                                .From(this.From);
                if (string.IsNullOrEmpty(this.TemplateName))
                    email = email.WithBody(this.Body, true);
                else
                    email.WithTemplateBody(this.TemplateName, this.PlaceHolders, true);

                //use custom SMPT settings
                if (this.WithCustomSettings)
                {
                    email.Server = new SmtpServer();
                    email.Server.Username = App.UserContext.LoggedInUser.GetAttribute("smtp:username");
                    email.Server.Password = App.UserContext.LoggedInUser.GetAttribute("smtp:password");
                    email.Server.Host = App.UserContext.LoggedInUser.GetAttribute("smtp:host");
                    email.Server.Port = int.Parse(App.UserContext.LoggedInUser.GetAttribute("smtp:port"));
                    email.Server.EnableSSL = bool.Parse(App.UserContext.LoggedInUser.GetAttribute("smtp:ssl"));
                }

                await email.SendAsync();
            }
            catch (Exception ex) { return ex.Message; }
            return null;
        }
        #endregion
    }
}