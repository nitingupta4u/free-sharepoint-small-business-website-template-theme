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
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.WebPartPages;


namespace BrickRed.Templates.SmallBusiness.ContactUsWebPart
{
    [ToolboxItemAttribute(false)]
    public class ContactUsWebPart :  System.Web.UI.WebControls.WebParts.WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/BrickRed.Templates.SmallBusiness/ContactUsWebPart/ContactUsWebPartUserControl.ascx";

        bool _rdoAdminEmail = true;
        [WebBrowsable(true),
        Category("BrickRed.SmallBusiness"),
        Personalizable(PersonalizationScope.Shared),
        DefaultValue(true),
        WebDisplayName("Send Mail to Administrators")]
        public bool rdoAdminEmail
        {
            get
            {
                return _rdoAdminEmail;
            }
            set
            {
                _rdoAdminEmail = value;
            }
        }


        string _userEmail = string.Empty;      
        [WebBrowsable(true),
        Category("BrickRed.SmallBusiness"),
        Personalizable(PersonalizationScope.Shared),
        WebDisplayName("CC Mail to these Users")]
        public string userEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (!isEmailAddressValid(value))
                        throw new WebPartPageUserException("Please enter a valid Email Address!");                    
                }
                _userEmail = value;
            }
        }
            
       
        protected override void CreateChildControls()
        {
            ContactUsWebPartUserControl control = Page.LoadControl(_ascxPath) as ContactUsWebPartUserControl;
            if (control != null)
            {
                control.UserEmailProp = this;             
                Controls.Add(control);
            }         
        }
               
        protected override void OnLoad(EventArgs e)
        {            
            //Get Email addresses of site administrators
            //userEmail = GetAdminsEmailAddresses();
        }

        public static bool isEmailAddressValid(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            bool IsValid = false;

            char[] splitterChar = { ',' };
            string[] entries = inputEmail.Split(splitterChar, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string entry in entries)
            {
                if (re.IsMatch(entry))
                    IsValid = true;
                else
                {
                    IsValid = false;
                    return false;
                }
            }
            return IsValid;
        }       
    }
}
