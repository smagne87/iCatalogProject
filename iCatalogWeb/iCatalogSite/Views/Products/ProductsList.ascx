<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iCatalogSite.Models.ProductModel>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<style>
    #example
    {
        width: 600px;
    }
    #container
    {
        width: 600px;
    }
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
        $(".dataTables_filter input").addClass("text");
        $(".dataTables_filter input").addClass("ui-widget-content");
    }
</script>
<div id="container">
    <% Html.Grid((List<iCatalogBB.Product>)ViewData["ProductsList"])
       .Columns(column =>
           {
               column.For(co => co.IdProduct).Named("Id Product");
               column.For(co => co.ProductName).Named("Name");
               column.For(co => co.IdProduct).Named("Edit").Action(co =>
                                { %>
                    <td>
                        <img src="../Content/themes/images/icon_edicion.gif" onclick="editProduct(
                        '<%= co.IdProduct  %>', 
                        '<%= co.ProductName  %>'), 
                        '<%= co.ProductDescription %>',
                        '<%= co.IdCategoryOne %>',
                        '<%= co.IdCategoryTwo %>',
                        '<%= co.IdCategoryThree %>'"
                        />
                    </td>
                    <% });
               column.For(co => co.IdProduct).Named("Delete").Action(co =>
               { %>
                    <td>
                        <img src="../Content/themes/images/icon-delete.gif" onclick="confirmDeleteProduct('<%= co.IdProduct  %>')" />
                    </td>
                    <% });
           }).Attributes(id => "example", @class => "table-list", @cellpadding => "0", @cellspacing => "0").Empty("No products available").Render();
    %>
</div>
