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

        $("#logIn").button()
    });
</script>

<h2>Login</h2>
<form id="loginForm" action="/Home/LogOn" method="post">
    <fieldset class="ui-corner-all" style="width:350px;">
        <p class="validateTips"></p>
        <div class="editor-label">
            <label for="UserName" id="lblUserName">User</label>
        </div>
        <div class="editor-field">
            <input type="text" name="UserName" id="UserName" class="text ui-widget-content ui-corner-all" />
        </div>
        <div class="editor-label">
            <label for="Password" id="lblPassword">Password</label>
        </div>
        <div class="editor-field">
            <input type="password" name="Password" id="Password" class="text ui-widget-content ui-corner-all" />
        </div>
        <div class="editor-field">
            <label>Remember Me</label><input type="checkbox" name="rememberMe" id="rememberMe"/>
        </div>
        <div class="editor-label">
            <button id="logIn">Log in</button>
        </div>
        <div class="editor-label">
            <%: Html.ActionLink("Forgot Password?", "ForgotPassword", "Home") %>
            <%: Html.ActionLink("Register!", "Register", "Home")%>
        </div>
    </fieldset>
</form>
</asp:Content>
