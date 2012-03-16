<%@ Page language="C#"   Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage,Microsoft.SharePoint.Publishing,Version=14.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePointWebControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="PublishingNavigation" Namespace="Microsoft.SharePoint.Publishing.Navigation" Assembly="Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceholderID="PlaceHolderPageTitle" runat="server">
	<SharePointWebControls:FieldValue id="PageTitle" FieldName="Title" runat="server" />
</asp:Content>
<asp:Content ContentPlaceholderID="PlaceHolderMain" runat="server">
 <table cellspacing="0" border="0" width="100%" cellpadding="0">  
	    <tr>
             <td valign="top" width="100%">
			   <div style="width: 100%" >
                    <div style="word-wrap: break-word">
                            <PublishingWebControls:RichImageField id="ImageField" FieldName="PublishingPageImage" runat="server"/> 
                    </div>
                </div>
                <div style="margin-top:-15px;">
                <p>
               <WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" ID="Top" Title="loc:Top" />
               </p>
               </div>
		   </td>
         </tr>
      <tr>
		<td>
		 <table width="100%" cellpadding="0" cellspacing="0">
		  <tr>
		   <td valign="top">
			    <div style="width: 100%" >
                <WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" ID="Left" Title="loc:Left" />
                    <div style="min-height: 60px; word-wrap: break-word">
                        <p>
                            <PublishingWebControls:RichHtmlField id="Content" FieldName="PublishingPageContent" runat="server"/>  
                        </p>                            
                    </div>
                </div>
               			  
		   </td>	  
             <td valign="top" class="wp-RPshadowSpace">
			   <WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" ID="Right" Title="loc:Right" />			  
		   </td>
		  </tr>
		 </table>
		</td>
	  </tr>
	</table>
</asp:Content>
