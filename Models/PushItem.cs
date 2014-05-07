using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notification.Models
{
    public class PushItem
    {
        public PushItem()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
            From = "John Doe";
        }
        public string Id { get; private set; }
        public string To { get; set; }
        public int ToType { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public string Badge { get; set; }
        public int Expiry { get; set; }
        public string PayLoad { get; set; }
        public DateTime Created { get; set; }
        public string CreatedStr { get { return Created.ToString("ddd dd/MM/yy hh:mm tt"); } }


        private static int _maxCount = 20;
        public static List<Notification.Models.PushItem> LoadData(int pageCount, int pageSize)
        {
            var list = new List<PushItem>();
            list.Add(new PushItem { To = "some@one.com", Message = "Push One", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@two.com", Message = "Push Two", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@three.com", Message = "Push Three", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@four.com", Message = "Push Four", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@five.com", Message = "Push Five", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@six.com", Message = "Push Six", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@seven.com", Message = "Push Seven", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@eight.com", Message = "Push Eight", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@nine.com", Message = "Push Nine", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });
            list.Add(new PushItem { To = "some@ten.com", Message = "Push Ten", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" });

            var dummyList = FillDataSet(_maxCount);
            var itemsToPopulate = pageSize;
            var counter = pageCount * pageSize;
            while (itemsToPopulate-- > 0)
            {
                if (counter >= _maxCount) break;
                dummyList[counter] = list[itemsToPopulate];
                counter++;
            }

            return dummyList;
        }

        public static PushItem Fetch(string id)
        {
            return new PushItem { To = "some@one.com", Message = "Push One", PayLoad = "{\"data\":{\"alert\":\"Hello\",\"badge\":\"2\"},\"platformoptions\":{\"ios\":{},\"android\":{},\"wp\":{\"notificationtype\":\"toast\",\"text1\":\"Amar\",\"text2\":\"Hello\",\"navigatepath\":\"MainPage.xaml\"}},\"broadcast\":true,\"expireafter\":\"\"}" };
        }

        public string Save()
        {
            try
            {
                //save to data store

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
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
    }
}