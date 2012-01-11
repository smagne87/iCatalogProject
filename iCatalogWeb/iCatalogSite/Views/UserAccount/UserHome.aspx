<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageUserProfile.master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Home</h2>

Welcome! <%: Model.FirstName + " " + Model.LastName %>

</asp:Content>