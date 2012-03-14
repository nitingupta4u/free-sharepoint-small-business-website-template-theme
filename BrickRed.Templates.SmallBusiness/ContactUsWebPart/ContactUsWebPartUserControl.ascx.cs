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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Web;
using System.Drawing;

namespace BrickRed.Templates.SmallBusiness.ContactUsWebPart
{
    public partial class ContactUsWebPartUserControl : UserControl
    {       
        public ContactUsWebPart UserEmailProp { get; set; }

        bool IsMailSent = false;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            btnContactMe.ImageUrl = SPContext.Current.Web.Url + "/PublishingImages/QueryButton.jpg";            
        }

        /// ******************************************************************************************
        /// <summary>
        /// Method to return concatenated value of email addresses of site collection administrators
        /// </summary>
        /// ******************************************************************************************

        public string GetAdminsEmailAddresses()
        {
            string siteAdminsEmailAddresses = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
               using (SPSite site = new SPSite(SPContext.Current.Site.ID))
               {
                   using (SPWeb web = site.OpenWeb())
                   {
                       //Get the Site Administrators of current site
                       SPUserCollection siteAdminsCollection = web.SiteAdministrators;
                       foreach (SPUser oSPUser in siteAdminsCollection)
                       {
                           //If Email Address of a site administrator exists
                           if (!string.IsNullOrEmpty(oSPUser.Email))
                           {
                               if (string.IsNullOrEmpty(siteAdminsEmailAddresses))
                                   siteAdminsEmailAddresses = oSPUser.Email;
                               else
                                   siteAdminsEmailAddresses = siteAdminsEmailAddresses + "," + oSPUser.Email;
                           }
                       }
                   }
                }
            });
            return siteAdminsEmailAddresses;
        }

        /// ******************************************************************************************
        /// <summary>
        /// Method to Handle button click, once user enters details and submits the query
        /// </summary>
        /// ******************************************************************************************
        
        protected void btnContactMe_Click(object sender, ImageClickEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                string toAddresses = string.Empty;

                //Check if Send Mail to admin is checked, if yes, send mail to admin and non admins if any, else just send mail to non admins specified by user
                if (!UserEmailProp.rdoAdminEmail)
                    toAddresses = UserEmailProp.userEmail;
                else
                    toAddresses = UserEmailProp.userEmail + "," + GetAdminsEmailAddresses();

                SendMail objMail = new SendMail();

                //Send the query to administrator
                IsMailSent = objMail.SendQuery(txtName.Text, txtCompany.Text, txtPhone.Text, txtEmail.Text, txtRequirement.Text, true, toAddresses, "Admin");
                
                //if mail sent to admin, send mail to self (user) also if the check box is ticked to copy mail to self                
                if (IsMailSent)
                {
                    if (chkSendMailMyself.Checked)
                    {
                        IsMailSent = objMail.SendQuery(txtName.Text, txtCompany.Text, txtPhone.Text, txtEmail.Text, txtRequirement.Text, true, txtEmail.Text, "Self");
                    }
                }
                

                //If mail sent successfully, redirect user to confirmation page
                if (IsMailSent)
                    SPUtility.Redirect("Thanks.aspx", SPRedirectFlags.Default, HttpContext.Current);
                else
                {
                    lblMsg.Text = "Your query could not be sent. Please contact the administrator!";
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Visible = true;
                }
            });
        }
    }
}
