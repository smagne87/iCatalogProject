<%@ Page Title="" Language="C#" MasterPageFile="~/Views/BackEndMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CountryModel>" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Countries
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<header>
    <h2>Countries</h2>
</header>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $('#example').dataTable({
            "iDisplayLength": 25,
            "aaSorting": [[1, "asc"]],
            "aoColumns": [{ "bSortable": true }, { "bSortable": true }, { "bSortable": false }]
        });
    });

    $(function () {
        // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
        $("#dialog:ui-dialog").dialog("destroy");

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
            debugger;
            var id = countryhdn.val();
            var name = countrytext.val();
            return (name == "") ? null : { IdCountry: id, CountryName: name };
        }

        $("#countryDialog-form").dialog({
            autoOpen: false,
            height: 200,
            width: 250,
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
			    $("#countryDialog-form").title = "New Country";
			    $("#countryDialog-form").dialog("open");
			});
    });
</script>

<style>
#example { width: 100%; }
#container { width: 600px; }
</style>
<header>
    <ul>
        <li><button id="create-country">New Country</button></li>
    </ul>
</header>
<div id="container">
<% Html.Grid((List<iCatalogSite.Models.CountryModel>)ViewData["CountriesList"])
       .Columns(column =>
           {
               column.For(co => Html.ActionLink(co.IdCountry.ToString(), "EditCountry", "Countries", new { id = co.IdCountry }, null)).Named("Id Country");
               column.For(co => co.CountryName);
               column.For(co => Html.ActionLink("Delete", "DeleteCountry", "Countries", new { id = co.IdCountry }, null)).Named("Delete");
           }).Attributes(id => "example").Render();
%>
</div>

<div id="countryDialog-form" title="Edit Country">
    <form action="CountriesPage.aspx">
    <fieldset>
        <legend></legend>
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
</asp:Content>
