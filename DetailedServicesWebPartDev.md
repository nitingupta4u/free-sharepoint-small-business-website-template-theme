# Components #

The following components are part of the Location WebPart which is used by the organization to show their office loaction on the google map.

## **The actual Web Part** ##
  * This is a **Visual web part** having a user control which comprises fields to capture the user data
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesSolution.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesSolution.jpg)

  * **Services List** of type Picture Library is created for the images to be shown in the webpart with the use of Jquery.
  * **Title and Detailed Description**, column of the Services List is used to show the Title and Description in the webpart.

![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesWebpart.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesWebpart.jpg)<br />
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesProperty.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesProperty.jpg)

  * **Web Part Properties:**
    * The title and detailed description is shown in the webpart as shown below
> ![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesDetail.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesDetail.jpg)<br />
    * WebPart also contains as property weather to show image in the webpart or not.
> ![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesWPProperty.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesWPProperty.jpg)<br />


## **Visual WebPart** ##
  * **DetailedServicesOffered.cs** is the class that contain the properties needed to show the images on the WebPart.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesWebPart.cs.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesWebPart.cs.jpg)

  * **DetailedServicesOfferedUserControl.ascx.cs** is the file that gets the data from the Services List and creates the Html display format
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesOfferedUserControl.ascx.cs.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesOfferedUserControl.ascx.cs.jpg)

  * **DetailedServicesOfferedUserControl.ascx** is the file that contains the look & feel.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesOfferedUserControl.ascx.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/DetailedServicesOfferedUserControl.ascx.jpg)


---

[Back to Developers RoadMap](http://code.google.com/p/free-sharepoint-small-business-website-template-theme/wiki/DeveloperRoadMap)