<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Register Company
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        var typesArr = ["text", "tel", "number", "url", "password", "email"];

        $("input").each(function () {
            if ($.inArray($(this).attr("type"), typesArr) >= 0) {
                $(this).watermark($(this).val(), { className: "watermark", userNative: false });
                $(this).val("");
                $(this).addClass("watermark");
            }
        });

        $("input").focusin(function () {
            if ($(this).val() != "") {
                $(this).removeClass("watermark");
            }
        });

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
            var mail = $("#email").val();
            if (mail.indexOf("@") > 0) {
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
            $.getJSON("/Cities/GetAllCitiesByCountryId", { IdCountry: idCountry },
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
    <h3><a href="/Home/Register">Ups, I'm a User.</a></h3>
    <form id="registerForm" action="Index">
        <fieldset class="ui-corner-all">
            <div>
                <div style="float:left; width:40%;">
                    <div class="editor-label">
                        <h3>Company Name</h3>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="companyName" value="Company Name" id="companyName" class="text ui-widget-content ui-corner-all" />
                    </div>
                    <div class="editor-label">
                        <h3>Company User Name</h3>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="userName" value="User Company Name" id="userName" class="text ui-widget-content ui-corner-all" />
                        <div id="status"></div>
                    </div>
                    <div class="editor-label">
                        <h3>Password</h3>
                    </div>
                    <div class="editor-field">
                        <input type="password" value="Password" name="password" id="password" class="text ui-widget-content ui-corner-all" />
                    </div>
                    <div class="editor-label">
                        <h3>E-Mail</h3>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="email" id="email" value="example: info@icatalog.com" class="text ui-widget-content ui-corner-all" />
                        <div id="statusEmail"></div>
                    </div>
                    <div class="editor-label">
                        <h3>Web</h3>
                    </div>
                    <div class="editor-field">
                        <input type="text" value="example: http://www.icatalog.com" name="webUrl" id="webUrl" class="text ui-widget-content ui-corner-all" />
                    </div>
                </div>
                <div style="float:left; width:50%;">
                    <div class="editor-label">
                        <h3>Phone Number</h3>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="phoneNumber" value="Phone Number" id="phoneNumber" class="text ui-widget-content ui-corner-all" />
                    </div>
                    <div class="editor-label">
                        <h3>Country</h3>
                    </div>
                    <div class="editor-field">
                        <%= Html.DropDownList("ddlCountriesList", (IEnumerable<SelectListItem>)ViewData["CountriesList"], "<Select Country>") %>
                    </div>
                    <div class="editor-label">
                        <h3>City</h3>
                    </div>
                    <div class="editor-field">
                        <%= Html.DropDownList("ddlCitiesList", new SelectList(Enumerable.Empty<SelectListItem>(), "IdCity", "CityName"), "<Select City>")%>
                    </div>
                    <div class="editor-label">
                        <h3>Address</h3>
                    </div>
                    <div class="editor-field">
                        <input type="text" name="street" id="street" value="Street" class="text ui-widget-content ui-corner-all" />
                        <input type="text" name="stNumber" id="stNumber" value="Number" class="text ui-widget-content ui-corner-all" style="width:70px;" />
                        <input type="text" name="stCp" id="stCp" value="Postal Code" class="text ui-widget-content ui-corner-all"  style="width:70px;" />
                    </div>
                </div>
                <div class="cleared"></div>
            </div>
            <div class="editor-label">
                <div style="float:left; width:65%;">
                    <div style="float:left;">
                        <button id="registerUser">Register</button>
                    </div>
                    <div style="float:left;" class="caja-checkBoxTexto">
                        <h3>
                            <input type="checkbox" name="termsOfUse" id="Checkbox1"/>I have read and accept the Terms of Use.
                        </h3>
                    </div>
                <div class="cleared"></div>
                </div>
            </div>
        </fieldset>
    </form>
</asp:Content>
