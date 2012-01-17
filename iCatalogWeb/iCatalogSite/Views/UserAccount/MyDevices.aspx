<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageUserProfile.master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.DeviceModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    My Devices
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>My Devices</h1>
<script type="text/javascript" >
    function confirmDeleteDevice(id) { 
        $("#devicehdntoDelete").val(id);
        $("#dialog-confirm").dialog('open');
    }
    $(function () {
        $("#dialog:ui-dialog").dialog("destroy");

        $("#dialog-confirm").dialog({
            resizable: false,
            autoOpen: false,
            height: 180,
            modal: true,
            buttons: {
                "Remove Device": function () {
                    var id = $("#devicehdntoDelete").val();
                    var json = JSON.stringify({ IdDevice: id });

                    $.ajax({
                        url: '/UserAccount/RemoveDevice',
                        type: 'POST',
                        dataType: 'json',
                        data: json,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            refreshTable();
                            // get the result and do some magic with it
                            var message = data.Message;
                            $("#message").html(message);
                            $("#dialog-message").dialog("open");
                        }
                    });
                    $("#dialog-confirm").dialog('close');
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
</script>

<div id="container">
<% Html.RenderPartial("DevicesList"); %>
</div>

<div id="deviceDialog-form" title="My Devices">
    <form action="MyDevices.aspx">
    </form>
</div>
<div id="dialog-confirm" title="Remove Devices?">
	<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>These item will be remove from my Devices. Are you sure?</p>
    <input type="hidden" name="devicehdntoDelete" id="devicehdntoDelete" />
</div>
</asp:Content>
