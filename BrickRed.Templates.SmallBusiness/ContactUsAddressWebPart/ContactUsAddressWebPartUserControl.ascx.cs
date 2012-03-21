using System;
using System.Web.UI;
using Microsoft.SharePoint;

namespace BrickRed.Templates.SmallBusiness.ContactUsAddressWebPart
{
    public partial class ContactUsAddressWebPartUserControl : UserControl
    {
        private static string _companyName = "Company Name Pvt Ltd";
        private static string _companyAddress = "1234 ABC Lane\r\nDrive 1234\r\nLocation\r\nPhone: +1.234.567.8901\r\nFax: +1.234.567.8901\r\nEmail: yourcompany@email.com\r\n";

        public static string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }

        public static string CompanyAddress
        {

            get
            {
                return _companyAddress;
            }
            set
            {
                _companyAddress = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string ImageURL = (SPContext.Current.Web.Site.ServerRelativeUrl + "/PublishingImages/ContactUsBanner.jpg").Replace("//","/");

            //Get the Div ID and Set the background image URL
            divContactUsImage.Style.Add("background-image", ImageURL);
            if (!string.IsNullOrEmpty(CompanyName) && !string.IsNullOrEmpty(CompanyAddress))
            {
                CompanyAddress = CompanyAddress.Replace("\r\n", "<br/>");
                divContactUsContent.InnerHtml = "<b>" + CompanyName + "</b><br/>" + CompanyAddress;
            }
        }
    }
}
