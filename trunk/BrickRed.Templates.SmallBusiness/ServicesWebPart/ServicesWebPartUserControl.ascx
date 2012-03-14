<%@ Assembly Name="BrickRed.Templates.SmallBusiness, Version=1.0.0.0, Culture=neutral, PublicKeyToken=dd88d6021363d679" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServicesWebPartUserControl.ascx.cs"
    Inherits="BrickRed.Templates.SmallBusiness.ServicesWebPart.ServicesWebPartUserControl" %>

<script type="text/javascript" src="/_layouts/BrickRed.Templates.SmallBusiness/jquery-1.6.2.min.js"></script>

<SharePoint:ScriptLink ID="ScriptLink1" Name="SP.js" runat="server" OnDemand="true"
    Localizable="false" />


<div style="text-align: left;margin:5px 5px 5px -0.5px;" id="gallery">
</div>
<div class="clear">
</div>

<script type="text/javascript">

    $(document).ready(function () {
        ExecuteOrDelayUntilScriptLoaded(ReadList, "sp.js");   //this is necessary to ensure the library is loaded before function triggered
    });

    var namedListItem;
    var c = 0;
    var t;
    var rotatingImages = '';
    var rotatingImagesTitle = '';
    var rotatingImagesDescription = '';
    var temp = 0;
    var innerImgHTML = '';
    var siteURl = window.location.pathname;
    var index = siteURl.lastIndexOf("/");
    siteURl = siteURl.substring(0, index);
    siteURl = siteURl.toLowerCase().replace("pages", "Lists/Services");


    function ReadList() {
        var clientContext = SP.ClientContext.get_current();
        //or use the syntax below for accessing a list that may not reside in the current site
        //var clientContext = new SP.ClientContext(rootUrl);

        var myList = clientContext.get_web().get_lists().getByTitle('Services'); //actual list name here

        var camlQuery = new SP.CamlQuery();
        camlQuery.set_viewXml('<OrderBy><FieldRef Name="FileLeafRef" /></OrderBy>'); //caml statement goes here between the single quotes
        namedListItem = myList.getItems(camlQuery);

        clientContext.load(namedListItem);
        //or use below to specify to load only the required properties for better performance
        //clientContext.load(namedListItem, 'Include(field1, field2)');

        clientContext.executeQueryAsync(Function.createDelegate(this, this.onQuerySucceeded), Function.createDelegate(this, this.onQueryFailed));
    }

    function onQuerySucceeded(sender, args) {
        var listItemInfo = '';
        var flag = true;

        var listItemEnumerator = namedListItem.getEnumerator();

        while (listItemEnumerator.moveNext()) {
            var oListItem = listItemEnumerator.get_current();

            rotatingImages = oListItem.get_item('FileLeafRef');               // gets the images name
            rotatingImagesDescription = oListItem.get_item('Description');    // gets the images description
            rotatingImagesTitle = oListItem.get_item('Title');                // gets the images title
            if (flag) {
                innerImgHTML += "<a href=\"#\" class=\"show\"><img src=\"" + siteURl + "/" + rotatingImages + "\" alt=\"" + rotatingImagesTitle + "\" width=\"325\" height=\"233\" title=\"\" alt=\"\" rel=\"<h3>" + rotatingImagesTitle + " </h3>" + rotatingImagesDescription + " \"/></a>";
                flag = false;
            }
            else {
                innerImgHTML += "<a href=\"#\"><img src=\"" + siteURl + "/" + rotatingImages + "\" alt=\"" + rotatingImagesTitle + "\" width=\"325\" height=\"233\" title=\"\" alt=\"\" rel=\"<h3>" + rotatingImagesTitle + " </h3>" + rotatingImagesDescription + " \"/></a>";
            }
            rotatingImages = '';
            rotatingImagesTitle = '';
            rotatingImagesDescription = '';
        }
        innerImgHTML += "<div class='caption'><div class='content'></div>    </div>       ";
        document.getElementById('gallery').innerHTML = innerImgHTML;

        //Execute the slideShow
        slideShow();
    }

    // if the Query Gets Failed
    function onQueryFailed(sender, args) {

        alert('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
    }

    function slideShow() {

        //Set the opacity of all images to 0
        $('#gallery a').css({ opacity: 0.0 });

        //Get the first image and display it (set it to full opacity)
        $('#gallery a:first').css({ opacity: 1.0 });

        //Set the caption background to semi-transparent
        $('#gallery .caption').css({ opacity: 0.7 });

        //Resize the width of the caption according to the image width
        $('#gallery .caption').css({ width: $('#gallery a').find('img').css('width') });

        //Get the caption of the first image from REL attribute and display it
        $('#gallery .content').html($('#gallery a:first').find('img').attr('rel'))
	.animate({ opacity: 0.7 }, 400);

        //Call the gallery function to run the slideshow, 6000 = change to next image after 6 seconds
        setInterval('gallery()', 6000);
    }

    function gallery() {

        //if no IMGs have the show class, grab the first image
        var current = ($('#gallery a.show') ? $('#gallery a.show') : $('#gallery a:first'));


        //Get next image, if it reached the end of the slideshow, rotate it back to the first image
        var next = ((current.next().length) ? ((current.next().hasClass('caption')) ? $('#gallery a:first') : current.next()) : $('#gallery a:first'));

        //Get next image caption
        var caption = next.find('img').attr('rel');

        //Set the fade in effect for the next image, show class has higher z-index
        next.css({ opacity: 0.0 })
	.addClass('show')
	.animate({ opacity: 1.0 }, 1000);

        //Hide the current image
        current.animate({ opacity: 0.0 }, 1000)
	.removeClass('show');

        //Set the opacity to 0 and height to 1px
        $('#gallery .caption').animate({ opacity: 0.0 }, { queue: false, duration: 0 }).animate({ height: '1px' }, { queue: true, duration: 300 });

        //Animate the caption, opacity to 0.7 and heigth to 100px, a slide up effect
        $('#gallery .caption').animate({ opacity: 0.7 }, 100).animate({ height: '100px' }, 500);

        //Display the content
        $('#gallery .content').html(caption);
    }

</script>

