<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CountryModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Countries
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<header>
    <h2>Countries</h2>
</header>
<script type="text/javascript" charset="utf-8">
    function editCountry(id, name) {
        $("#countryhdn").val(id);
        $("#countrytext").val(name);
        $("#countryDialog-form").dialog("open");
    }

    function confirmDeleteCountry(id) {
        $("#countryhdntoDelete").val(id);
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
                "Delete Country": function () {
                    var id = $("#countryhdntoDelete").val();
                    var json = JSON.stringify({ IdCountry: id });

                    $.ajax({
                        url: '/Countries/DeleteCountry',
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

        var countrytext = $("#countrytext"),
            countryhdn = $("#countryhdn"),
			allFields = $([]).add(countrytext).add(countryhdn),
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

        function getCountry() {
            var id = countryhdn.val();
            var name = countrytext.val();
            return (name == "") ? null : { IdCountry: id, CountryName: name };
        }

        $("#countryDialog-form").dialog({
            autoOpen: false,
            height: 250,
            width: 300,
            modal: true,
            buttons: {
                "Save Country": function () {
                    var bValid = true;
                    allFields.removeClass("ui-state-error");

                    bValid = bValid && checkEmpty(countrytext, "Country Name");

                    if (bValid) {
                        var country = getCountry();
                        var json = JSON.stringify(country);

                        $(this).dialog("close");

                        $.ajax({
                            url: '/Countries/SaveCountry',
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

        $("#create-country")
			.button()
			.click(function () {
			    $("span.ui-dialog-title").text('New Country');
			    $("#countryDialog-form").dialog("open");
			});

        function refreshTable() {
            $.ajaxSetup({
                async: false,
                cache: false,
                dataType: "html"
            });
            $.get('<%= Url.Action("CountriesPage", "Countries") %>',
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
        <li><button id="create-country">New Country</button></li>
    </ul>
</header>
<div id="container">
<% Html.RenderPartial("CountriesList"); %>
</div>

<div id="countryDialog-form" title="Edit Country">
    <form action="CountriesPage.aspx">
    <fieldset>
        <legend></legend>
        <p class="validateTips">All form fields are required.</p>
        <input type="hidden" name="countryhdn" id="countryhdn" />
        <div class="editor-label">
            <label for="CountryName" id="lblCountryName">Country Name</label>
        </div>
        <div class="editor-field">
            <input type="text" name="countrytext" id="countrytext" class="text ui-widget-content ui-corner-all" />
        </div>
    </fieldset>
    </form>
</div>
<div id="dialog-message" title="Country Saved">
    <div id="message"></div>
</div>

<div id="dialog-confirm" title="Delete Country?">
	<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>These item will be permanently deleted and cannot be recovered. Are you sure?</p>
    <input type="hidden" name="countryhdntoDelete" id="countryhdntoDelete" />
</div>
</asp:Content>
