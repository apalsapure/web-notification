using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notification.Models
{
    public class PushItem
    {
        public string Id { get; set; }
        public string To { get; set; }
        public int ToType { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public string Badge { get; set; }
        public int Expiry { get; set; }

        public DateTime Created { get; set; }
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
        private static int _maxCount = 20;
        public async static Task<List<PushItem>> LoadData(int pageCount, int pageSize)
        {
            try
            {
                var list = new List<PushItem>();

                //get the items from appacitive
                //TODO
                list.Add(new PushItem { To = "some@one.com", Message = "Push One" });
                list.Add(new PushItem { To = "some@two.com", Message = "Push Two" });
                list.Add(new PushItem { To = "some@three.com", Message = "Push Three" });
                list.Add(new PushItem { To = "some@four.com", Message = "Push Four" });
                list.Add(new PushItem { To = "some@five.com", Message = "Push Five" });
                list.Add(new PushItem { To = "some@six.com", Message = "Push Six" });
                list.Add(new PushItem { To = "some@seven.com", Message = "Push Seven" });
                list.Add(new PushItem { To = "some@eight.com", Message = "Push Eight" });
                list.Add(new PushItem { To = "some@nine.com", Message = "Push Nine" });
                list.Add(new PushItem { To = "some@ten.com", Message = "Push Ten" });
                list.Add(new PushItem { To = "some@one.com", Message = "Push One" });
                list.Add(new PushItem { To = "some@two.com", Message = "Push Two" });
                list.Add(new PushItem { To = "some@three.com", Message = "Push Three" });
                list.Add(new PushItem { To = "some@four.com", Message = "Push Four" });
                list.Add(new PushItem { To = "some@five.com", Message = "Push Five" });
                list.Add(new PushItem { To = "some@six.com", Message = "Push Six" });
                list.Add(new PushItem { To = "some@seven.com", Message = "Push Seven" });
                list.Add(new PushItem { To = "some@eight.com", Message = "Push Eight" });
                list.Add(new PushItem { To = "some@nine.com", Message = "Push Nine" });
                list.Add(new PushItem { To = "some@ten.com", Message = "Push Ten" });

                //populate empty list for paging
                var dummyList = FillDataSet(_maxCount);
                var itemsToPopulate = 0;
                var counter = pageCount * pageSize;

                while (itemsToPopulate < pageSize)
                {
                    if (counter >= _maxCount) break;
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
                //todo 
                return new PushItem { To = "some@one.com", Message = "Push One", From = "John Doe" };
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
                //TODO
                throw new NotImplementedException();
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