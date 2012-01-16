<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Register</h2>

<script type="text/javascript">
    $(document).ready(function () {
        var typesArr = ["text", "tel", "number", "url", "password", "email"];

        $("input").each(function () {
            if ($.inArray($(this).attr("type"), typesArr) >= 0) {
                if ($(this).attr("placeholder") == undefined) {
                    $(this).watermark($(this).attr("watermarktext"), { className: "watermark", userNative: false });
                    $(this).addClass("watermark");
                }
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
                firstName: "required",
                lastName: "required",
                termsOfUse: "required",
                password: {
                    required: true,
                    minlength: 5
                },
                userName: {
                    required: true,
                    checkUserNameUnique: true,
                    minlength: 5
                },
                email: {
                    required: true,
                    email: true,
                    checkMailUnique: true
                }
            },
            messages:
            {
                firstName: "Please specify your first name",
                lastName: "Please specify your last name",
                termsOfUse: "Accept terms of use",
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
            },
            invalidHandler: function (form, validator) {
                for (var i = 0; i < validator.errorList.length; i++) {
                    var field = validator.errorList[i];
                    var ele = field.element;
                    var message = field.message;
                    $(ele).attr("title", field.message);
                    $(ele).tooltip(
                    {
                        offset: [50, 0],
                        position: 'middle right',
                        events: {
                            input: "focus mouseover,blur mouseout"
                        }
                    });
                }
            }
        });

        $("#userName").focusout(function () {
            var user = $("#userName").val();
            if (user.length > 4) {
                $("#status").html('<img src="../../Content/themes/images/loader.gif" align="absmiddle" style="width:16px; height:16px;" /> Checking User Name...');

                var json = JSON.stringify({ UserName: user });

                $.ajax({
                    url: '/UserAccount/UserAvailavility',
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
                $("#statusEmail").html('<img src="../../Content/themes/images/loader.gif" align="absmiddle" style="width:16px; height:16px;" /> Checking Email...');

                var json = JSON.stringify({ Email: mail });

                $.ajax({
                    url: '/UserAccount/UserEmailAvailavility',
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
			        var user = getUser();
			        var json = JSON.stringify(user);

			        $.ajax({
			            url: '/UserAccount/RegisterUser',
			            type: 'POST',
			            dataType: 'json',
			            data: json,
			            contentType: 'application/json; charset=utf-8',
			            success: function (data) {
			                if (data.Url) {
			                    var url = data.Url;
			                    window.location = url;
			                }
			            }
			        });
			    }
			});

        function getUser() {
            var fname = $("#firstName").val();
            var lname = $("#lastName").val();
            var uname = $("#userName").val();
            var pass = $("#password").val();
            var email = $("#email").val();
            return (uname == "") ? null : { FirstName: fname, LastName: lname, UserName: uname, Password: pass, Email: email };
        }

    });
</script>
<article>
    <h3>
        <a href="/CompanyAccount/RegisterCom">Ups, I'm a Company.</a>
    </h3>
    <form id="registerForm">
        <fieldset class="ui-corner-all">
            <div class="editor-label">
                <h3>First Name</h3>
            </div>
            <div class="editor-field">
                <input type="text" name="firstName" watermarktext="First Name" id="firstName" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <h3>Last Name</h3>
            </div>
            <div class="editor-field">
                <input type="text" name="lastName" id="lastName" watermarktext="Last Name" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <h3>User Name</h3>
            </div>
            <div class="editor-field">
                <input type="text" name="userName" id="userName" watermarktext="User Name" class="text ui-widget-content ui-corner-all" />
                <div id="status"></div>
            </div>
            <div class="editor-label">
                <h3>Password</h3>
            </div>
            <div class="editor-field">
                <input type="password" name="password" id="password" watermarktext="Password" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <h3>E-Mail</h3>
            </div>
            <div class="editor-field">
                <input type="text" name="email" id="email" watermarktext="example: info@icatalog.com" class="text ui-widget-content ui-corner-all" />
                <div id="statusEmail"></div>
            </div>
            <div class="editor-label">
                <div style="float:left;">
                    <button id="registerUser">Register</button>
                </div>
                <div style="float:left;" class="caja-checkBoxTexto">
                    <h3>
                        <input type="checkbox" name="termsOfUse" id="termsOfUse"/>I have read and accept the Terms of Use.
                    </h3>
                </div>
                <div class="cleared"></div>
            </div>
        </fieldset>
    </form>
</article>
</asp:Content>
