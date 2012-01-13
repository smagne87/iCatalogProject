<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        $("#loginForm").validate({
            rules: {
                Password: {
                    required: true
                },
                UserName: {
                    required: true
                }
            },
            messages:
            {
                Password: {
                    required: "Please specify your password"
                },
                UserName: {
                    required: "Please specify your user name"
                }
            }
        });

        $("#loginForm").submit(function () {
            return false;
        });

        $("#logIn").button()
            .click(function () {
                if ($("#loginForm").valid()) {
                    var user = getUser();
                    var json = JSON.stringify(user);

                    $.ajax({
                        url: '/Home/LogOn',
                        type: 'POST',
                        dataType: 'json',
                        data: json,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            if (data.Message) {
                                var message = data.Message;
                                updateTips(message);
                            }
                            if (data.Url) {
                                var url = data.Url;
                                window.location = url;
                            }
                        }
                    });
                }
            });

        function updateTips(t) {
            $(".validateTips").text(t);
            $(".validateTips").addClass("ui-state-highlight");
            setTimeout(function () {
                $(".validateTips").removeClass("ui-state-highlight", 1500);
            }, 500);
        }

        function getUser() {
            var uname = $("#UserName").val();
            var pass = $("#Password").val();
            return (uname == "") ? null : { UserName: uname, Password: pass };
        }
    });
</script>

<h2>Login</h2>
<form id="loginForm">
    <fieldset class="ui-corner-all" style="width:350px;">
        <p class="validateTips"></p>
        <div class="editor-label">
            <h3>User</h3>
        </div>
        <div class="editor-field">
            <input type="text" name="UserName" id="UserName" class="text ui-widget-content ui-corner-all" />
        </div>
        <div class="editor-label">
            <h3>Password</h3>
        </div>
        <div class="editor-field">
            <input type="password" name="Password" id="Password" class="text ui-widget-content ui-corner-all" />
        </div>
        <div class="editor-field">
        </div>
        <div class="editor-label">
            <div style="float:left;">
                <button id="logIn">Log in</button>
            </div>
            <div style="float:left;" class="caja-checkBoxTexto">
                <h3>
                    <input type="checkbox" name="rememberMe" id="rememberMe" />Remember Me
                </h3>
            </div>
            <div class="cleared"></div>
        </div>
        <div class="cajaForgot">
            <h6>
                <%: Html.ActionLink("Forgot Password?", "ForgotPassword", "Home") %>
                <%: Html.ActionLink("Register!", "Register", "Home")%>
            </h6>
        </div>
    </fieldset>
</form>
</asp:Content>
