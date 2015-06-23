# What is Query Us Web Part #

  * This webpart send mails to the organization by the end user in order to query.
  * This webpart sends mail to the administrators and other specific users mentioned in the webpart property.
  * It also gives the option of sending the mail to the user if the "Send me a copy" field is checked.

# Where Query Us Web Part is used #
  * It is used on all the default pages of the site.

# Web Part Properties configurable by Administrator #

  * There are 2 properties in this web part that the Site administrator can configure.
    1. First, **Send Mail to Administrators** - Checked by default, this sends the user query mail to site administrators.
    1. Second, **CC Mail to these users** - Administrator can enter comma separated email addresses, to whom the user query is also sent.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/QueryUsProperties.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/QueryUsProperties.jpg)

# Web Part Fields #

  * Name: Name of the user submitting the query
  * Company: Company of the user submitting the query
  * Phone: Phone of the user submitting the query
  * Email: Email address of the user submitting the query
  * Your Requirement: The actual query of the user browsing the site
  * Send me a copy: The user browsing the site can check this option in order to receive a confirmation mail of his query being sent to site administrator.
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/QueryUsFields.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/QueryUsFields.jpg)

# Sending Emails #

  * There are 2 types of mails being sent in this web part
  * First, mail is sent to administrators and users specified in the web part properties
  * Secondly, mail is sent to the user who is raising the query is he has checked the box in the web part fields (Send copy to me)
  * The body templates for both these mails is stored as HTML in the **Configurations** list on the site.
  * The Subjects for both these mails is stored as HTML in the **Configurations** list on the site.
  * The HTML content in the list contains specified tokens that are dynamically replaced by the user input that is provided in the web part fields. Example, the tokens shown in the screenshot below, would be replaced by relevant values specified by the user.
  * The list of Tokens that can be provided in the Value column are:
    1. Name of User: #FROMNAME#
    1. Company: #FROMCOMPANY#
    1. Phone: #FROMPHONE#
    1. Email: #FROMEMAIL#
    1. User Query: #BODY#

![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/MailTemplates.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/MailTemplates.jpg)

  * Lets discuss the templates stored in configurations list one by one:
    1. **AdminMailContent:** This is the template for the mail that is sent to Site Administrators and users specified in web part property (CC mail to these users).
    1. **UserMailContent:** This is the template for the mail that is sent to the user raising the query and if he checks the option to send a copy of mail to himself also.
    1. **AdminMailSubject:** This is subject of the mail sent to Site Administrators.
    1. **UserMailSubject:** This is subject of the mail sent to the user raising the query.

All these mail templates along with mail subjects can be modified in the configurations list according to the requirements.

---

[Back to End User RoadMap](http://code.google.com/p/free-sharepoint-small-business-website-template-theme/wiki/EndUserRoadMap)