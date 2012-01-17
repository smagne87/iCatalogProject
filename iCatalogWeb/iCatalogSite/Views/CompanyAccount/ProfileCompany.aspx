<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPageCompanyProfile.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.CompanyAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Profile Company
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Profile Company</h2>
<script type="text/javascript">
    $(document).ready(function () {
        var companyName = $("#companyName"), phone = $("#phone"), idCompany = $("#idCompany"), //inputs
            weburl = $("#weburl"), street = $("#street"), numberST = $("#numberST"), postalCode = $("#postalCode"), //inputs
            ddlCountry = $("#ddlCountriesList"), ddlCity = $("#ddlCitiesList"), email = $("#email"), //inputs
            cnameDetail = $("#cnameDetail"), emailDetail = $("#emailDetail"), countryDetail = $("#countryDetail"), //details
            phoneDetail = $("#phoneDetail"), urlDetail = $("#urlDetail"), streetDetail = $("#streetDetail"), numSTDetail = $("#numSTDetail"), //details
            pcDetail = $("#pcDetail"), cityDetail = $("#cityDetail"), hdnCity = $("#idCity"), hdnCountry = $("#idCountry"), //details
            allFields = $([]).add(companyName).add(email).add(ddlCountry).add(ddlCity);

        companyName.hide();
        phone.hide();
        email.hide();
        weburl.hide();
        street.hide();
        numberST.hide();
        postalCode.hide();
        ddlCity.hide();
        ddlCountry.hide();
        $("#savePersonalData").hide();
        $("#saveLocationData").hide();

        function checkEmpty(o, n) {
            if (o.val() == "") {
                o.addClass("error");
                $(o).attr("title", n + " is Mandatory.");
                $(o).tooltip(
                    {
                        offset: [50, 0],
                        position: 'middle right',
                        events: {
                            input: "focus mouseover,blur mouseout"
                        }
                    });
                return false;
            } else {
                return true;
            }
        }

        function getCompanyData() {
            var cname = companyName.val();
            var pho = phone.val();
            var e = email.val();
            var ww = weburl.val();
            var st = street.val();
            var nst = numberST.val();
            var pc = postalCode.val();
            var id = idCompany.val();
            var idC = hdnCity.val();
            var idCo = hdnCountry.val();
            var city = cityDetail.text();
            var country = countryDetail.text();
            return (id == "") ? null : { IdCompany: id, CompanyName: cname, Phone: pho, Email: e, IdCountry: idCo, IdCity: idC, CountryName: country, CityName: city, WebUrl: ww, Street: st, NumberST: nst, PostalCode: pc };
        }

        $("#savePersonalData")
        .button()
            .click(function () {
                var bValid = true;
                allFields.removeClass("error");

                bValid = bValid && checkEmpty(companyName, "Company Name");
                bValid = bValid && checkEmpty(email, "Email");

                if (bValid) {
                    var user = getCompanyData();
                    var json = JSON.stringify(user);

                    $.ajax({
                        url: '/CompanyAccount/SaveUserData',
                        type: 'POST',
                        dataType: 'json',
                        data: json,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            var message = data.Message;
                            cnameDetail.text(companyName.val());
                            phoneDetail.text(phone.val());
                            emailDetail.text(email.val());
                            urlDetail.text(weburl.val());
                            streetDetail.text(street.val() + " ");
                            numSTDetail.text(numberST.val() + " ");
                            pcDetail.text(postalCode.val());
                            settingEdit();
                            $("#message").html(message);
                            $("#dialog-messageData").dialog("open");
                        }
                    });
                }
            });

        $("#saveLocationData")
        .button()
            .click(function () {
                if (ddlCity.val() != "0" && ddlCity.val() != "" && ddlCity.val() != undefined) {
                    cityDetail.text($("#ddlCitiesList option:selected").text());
                    hdnCity.val(ddlCity.val());
                }
                if (ddlCountry.val() != "0" && ddlCountry.val() != "" && ddlCountry.val() != undefined) {
                    countryDetail.text($("#ddlCountriesList option:selected").text());
                    hdnCountry.val(ddlCountry.val());
                }
                var user = getCompanyData();
                var json = JSON.stringify(user);

                $.ajax({
                    url: '/CompanyAccount/SaveUserData',
                    type: 'POST',
                    dataType: 'json',
                    data: json,
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        streetDetail.text(street.val());
                        numSTDetail.text(numberST.val());
                        pcDetail.text(postalCode.val());
                        var message = data.Message;
                        settingEditLD();
                        $("#message").html(message);
                        $("#dialog-messageData").dialog("open");
                    }
                });
            });


        function checkSelectEmpty(o, n) {
            if (o.val() == undefined || o.val() == "0") {
                o.addClass("error");
                updateTips(n + " is Mandatory.", o);
                return false;
            } else {
                return true;
            }
        }

        $("#iconEditPD").click(function () {
            settingEdit();
            companyName.val(cnameDetail.text());
            phone.val(phoneDetail.text());
            email.val(emailDetail.text());
            weburl.val(urlDetail.text());
        });

        $("#iconEditLD").click(function () {
            settingEditLD();
            if (hdnCountry.val() != "0" && hdnCountry.val() != "") {
                ddlCountry.val(hdnCountry.val());
                ddlCountry.change();
                ddlCity.val(hdnCity.val());
            }
            street.val(streetDetail.text());
            numberST.val(numSTDetail.text());
            postalCode.val(pcDetail.text());
        });

        function settingEditLD() {
            if ($("#iconEditLD span").attr("class").indexOf("pencil") > 0) {
                $("#iconEditLD span").removeClass("ui-icon-pencil");
                $("#iconEditLD span").addClass("ui-icon-cancel");
                $("#iconEditLD div").attr("title", "Cancel");
                cityDetail.hide();
                countryDetail.hide();
                streetDetail.hide();
                numSTDetail.hide();
                pcDetail.hide();
                street.show();
                numberST.show();
                postalCode.show();
                ddlCountry.show();
                ddlCity.show();
                $("#saveLocationData").show();
            }
            else {
                $("#iconEditLD span").removeClass("ui-icon-cancel");
                $("#iconEditLD span").addClass("ui-icon-pencil");
                $("#iconEditLD div").attr("title", "Edit Location Data");
                $("#saveLocationData").hide();
                ddlCountry.hide();
                ddlCity.hide();
                street.hide();
                numberST.hide();
                postalCode.hide();
                cityDetail.show();
                countryDetail.show();
                streetDetail.show();
                numSTDetail.show();
                pcDetail.show();
            }
        }

        function settingEdit() {
            if ($("#iconEditPD span").attr("class").indexOf("pencil") > 0) {
                $("#iconEditPD span").removeClass("ui-icon-pencil");
                $("#iconEditPD span").addClass("ui-icon-cancel");
                $("#iconEditPD div").attr("title", "Cancel");
                cnameDetail.hide();
                phoneDetail.hide();
                emailDetail.hide();
                urlDetail.hide();
                companyName.show();
                phone.show();
                email.show();
                weburl.show();
                $("#savePersonalData").show();
            }
            else {
                $("#iconEditPD span").removeClass("ui-icon-cancel");
                $("#iconEditPD span").addClass("ui-icon-pencil");
                $("#iconEditPD div").attr("title", "Edit Location Data");
                $("#savePersonalData").hide();
                companyName.hide();
                phone.hide();
                email.hide();
                weburl.hide();
                cnameDetail.show();
                phoneDetail.show();
                emailDetail.show();
                urlDetail.show();
            }
        }

        $("#dialog-messageData").dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#ddlCountriesList").change(function () {
            var idCountry = $(this).val();
            $.getJSON("/Cities/GetAllCitiesByCountryId", { IdCountry: idCountry },
                function (data) {
                    ddlCity.empty();
                    ddlCity.append($('<option/>', {
                        value: 0,
                        text: "<Select City>"
                    }));
                    $.each(data, function (index, itemData) {
                        ddlCity.append($('<option/>', {
                            value: itemData.IdCity,
                            text: itemData.CityName
                        }));
                    });
                    ddlCity.val(hdnCity.val());
                });
        });
    });
