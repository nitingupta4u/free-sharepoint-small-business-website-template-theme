# Site Features #

  * There are 2 features used in this template
    1. **A Site scoped feature** - Query Us WebPart, Location WebPart, Services WebPart, Contact us Address WebPart, Contact Us WebPart, Detailed Services Offered WebPart.
    1. **A Web scoped feature** - This feature wraps all the other modules (Master Page, Theme, Page Layout Content Type, Internal Pages, Images, Custom page Layout, CSS) and also has a receiver which gets triggered when the feature is activated.

  * Both these features are activated automatically when the site is created.

  * Site Scoped Feature - For all visual WebPart
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/Feature2Site.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/Feature2Site.jpg)

  * Web Scoped Feature - For all other components
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/Feature1Web.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/Feature1Web.jpg)

### Feature Receiver ###
  * The Web scoped feature also has a feature receiver attached with it which performs the following tasks when the feature is activated, i.e. when the site is created.
    1. Assign default page of the site to **Home.aspx**
    1. Hide **Thanks.aspx** page from the Top Navigation, as it is only required when the mail is sent.
    1. Delete the **default.aspx** page created when publishing feature is activated on the site.
    1. Create **Configurations List** with required columns.
    1. **Add Configuration Items** to the Configuration List (Footer, Header, Admin mail, Self Mail, Admin Mail subject, Self mail subject)
    1. **Set default page layout** of the site to Inner Page (Custom page layout)
    1. **Apply custom theme** to the site
    1. **Add Detailed Description column** to the Services Offered list and update items.

---

[Back to Developers RoadMap](http://code.google.com/p/free-sharepoint-small-business-website-template-theme/wiki/DeveloperRoadMap)