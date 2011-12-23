<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CountryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewData["Title"].ToString() %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<header>
<h2><%: ViewData["Title"].ToString() %></h2>
</header>

<% using(Html.BeginForm("SaveCountry", "Countries")){ %>
         <%: Html.ValidationSummary(true, "Login fail.") %> 

<%} %>

</asp:Content>
