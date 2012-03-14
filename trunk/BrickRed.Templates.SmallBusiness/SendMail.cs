/*
===========================================================================
Copyright (c) 2010 BrickRed Technologies Limited

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sub-license, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
===========================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace BrickRed.Templates.SmallBusiness
{
    public class SendMail
    {
        #region Send Mail methods
        
        /// ***********************************************************************************************
        /// <summary>
        /// Sends mail/query of the user to site admin or to the user specified in the web part property
        /// </summary>
        /// <param name="FromName">Name of sender</param>
        /// <param name="FromCompany">Company of sender</param>
        /// <param name="FromPhone">Contact number of sender</param>
        /// <param name="FromEmail">Email of sender</param>
        /// <param name="Body">Requirement/message of sender</param>
        /// <param name="IsBodyHtml">Is the message HTML or not</param>
        /// <param name="To">Receivers email address</param>
        /// <returns></returns>
        /// ***********************************************************************************************
        
        public bool SendQuery(string FromName, string FromCompany, string FromPhone, string FromEmail, string Body, bool IsBodyHtml, string To, string AdminOrSelf)
        {
            bool mailSent = false;
            string message = string.Empty;
            MailMessage mailMessage = null;
            string subject = string.Empty;

            #region Get Mail body from template and replace tokens
            message = GetMailBodyFromConfigList(AdminOrSelf);
            message = message.Replace("#FROMNAME#", FromName);
            message = message.Replace("#FROMCOMPANY#", FromCompany);
            message = message.Replace("#FROMPHONE#", FromPhone);
            message = message.Replace("#FROMEMAIL#", FromEmail);
            message = message.Replace("#BODY#", Body);
            #endregion

            #region Get Mail subject from configuration list and replace tokens

            subject = GetMailSubjectFromConfigList(AdminOrSelf);
            subject = subject.Replace("#FROMNAME#", FromName);
            subject = subject.Replace("#FROMCOMPANY#", FromCompany);
            subject = subject.Replace("#FROMPHONE#", FromPhone);
            subject = subject.Replace("#FROMEMAIL#", FromEmail);
            subject = subject.Replace("#BODY#", Body);

            #endregion

            #region Send Mail

            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = SPContext.Current.Site.WebApplication.OutboundMailServiceInstance.Server.Address;
                mailMessage = CreateMailMessasge(FromEmail, FromName, FromEmail, To, subject, "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"><HTML><head></head><body style='margin: 0; padding: 0;'> " + message + "</body></HTML>");       
                mailMessage.IsBodyHtml = IsBodyHtml;
                smtpClient.Send(mailMessage);
                mailSent = true;
            }
            catch (Exception) { return mailSent; }
            return mailSent;

            #endregion
        }

        /// *******************************************************************************
        /// <summary>
        /// This method gets mail subject from Configuration list
        /// </summary>
        /// <param name="AdminOrSelf">Whether mail is being sent to Admin or Self</param>
        /// <returns>Subject as string</returns>
        /// *******************************************************************************
        
        private string GetMailSubjectFromConfigList(string AdminOrSelf)
        {
            string mailSubject = string.Empty;
            string query = string.Empty;
            SPQuery cmlQuery = new SPQuery();
            SPListItemCollection items = null;
            SPListItem mailItem;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        //Get the configurations list to read mail Subject
                        SPList configList = web.Lists[Constants.CONFIG_LIST];

                        //If AdminOrSelf=Admin
                        if (AdminOrSelf == "Admin")
                        {
                            //Create CAML query
                            query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + Constants.CONFIG_LIST_ADMINMAILSUBJECT + "</Value></Eq></Where>";
                        }
                        else if (AdminOrSelf == "Self")
                        {
                            //Create CAML query
                            query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + Constants.CONFIG_LIST_USERMAILSUBJECT + "</Value></Eq></Where>";
                        }

                        cmlQuery.Query = query;
                        //fetch items by executing caml query
                        items = configList.GetItems(cmlQuery);
                        if (items.Count > 0)
                        {
                            mailItem = items[0];
                            mailSubject = mailItem["Value"].ToString();
                        }
                    }
                }
            });
            return mailSubject;
        }

        /// *******************************************************************************
        /// <summary>
        /// This method gets Mail Body from Configuration list
        /// </summary>
        /// <param name="AdminOrSelf">Whether mail is being sent to Admin or Self</param>
        /// <returns>Mail body as string</returns>
        /// *******************************************************************************

        private string GetMailBodyFromConfigList(string AdminOrSelf)
        {
            string mailBody = string.Empty;
            string query = string.Empty;
            SPQuery cmlQuery = new SPQuery();
            SPListItemCollection items = null;
            SPListItem mailItem;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        //Get the configurations list to read mail body
                        SPList configList = web.Lists[Constants.CONFIG_LIST];
                        
                        //If AdminOrSelf=Admin
                        if (AdminOrSelf == "Admin")
                        {
                            //Create CAML query
                            query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + Constants.CONFIG_LIST_ADMINMAILVALUE + "</Value></Eq></Where>";                           
                        }
                        else if (AdminOrSelf == "Self")
                        {
                            //Create CAML query
                            query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + Constants.CONFIG_LIST_USERMAILVALUE + "</Value></Eq></Where>";                                                   
                        }

                        cmlQuery.Query = query;
                        //fetch items by executing caml query
                        items = configList.GetItems(cmlQuery);
                        if (items.Count > 0)
                        {
                            mailItem = items[0];
                            mailBody = mailItem["Value"].ToString();
                        }
                    }
                }
            });
            return mailBody;
        }

        /// ********************************************************************************
        /// <summary>
        /// Creates mailAddress object out of the addresses passed 
        /// as string array and adds these to the MailAddressCollection.
        /// </summary>
        /// <param name="from">Address of the sender.</param>
        /// <param name="fromDisplayName">Name to be displayed in the From section 
        ///     of mail.</param>
        /// <param name="replyTo">Address where the reply to this mail will go.</param>
        /// <param name="to">Address of the recepient mailbox</param>
        /// <param name="subject">Subject of the mail.</param>
        /// <param name="body">Body or content of the mail.</param>
        /// <returns>System.Net.Mail.MailMessage object</returns>
        /// ********************************************************************************
        
        private static MailMessage CreateMailMessasge(string from, string fromDisplayName, string replyTo, string to,  string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            //Add subject
            mailMessage.Subject = subject;

            //Add body
            mailMessage.Body = body;
            char[] splitterChar = { ',' };

            //Add replyTo Mail ID
            if (replyTo != null && !replyTo.Equals(string.Empty))
                mailMessage.ReplyTo = new MailAddress(replyTo);

            //Add To Mail IDs
            if (to != null && !to.Equals(string.Empty))
                AddMailAddresses(mailMessage.To, to.Split(splitterChar, System.StringSplitOptions.RemoveEmptyEntries));
             
            //Add From Mail ID
            mailMessage.From = new MailAddress(from, fromDisplayName);

            return mailMessage;
        }

        /// ********************************************************************************
        /// <summary>
        /// Creates mailAddress object out of the addresses passed 
        ///     as string array and adds these to the MailAddressCollection
        /// </summary>
        /// <param name="addressCollection">Address collection where the mail 
        ///     addresses will be added.</param>
        /// <param name="mailIDs">Array of strings containing mail addresses.</param>
        /// ********************************************************************************
        
        private static void AddMailAddresses(MailAddressCollection addressCollection, string[] mailIDs)
        {
            foreach (string mailID in mailIDs)
            {
                MailAddress address = new MailAddress(mailID.Trim());
                addressCollection.Add(address);
            }
        }
      

        #endregion
    }
}
