<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewData["Title"].ToString() %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%: ViewData["Title"].ToString() %></h2>

</asp:Content>
