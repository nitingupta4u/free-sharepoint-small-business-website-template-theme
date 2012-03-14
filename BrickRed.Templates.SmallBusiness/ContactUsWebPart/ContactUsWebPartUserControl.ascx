<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUsWebPartUserControl.ascx.cs" Inherits="BrickRed.Templates.SmallBusiness.ContactUsWebPart.ContactUsWebPartUserControl" %>

<script type="text/javascript">
    function validate() {
        var objName = getTagFromIdentifierAndTitle('input', 'txtName', 'Name');
        var objEmail = getTagFromIdentifierAndTitle('input', 'txtEmail', 'Email');
        
        //Validation for empty Name provided by the user
        if (objName.value == "") {
            alert('Please enter your Name');
            objName.focus();
            return false;
        }
        
        //Validation for empty email address provided by the user
        if (objEmail.value == "") {
            alert('Please enter your Email Address');
            objEmail.focus();
            return false;
        }

        //Validation for incorrect email syntax provided by the user
        if (!emailCheck(objEmail.value)) {            
            objEmail.focus();
            return false;
        }
    }

    //Function to validate the email address entered by the user
    function emailCheck(str) {
        var at = "@";
        var dot = ".";
        var lat = str.indexOf(at);
        var lstr = str.length;
        var ldot = str.indexOf(dot);
        if (str.indexOf(at) == -1) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(at, (lat + 1)) != -1) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(dot, (lat + 2)) == -1) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(" ") != -1) {
            alert("Invalid E-mail ID");
            return false;
        }
        return true;
    }
      
    //function to find id of a control dynamically
    function getTagFromIdentifierAndTitle(tagName, identifier, title) {
        var len = identifier.length;
        var tags = document.getElementsByTagName(tagName);
        for (var i = 0; i < tags.length; i++) {
            var tempString = tags[i].id;
            if (tags[i].title == title && (identifier == "" ||
          tempString.indexOf(identifier) == tempString.length - len)) {
                return tags[i];
            }
        }
        return null;
    }
</script>

<div id="divContactControl">
    <table style="width:100%;">
        <tr>
            <td colspan="2" height="5"></td>
        </tr>
        <tr>
            <td width="50%">
                Name <font color="red">*</font></td>
            <td>
                <asp:TextBox ID="txtName" ToolTip="Name" runat="server"></asp:TextBox>
            </td>     
        </tr>
        <tr>
            <td width="50%">
                Company</td>
            <td>
                <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
            </td>     
        </tr>
        <tr>
            <td width="50%">
                Phone</td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            </td>     
        </tr>
        <tr>
            <td width="50%">
                Email <font color="red">*</font></td>
            <td style="margin-left: 40px">
                <asp:TextBox ID="txtEmail" ToolTip="Email" runat="server"></asp:TextBox>
            </td>     
        </tr>
        <tr>
            <td colspan="2">Your Requirement
               </td>     
        </tr>
         <tr>
            <td colspan="2"><asp:TextBox Width="98%" ID="txtRequirement" runat="server" CssClass="multiTxtClass" TextMode="MultiLine"></asp:TextBox></td>
        </tr>       
        <tr>
            <td width="50%">
               </td>
            <td align="right">
                Send Me a copy&nbsp;&nbsp;<asp:CheckBox ID="chkSendMailMyself" runat="server" />
            </td>     
        </tr>
         <tr>
            <td colspan="2" height="5">
            </td>
         </tr>   
         <tr>
            <td colspan="2" align="center">            
                 <asp:ImageButton ID="btnContactMe" runat="server" 
                    ImageUrl="" Width="150px"
                    onclick="btnContactMe_Click" OnClientClick="javascript:return validate();" />
               </td>     
        </tr>      
         <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" Visible="false" />
               </td>     
        </tr>
    </table>
</div>

