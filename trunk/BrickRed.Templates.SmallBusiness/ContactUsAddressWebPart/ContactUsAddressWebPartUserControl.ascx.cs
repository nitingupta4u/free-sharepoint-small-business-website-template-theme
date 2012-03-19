using System;
using System.Web.UI;
using Microsoft.SharePoint;

namespace BrickRed.Templates.SmallBusiness.ContactUsAddressWebPart
{
    public partial class ContactUsAddressWebPartUserControl : UserControl
    {

        public static string CompanyName { get; set; }

        public static string CompanyAddress { get ; set; }

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
