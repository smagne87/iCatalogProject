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
            "aoColumns": [{ "bSortable": true }, { "bSortable": false}]
        });
    });
</script>

<style>
#example { width: 100%; }
#container { width: 600px; }
</style>
    
<div id="container">
<% Html.Grid((List<iCatalogSite.Models.CountryModel>)ViewData["CountriesList"])
       .Columns(column =>
           {
               column.For(co => co.IdCountry);
               column.For(co => co.CountryName);
           }).Attributes(id => "example").Render();
%>
</div>
</asp:Content>
