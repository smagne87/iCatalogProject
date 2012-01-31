<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageCompanyProfile.Master"
    Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ProductsPage
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header>
    <h2>Products</h2>
</header>

<script type="text/javascript" charset="utf-8">

    function editProduct(id, name, description, idCategoryOne, idCategoryTwo, idCategoryThree) {
        $("#producthdn").val(id);
        $("#ProductName").val(name);
        $("#ProductDescription").val(description);
        $("#ddlCategoryOne").val(idCategoryOne);
        $("#ddlCategoryTwo").val(idCategoryTwo);
        $("#ddlCategoryThree").val(idCategoryThree);
        $("#productDialog-form").dialog("open");
    }

    function confirmDeleteProduct(id) {
        $("#producthdntoDelete").val(id);
        $("#dialog-confirm").dialog('open');
    }

    $(function () {
        var ddlCategoryOne = $("#ddlCategoryOne"), ddlCategoryTwo = $("#ddlCategoryTwo"), ddlCategoryThree = $("#ddlCategoryThree"), hdnIdCompany = $("#hdnIdCompany");

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
                o.addClass("ui-state-error");
                updateTips(n + " is Mandatory.", o);
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
            return (empty) ? null : { IdProduct: id, ProductName: name, ProductDescription: description, IdCategoryOne: ddlCategoryOne.val() == 0 ? null : ddlCategoryOne.val(), IdCategoryTwo: ddlCategoryTwo.val() == 0 ? null : ddlCategoryTwo.val(), IdCategoryThree: ddlCategoryThree.val() == 0 ? null : ddlCategoryThree.val() };
        }

        $("#productDialog-form").dialog({
            autoOpen: false,
            height: 625,
            width: 500,
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

        $("#ddlCategoriesOne").change(function () {
            var categoryOneName = $("#ddlCategoriesOne option:selected").text();
            $.getJSON("/Categories/GetAllCategoryOneByCategoryOneName", { CategoryOneName: categoryOneName, IdCompany: hdnIdCompany.val() },
                function (data) {
                    ddlCategoryOne.empty();
                    ddlCategoryOne.append($('<option/>', {
                        value: 0,
                        text: "<Select Category One>"
                    }));
                    $.each(data, function (index, itemData) {
                        ddlCategoryOne.append($('<option/>', {
                            value: itemData.IdCategoryOne,
                            text: itemData.CategoryOneDescription
                        }));
                    });
                });
        });

        $("#ddlCategoriesTwo").change(function () {
            var categoryTwoName = $("#ddlCategoriesTwo option:selected").text();
            $.getJSON("/Categories/GetAllCategoryTwoByCategoryTwoName", { CategoryTwoName: categoryTwoName, IdCompany: hdnIdCompany.val() },
                function (data) {
                    ddlCategoryTwo.empty();
                    ddlCategoryTwo.append($('<option/>', {
                        value: 0,
                        text: "<Select Category Two>"
                    }));
                    $.each(data, function (index, itemData) {
                        ddlCategoryTwo.append($('<option/>', {
                            value: itemData.IdCategoryTwo,
                            text: itemData.CategoryTwoDescription
                        }));
                    });
                });
        });

        $("#ddlCategoriesThree").change(function () {
            var categoryThreeName = $("#ddlCategoriesThree option:selected").text();
            $.getJSON("/Categories/GetAllCategoryThreeByCategoryThreeName", { CategoryThreeName: categoryThreeName, IdCompany: hdnIdCompany.val() },
                function (data) {
                    ddlCategoryThree.empty();
                    ddlCategoryThree.append($('<option/>', {
                        value: 0,
                        text: "<Select Category Three>"
                    }));
                    $.each(data, function (index, itemData) {
                        ddlCategoryThree.append($('<option/>', {
                            value: itemData.IdCategoryThree,
                            text: itemData.CategoryThreeDescription
                        }));
                    });
                });
        });
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
        <button id="create-product">New Product</button>
        <input type="hidden" id="hdnIdCompany" value="<%= ViewData["IdCompany"] %>" />
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
                <h3>Company</h3>
            </div>
            <div class="editor-label">
                <h3><%= ViewData["CompanyName"] %></h3>
            </div>
            <div class="editor-label">
                <h3>Product Name</h3>
            </div>
            <div class="editor-field">
                <input type="text" name="ProductName" id="ProductName" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <h3>Product Description</h3>
            </div>
            <div class="editor-field">
                <input type="text" name="ProductDescription" id="ProductDescription" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <h3>Category One</h3>
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("ddlCategoriesOne", (IEnumerable<SelectListItem>)ViewData["CategoriesOneList"], "<Select Category One>")%>
                <%= Html.DropDownList("ddlCategoryOne", new SelectList(Enumerable.Empty<SelectListItem>(), "IdCategoryOne", "CategoryOneName"), "<Select Category One>")%>
            </div>
            <div class="editor-label">
                <h3>Category Two</h3>
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("ddlCategoriesTwo", (IEnumerable<SelectListItem>)ViewData["CategoriesTwoList"], "<Select Category Two>")%>
                <%= Html.DropDownList("ddlCategoryTwo", new SelectList(Enumerable.Empty<SelectListItem>(), "IdCategoryTwo", "CategoryTwoName"), "<Select Category Two>")%>
            </div>
            <div class="editor-label">
                <h3>Category Three</h3>
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("ddlCategoriesThree", (IEnumerable<SelectListItem>)ViewData["CategoriesThreeList"], "<Select Category Three>")%>
                <%= Html.DropDownList("ddlCategoryThree", new SelectList(Enumerable.Empty<SelectListItem>(), "IdCategoryThree", "CategoryThreeName"), "<Select Category Three>")%>
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
