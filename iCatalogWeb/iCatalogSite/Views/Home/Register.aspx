<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Register</h2>

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
                firstName: "required",
                lastName: "required",
                termsOfUse: "required",
                password: {
                    required: true,
                    minlength: 5
                },
                userName: {
                    required: true,
                    checkUserNameUnique: true
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
                termsOfUse: "*",
                password: {
                    required: "Please specify your password",
                    minlength: "Please enter at least 5 characters."
                },
                userName: {
                    required: "Please specify your user name",
                    checkUserNameUnique: "User Name Already Exists."
                },
                email: {
                    required: "Please specify your Email address.",
                    email: "Your email address must be in the format of name@domain.com",
                    checkMailUnique: "Email Address Already Exists."
                }
            }
        });

        $("#userName").keyup(function () {
            var user = $("#userName").val();
            if (user.length > 4) {
                $("#status").html('<img src="../../Content/themes/imgs/loader.gif" align="absmiddle" style="width:16px; height:16px;" /> Checking User Name...');

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
                            $("#status").html('<img src="../../Content/themes/imgs/available.png" />');
                        }
                        else {
                            $("#userName").addClass("ui-state-error");
                            $("#status").html('<img src="../../Content/themes/imgs/not-available.png" />');
                        }
                    }
                });
            }
            else {
                $("#status").html('');
            }
        });

        $("#email").keyup(function () {
            if ($("#registerForm").validate().element("#email")) {
                var mail = $("#email").val();
                $("#statusEmail").html('<img src="../../Content/themes/imgs/loader.gif" align="absmiddle" style="width:16px; height:16px;" /> Checking Email...');

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
                            $("#statusEmail").html('<img src="../../Content/themes/imgs/available.png" />');
                        }
                        else {
                            $("#email").addClass("ui-state-error");
                            $("#statusEmail").html('<img src="../../Content/themes/imgs/not-available.png" />');
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
			    var user = getUser();
			    var json = JSON.stringify(user);

			    $.ajax({
			        url: '/UserAccount/RegisterUser',
			        type: 'POST',
			        dataType: 'json',
			        data: json,
			        contentType: 'application/json; charset=utf-8',
			        success: function (data) {
			            var message = data.Message;
			        }
			    });
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
<article style="float:left;">
    <form id="registerForm">
        <fieldset class="ui-corner-all">
            <div class="editor-label">
                <label for="FirstName" id="lblFirstName">First Name</label>
            </div>
            <div class="editor-field">
                <input type="text" name="firstName" id="firstName" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <label for="LastName" id="lblLastName">Last Name</label>
            </div>
            <div class="editor-field">
                <input type="text" name="lastName" id="lastName" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <label for="UserName" id="lblUserName">User Name</label>
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
                <label for="AceptTerms" id="lblAceptTerms">I have read and accept the Terms of Use.</label>
            </div>
            <div class="editor-field">
                <input type="checkbox" name="termsOfUse" id="termsOfUse"/>
            </div>
            <div class="editor-label">
                <button id="registerUser">Register</button>
            </div>
        </fieldset>
    </form>
</article>
</asp:Content>
