using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint;

namespace BrickRed.Templates.SmallBusiness.ControlTemplates.BrickRed.Templates.SmallBusiness
{
    public partial class BrickRed : UserControl
    {
        SPList contactUsList;       
                     
        protected void Page_Load(object sender, EventArgs e)
        {           
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {                
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        //fetch the contact us list
                        contactUsList = web.Lists.TryGetList(Constants.CONFIG_LIST);
                        if (contactUsList != null)
                        {
                            SPListItemCollection collection = contactUsList.Items;
                                                      
                            //Get list item from contact us list and assign value to control
                            foreach (SPListItem li in collection)
                            {
                                if (li["Title"].ToString() == Constants.CONFIG_LIST_CONTACTVALUE)
                                {
                                    divContactUsControl.InnerHtml = li["Value"].ToString();
                                }
                            }               
                        }
                    }
                }
            });
        }
    }
}
