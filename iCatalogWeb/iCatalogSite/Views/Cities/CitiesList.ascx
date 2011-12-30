<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iCatalogBB.City>>" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>

<style>
#citiesGrid { width: 600px; }
#container { width: 600px; }
</style>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        var oTable;
        defineTable();
    });

    function defineTable() {
        oTable = $('#citiesGrid').dataTable({
            "iDisplayLength": 25,
            "bRetrieve": true,
            "bDestroy": true,
            "aaSorting": [[1, "asc"]],
            "aoColumns": [{ "bSortable": true }, { "bSortable": true }, { "bSortable": true }, { "bSortable": false }, { "bSortable": false}]
        });
        oTable.fnDraw(true);
        oTable.fnDeleteRow(0); //this resolves the error when the grid is empty.
    }
</script>
<div id="container">
<% Html.Grid((List<iCatalogBB.City>)ViewData["CitiesList"])
       .Columns(column =>
           {
               column.For(c => c.IdCity).Named("Id Country"); 
               column.For(c => c.CityName).Named("City");
               column.For(c => c.CountryName).Named("Country");
               column.For(c => c.IdCity).Named("Edit").Action(co =>
               { %>  
                                    <td><img src="../Content/themes/imgs/icon_edicion.gif" onclick="editCity('<%= co.IdCity %>', '<%= co.CityName  %>', '<%= co.IdCountry  %>')" /></td> <% });
               column.For(c => c.IdCity).Named("Delete").Action(co =>
               { %>  
                                    <td><img src="../Content/themes/imgs/icon-delete.gif" onclick="confirmDeleteCity('<%= co.IdCity  %>')" /></td> <% });
           }).Attributes(id => "citiesGrid", @class => "table-list").Empty("No cities availables").Render();
%>
</div>