<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master"
    Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ProductsPage
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header>
    <h2>Products</h2>
</header>

<script type="text/javascript" charset="utf-8">
    function editProduct(id, name, description) {
        $("#producthdn").val(id);
        $("#ProductName").val(name);
        $("#ProductDescription").val(description);
        $("#productDialog-form").dialog("open");
    }

    function confirmDeleteProduct(id) {
        $("#producthdntoDelete").val(id);
        $("#dialog-confirm").dialog('open');
    }

    $(function () {
        $("#dialog:ui-dialog").dialog("destroy");

        $("#dialog-confirm").dialog({
            resizable: false,
            autoOpen: false,
            height: 180,
            modal: true,
            buttons: {
                "Delete Product": function () {
                    var id = $("#producthdntoDelete").val();
                    var json = JSON.stringify({ IdProduct: id });

                    $.ajax({
                        url: '/Product/DeleteProduct',
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

        var productName = $("#ProductName"),
            productDescription = $("#ProductDescription"),
            producthdn = $("#producthdn"),
			allFields = $([]).add(productName).add(producthdn),
			tips = $(".validateTips");

        function updateTips(t) {
            tips
				.text(t)
				.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }

        function checkEmpty(o, n) {
            if (o.val() == "") {
                o.addClass("ui-state-error");
                updateTips(n + " is Mandatory.");
                return false;
            } else {
                return true;
            }
        }

        function getProduct() {
            var id = producthdn.val();
            var name = productName.val();
            var description = productDescription.val();
            var empty = name == "" || description == "";
            return (empty) ? null : { IdProduct: id, ProductName: name, ProductDescription: description };
        }

        $("#productDialog-form").dialog({
            autoOpen: false,
            height: 250,
            width: 300,
            modal: true,
            buttons: {
                "Save Product": function () {
                    var bValid = true;
                    allFields.removeClass("ui-state-error");

                    bValid = bValid && checkEmpty(productName, "Product Name") && checkEmpty(productDescription, "Product Description");

                    if (bValid) {
                        var product = getProduct();
                        var json = JSON.stringify(product);

                        $(this).dialog("close");

                        $.ajax({
                            url: '/Products/SaveProduct',
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
                allFields.val("").removeClass("ui-state-error");
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

        $("#create-product")
			.button()
			.click(function () {
			    $("span.ui-dialog-title").text('New Product');
			    $("#productDialog-form").dialog("open");
			});

        function refreshTable() {
            $.ajaxSetup({
                async: false,
                cache: false,
                dataType: "html"
            });
            $.get('<%= Url.Action("ProductsPage", "Products") %>',
                function (response) {
                    $("#container").replaceWith(response);
                });
        }
    });
</script>

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
    <header>
    <ul>
        <li><button id="create-product">New Product</button></li>
    </ul>
</header>
    <div id="container">
        <% Html.RenderPartial("ProductsList"); %>
    </div>
    <div id="productDialog-form" title="Edit Product">
        <form action="ProductsPage.aspx">
        <fieldset>
            <legend></legend>
            <p class="validateTips">
                All form fields are required.</p>
            <input type="hidden" name="producthdn" id="producthdn" />
            <div class="editor-label">
                <label for="ProductName" id="lblProductName">
                    Product Name</label>
            </div>
            <div class="editor-field">
                <input type="text" name="ProductName" id="ProductName" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <label for="ProductDescription" id="lblProductDescription">
                    Product Description</label>
            </div>
            <div class="editor-field">
                <input type="text" name="ProductDescription" id="ProductDescription" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <label for="CategoryOne" id="lblCategoryOne">
                    Category One</label>
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("ddlCategoryOne", (IEnumerable<SelectListItem>)ViewData["CategoriesOneList"], "<Select Category One>")%>
            </div>
            <div class="editor-label">
                <label for="CategoryTwo" id="lblCategoryTwo">
                    Category Two</label>
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("ddlCategoryTwo", (IEnumerable<SelectListItem>)ViewData["CategoriesTwoList"], "<Select Category Two>")%>
            </div>
            <div class="editor-label">
                <label for="CategoryThree" id="lblCategoryThree">
                    Category Three</label>
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("ddlCategoryThree", (IEnumerable<SelectListItem>)ViewData["CategoriesThreeList"], "<Select Category Three>")%>
            </div>
            <div class="editor-label">
                <label for="Company" id="lblCompany">
                    Company</label>
            </div>
            <div class="editor-field">
                <select name="Company" id="Company" class="text ui-widget-content ui-corner-all" />
            </div>
        </fieldset>
        </form>
    </div>
    <div id="dialog-message" title="Product Saved">
        <div id="message">
        </div>
    </div>
    <div id="dialog-confirm" title="Delete Product?">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            These item will be permanently deleted and cannot be recovered. Are you sure?</p>
        <input type="hidden" name="producthdntoDelete" id="producthdntoDelete" />
    </div>
</asp:Content>
