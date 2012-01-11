<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageUserProfile.master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Profile</h1>

<script type="text/javascript">
    $(document).ready(function () {
        var firstName = $("#firstName"), lastName = $("#lastName"), idUser = $("#idUser"),
            fnameDetail = $("#fnameDetail"), lnameDetail = $("#lnameDetail"), emailDetail = $("#emailDetail"), email = $("#email"),
            allFields = $([]).add(firstName).add(lastName).add(email),
            tips = $(".validateTips");
        firstName.hide();
        lastName.hide();
        email.hide();
        $("#savePersonalData").hide();

        function updateTips(t) {
            tips
				.text(t)
				.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }

        function checkEmpty(o, n) {
            if (o.val() == "") {
                o.addClass("ui-state-error");
                updateTips(n + " is Mandatory.");
                return false;
            } else {
                return true;
            }
        }

        function getUserData() {
            var fname = firstName.val();
            var lname = lastName.val();
            var e = email.val();
            var id = idUser.val();
            return (id == "") ? null : { IdUser: id, FirstName: fname, LastName: lname, Email: e };
        }

        $("#savePersonalData")
        .button()
            .click(function () {
                var bValid = true;
                allFields.removeClass("ui-state-error");

                bValid = bValid && checkEmpty(firstName, "First Name");
                bValid = bValid && checkEmpty(lastName, "Last Name");
                bValid = bValid && checkEmpty(email, "Email");

                if (bValid) {
                    var user = getUserData();
                    var json = JSON.stringify(user);

                    $.ajax({
                        url: '/UserAccount/SaveUserData',
                        type: 'POST',
                        dataType: 'json',
                        data: json,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            var message = data.Message;
                            fnameDetail.text(firstName.val());
                            lnameDetail.text(lastName.val());
                            emailDetail.text(email.val());
                            settingEdit();
                            $("#message").html(message);
                            $("#dialog-message").dialog("open");
                        }
                    });
                }
            });

        $("#iconEditPD").click(function () {
            settingEdit();
            firstName.val(fnameDetail.text());
            lastName.val(lnameDetail.text());
            email.val(emailDetail.text());
        });

        function settingEdit() {
            if ($("#iconEditPD span").attr("class").indexOf("pencil") > 0) {
                $("#iconEditPD span").removeClass("ui-icon-pencil");
                $("#iconEditPD span").addClass("ui-icon-cancel");
                $("#iconEditPD div").attr("title", "Cancel");
                fnameDetail.hide();
                lnameDetail.hide();
                emailDetail.hide();
                firstName.show();
                lastName.show();
                email.show();
                $("#savePersonalData").show();
            }
            else {
                $("#iconEditPD span").removeClass("ui-icon-cancel");
                $("#iconEditPD span").addClass("ui-icon-pencil");
                $("#iconEditPD div").attr("title", "Edit Location Data");
                $("#savePersonalData").hide();
                firstName.hide();
                lastName.hide();
                email.hide();
                fnameDetail.show();
                lnameDetail.show();
                emailDetail.show();
            }
        }

        $("#dialog-message").dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
</script>

<div>
    <div style="float:left; width:50%">
        <input type="hidden" id="idUser" value="<%: Model.IdUser %>" />
        <h2>
            <span style="float:left;">Personal Data</span>
            <div id="iconEditPD" class="ui-widget ui-helper-clearfix icons">
                <div class="ui-state-default ui-corner-all" title="Edit Location Data">
                    <span class="ui-icon ui-icon-pencil"></span>
                </div>
            </div>
            <div class="cleared"></div>
        </h2>
        <p class="validateTips"></p>
        <h3>First Name:</h3> <h4><span id="fnameDetail"><%: Model.FirstName %></span><input type="text" name="firstName" id="firstName" class="text ui-widget-content ui-corner-all" value="<%: Model.FirstName %>" /></h4>
        <h3>Last Name:</h3> <h4><span id="lnameDetail"><%: Model.LastName %></span><input type="text" name="lastName" id="lastName" class="text ui-widget-content ui-corner-all" value="<%: Model.LastName %>" /></h4>
        <h3>Email:</h3> <h4><span id="emailDetail"><%: Model.Email %></span><input type="text" name="email" id="email" class="text ui-widget-content ui-corner-all" value="<%: Model.Email %>" /></h4>
        <h3>User Name:</h3> <h4><span><%: Model.UserName %></span></h4>
        <h3>
            <span style="float:left; margin-top:5px;">Password: </span>
            <% Html.RenderPartial("ChangePassword", Model); %>
        </h3>
        <h4>************</h4>
        <button id="savePersonalData">Save</button>
    </div>
    <div style="float:left; width:50%">
        <h2>
            <span style="float:left;">Location Data</span>
            <div class="ui-widget ui-helper-clearfix icons">
                <div class="ui-state-default ui-corner-all" title="Edit Location Data">
                    <span class="ui-icon ui-icon-pencil"></span>
                </div>
            </div>
            <div class="cleared"></div>
        </h2>
        <h3>Country:</h3><h4></h4>
        <h3>City:</h3><h4></h4>
        <blockquote>
            <p>
                &#8220;Remember to complete your location data. <br /> The location data helps to find iCatalogs in your city and country.&#8221;
            </p>
        </blockquote>
    </div>
    <div class="cleared"></div>
</div>
<div id="dialog-message" title="Saved">
    <div id="message"></div>
</div>
</asp:Content>
