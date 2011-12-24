<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CountriesPage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<header>
    <h2>CountriesPage</h2>
</header>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $('#example').dataTable({
            "iDisplayLength": 25,
            "aaSorting": [[1, "asc"]],
            "aoColumns": [{ "bSortable": true }, { "bSortable": true}, null]
        });
    });
</script>

<style>
#example { width: 100%; }
#container { width: 600px; }
</style>
<header>
    <ul>
        <%: Html.ActionLink("New", "EditCountry", new { id = 0 })%>
    </ul>
</header>
<div id="container">
<% Html.Grid((List<iCatalogSite.Models.CountryModel>)ViewData["CountriesList"])
       .Columns(column =>
           {
               column.For(co => Html.ActionLink(co.IdCountry.ToString(), "EditCountry", new { id = co.IdCountry })).Named("Id Country");
               column.For(co => co.CountryName);
               column.For(co => Html.ActionLink("Delete", "DeleteCountry", new { id = co.IdCountry })).Named("Delete");
           }).Attributes(id => "example").Render();
%>
</div>
</asp:Content>
