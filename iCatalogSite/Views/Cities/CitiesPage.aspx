<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CityModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Cities
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Cities</h2>
<script type="text/javascript" charset="utf-8">
    function editCity(id, name, idco) {
        $("#cityhdn").val(id);
        $("#citytext").val(name);
        $("#ddlCountriesList").val(idco);
        $("#cityDialog-form").dialog("open");
    }

    function confirmDeleteCountry(id) {
        $("#cityhdntoDelete").val(id);
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
                "Delete City": function () {
                    var id = $("#cityhdntoDelete").val();
                    var json = JSON.stringify({ IdCity: id });

                    $.ajax({
                        url: '/Cities/DeleteCity',
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

        var citytext = $("#citytext"),
            cityhdn = $("#cityhdn"),
            countrySelect = $("#ddlCountriesList"),
			allFields = $([]).add(citytext).add(cityhdn).add(countrySelect),
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

        function checkSelectEmpty(o, n) {
            if (o.val() == undefined || o.val() == "0") {
                o.addClass("ui-state-error");
                updateTips(n + " is Mandatory.");
                return false;
            } else {
                return true;
            }
        }

        function getCity() {
            var id = cityhdn.val();
            var name = citytext.val();
            var idco = countrySelect.val();
            return (name == "") ? null : { IdCity: id, CityName: name, IdCountry: idco };
        }

        $("#cityDialog-form").dialog({
            autoOpen: false,
            height: 300,
            width: 350,
            modal: true,
            buttons: {
                "Save City": function () {
                    var bValid = true;
                    allFields.removeClass("ui-state-error");

                    bValid = bValid && checkEmpty(citytext, "City Name");
                    bValid = bValid && checkSelectEmpty(countrySelect, "Country");

                    if (bValid) {
                        var city = getCity();
                        var json = JSON.stringify(city);

                        $(this).dialog("close");

                        $.ajax({
                            url: '/Cities/SaveCity',
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

        $("#create-city")
			.button()
			.click(function () {
			    $("span.ui-dialog-title").text('New City');
			    $("#cityDialog-form").dialog("open");
			});

        function refreshTable() {
            $.ajaxSetup({
                async: false,
                cache: false,
                dataType: "html"
            });
            $.get('<%= Url.Action("CitiesPage", "Cities") %>',
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
    <ul>
        <li><button id="create-city">New City</button></li>
    </ul>
</header>
<div id="container">
<% Html.RenderPartial("CitiesList"); %>
</div>

<div id="cityDialog-form" title="Edit City">
    <form action="CitiesPage.aspx">
    <fieldset>
        <legend></legend>
        <p class="validateTips">All form fields are required.</p>
        <input type="hidden" name="cityhdn" id="cityhdn" />
        <div class="editor-label">
            <label for="CityName" id="lblCityName">City Name</label>
        </div>
        <div class="editor-field">
            <input type="text" name="citytext" id="citytext" class="text ui-widget-content ui-corner-all" />
        </div>
        <div class="editor-label">
            <label for="IdCountry" id="lblIdCountry">Country</label>
        </div>
        <div class="editor-field">
            <%= Html.DropDownList("ddlCountriesList", (IEnumerable<SelectListItem>)ViewData["CountriesList"])%>
        </div>
    </fieldset>
    </form>
</div>
<div id="dialog-message" title="City Saved">
    <div id="message"></div>
</div>

<div id="dialog-confirm" title="Delete City?">
	<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>These item will be permanently deleted and cannot be recovered. Are you sure?</p>
    <input type="hidden" name="cityhdntoDelete" id="cityhdntoDelete" />
</div>

</asp:Content>
