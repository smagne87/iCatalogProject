<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iCatalogSite.Models.CompanyAccountModel>" %>
<script type="text/javascript" charset="utf-8">
    $(function () {
        var oldPasswordtext = $("#oldPasswordtext"), newPasswordtext = $("#newPasswordtext"),
            repeatPasswordtext = $("#repeatPasswordtext"), userNameHdn = $("#userNameHdn"),
            allFields = $([]).add(oldPasswordtext).add(newPasswordtext).add(repeatPasswordtext),
            tips = $(".validateTips");

        function getNewPass() {
            var oldpas = oldPasswordtext.val();
            var newpas = newPasswordtext.val();
            var userName = userNameHdn.val();
            return (userName == "") ? null : { CompanyUserName: userName, Password: oldpas, NewPassword: newpas };
        }

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

        function checkEquals(n, r, desn, desr) {
            if (n.val() != r.val()) {
                n.addClass("error");
                r.addClass("error");
                updateTips(desn + " and " + desr + " must be equals.", n);
                updateTips(desn + " and " + desr + " must be equals.", r);
                return false;
            } else {
                return true;
            }
        }

        function checkNotEquals(n, r, desn, desr) {
            if (n.val() == r.val()) {
                n.addClass("error");
                r.addClass("error");
                updateTips(desn + " and " + desr + " must be equals.", n);
                updateTips(desn + " and " + desr + " must be equals.", r);
                return false;
            } else {
                return true;
            }
        }

        $("#passwordDialog-form").dialog({
            autoOpen: false,
            height: 420,
            width: 400,
            modal: true,
            buttons: {
                "Change Password": function () {
                    var bValid = true;
                    allFields.removeClass("error");

                    bValid = bValid && checkEmpty(oldPasswordtext, "Old Password");
                    bValid = bValid && checkEmpty(newPasswordtext, "New Password");
                    bValid = bValid && checkEmpty(repeatPasswordtext, "Repeat Password");
                    bValid = bValid && checkEquals(newPasswordtext, repeatPasswordtext, "New Password", "Repeat Password");
                    bValid = bValid && checkNotEquals(newPasswordtext, oldPasswordtext, "New Password", "Old Password");

                    if (bValid) {
                        var Password = getNewPass();
                        var json = JSON.stringify(Password);
                        $(this).dialog("close");
                        $.ajax({
                            url: '/CompanyAccount/ChangePassword',
                            type: 'POST',
                            dataType: 'json',
                            data: json,
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                // get the result and do some magic with it
                                var message = data.Message;
                                $("#messagePassword").html(message);
                                $("#dialog-message").dialog("open");
                            }
                        });
                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
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

		//hover states on the static widgets
		$('.icons div').hover(
				function () { $(this).addClass('ui-state-hover'); },
				function () { $(this).removeClass('ui-state-hover'); }
			);
		$('#iconChangePassword').click(function () {
			$("#passwordDialog-form").dialog("open");
		});
    });
</script>
<div class="ui-widget ui-helper-clearfix icons" id="iconChangePassword">
    <div class="ui-state-default ui-corner-all" title="Change Password">
        <span class="ui-icon ui-icon-key"></span>
    </div>
</div>
<div class="cleared"></div>
<div id="passwordDialog-form" title="Change Password">
    <form>
        <fieldset>
            <legend></legend>
            <p class="validateTips">All form fields are required.</p>
            <input type="hidden" id="userNameHdn" value="<%: Model.CompanyUserName %>" />
            <div class="editor-label">
                <label for="Change Password" id="lblUserName"><%: Model.CompanyUserName %></label>
            </div>
            <div class="editor-label">
                <label for="Change Password" id="Label1">Old Password</label>
            </div>
            <div class="editor-field">
                <input type="password" name="oldPasswordtext" id="oldPasswordtext" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <label for="Change Password" id="Label2">New Password</label>
            </div>
            <div class="editor-field">
                <input type="password" name="newPasswordtext" id="newPasswordtext" class="text ui-widget-content ui-corner-all" />
            </div>
            <div class="editor-label">
                <label for="RepeatPassword" id="lblrepeatPassword">Repeat New Password</label>
            </div>
            <div class="editor-field">
                <input type="password" name="repeatPasswordtext" id="repeatPasswordtext" class="text ui-widget-content ui-corner-all" />
            </div>
        </fieldset>
    </form>
</div>
<div id="dialog-message" title="New Password Saved">
    <div id="messagePassword"></div>
</div>

