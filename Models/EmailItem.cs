using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notification.Models
{
    public class EmailItem
    {
        public EmailItem()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
            From = "noreply@appacitive.com";
        }
        public string Id { get; private set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> PlaceHolders { get; set; }
        public DateTime Created { get; set; }
        public string CreatedStr { get { return Created.ToString("ddd dd/MM/yy hh:mm tt"); } }

        private static int _maxCount = 20;
        public static List<EmailItem> LoadData(int pageCount, int pageSize)
        {
            var list = new List<EmailItem>();
            list.Add(new EmailItem { To = "some@one.com", CC = "john@doe.com", Subject = "Email One", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@two.com", CC = "john@doe.com", Subject = "Email Two", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@three.com", CC = "john@doe.com", Subject = "Email Three", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@four.com", CC = "john@doe.com", Subject = "Email Four", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@five.com", CC = "john@doe.com", Subject = "Email Five", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@six.com", CC = "john@doe.com", Subject = "Email Six", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@seven.com", CC = "john@doe.com", Subject = "Email Seven", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@eight.com", CC = "john@doe.com", Subject = "Email Eight", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@nine.com", CC = "john@doe.com", Subject = "Email Nine", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });
            list.Add(new EmailItem { To = "some@ten.com", CC = "john@doe.com", Subject = "Email Ten", Body = "Mauris eros massa, malesuada eu augue nec, commodo porttitor mauris. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin a cursus eros. Sed nec tincidunt purus. Sed fringilla ipsum at elit cursus, nec cursus ante ultricies." });

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

        public static EmailItem Fetch(string id)
        {
            return new EmailItem
            {
                To = "some@one.com",
                CC = "john@doe.com",
                Subject = "Email One",
                Body = "<div style='font-size: 15px; font-family: \"Helvetica Neue\", \"Helvetica\", Helvetica, Arial, sans-serif; line-height: 24px; margin: 0; padding: 0; color: #555555'> <div style='margin: 0 auto;'> <table width=\"100%\" border=\"0\" style='background-color: #3A3A3A; border-bottom: solid 2px #00B0F0; height: 50px; width: 100%;'> <tr> <td> <div style=\"max-width: 625px; margin: 0 auto; padding: 0 12px\"> <table width=\"100%\" border=\"0\" style=\"width: 100%;\"> <tr> <td style=\"width: 85%;\"> <h2 style='margin: 0; padding: 4px 0; font-weight: normal'><a style='color: #FFF; text-decoration: none; font-size: 28px; line-height: 36px;' href='[#loginUrl]'>appacitive</a></h2> </td> <td style=\"min-width: 80px;\"><a href='[#activateUrl]' style='background-color: #0093C8; display: inline-block; float: right; font-size: 14px; width: 70px; line-height: 34px; text-align: center; text-decoration: none; color: #FFF;'>Verify</a></td> </tr> </table> </div> </td> </tr> </table> </div> <div style=\"max-width: 625px; margin: 0 auto;\"> <div style='padding: 12px 8px;'> <div style='margin: 10px 0 10px 0; float: left; width: 100%;'> <div style='float: left;'> <h2 style='font-size: 22px; line-height: 26px; padding: 0; margin: 0; color: #0093C8; font-weight: normal;'>Welcome to Appacitive!</h2> </div> </div> <div style='clear: both; padding-top: 5px;'>Hi [#userfullname]!</div> <p style='margin: 15px 0 15px;'>Your account has been created and is ready for use. Please verify your email address by clicking the following url <a style=\"color: #d0695b\" href='[#activateUrl]'>[#activateUrl]</a>.</p> <p style='margin: 15px 0 15px;'>Incase this does not work, you can copy the url and paste it in your browser address bar to activate the email verification process. This is a mandatory one time activity only and post verification you can access your administration console at <a style=\"color: #d0695b\" href='[#loginUrl]'>[#loginUrl]</a>. </p> <div style=\"margin-bottom: 15px;\"> <p style='margin: 0; padding: 15px 15px 10px; background-color: #ECF8FF;'>Here's what you will need to get started immediately:</p> <ul style='margin: 0; padding: 0 15px 25px 50px; background-color: #ECF8FF; list-style-type: square;'> <li>Your username is <strong>[#username]</strong></li> <li>Your Appacitive account name is <strong>[#accName]</strong></li> </ul> </div> <h3 style='font-size: 20px; line-height: 32px; padding: 0; margin: 0; color: #0093C8; font-weight: normal;'>Getting Started</h3> <p style='margin: 10px 0 30px;'> If your have any questions, you can look up online help at <a style=\"color: #d0695b\" href='http://docs.appacitive.com/'>our documentation site</a>, or send us an email at <a style=\"color: #d0695b\" href='mailto:support@appacitive.com'>support@appacitive.com</a>. <br /> </p> <div style='display: inline-block; float: left; font-size: 16px; font-weight: bold; text-align: left; color: #333'> Thanks, <br /> The Appacitive Team </div> <br /> <br /> <br /> <table width=\"100%\" style=\"background-color: #ebebeb; padding: 15px 18px 0\" border=\"0\"> <tr> <td> <table align=\"left\" style=\"min-width: 300px; width: 50%; padding-right: 35px;\" border=\"0\"> <tr> <td> <h5 style=\"font-size: 16px; color: #444; margin: 0\">Connect with Us:</h5> <div style=\"margin: 13px 0 10px\"> <a href=\"#\" style=\"background-color: #3B5998; line-height: 24px; font-size: 12px; text-decoration: none; color: #FFF; width: 100%; display: inline-block; text-align: center;\">Facebook</a> </div> <div style=\"margin: 0 0 10px\"> <a href=\"#\" style=\"background-color: #1daced; line-height: 24px; font-size: 12px; text-decoration: none; color: #FFF; width: 100%; display: inline-block; text-align: center;\">Twitter</a> </div> <div style=\"margin: 0 0 20px\"> <a href=\"#\" style=\"background-color: #DB4A39; line-height: 24px; font-size: 12px; text-decoration: none; color: #FFF; width: 100%; display: inline-block; text-align: center;\">Google+</a> </div> </td> </tr> </table> <table align=\"left\" style=\"min-width: 250px; width: 40%;\" border=\"0\"> <tr> <td> <h5 style=\"font-size: 15px; color: #444; margin: 0;\">Contact Info:</h5> <p style=\"font-size: 15px; line-height: 28px; margin: 8px 0 10px\"> Phone: <strong>+91 997 094 6639</strong><br /> Email: <strong><a style=\"color: #d0695b\" href=\"emailto:connect@appacitive.com\">connect@appacitive.com</a></strong> </p> </td> </tr> </table> </td> </tr> </table> <br /> <div style='clear: both; padding: 5px; border-top: solid 1px #888; color: #888; font-size: 10px; line-height: 14px;'>Please do not reply to this message; it was sent from an unmonitored email address. This message is a service email related to your request. For general inquiries or to request support, please contact our Support. </div> <br /> </div> </div> </div> ",
                TemplateName = null,//"welcome_appacitive",
                PlaceHolders = new Dictionary<string, string>
                {
                    { "activateUrl", "http://google.com" },
                    { "loginUrl", "http://appacitive.com" }
                }

            };
        }

        public string Save()
        {
            try
            {
                //save to data store

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        private static List<EmailItem> FillDataSet(int maxRecords)
        {
            var list = new List<EmailItem>();
            while (maxRecords-- > 0)
            {
                list.Add(new EmailItem());
            }
            return list;
        }

    }
}