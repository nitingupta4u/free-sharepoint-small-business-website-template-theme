<%@ Assembly Name="BrickRed.Templates.SmallBusiness, Version=1.0.0.0, Culture=neutral, PublicKeyToken=dd88d6021363d679" %>
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
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>

<table width="100%">
    <tbody>
        <tr>
            <td>
                <div id="dvGoogleMap">
                </div>
                <div id="mapCanvas">
                </div>
                <div id="infoPanel">
                    <b>Current position:</b>
                    <br />
                    <div id="info">
                    </div>
                </div>
            </td>
        </tr>
    </tbody>
</table>


<script type="text/javascript">
    var geocoder = new google.maps.Geocoder();
    var hf_latlang = 'abc';
    var initialLat = '28.60502008328845';
    var initialLon = '77.36267566680908';
    var Title = 'Company Name';

    function updateMarkerPosition(latLng) {
        document.getElementById(hf_latlang).value = [
    latLng.lat(),
    latLng.lng()
  ].join(', ');
        document.getElementById('info').innerHTML = [
    latLng.lat(),
    latLng.lng()
  ].join(', ');
    }

    function initialize() {
        var latLng = new google.maps.LatLng(initialLat, initialLon);
        var map = new google.maps.Map(document.getElementById('mapCanvas'), {
            zoom: 8,
            center: latLng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        var marker = new google.maps.Marker({
            position: latLng,
            title: Title,
            map: map,
            draggable: true
        });

        // Update current position info.
        updateMarkerPosition(latLng);
        //geocodePosition(latLng);

        // Add dragging event listeners.
        google.maps.event.addListener(marker, 'dragstart', function () {
        });

        google.maps.event.addListener(marker, 'drag', function () {
            updateMarkerPosition(marker.getPosition());
        });

        google.maps.event.addListener(marker, 'dragend', function () {
        });
    }

    function ShowEditMap(hf_id, lat, lon,title) {
        hf_latlang = hf_id;
        initialLat = lat;
        initialLon = lon;
        Title = title;

        // Onload handler to fire off the app.
        google.maps.event.addDomListener(window, 'load', initialize);
    }


    function ShowMap(src) {
        document.getElementById("mapCanvas").style.display = "none";
        document.getElementById("infoPanel").style.display = "none";
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