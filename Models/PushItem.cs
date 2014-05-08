﻿using Appacitive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notification.Models
{
    public class PushItem : APObject
    {
        public const string APPACITIVE_TYPE = "push";

        public PushItem() : base(APPACITIVE_TYPE) { }

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
        public string CreatedStr { get { return Created.ToString("ddd dd/MM/yy hh:mm tt"); } }


        public async static Task<List<PushItem>> LoadData(int pageCount, int pageSize)
        {
            try
            {
                var list = new List<PushItem>();

                //get the items from appacitive
                var result = await APObjects.FindAllAsync(APPACITIVE_TYPE, page: pageCount, pageSize: pageSize);
                result.ForEach((r) => { list.Add(r as PushItem); });

                //populate empty list for paging
                var dummyList = FillDataSet(result.TotalRecords);
                var itemsToPopulate = pageSize;
                var counter = pageCount * pageSize;
                while (itemsToPopulate-- > 0)
                {
                    if (counter >= result.TotalRecords) break;
                    dummyList[counter] = list[itemsToPopulate];
                    counter++;
                }

                return dummyList;
            }
            catch { return null; }
        }

        public async static Task<PushItem> Fetch(string id)
        {
            try
            {
                return await APObjects.GetAsync(APPACITIVE_TYPE, id) as PushItem;
            }
            catch { return null; }
        }

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
                                .FromExistingObject("user", App.UserContext.LoggedInUser.Id)
                                .ToNewObject("push", this)
                                .SaveAsync();
                await this.SaveAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        private async Task<string> SendPush()
        {
            try
            {
                PushNotification push = null;
                var message = string.Format("{0}: {1}", this.From, this.Message);
                switch (this.ToType)
                {
                    case 0: push = PushNotification.ToChannels(message, this.To.Split(',')); break;
                    case 1: push = PushNotification.ToDeviceIds(message, this.To.Split(',')); break;
                    default: push = PushNotification.Broadcast(message); break;
                }
                if (string.IsNullOrEmpty(this.Badge) == false) push = push.WithBadge(this.Badge);
                push = push.WithExpiry(push.ExpiryInSeconds);

                //for this sample we will send toast notification
                var toast = new ToastNotification(this.From, this.Message, "/MainPage.xaml");
                push.WithPlatformOptions(WindowsPhoneOptions.WithToast(toast));

                //send push
                await push.SendAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region Private Helper Methods
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