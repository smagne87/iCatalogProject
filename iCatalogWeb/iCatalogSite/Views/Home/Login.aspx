<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.LogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Login</h2>

<%using (Html.BeginForm("Login", "User"))
  {%>

  <%
  }%>
</asp:Content>
