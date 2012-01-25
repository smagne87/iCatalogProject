<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageCompanyProfile.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CategoryTwoModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Category Two
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Category Two</h2>
<script type="text/javascript">
    function editCategory(id, name, desc) {
        $("#catIdhdn").val(id);
        $("#catName").text(name);
        $("#catDescText").val(desc);
        $("#categoryDialog-form").dialog("open");
    }

    function confirmDeleteCategory(id) {
        $("#cathdntoDelete").val(id);
        $("#dialog-confirm").dialog('open');
    }

    $(document).ready(function () {
        if (window.location.search.indexOf("CategoryTwoName") > 0) {
            $("#catTwoNameDetail").text($("#catTwoName").val());
            $("#catTwoName").hide();
        }
        else {
            $("#catTwoNameDetail").hide();
        }
    });

    $(function () {
        $("#dialog:ui-dialog").dialog("destroy");

        $("#dialog-confirm").dialog({
            resizable: false,
            autoOpen: false,
            height: 180,
            modal: true,
            buttons: {
                "Delete Category": function () {
                    var id = $("#cathdntoDelete").val();
                    var json = JSON.stringify({ IdCategoryTwo: id });

                    $.ajax({
                        url: '/Categories/DeleteSingleCategoryTwo',
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

        var catDescText = $("#catDescText"),
            catIdhdn = $("#catIdhdn"),
            catTwoName = $("#catTwoName"),
            companyId = $("#companyId"),
			allFields = $([]).add(catDescText).add(catIdhdn);

        function updateTips(t, o) {
            $(o).attr("title", t);
            $(o).tooltip(
                    {
                        offset: [50, 0],
                        position: 'middle right',
                        events: {
                            input: "focus mouseover,blur mouseout"
                        }
                    });
        }

        function checkEmpty(o, n) {
            if (o.val() == "") {
                o.addClass("error");
                updateTips(n + " is Mandatory.", o);
                return false;
            } else {
                return true;
            }
        }

        function getCat() {
            var id = catIdhdn.val();
            var name = catTwoName.val();
            var desc = catDescText.val();
            var idcom = companyId.val()
            return (name == "") ? null : { IdCategoryTwo: id, CategoryTwoName: name, CategoryTwoDescription: desc, IdCompany: idcom };
        }

        $("#categoryDialog-form").dialog({
            autoOpen: false,
            height: 300,
            width: 350,
            modal: true,
            buttons: {
                "Save Category": function () {
                    var bValid = true;
                    allFields.removeClass("error");

                    bValid = bValid && checkEmpty(catDescText, "Category Description");

                    if (bValid) {
                        var cat = getCat();
                        var json = JSON.stringify(cat);

                        $(this).dialog("close");

                        $.ajax({
                            url: '/Categories/CategoryTwoSave',
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

                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {
                allFields.val("").removeClass("error");
                $("#catName").text("");
            }
        });

        $("#dialog-message").dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#backButton").button()
            .click(function () {
                var url = window.location.toString().replace(window.location.pathname, "/Categories/CategoriesTwo");
                url = url.substring(0, url.indexOf("?"));
                window.location = url;
            });

        $("#create-categoryTwo").button()
			.button()
			.click(function () {
			    var bValid = true;
			    allFields.removeClass("error");

			    bValid = bValid && checkEmpty(catTwoName, "Category Name");

			    if (bValid) {
			        $("#catName").text(catTwoName.val());
			        catTwoName.removeClass("error");
			        $("#catTwoNameDetail").text(catTwoName.val());
			        $("#catTwoNameDetail").show();
			        catTwoName.hide();
			        $("span.ui-dialog-title").text('New Category');
			        $("#categoryDialog-form").dialog("open");
			    }
			});

        function refreshTable() {
            $.ajaxSetup({
                async: false,
                cache: false,
                dataType: "html"
            });
            $.get('/Categories/CategoryTwo?IdCompany=' + companyId.val() + '&CategoryTwoName=' + catTwoName.val(),
                function (response) {
                    $("#container").replaceWith(response);
                });
        }
    });
</script>
<style>
#example { width: 600px; }
#container { width: 600px; }
</style>
<header>
    <input type="hidden" id="companyId" value="<%= ViewData["IdCompany"] %>" />
    <div class="editor-label">
        <h3>Category Name</h3>
    </div>
    <div class="editor-field">
        <input type="text" name="catTwoName" id="catTwoName" class="text ui-widget-content ui-corner-all"  value="<%= ViewData["CategoryTwoName"] %>"  />
        <h3>
            <label id="catTwoNameDetail"></label>
        </h3>
    </div>
    <div class="editor-label">
        <button id="backButton">Back</button>
        <button id="create-categoryTwo">New Category Two</button>
    </div>
</header>
<div id="container">
<% Html.RenderPartial("CategoryTwoList"); %>
</div>

<div id="categoryDialog-form" title="Edit Category">
    <form>
    <fieldset>
        <legend></legend>
        <p class="validateTips">All form fields are required.</p>
        <input type="hidden" name="catIdhdn" id="catIdhdn" />
        <div class="editor-label">
            <label for="CategoryTwoName" id="lblCategoryTwoName">Category Name</label>
        </div>
        <div class="editor-label">
            <label for="CategoryTwoName" id="catName"></label>
        </div>
        <div class="editor-label">
            <label for="CategoryTwoName" id="Label1">Category Description</label>
        </div>
        <div class="editor-field">
            <input type="text" name="catDescText" id="catDescText" class="text ui-widget-content ui-corner-all" />
        </div>
    </fieldset>
    </form>
</div>
<div id="dialog-message" title="Categpry Saved">
    <div id="message"></div>
</div>

<div id="dialog-confirm" title="Delete Category?">
	<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>These item will be permanently deleted and cannot be recovered. Are you sure?</p>
    <input type="hidden" name="cathdntoDelete" id="cathdntoDelete" />
</div>
</asp:Content>