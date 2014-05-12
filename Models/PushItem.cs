using Appacitive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notification.Models
{
    public class PushItem : APObject
    {
        public PushItem() : base("push") { }

        public PushItem(APObject existing) : base(existing) { }

        public string To
        {
            get { return base.Get<string>("to"); }
            set { base.Set<string>("to", value); }
        }
        public int ToType
        {
            get { return base.Get<int>("totype"); }
            set { base.Set<int>("totype", value); }
        }
        public string From
        {
            get { return base.Get<string>("from"); }
            set { base.Set<string>("from", value); }
        }
        public string Message
        {
            get { return base.Get<string>("message"); }
            set { base.Set<string>("message", value); }
        }
        public string Badge
        {
            get { return base.Get<string>("badge"); }
            set { base.Set<string>("badge", value); }
        }
        public int Expiry
        {
            get { return base.Get<int>("expiry"); }
            set { base.Set<int>("expiry", value); }
        }

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

        //get the data for the specified page number
        public async static Task<List<PushItem>> LoadData(int pageCount, int pageSize)
        {
            try
            {
                var list = new List<PushItem>();

                //get the items from appacitive
                var user = AppContext.UserContext.LoggedInUser;
                var result = await user.GetConnectedObjectsAsync("user_push",
                                                                fields: new[] { "to", "message", "__utcdatecreated" },
                                                                pageNumber: pageCount + 1,
                                                                pageSize: pageSize,
                                                                orderBy: "__id",
                                                                sortOrder: SortOrder.Descending);
                result.ForEach((r) => { list.Add(r as PushItem); });

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
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex))
                    throw;
                return null;
            }
        }

        //get the push item details
        public async static Task<PushItem> Fetch(string id)
        {
            try
            {
                return await APObjects.GetAsync("push", id) as PushItem;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex))
                    throw;
                return null;
            }
        }

        //save the push item in context of logged in user
        public async Task<string> Save()
        {
            try
            {
                var result = await SendPush();
                if (string.IsNullOrEmpty(result) == false) return result;
                //as we need to store this push object in context of user
                //we will create a connection between user and the push object
                //when connection is saved, push object is automatically created
                await Appacitive.Sdk.APConnection
                                .New("user_push")
                                .FromExistingObject("user", AppContext.UserContext.LoggedInUser.Id)
                                .ToNewObject("push", this)
                                .SaveAsync();
                await this.SaveAsync();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex))
                    throw;
                return ex.Message;
            }
            return null;
        }

        #region Private Helper Methods
        private async Task<string> SendPush()
        {
            try
            {
                PushNotification push = null;
                var message = string.Format("{0}: {1}", this.From, this.Message);

                //depending upon type of recipients we will construct the push object
                switch (this.ToType)
                {
                    case 0: push = PushNotification.ToChannels(message, this.To.Split(',')); break;
                    case 1: push = PushNotification.ToDeviceIds(message, this.To.Split(',')); break;
                    default: push = PushNotification.Broadcast(message); this.To = "Broadcast"; break;
                }

                //set the badge
                if (string.IsNullOrEmpty(this.Badge) == false) push = push.WithBadge(this.Badge);

                //set the expiry
                if (push.ExpiryInSeconds > 0) push = push.WithExpiry(push.ExpiryInSeconds);

                //for this sample we will send toast notification
                var toast = new ToastNotification(this.From, this.Message, "/MainPage.xaml");
                push.WithPlatformOptions(WindowsPhoneOptions.WithToast(toast));

                //send push
                await push.SendAsync();

                //using same push object we will send a tile notification
                //this will show a badge on the tile
                var tile = TileNotification.CreateNewFlipTile(new FlipTile
                {
                    BackContent = this.From,
                    BackTitle = "New Message",
                    FrontCount = "+0"
                });
                push.WithPlatformOptions(WindowsPhoneOptions.WithTile(tile));

                //send the push notification
                await push.SendAsync();

                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex))
                    throw;
                return ex.Message;
            }
        }

        private static List<PushItem> FillDataSet(int maxRecords)
        {
            var list = new List<PushItem>();
            while (maxRecords-- > 0)
            {
                list.Add(new PushItem());
            }
            return list;
        }
        #endregion
    }
}