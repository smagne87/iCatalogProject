<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iCatalogBB.CategoryOne>>" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>

<style>
#categoriesOneGrid { width: 600px; }
#container { width: 600px; }
</style>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        var oTable;
        defineTable();
    });

    function defineTable() {
        oTable = $('#categoriesOneGrid').dataTable({
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
<% Html.Grid((List<iCatalogBB.CategoryOne>)ViewData["CategoryList"])
       .Columns(column =>
           {
               column.For(c => c.IdCategoryOne).Named("Id Category");
               column.For(c => c.CategoryOneDescription).Named("Category Description");
               column.For(c => c.IdCategoryOne).Named("Edit").Action(co =>
               { %>  
                        <td><img src="../Content/themes/images/icon_edicion.gif" onclick="editCategory('<%= co.IdCategoryOne  %>', '<%= co.CategoryOneName  %>', '<%= co.CategoryOneDescription  %>')" /></td> <% });
               column.For(c => c.IdCategoryOne).Named("Delete").Action(co =>
               { %>  
                        <td><img src="../Content/themes/images/icon-delete.gif" onclick="confirmDeleteCategory('<%= co.IdCategoryOne %>')" /></td> <% });
           }).Attributes(id => "categoriesOneGrid", @class => "table-list", @cellpadding => "0", @cellspacing => "0").Empty("No Categories One availables").Render();
%>
</div>