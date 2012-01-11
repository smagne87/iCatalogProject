<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Register Company
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $.validator.addMethod("checkUserNameUnique", function (value, element) {
            var result = true;
            if ($("#userName").attr("class").indexOf("ui-state-error") > 0) {
                result = false;
            }
            return result;
        },
        "User Name already exists.");

        $.validator.addMethod("checkMailUnique", function (value, element) {
            var result = true;
            if ($("#email").attr("class").indexOf("ui-state-error") > 0) {
                result = false;
            }
            return result;
        },
        "Email already exists.");

        $("#registerForm").validate({
            rules: {
                companyName: { required: true },
                userName: {
                    required: true,
                    checkUserNameUnique: true,
                    minlength: 5
                },
                password: {
                    required: true,
                    minlength: 5
                },
                email: {
                    required: true,
                    email: true,
                    checkMailUnique: true
                },
                termsOfUse: { required: true },
                webUrl: {
                    url: true
                }
            },
            messages:
            {
                webUrl: "Invalid url format",
                companyName: "Please specify your first name",
                termsOfUse: "*",
                password: {
                    required: "Please specify your password",
                    minlength: "Please enter at least 5 characters."
                },
                userName: {
                    required: "Please specify your user name",
                    checkUserNameUnique: "User Name Already Exists.",
                    minlength: "Please enter at least 5 characters."
                },
                email: {
                    required: "Please specify your Email address.",
                    email: "Your email address must be in the format of name@domain.com",
                    checkMailUnique: "Email Address Already Exists."
                }
            }
        });

        $("#userName").focusout(function () {
            var user = $("#userName").val();
            if (user.length > 4) {
                $("#status").html('<img src="../../Content/themes/images/loader.gif" align="absmiddle" style="width:16px; height:16px;" /> Checking User Name...');

                var json = JSON.stringify({ CompanyUserName: user });

                $.ajax({
                    url: '/CompanyAccount/UserAvailavility',
                    type: 'POST',
                    dataType: 'json',
                    data: json,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        var message = data.Message;
                        if (message == 'OK') {
                            $("#userName").removeClass("ui-state-error");
                            $("#status").html('<img src="../../Content/themes/images/available.png" />');
                        }
                        else {
                            $("#userName").addClass("ui-state-error");
                            $("#status").html('<img src="../../Content/themes/images/not-available.png" />');
                        }
                    }
                });
            }
            else {
                $("#status").html('');
            }
        });

        $("#email").focusout(function () {
            if ($("#registerForm").validate().element("#email")) {
                var mail = $("#email").val();
                $("#statusEmail").html('<img src="../../Content/themes/images/loader.gif" align="absmiddle" style="width:16px; height:16px;" /> Checking Email...');

                var json = JSON.stringify({ Email: mail });

                $.ajax({
                    url: '/CompanyAccount/UserEmailAvailavility',
                    type: 'POST',
                    dataType: 'json',
                    data: json,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        var message = data.Message;
                        if (message == 'OK') {
                            $("#email").removeClass("ui-state-error");
                            $("#statusEmail").html('<img src="../../Content/themes/images/available.png" />');
                        }
                        else {
                            $("#email").addClass("ui-state-error");
                            $("#statusEmail").html('<img src="../../Content/themes/images/not-available.png" />');
                        }
                    }
                });
            }
            else {
                $("#statusEmail").html('');
            }
        });

        $("#registerUser")
			.button()
			.click(function () {
			    if ($("#registerForm").valid()) {
			        var user = getCompany();
			        var json = JSON.stringify(user);

			        $.ajax({
			            url: '/CompanyAccount/RegisterCompany',
			            type: 'POST',
			            dataType: 'json',
			            data: json,
			            contentType: 'application/json; charset=utf-8',
			            success: function (data) {
			                var message = data.Message;
			            }
			        });
			    }
			});

        function getCompany() {
            debugger;
            var cname = $("#companyName").val();
            var uname = $("#userName").val();
            var pass = $("#password").val();
            var email = $("#email").val();
            var web = $("#webUrl").val();
            var phone = $("#phoneNumber").val();
            var idcountry = $("#ddlCountriesList").val();
            var idcity = $("#ddlCitiesList").val();
            var st = $("#street").val();
            var stNumber = $("#stNumber").val();
            var stCp = $("#stCp").val();
            var address = st + ' ' + stNumber + ' ' + stCp;
            return (uname == "") ? null : { CompanyName: cname, CompanyUserName: uname, Password: pass, Email: email, WebUrl: web, Phone: phone, IdCountry: idcountry, IdCity: idcity, Address: address };
        }

        $("#ddlCountriesList").change(function () {
            var idCountry = $(this).val();
            $.getJSON("/CompanyAccount/GetAllCitiesByCountryId", { IdCountry: idCountry },
                function (data) {
                    var ddlCi = $("#ddlCitiesList");
                    ddlCi.empty();
                    ddlCi.append($('<option/>', {
                        value: 0,
                        text: "<Select City>"
                    }));
                    $.each(data, function (index, itemData) {
                        ddlCi.append($('<option/>', {
                            value: itemData.IdCity,
                            text: itemData.CityName
                        }));
                    });
                });
        });
    });
