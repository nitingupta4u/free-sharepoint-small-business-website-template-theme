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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickRed.Templates.SmallBusiness
{
    public static class Constants
    {
        public const string THEME_NAME = "BrickRedSmallBusiness"; //Custom Theme name
        public const string CONTACTUS_LIST = "Contact Us"; //ContactUs List name
        public const string CONFIG_LIST = "Configurations"; //Config List name
        public const string CONFIG_LIST_FOOTERVALUE = "Footer"; //Config List Footer Value Name
        public const string CONFIG_LIST_CONTACTVALUE = "ContactUs"; //Config List Control Value name
        public const string CONFIG_LIST_ADMINMAILVALUE = "AdminMailContent"; //Config List Admin mail Value name
        public const string CONFIG_LIST_USERMAILVALUE = "UserMailContent"; //Config List User mail Value name
        public const string CONFIG_LIST_ADMINMAILSUBJECT = "AdminMailSubject"; //Config List Admin mail Value name
        public const string CONFIG_LIST_USERMAILSUBJECT = "UserMailSubject"; //Config List User mail Value name
        
        //Values of Contact control, footer control
        public const string CONTACT_CONTROL_VALUE = "<table style='width:100%;' cellpadding='2'><tr><td style='border-right-style:dotted; border-right-width:thin;' colspan='4'>+1.703.666.8801</td><td style='border-right-style:dotted; border-right-width:thin;' colspan='4'>+44.208.816.7516</td><td style='border-right-style:dotted; border-right-width:thin;' colspan='4'>+31.020.894.6477</td><td colspan='4'><a href='mailto:contact@mycompany.com'>contact@mycompany.com</a></td></tr></table>";
        public const string FOOTER_VALUE = "Copyright © 2012 Your Company Pvt. Ltd. All Rights Reserved.";
        //values of Admin mail content and User mail content
        public const string ADMINMAILCONTENT_VALUE = "Dear System Administrator, <br /><br />A new query has been received by the user <b> #FROMNAME# </b><br /><br />The details are mentioned below:<br /><br /><table border='1'><tr><td><b>Name of the User:</b> </td><td>#FROMNAME#</td></tr><tr><td><b>Company:</b> </td><td>#FROMCOMPANY#</td></tr><tr><td><b>Phone Number:</b> </td><td>#FROMPHONE#</td></tr><tr><td><b>Email Address:</b> </td><td>#FROMEMAIL#</td></tr><tr><td><b>User Query:<b> </td><td>#BODY#</td></tr></table><br />Please take appropriate action for the same.<br /><br />Email Admin.";
        public const string USERMAILCONTENT_VALUE = "Dear #FROMNAME#, <br /><br />Your query has been forwarded to the concerned person in our team. <br /><br />The details are mentioned below:<br /><br /><table border='1'><tr><td><b>Name of the User:</b> </td><td>#FROMNAME#</td></tr><tr><td><b>Company:</b> </td><td>#FROMCOMPANY#</td></tr><tr><td><b>Phone Number:</b> </td><td>#FROMPHONE#</td></tr><tr><td><b>Email Address:</b> </td><td>#FROMEMAIL#</td></tr><tr><td><b>User Query:<b> </td><td>#BODY#</td></tr></table><br />Someone from our team will get back to you shortly on this.<br /><br />Thanks. <br /><br />Company Admin.";
        //Values of Admin mail subject and User mail subject
        public const string ADMINMAILSUBJECT_VALUE = "Query raised by #FROMNAME# from #FROMCOMPANY#";
        public const string USERMAILSUBJECT_VALUE = "Your query has been received by our team";

        
        
    }
}
