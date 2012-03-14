<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationWebPartUserControl.ascx.cs"
    Inherits="BrickRed.Templates.SmallBusiness.LocationWebPart.LocationWebPartUserControl" %>

<table width="100%">
    <tbody>
        <tr>
            <td>
                <div id="dvGoogleMap">
                </div>
            </td>
        </tr>
    </tbody>
</table>

<script type="text/javascript" language="javascript">
    function ShowMap(src) {
        ifrm = document.createElement("IFRAME");
        ifrm.src = src;
        ifrm.style.width = 714 + "px";
        ifrm.style.height = 306 + "px";
        ifrm.scrolling = "no";
        ifrm.allowtransparency = "true";
        ifrm.frameborder = 0 + "px";
        ifrm.marginwidth = 0 + "px";
        ifrm.marginheight = 0 + "px";
        ifrm.style.border = 0 + "px ";
        ifrm.style.margin = 5 + "px " + 0 + "px " + 0 + "px " + 0 + "px";
        document.getElementById('dvGoogleMap').appendChild(ifrm);
    }
    
</script>
