<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" charset="utf-8">
    $(function () {
        function getNewPass() {
            var name = passwordtext.val();
            return (name == "") ? null : { Password: name };
            }

        var passwordtext = $("#passwordtext"),
        tips = $(".validateTips");

        $("#passwordDialog-form").dialog({
            autoOpen: false,
            height: 250,
            width: 300,
            modal: true,
            buttons: {
                "Change Password": function () {                    
                    var Password = getNewPass();
                    var json = JSON.stringify(Password);                    
                    $(this).dialog("close");
                    $.ajax({
                        url: '/Home/ChangePassword',
                        type: 'POST',
                        dataType: 'json',
                        data: json,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            // get the result and do some magic with it
                            var message = data.Message;
                            $("#message").html(message);
                            $("#dialog-message").dialog("open");
                        }
                    });
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
        });
        $("#dialog-message").dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#change-password")
			.button()
			.click(function () {
			    $("#passwordDialog-form").dialog("open");
			});

    });
</script>
<style>
#example { width: 600px; }
#container { width: 600px; }
</style>
<header>
    <ul>
        <li><button id="change-password">Change Password?</button></li>
    </ul>
</header>
<div id="passwordDialog-form" title="Change Password">
    <form action="IndexBackEnd.aspx">
    <fieldset>
        <legend></legend>
        <p class="validateTips">All form fields are required.</p>
        <div class="editor-label">
            <label for="Change Password" id="lblChangePassword">Change Password?</label>
        </div>
        <div class="editor-field">
            <input type="password" name="passwordtext" id="passwordtext" class="text ui-widget-content ui-corner-all" />
        </div>
    </fieldset>
    </form>
</div>
<div id="dialog-message" title="New Password Saved">
    <div id="message"></div>
</div>
</asp:Content>
