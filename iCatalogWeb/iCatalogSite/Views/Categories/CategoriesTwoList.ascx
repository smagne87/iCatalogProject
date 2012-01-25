<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iCatalogBB.CategoryTwo>>" %>


<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>

<style>
#categoriesTwoGrid { width: 600px; }
#container { width: 600px; }
</style>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        var oTable;
        defineTable();
    });

    function defineTable() {
        oTable = $('#categoriesTwoGrid').dataTable({
            "iDisplayLength": 25,
            "bRetrieve": true,
            "bDestroy": true,
            "aaSorting": [[1, "asc"]],
            "aoColumns": [{ "bSortable": true }, { "bSortable": false }, { "bSortable": false}]
        });
        oTable.fnDraw(true);
        oTable.fnDeleteRow(0); //this resolves the error when the grid is empty.
        $(".dataTables_filter input").addClass("text");
        $(".dataTables_filter input").addClass("ui-widget-content");
    }
</script>
<div id="container">
<% Html.Grid((List<iCatalogBB.CategoryTwo>)ViewData["CategoriesList"])
       .Columns(column =>
           {
               column.For(c => c.CategoryTwoName).Named("Category Name");
               column.For(c => c.IdCategoryTwo).Named("Edit").Action(co =>
               { %>  
                        <td><img src="../Content/themes/images/icon_edicion.gif" onclick="editCategory('<%= co.CategoryTwoName  %>', '<%= co.IdCompany  %>')" /></td> <% });
               column.For(c => c.IdCategoryTwo).Named("Delete").Action(co =>
               { %>  
                        <td><img src="../Content/themes/images/icon-delete.gif" onclick="confirmDeleteCategory('<%= co.CategoryTwoName %>', '<%= co.IdCompany  %>')" /></td> <% });
           }).Attributes(id => "categoriesTwoGrid", @class => "table-list", @cellpadding => "0", @cellspacing => "0").Empty("No Categories Two availables").Render();
%>
</div>