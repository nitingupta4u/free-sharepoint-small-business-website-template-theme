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
using System.Web.Script.Serialization;
using Microsoft.SharePoint;
using System.Text;

namespace BrickRed.Templates.SmallBusiness.ServicesWebPart
{
    public partial class ServicesWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string HtmlContent = string.Empty;

            using (SPSite objSite = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb objWeb = objSite.OpenWeb())
                {
                    SPList objList = objWeb.Lists.TryGetList("Services");

                    if (objList != null)
                    {
                        bool flag = true;
                        foreach (SPListItem item in objList.Items)
                        {
                            HtmlContent = HtmlContent + GetHtmlContent(item,flag);
                            flag = false;
                        }

                        HtmlContent += "<div class='caption'><div class='content'></div></div>";

                        string serialize = (new JavaScriptSerializer()).Serialize(HtmlContent);
                        serialize = serialize.Remove(0, 1);
                        serialize = serialize.Remove(serialize.LastIndexOf('"'), 1);

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowServicesOffered", "ShowServicesSlides('" + serialize + "');", true);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the HTML style for the display
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetHtmlContent(SPListItem item, bool flag)
        {
            StringBuilder createHtml = new StringBuilder();
            string imagePath = SPContext.Current.Web.Url + "/Lists/Services/" + item.Name;
            string Title = item.Title;
            string Description = string.Empty;
            try { Description = item["Description"].ToString(); }
            catch { }

            if (flag)
                createHtml.Append("<a href=\"services.aspx\" class=\"show\">");
            else
                createHtml.Append("<a href=\"services.aspx\">");

            createHtml.Append("<img src=\"" + imagePath + "\" alt=\"" + Title + "\" width=\"325\" height=\"233\" title=\"\" alt=\"\" rel=\"<h3>" + Title + " </h3>" + Description + " \"/></a>");

            return createHtml.ToString();
        }
    }
}
