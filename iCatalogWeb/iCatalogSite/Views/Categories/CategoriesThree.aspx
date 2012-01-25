<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageCompanyProfile.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CategoryThreeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Categories Three
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Categories Three</h2>
<script type="text/javascript">
    $(document).ready(function () {
        $("#create-categoryThree").button()
        .click(function () {
            var idcompany = $("#companyId").val();
            var url = window.location.toString().replace(window.location.pathname, "/Categories/CategoryThree?IdCompany=" + idcompany);
            window.location.href = url;
        });

        $("#dialog-confirm").dialog({
            resizable: false,
            autoOpen: false,
            height: 180,
            modal: true,
            buttons: {
                "Delete Category": function () {
                    var idcom = $("#catIdComhdntoDelete").val();
                    var catName = $("#catNamehdntoDelete").val();
                    var json = JSON.stringify({ CategoryThreeName: catName, IdCompany: idcom });

                    $.ajax({
                        url: '/Categories/DeleteAllCategoryThree',
                        type: 'POST',
                        dataType: 'json',
                        data: json,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            refreshTable();
                            // get the result and do some magic with it
                            var message = data.Message;
                            $("#message").html(message);
                            $("#dialog-message").dialog("open");
                        }
                    });
                    $("#dialog-confirm").dialog('close');
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

        function refreshTable() {
            $.ajaxSetup({
                async: false,
                cache: false,
                dataType: "html"
            });
            $.get('/Categories/CategoriesThree',
                function (response) {
                    $("#container").replaceWith(response);
                });
        }

    });

    function editCategory(catName, idcompany) {
        var url = window.location.toString().replace(window.location.pathname, "/Categories/CategoryThree?IdCompany=" + idcompany + "&CategoryThreeName=" + catName);
        window.location.href = url;
    }

    function confirmDeleteCategory(catName, idcompany) {
        $("#catIdComhdntoDelete").val(idcompany);
        $("#catNamehdntoDelete").val(catName);
        $("#dialog-confirm").dialog('open');
    }
</script>
<style>
#example { width: 600px; }
#container { width: 600px; }
</style>
<header>
    <input type="hidden" id="companyId" value="<%= ViewData["IdCompany"] %>" />
    <button id="create-categoryThree">New Category Three</button>
</header>
<div id="container">
<% Html.RenderPartial("CategoriesThreeList"); %>
</div>

<div id="dialog-confirm" title="Delete Category?">
	<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>These item will be permanently deleted and cannot be recovered. Are you sure?</p>
    <input type="hidden" name="catNamehdntoDelete" id="catNamehdntoDelete" />
    <input type="hidden" name="catIdComhdntoDelete" id="catIdComhdntoDelete" />
</div>
</asp:Content>
