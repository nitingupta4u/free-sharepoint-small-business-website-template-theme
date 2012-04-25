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
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Publishing;
using System.Web.UI.WebControls;
using System.Linq;

namespace BrickRed.Templates.SmallBusiness.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("4e456b7d-1315-4192-a57d-3fd4d8e78235")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPList objPagesList = null;
                Guid ListId;

                SPSite site = properties.Feature.Parent as SPSite;
                SPWeb web = null;
                if (site == null)
                {
                    web = properties.Feature.Parent as SPWeb;
                    if (web == null) return;

                    site = web.Site;
                }
                else web = site.RootWeb;

                web.AllowUnsafeUpdates = true;

                #region Site navigation and welcome page

                //Assign default page of the site to home.aspx
                PublishingWeb objPublishingWeb = PublishingWeb.GetPublishingWeb(web);
                SPFile objHomePageFile = web.GetFile(objPublishingWeb.PagesListName + "/Home.aspx");
                objPublishingWeb.DefaultPage = objHomePageFile;
                objPublishingWeb.Update();
                web.Update();

                //Hide the thanks.aspx page from the top navigation
                PublishingPageCollection pages = objPublishingWeb.GetPublishingPages();
                foreach (PublishingPage page in pages)
                {
                    if (page.Name.ToLower() == "thanks.aspx")
                    {
                        page.IncludeInGlobalNavigation = false;
                    }
                }

                //Delete default.aspx page created when activated publishing feature
                //Get the Pages List for current Locale
                ListId = PublishingWeb.GetPagesListId(web);
                objPagesList = web.Lists[ListId];
                string urlPagetoDelete = objPagesList.RootFolder.ServerRelativeUrl + "/default.aspx";
                if (web.GetFile(urlPagetoDelete).Exists)
                {
                    SPFile objFile = objPagesList.RootFolder.Files["default.aspx"];
                    if (objFile != null)
                    {
                        if (objFile.CheckOutStatus == SPFile.SPCheckOutStatus.None)
                        {
                            objFile.CheckOut();
                        }
                        objFile.Delete();
                    }
                }

                #endregion

                #region Create Config List & Add Columns

                //Create Config List
                SPList configList;
                configList = web.Lists.TryGetList(Constants.CONFIG_LIST);
                if (configList == null)
                {
                    web.Lists.Add(Constants.CONFIG_LIST, "This list contains the HTML for the Global Contact Us Control, Footer, Admin & Self Mail contents and subjects", SPListTemplateType.GenericList);
                    configList = web.Lists.TryGetList(Constants.CONFIG_LIST);
                }

                //Add Columns in List
                if (configList != null)
                {
                    if (!configList.Fields.ContainsField("Value"))
                    {
                        string FieldXML = "<Field Type='Note' Description='The list of Tokens that can be provided in this column are: \nName of User: #FROMNAME#\nCompany: #FROMCOMPANY#\nPhone: #FROMPHONE#\nEmail: #FROMEMAIL#\nUser Query: #BODY#' DisplayName='Value' Required='TRUE' NumLines='6' StaticName='Value' Name='Value' RichText='TRUE' RichTextMode='FullHtml' IsolateStyles='FALSE' />";
                        configList.Fields.AddFieldAsXml(FieldXML, true, SPAddFieldOptions.Default);
                        //configList.Fields.Add("Value", SPFieldType.Note, false);
                        configList.Update();
                    }
                }

                #endregion

                #region Add Configuration Items to Config List

                //Add Footer configuration value
                SPListItem itemFooter = configList.Items.Add();
                itemFooter["Title"] = Constants.CONFIG_LIST_FOOTERVALUE;
                itemFooter["Value"] = Constants.FOOTER_VALUE;
                itemFooter.Update();

                //Add Contact Us Control configuration value
                SPListItem itemContactInfo = configList.Items.Add();
                itemContactInfo["Title"] = Constants.CONFIG_LIST_CONTACTVALUE;
                itemContactInfo["Value"] = Constants.CONTACT_CONTROL_VALUE;
                itemContactInfo.Update();

                //Add Administrator mail content configuration value
                SPListItem itemAdminMailContent = configList.Items.Add();
                itemAdminMailContent["Title"] = Constants.CONFIG_LIST_ADMINMAILVALUE;
                itemAdminMailContent["Value"] = Constants.ADMINMAILCONTENT_VALUE;
                itemAdminMailContent.Update();

                //Add User mail content configuration value
                SPListItem itemUserMailContent = configList.Items.Add();
                itemUserMailContent["Title"] = Constants.CONFIG_LIST_USERMAILVALUE;
                itemUserMailContent["Value"] = Constants.USERMAILCONTENT_VALUE;
                itemUserMailContent.Update();

                //Add Admin mail Subject configuration value
                SPListItem itemAdminMailSubject = configList.Items.Add();
                itemAdminMailSubject["Title"] = Constants.CONFIG_LIST_ADMINMAILSUBJECT;
                itemAdminMailSubject["Value"] = Constants.ADMINMAILSUBJECT_VALUE;
                itemAdminMailSubject.Update();

                //Add User mail Subject configuration value
                SPListItem itemUserMailSubject = configList.Items.Add();
                itemUserMailSubject["Title"] = Constants.CONFIG_LIST_USERMAILSUBJECT;
                itemUserMailSubject["Value"] = Constants.USERMAILSUBJECT_VALUE;
                itemUserMailSubject.Update();

                #endregion

                #region Set Default page layout

                //Set default page layout of site to internal page layout                
                if (objPublishingWeb != null)
                {
                    PageLayout _pageLayout = (from _pl in objPublishingWeb.GetAvailablePageLayouts()
                                              where _pl.Name == "BrickRedSmallBusinessInnerPageLayout.aspx"
                                              select _pl).FirstOrDefault();
                    objPublishingWeb.SetDefaultPageLayout(_pageLayout, true);
                    objPublishingWeb.Update();
                }

                #endregion

                #region Apply Custom Theme

                //Apply brickRed Small Business theme to the site created
                using (ThmxTheme theme = ThmxTheme.Open(web.Site, "./_catalogs/theme/" + Constants.THEME_NAME + ".thmx"))
                {
                    theme.ApplyTo(web, false);
                }

                #endregion

                #region Add Column and Item to the Service List

                SPList servicesList = web.Lists.TryGetList(Constants.SERVICES_LIST_NAME);
                if (servicesList != null)
                {

                    if (!servicesList.Fields.ContainsField("DetailedDescription"))
                    {
                        //create the column if it does not exists
                        string FieldXML = "<Field Type='Note' Description='Displays the detailed description of the services offered' DisplayName='Detailed Description' Required='TRUE' NumLines='6' StaticName='DetailedDescription' Name='DetailedDescription' RichText='TRUE' RichTextMode='FullHtml' IsolateStyles='FALSE' UnlimitedLengthInDocumentLibrary='TRUE'/>";
                        servicesList.Fields.AddFieldAsXml(FieldXML, true, SPAddFieldOptions.Default);
                        servicesList.Update();

                        string[] DETAILED_DESCRIPTION_SERVICE = { "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis turpis eros, mattis et vestibulum in, suscipit et metus. Duis nec placerat felis. Nam eget pulvinar sem. Sed dictum gravida lobortis. Phasellus ut pretium diam. Vestibulum vel velit magna, congue sollicitudin orci. Donec quis mauris at ante volutpat ornare. Nulla metus enim, fringilla at eleifend a, pulvinar a turpis. Sed ut laoreet libero. Ut ipsum felis, varius eget pharetra et, mattis vitae augue. Aliquam erat volutpat. Quisque viverra turpis ut lorem gravida accumsan. Integer est risus, aliquam eget convallis id, malesuada vitae nunc. Ut eu ipsum velit, eget semper dui. </p><p>Integer commodo dui id nulla accumsan ullamcorper. Donec eu urna id lorem venenatis vestibulum. Donec nec nisl dolor. Nulla porta condimentum lacus eu adipiscing. Aliquam nec tellus eu lectus ultrices consequat et nec turpis. Suspendisse vehicula porta urna, ac lobortis nisl viverra at. Curabitur ac velit nibh. Mauris molestie, nibh quis vestibulum fringilla, arcu eros hendrerit mi, id tempor odio lacus in quam. Mauris tristique urna eget est condimentum mattis. Sed nec lacus massa, ut dignissim elit. Vivamus augue libero, varius dictum pretium quis, auctor eget urna. Donec semper luctus dolor, at dapibus nunc fermentum a. </p>", 
                                                                    "<p>Suspendisse laoreet, nulla vitae volutpat commodo, nulla nunc porta risus, eget laoreet nunc augue in felis. Quisque sed felis felis, a dictum felis. Nullam vulputate rhoncus odio, vitae tincidunt nisl vehicula eget. Donec porttitor ante orci, nec luctus elit. Vivamus massa augue, consectetur in tempus sit amet, rhoncus tristique neque. Nam elit nibh, euismod auctor tristique ut, tincidunt sed nibh. Duis tempor, dui sit amet condimentum ullamcorper, eros risus interdum nulla, ut facilisis sapien neque nec urna. Suspendisse urna lorem, rutrum ac varius vel, tempor eget risus. Suspendisse potenti. Etiam tempus gravida erat, ut commodo leo vulputate non. Praesent et risus nisl. Mauris pharetra pellentesque enim, non pulvinar tortor eleifend congue.</p>Integer commodo dui id nulla accumsan ullamcorper. Donec eu urna id lorem venenatis vestibulum. Donec nec nisl dolor. Nulla porta condimentum lacus eu adipiscing. Aliquam nec tellus eu lectus ultrices consequat et nec turpis. Suspendisse vehicula porta urna, ac lobortis nisl viverra at. Curabitur ac velit nibh. Mauris molestie, nibh quis vestibulum fringilla, arcu eros hendrerit mi, id tempor odio lacus in quam. Mauris tristique urna eget est condimentum mattis. Sed nec lacus massa, ut dignissim elit. Vivamus augue libero, varius dictum pretium quis, auctor eget urna. Donec semper luctus dolor, at dapibus nunc fermentum a.</p>",
                                                                    "<p>Integer commodo dui id nulla accumsan ullamcorper. Donec eu urna id lorem venenatis vestibulum. Donec nec nisl dolor. Nulla porta condimentum lacus eu adipiscing. Aliquam nec tellus eu lectus ultrices consequat et nec turpis. Suspendisse vehicula porta urna, ac lobortis nisl viverra at. Curabitur ac velit nibh. Mauris molestie, nibh quis vestibulum fringilla, arcu eros hendrerit mi, id tempor odio lacus in quam. Mauris tristique urna eget est condimentum mattis. Sed nec lacus massa, ut dignissim elit. Vivamus augue libero, varius dictum pretium quis, auctor eget urna. Donec semper luctus dolor, at dapibus nunc fermentum a.</p><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis turpis eros, mattis et vestibulum in, suscipit et metus. Duis nec placerat felis. Nam eget pulvinar sem. Sed dictum gravida lobortis. Phasellus ut pretium diam. Vestibulum vel velit magna, congue sollicitudin orci. Donec quis mauris at ante volutpat ornare. Nulla metus enim, fringilla at eleifend a, pulvinar a turpis. Sed ut laoreet libero. Ut ipsum felis, varius eget pharetra et, mattis vitae augue. Aliquam erat volutpat. Quisque viverra turpis ut lorem gravida accumsan. Integer est risus, aliquam eget convallis id, malesuada vitae nunc. Ut eu ipsum velit, eget semper dui. </p>",
                                                                    "<p>Integer commodo dui id nulla accumsan ullamcorper. Donec eu urna id lorem venenatis vestibulum. Donec nec nisl dolor. Nulla porta condimentum lacus eu adipiscing. Aliquam nec tellus eu lectus ultrices consequat et nec turpis. Suspendisse vehicula porta urna, ac lobortis nisl viverra at. Curabitur ac velit nibh. Mauris molestie, nibh quis vestibulum fringilla, arcu eros hendrerit mi, id tempor odio lacus in quam. Mauris tristique urna eget est condimentum mattis. Sed nec lacus massa, ut dignissim elit. Vivamus augue libero, varius dictum pretium quis, auctor eget urna. Donec semper luctus dolor, at dapibus nunc fermentum a. </p><p>Suspendisse laoreet, nulla vitae volutpat commodo, nulla nunc porta risus, eget laoreet nunc augue in felis. Quisque sed felis felis, a dictum felis. Nullam vulputate rhoncus odio, vitae tincidunt nisl vehicula eget. Donec porttitor ante orci, nec luctus elit. Vivamus massa augue, consectetur in tempus sit amet, rhoncus tristique neque. Nam elit nibh, euismod auctor tristique ut, tincidunt sed nibh. Duis tempor, dui sit amet condimentum ullamcorper, eros risus interdum nulla, ut facilisis sapien neque nec urna. Suspendisse urna lorem, rutrum ac varius vel, tempor eget risus. Suspendisse potenti. Etiam tempus gravida erat, ut commodo leo vulputate non. Praesent et risus nisl. Mauris pharetra pellentesque enim, non pulvinar tortor eleifend congue.</p>" };

                        //Enter the dummy data
                        for (int i = 1; i <= servicesList.ItemCount; i++)
                        {
                            SPListItem item = servicesList.GetItemById(i);
                            item["DetailedDescription"] = DETAILED_DESCRIPTION_SERVICE[i-1];
                            item.Update();
                        }

                        servicesList.Update();
                    }
                }

                #endregion

                web.AllowUnsafeUpdates = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