</script>

<div>
    <div style="float:left; width:50%">
        <input type="hidden" id="idCompany" value="<%: Model.IdCompany %>" />
        <h2>
            <span style="float:left;">Personal Data</span>
            <div id="iconEditPD" class="ui-widget ui-helper-clearfix icons">
                <div class="ui-state-default ui-corner-all" title="Edit Location Data">
                    <span class="ui-icon ui-icon-pencil"></span>
                </div>
            </div>
            <div class="cleared"></div>
        </h2>
        <h3>Company Name:</h3> <h4><span id="cnameDetail"><%: Model.CompanyName %></span><input type="text" name="companyName" id="companyName" class="text ui-widget-content ui-corner-all" value="<%: Model.CompanyName %>" /></h4>
        <h3>Phone:</h3> <h4><span id="phoneDetail"><%: Model.Phone %></span><input type="text" name="phone" id="phone" class="text ui-widget-content ui-corner-all" value="<%: Model.Phone %>" /></h4>
        <h3>Web:</h3> <h4><span id="urlDetail"><%: Model.WebUrl %></span><input type="text" name="weburl" id="weburl" class="text ui-widget-content ui-corner-all" value="<%: Model.WebUrl %>" /></h4>
        <h3>Email:</h3> <h4><span id="emailDetail"><%: Model.Email %></span><input type="text" name="email" id="email" class="text ui-widget-content ui-corner-all" value="<%: Model.Email %>" /></h4>
        <button id="savePersonalData">Save</button>
        <h3>Company User Name:</h3> <h4><span><%: Model.CompanyUserName %></span></h4>
        <h3>
            <span style="float:left; margin-top:5px;">Password: </span>
            <% Html.RenderPartial("ChangePassword", Model); %>
        </h3>
        <h4>************</h4>
    </div>
    <div style="float:left; width:50%">
        <h2>
            <span style="float:left;">Location Data</span>
            <div class="ui-widget ui-helper-clearfix icons" id="iconEditLD">
                <div class="ui-state-default ui-corner-all" title="Edit Location Data">
                    <span class="ui-icon ui-icon-pencil"></span>
                </div>
            </div>
            <div class="cleared"></div>
        </h2>
        <h3>Country:</h3><h4><input type="hidden" value="<%: Model.IdCountry %>" id="idCountry" /><span id="countryDetail"><%: Model.CountryName %></span><%= Html.DropDownList("ddlCountriesList", (IEnumerable<SelectListItem>)ViewData["CountriesList"], "<Select Country>") %></h4>
        <h3>City:</h3><h4><input type="hidden" value="<%: Model.IdCity %>" id="idCity" /><span id="cityDetail"><%: Model.CityName %></span><%= Html.DropDownList("ddlCitiesList", new SelectList(Enumerable.Empty<SelectListItem>(), "IdCity", "CityName"), "<Select City>")%></h4>
        <h3>Address:</h3> <h4><span id="streetDetail"><%: Model.Street %></span>
                              <span id="numSTDetail"><%: " " + Model.NumberST + " "%></span>
                              <span id="pcDetail"><%: Model.PostalCode %></span>
                              <input type="text" name="street" id="street" class="text ui-widget-content ui-corner-all" value="<%: Model.Street %>" />
                              <input type="text" name="numberST" id="numberST" class="text ui-widget-content ui-corner-all" value="<%: Model.NumberST %>" style="width:70px;"/>
                              <input type="text" name="postalCode" id="postalCode" class="text ui-widget-content ui-corner-all" value="<%: Model.PostalCode %>" style="width:70px;" /></h4>
        <button id="saveLocationData">Save</button>
    </div>
    <div class="cleared"></div>
    <blockquote>
        <p>
            &#8220;Remember to complete all the Company info. <br /> This info helps to the users to found you.&#8221;
        </p>
    </blockquote>
</div>
<div id="dialog-messageData" title="Saved">
    <div id="message"></div>
</div>
</asp:Content>