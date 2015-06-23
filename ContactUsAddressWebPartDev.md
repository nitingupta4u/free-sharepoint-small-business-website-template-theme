# Components #

The following components are part of the Contact Us Address Web Part which is used by the organization to show their office address.

## **The actual Web Part** ##
  * This is a **Visual web part** having a user control.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPartSolution.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPartSolution.jpg)

  * **Web Part Properties:**
    * Comapny name: This is a single line textbox to enter the name of the company.
    * Company Address: This is a multi-line textbox to enter the full address of the company.

![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.jpg)<br />
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressProperty.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressProperty.jpg)

## **Visual WebPart** ##
  * **ContactUsAddressWebPart.cs** is the class that contain the properties needed to show the address on the webpart.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.cs.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.cs.jpg)

  * **ContactUsAddressWebPartUserControl.ascx** is the file that contains the look and feel of the webpart.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.ascx.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.ascx.jpg)

  * **ContactUsAddressWebPartUserControl.ascx.cs** is the file that contains code which replaces the details from the properties to the webpart at runtime.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.ascx.cs.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/ContactUsAddressWebPart.ascx.cs.jpg)



---

[Back to Developers RoadMap](http://code.google.com/p/free-sharepoint-small-business-website-template-theme/wiki/DeveloperRoadMap)