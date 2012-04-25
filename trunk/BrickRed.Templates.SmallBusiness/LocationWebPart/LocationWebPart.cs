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
using System.Web.Script.Serialization;
using System.Reflection;

namespace BrickRed.Templates.SmallBusiness.LocationWebPart
{
    [ToolboxItemAttribute(false)]
    public class LocationWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/BrickRed.Templates.SmallBusiness/LocationWebPart/LocationWebPartUserControl.ascx";

        #region WebPart Properties

        private string _lattitude = "28.60502008328845";
        [WebBrowsable(false),
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
        [WebBrowsable(false),
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

            string latlan = string.Empty;

            //Get the Updated Lattitude and Longitude values in Edit Mode
            GetLatLanIndEditMode();

            // Show the non editable Map if the page is not in editable mode
            if (SPContext.Current.FormContext.FormMode != SPControlMode.Edit)
            {
                latlan = this.Lattitude + "%2C" + this.Longitude;

                string googleMapSrc = "http://www.google.com/uds/modules/elements/mapselement/iframe.html?maptype=roadmap&latlng=" + latlan + "&mlatlng=" + latlan + "&maddress1=" + DisplayAddress + "&zoom=9&mtitle=" + LocationDisplayTitle + "&element=true";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowGoogleMaps", "ShowMap('" + googleMapSrc + "');", true);
            }
            else   // Show the Editable Map
            {
                HiddenField hf_latlan = new HiddenField();
                hf_latlan.Value = this.Lattitude + "," + this.Longitude;
                Controls.Add(hf_latlan);

                string hf_latlanID = SerializeString(hf_latlan.ClientID);
                string lat = SerializeString(this.Lattitude);
                string lon = SerializeString(this.Longitude);
                string title = SerializeString(this.LocationDisplayTitle);

                ViewState["hf_latlanID"] = hf_latlanID;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowGoogleMaps", "ShowEditMap('" + hf_latlanID + "','" + lat + "','" + lon + "','" + title + "');", true);
            }
        }

        /// <summary>
        /// Gets the Lattitude and Longitude values in the Edit Mode.
        /// </summary>
        private void GetLatLanIndEditMode()
        {
            string latlan = string.Empty;
            if (SPContext.Current.FormContext.FormMode == SPControlMode.Edit)
            {
                //check if the keys count is not null
                if (HttpContext.Current.Request.Form.Keys.Count > 0)
                {
                    //check if the viewstate is not null
                    if (ViewState["hf_latlanID"] != null)
                    {
                        int Index = 0;

                        foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                        {
                            string keyId = key.Replace("$", "_");

                            //search the control with the hidden field value
                            if (keyId.Equals(ViewState["hf_latlanID"].ToString()))
                            {
                                break;
                            }
                            Index++;
                        }
                        try
                        {
                            latlan = HttpContext.Current.Request.Form[Index];
                        }
                        catch
                        {
                            latlan = this.Lattitude + "," + this.Longitude;
                        }

                        //Update only if there is a change
                        if (!latlan.Split(',')[0].Equals(this.Lattitude) || !latlan.Split(',')[1].Equals(this.Longitude))
                        {
                            this.Lattitude = latlan.Split(',')[0];
                            this.Longitude = latlan.Split(',')[1];
                            SaveCustomProperty();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Updates the lattitude and longitude values.
        /// </summary>
        private void SaveCustomProperty()
        {
            using (SPSite objSite = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb objWeb = objSite.OpenWeb())
                {
                    SPFile objPage = objWeb.GetFile(HttpContext.Current.Request.Url.ToString());
                    SPLimitedWebPartManager mgr = objPage.GetLimitedWebPartManager(PersonalizationScope.Shared);
                    System.Web.UI.WebControls.WebParts.WebPart objWebPart = mgr.WebParts[this.ID];

                    if (objWebPart != null)
                    {
                        ((BrickRed.Templates.SmallBusiness.LocationWebPart.LocationWebPart)(objWebPart.WebBrowsableObject)).Lattitude = this.Lattitude;
                        ((BrickRed.Templates.SmallBusiness.LocationWebPart.LocationWebPart)(objWebPart.WebBrowsableObject)).Longitude = this.Longitude;
                        mgr.SaveChanges(objWebPart);
                    }
                }
            }
        }


        /// <summary>
        /// Serializes the string for passing to the JS function
        /// </summary>
        /// <param name="serializeString"></param>
        /// <returns></returns>
        private string SerializeString(string serializeString)
        {
            string serialized = (new JavaScriptSerializer()).Serialize(serializeString);
            serialized = serialized.Remove(0, 1);
            serialized = serialized.Remove(serialized.LastIndexOf('"'), 1);
            return serialized;
        }
    }
}
