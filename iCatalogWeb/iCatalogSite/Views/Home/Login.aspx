<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterDefaultPage.Master" Inherits="System.Web.Mvc.ViewPage<iCatalogSite.Models.UserAccountModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        var tips = $(".validateTips");

        $("#UserName").focus();

        $("#loginForm").validate({
            rules: {
                Password: {
                    required: true
                },
                UserName: {
                    required: true
                }
            },
            messages:
            {
                Password: {
                    required: "Please specify your password"
                },
                UserName: {
                    required: "Please specify your user name"
                }
            },
            invalidHandler: function (form, validator) {
                for (var i = 0; i < validator.errorList.length; i++) {
                    var field = validator.errorList[i];
                    var ele = field.element;
                    var message = field.message;
                    $(ele).attr("title", field.message);
                    $(ele).tooltip(
                    {
                        offset: [50, 0],
                        position: 'middle right',
                        events: {
                            input: "focus mouseover,blur mouseout"
                        }
                    });
                }
            }
        });

        $("#loginForm").submit(function () {
            return false;
        });

        $("#logIn").button()
            .click(function () {
                if ($("#loginForm").valid()) {
                    var user = getUser();
                    var json = JSON.stringify(user);

                    $.ajax({
                        url: '/Home/LogOn',
                        type: 'POST',
                        dataType: 'json',
                        data: json,
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            if (data.Message) {
                                var message = data.Message;
                                updateTips(message);
                            }
                            if (data.Url) {
                                var url = data.Url;
                                window.location = url;
                            }
                        }
                    });
                }
            });

        function getUser() {
            var uname = $("#UserName").val();
            var pass = $("#Password").val();
            return (uname == "") ? null : { UserName: uname, Password: pass };
        }


        //Company login

        $("#loginComForm").validate({
            rules: {
                comPassword: {
                    required: true
                },
                comUserName: {
                    required: true
                }
            },
            messages:
            {
                comPassword: {
                    required: "Please specify your password"
                },
                comUserName: {
                    required: "Please specify your company user"
                }
            },
            invalidHandler: function (form, validator) {
                for (var i = 0; i < validator.errorList.length; i++) {
                    var field = validator.errorList[i];
                    var ele = field.element;
                    var message = field.message;
                    $(ele).attr("title", field.message);
                    $(ele).tooltip(
                    {
                        offset: [50, 0],
                        position: 'middle right',
                        events: {
                            input: "focus mouseover,blur mouseout"
                        }
                    });
                }
            }
        });

        $("#loginComForm").submit(function () {
            return false;
        });

        $("#loginCompany").button()
                    .click(function () {
                        if ($("#loginComForm").valid()) {
                            var com = getComUser();
                            var json = JSON.stringify(com);

                            $.ajax({
                                url: '/Home/LogOn',
                                type: 'POST',
                                dataType: 'json',
                                data: json,
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    if (data.Message) {
                                        var message = data.Message;
                                        updateTips(message);
                                    }
                                    if (data.Url) {
                                        var url = data.Url;
                                        window.location = url;
                                    }
                                }
                            });
                        }
                    });

        function getComUser() {
            var uname = $("#comUserName").val();
            var pass = $("#comPassword").val();
            return (uname == "") ? null : { UserName: uname, Password: pass };
        }

        function updateTips(t) {
            tips
				.text(t)
				.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }
    });
</script>

<h2>Login</h2>
<div>
    <p class="validateTips"></p>
    <div style="float:left;">
        <h3>Users</h3>
        <form id="loginForm">
            <fieldset class="ui-corner-all" style="width:350px;">
                <div class="editor-label">
                    <h3>User</h3>
                </div>
                <div class="editor-field">
                    <input type="text" name="UserName" id="UserName" class="text ui-widget-content ui-corner-all" />
                </div>
                <div class="editor-label">
                    <h3>Password</h3>
                </div>
                <div class="editor-field">
                    <input type="password" name="Password" id="Password" class="text ui-widget-content ui-corner-all" />
                </div>
                <div class="editor-field">
                </div>
                <div class="editor-label">
                    <div style="float:left;">
                        <button id="logIn">Log in</button>
                    </div>
                    <div style="float:left;" class="caja-checkBoxTexto">
                        <h3>
                            <input type="checkbox" name="rememberMe" id="rememberMe" />Remember Me
                        </h3>
                    </div>
                    <div class="cleared"></div>
                </div>
                <div class="cajaForgot">
                    <h6>
                        <%: Html.ActionLink("Forgot Password?", "ForgotPassword", "Home") %>
                        <%: Html.ActionLink("Register!", "Register", "Home")%>
                    </h6>
                </div>
            </fieldset>
        </form>
    </div>
    <div style="float:left; margin:0px 10px;">
        <h3>Companies</h3>
        <form id="loginComForm">
            <fieldset class="ui-corner-all" style="width:350px;">
                <div class="editor-label">
                    <h3>Company User</h3>
                </div>
                <div class="editor-field">
                    <input type="text" name="comUserName" id="comUserName" class="text ui-widget-content ui-corner-all" />
                </div>
                <div class="editor-label">
                    <h3>Password</h3>
                </div>
                <div class="editor-field">
                    <input type="password" name="comPassword" id="comPassword" class="text ui-widget-content ui-corner-all" />
                </div>
                <div class="editor-field">
                </div>
                <div class="editor-label">
                    <div style="float:left;">
                        <button id="loginCompany">Log in</button>
                    </div>
                    <div style="float:left;" class="caja-checkBoxTexto">
                        <h3>
                            <input type="checkbox" name="comRememberMe" id="comRememberMe" />Remember Me
                        </h3>
                    </div>
                    <div class="cleared"></div>
                </div>
                <div class="cajaForgot">
                    <h6>
                        <%: Html.ActionLink("Forgot Password?", "ForgotPassword", "Home") %>
                        <%: Html.ActionLink("Register!", "RegisterCom", "CompanyAccount")%>
                    </h6>
                </div>
            </fieldset>
        </form>
    </div>
    <div class="cleared"></div>
</div>
</asp:Content>
