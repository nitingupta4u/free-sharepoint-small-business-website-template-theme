# Components #

The following components are part of the Query Us Web Part which is used by the end user to send queries to the organization.

## **The actual Web Part** ##
  * This is a **Visual web part** having a user control which comprises fields to capture the user data.
> ![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/QueryUsWebPartSolution.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/QueryUsWebPartSolution.jpg)

  * **Send Me a copy** field, which if checked, sends a mail to the user itself who is sending the query.
  * **Send Query button**, which processes the user input and sends query to the administrators.
  * **Web Part Properties:**
    * Send Mail to Administrators: By default true, and sends mails to site administrators.
    * CC Mail to these users: Sends mails to the users mentioned in the text box also.

![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/WebPart.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/WebPart.jpg)<br />
![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/WebPartProperty.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/WebPartProperty.jpg)

## **Send Mail** ##
  * Mail can be sent to Site Administrators, other users mentioned in the web part property and to self (User who is sending the query)
  * **SendMail.cs** is the class that contain common methods used in Sending Mail. Some of these common methods are:
    * **GetMailSubjectFromConfigList** - used to fetch the subject of mail from configuration list, based on whether it is admin or self mail.
    * **GetMailBodyFromConfigList** - used to fetch the body of the mail from configuration list, based on whether it is admin or self mail.
    * **CreateMailMessage** - Creates an e-mail message (MailMessage) that can be sent using the System.Net.Mail.SmtpClient class.
    * **AddMailAddresses** - Adds user email addresses in a collection
    * **SendQuery** -
      * Replaces tokens from Mail Body with dynamic user values
      * Replaces tokens from Mail subject with dynamic user values
      * Sends Mail.
> ![http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/SendMail.cs.jpg](http://free-sharepoint-small-business-website-template-theme.googlecode.com/svn/wiki/Images/SendMail.cs.jpg)
    * **Constants.cs** is the class that contains all the constant values used in the solution.
      * The default values of Admin Mail and Self Mail content / subject which are there in the configuration list are also stored in this class.
      * Also, the list item identity names for these mail objects are placed in this class.
    * There are pre defined tokens stored in the configuration list. Administrators can use these tokens in either the Admin mail body, Self Mail body, Admin mail subject or Self Mail subject.
    * These tokens are read in the code and are replaced with the actual values entered by the user.


---

[Back to Developers RoadMap](http://code.google.com/p/free-sharepoint-small-business-website-template-theme/wiki/DeveloperRoadMap)