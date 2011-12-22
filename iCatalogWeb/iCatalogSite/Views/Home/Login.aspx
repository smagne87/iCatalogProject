<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Login</h2>

<%using (Html.BeginForm("LogOn", "Home"))
  {%>
         <%: Html.ValidationSummary(true, "Login fail.") %> 
        <fieldset>
            <div class="editor-label">
                <%: Html.LabelFor(m => m.UserName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.UserName) %>
                <%: Html.ValidationMessageFor(m => m.UserName) %>
            </div>
            <div class="editor-label">
                <%: Html.LabelFor(m => m.Password) %>
            </div>
            <div class="editor-field">
                <%: Html.PasswordFor(m => m.Password) %>
                <%: Html.ValidationMessageFor(m => m.Password) %>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(m => m.RememberMe) %>
            </div>
            <div class="editor-label">
                <%: Html.LabelFor(m => m.RememberMe) %>
            </div>
            <input type="submit" value="Log on" />
            <%: Html.ActionLink("Forgot Password?", "ForgotPassword", "Home") %>
            <%: Html.ActionLink("Register!", "Register", "Home")%>
        </fieldset>
  <%
  }%>
</asp:Content>
