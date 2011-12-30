<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iCatalogBB.Country>>" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>

<style>
#example { width: 600px; }
#container { width: 600px; }
</style>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        var oTable;
        defineTable();
    });

    function defineTable() {
        oTable = $('#example').dataTable({
            "iDisplayLength": 25,
            "bRetrieve": true,
            "bDestroy": true,
            "aaSorting": [[1, "asc"]],
            "aoColumns": [{ "bSortable": true }, { "bSortable": true }, { "bSortable": false }, { "bSortable": false}]
        });
        oTable.fnDraw(true);
        oTable.fnDeleteRow(0); //this resolves the error when the grid is empty.
    }
</script>
<div id="container">
<% Html.Grid((List<iCatalogBB.Country>)ViewData["CountriesList"])
       .Columns(column =>
           {
               column.For(co => co.IdCountry).Named("Id Country"); 
               column.For(co => co.CountryName).Named("Country");
               column.For(co => co.IdCountry).Named("Edit").Action(co => { %>  
                                    <td><img src="../Content/themes/imgs/icon_edicion.gif" onclick="editCountry('<%= co.IdCountry  %>', '<%= co.CountryName  %>')" /></td> <% }); 
               column.For(co => co.IdCountry).Named("Delete").Action(co => { %>  
                                    <td><img src="../Content/themes/imgs/icon-delete.gif" onclick="confirmDeleteCountry('<%= co.IdCountry  %>')" /></td> <% });
           }).Attributes(id => "example", @class => "table-list").Empty("No countries available").Render();
%>
</div>