</script>

<h2>Register Company</h2>
    <a href="/Home/Register">Ups, I'm a User.</a>
    <form id="registerForm" action="Index">
        <fieldset class="ui-corner-all">
            <div>
                <div style="float:left; width:40%;">
                    <div class="editor-label">
                        <label for="CompanyName" id="lblCompanyName">Company Name</label>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="companyName" id="companyName" class="text ui-widget-content ui-corner-all" />
                    </div>
                    <div class="editor-label">
                        <label for="UserName" id="lblUserName">Company User Name</label>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="userName" id="userName" class="text ui-widget-content ui-corner-all" />
                        <div id="status"></div>
                    </div>
                    <div class="editor-label">
                        <label for="Password" id="lblPassword">Password</label>
                    </div>
                    <div class="editor-field">
                        <input type="password" name="password" id="password" class="text ui-widget-content ui-corner-all" />
                    </div>
                    <div class="editor-label">
                        <label for="Email" id="lblEmail">E-Mail</label>
                    </div>
                    <div class="editor-field">
                        <input type="email" name="email" id="email" class="text ui-widget-content ui-corner-all" />
                        <div id="statusEmail"></div>
                    </div>
                    <div class="editor-label">
                        <label for="WebUrl" id="lblweb">Web</label>
                    </div>
                    <div class="editor-field">
                        <input type="url" name="webUrl" id="webUrl" class="text ui-widget-content ui-corner-all" />
                    </div>
                </div>
                <div style="float:left; width:50%;">
                    <div class="editor-label">
                        <label for="PhoneNumber" id="Label1">Phone Number</label>
                    </div>
                    <div class="editor-field">
                        <input type="tel" name="phoneNumber" id="phoneNumber" class="text ui-widget-content ui-corner-all" />
                    </div>
                    <div class="editor-label">
                        <label for="IdCountry" id="lblIdCountry">Country</label>
                    </div>
                    <div class="editor-field">
                        <%= Html.DropDownList("ddlCountriesList", (IEnumerable<SelectListItem>)ViewData["CountriesList"], "<Select Country>") %>
                    </div>
                    <div class="editor-label">
                        <label for="IdCity" id="Label2">City</label>
                    </div>
                    <div class="editor-field">
                        <%= Html.DropDownList("ddlCitiesList", new SelectList(Enumerable.Empty<SelectListItem>(), "IdCity", "CityName"), "<Select City>")%>
                    </div>
                    <div class="editor-label">
                        <label for="Address" id="lblAddress">Address</label>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="street" id="street" class="text ui-widget-content ui-corner-all" />
                        <input type="number" name="stNumber" id="stNumber" class="text ui-widget-content ui-corner-all" style="width:70px;" />
                        <input type="text" name="stCp" id="stCp" class="text ui-widget-content ui-corner-all"  style="width:70px;" />
                    </div>
                    <div class="editor-label">
                        <label for="AceptTerms" id="lblAceptTerms">I have read and accept the Terms of Use.</label>
                    </div>
                    <div class="editor-field">
                        <input type="checkbox" name="termsOfUse" id="termsOfUse"/>
                    </div>
                </div>
                <div class="cleared"></div>
            </div>
            <div class="editor-label" style="padding-left:40%; float:right;">
                <button id="registerUser">Register</button>
            </div>
        </fieldset>
    </form>
</asp:Content>
