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
using System.Text;
using System.Web.Script.Serialization;

namespace BrickRed.Templates.SmallBusiness.DetailedServicesOffered
{
    public partial class DetailedServicesOfferedUserControl : UserControl
    {

        private static bool _showImages = true;
        public static bool ShowImages
        {
            get { return _showImages; }
            set { _showImages = value; }
        }

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
                        foreach (SPListItem item in objList.Items)
                        {
                            HtmlContent = HtmlContent + GetHtmlContent(item);
                        }

                        string serialize = (new JavaScriptSerializer()).Serialize(HtmlContent);
                        serialize = serialize.Remove(0, 1);
                        serialize = serialize.Remove(serialize.LastIndexOf('"'), 1);

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDetailedDescription", "ShowServices('" + serialize + "');", true);
                    }
                }
            }
        }


        /// <summary>
        /// Creates the HTML style for the display
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetHtmlContent(SPListItem item)
        {
            StringBuilder createHtml = new StringBuilder();

            string imagePath = SPContext.Current.Web.Url + "/Lists/Services/" + item.Name;
            string Title = item.Title;
            string DeatailedDesc = string.Empty;
            try { DeatailedDesc = item["DetailedDescription"].ToString(); }
            catch { }

            createHtml.Append("<tr>");
            createHtml.Append("<td class=\"services\">");
            if (ShowImages)
            {
                createHtml.Append("<div id=\"ServicesImage\">");
                createHtml.Append("<img src=\"" + imagePath + "\" alt=\"\" width=\"204\" height=\"136\" />");
                createHtml.Append("</div>");
            }
            createHtml.Append("<p style=\"font-size:large;margin-top: -5px;\";><b>" + Title + "</b></p>");
            createHtml.Append("<p>" + DeatailedDesc + "</p>");
            createHtml.Append("</td>");
            createHtml.Append("</tr>");

            return createHtml.ToString();
        }
    }
}
