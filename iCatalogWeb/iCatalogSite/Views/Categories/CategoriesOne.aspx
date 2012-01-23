<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageCompanyProfile.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CategoryOneModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Categories One
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Categories One</h2>
<script type="text/javascript">
    $(document).ready(function () {
        $("#create-categoryOne").button()
        .click(function () {
            var a = window.location.toString().replace(window.location.pathname,"/Categories/CategoryOne/IdCompany=");
            alert(a);
        });
    });
</script>
<style>
#example { width: 600px; }
#container { width: 600px; }
</style>
<header>
    <button id="create-categoryOne">New Category One</button>
</header>
<div id="container">
<% Html.RenderPartial("CategoriesList"); %>
</div>
</asp:Content>
