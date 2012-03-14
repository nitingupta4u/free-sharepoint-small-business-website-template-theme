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
using Microsoft.SharePoint.WebPartPages;

namespace BrickRed.Templates.SmallBusiness.LocationWebPart
{
    [ToolboxItemAttribute(false)]
    public class LocationWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/BrickRed.Templates.SmallBusiness/LocationWebPart/LocationWebPartUserControl.ascx";

        #region WebPart Properties

        private string _lattitude = "28.60502008328845";
        [WebBrowsable(true),
        Category("BrickRed.SmallBusiness"),
        Personalizable(PersonalizationScope.Shared),
        Description("Enter the lattitude of the office"),
        WebDisplayName("Lattitude")]
        public string Lattitude
        {
            get
            {
                return _lattitude;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new WebPartPageUserException("Please enter a Lattitude");
                }
                _lattitude = value;
            }
        }

        private string _longitude = "77.36267566680908";
        [WebBrowsable(true),
        Category("BrickRed.SmallBusiness"),
        Personalizable(PersonalizationScope.Shared),
        Description("Enter the longitude of the office"),
        WebDisplayName("Longitude")]
        public string Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new WebPartPageUserException("Please enter a Longitude");
                }
                _longitude = value;
            }
        }

        private string _title = "BrickRed Technologies";
        [WebBrowsable(true),
        Category("BrickRed.SmallBusiness"),
        Personalizable(PersonalizationScope.Shared),
        Description("Enter the Title of the location"),
        WebDisplayName("Title")]
        public string LocationDisplayTitle
        {
            get
            {
                return _title;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new WebPartPageUserException("Please enter a Title");
                }
                _title = value;
            }
        }

        private string _displayAddress = "B-25 Sector-58,BrickRed Technologies Pvt Ltd";
        [WebBrowsable(true),
        Category("BrickRed.SmallBusiness"),
        Personalizable(PersonalizationScope.Shared),
        Description("Enter the display address of the location"),
        WebDisplayName("Display Address")]
        public string DisplayAddress
        {
            get
            {
                return _displayAddress;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new WebPartPageUserException("Please enter the display address");
                }
                _displayAddress = value;
            }
        }

        #endregion

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);

            string latlan = Lattitude + "%2C" + Longitude;

            string googleMapSrc = "http://www.google.com/uds/modules/elements/mapselement/iframe.html?maptype=roadmap&latlng=" + latlan + "&mlatlng=" + latlan + "&maddress1=" + DisplayAddress + "&zoom=9&mtitle=" + LocationDisplayTitle + "&element=true";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowGoogleMaps", "ShowMap('" + googleMapSrc + "');", true);
        }
    }
}
