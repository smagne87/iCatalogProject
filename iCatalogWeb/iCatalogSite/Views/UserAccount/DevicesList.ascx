<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iCatalogBB.Device>>" %>

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
            "iDisplayLength": 5,
            "bRetrieve": true,
            "bDestroy": true,
            "aaSorting": [[1, "asc"]],
            "aoColumns": [{ "bSortable": true }, { "bSortable": false }, { "bSortable": false }, { "bSortable": false}]
        });
        oTable.fnDraw(true);
        oTable.fnDeleteRow(0); //this resolves the error when the grid is empty.
    }
</script>
<div id="container">
<% Html.Grid((List<iCatalogBB.Device>)ViewData["DevicesList"])
       .Columns(column =>
           {
               column.For(co => co.DeviceCode).Named("Device Code"); 
               column.For(co => co.DeviceDescription).Named("Device Description");
               column.For(co => co.LastSync).Named("Last Syncronization");               
               column.For(co => co.IdDevice).Named("Delete").Action(co => { %>  
                                    <td><img src="../Content/themes/images/icon-delete.gif" onclick="confirmDeleteDevice('<%= co.DeviceCode  %>')" /></td> <% });
           }).Attributes(id => "example", @class => "table-list", @cellpadding => "0", @cellspacing => "0").Empty("No Devices available").Render();
%>
</div>