<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
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
            return (userName == "") ? null : { UserName: userName, Password: oldpas, NewPassword: newpas };
        }

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

        function checkEquals(n, r, desn, desr) {
            if (n.val() != r.val()) {
                n.addClass("ui-state-error");
                r.addClass("ui-state-error");
                updateTips(desn + " and " + desr + " must be equals.");
                return false;
            } else {
                return true;
            }
        }

        function checkNotEquals(n, r, desn, desr) {
            if (n.val() == r.val()) {
                n.addClass("ui-state-error");
                r.addClass("ui-state-error");
                updateTips(desn + " and " + desr + " must be differents.");
                return false;
            } else {
                return true;
            }
        }

        $("#passwordDialog-form").dialog({
            autoOpen: false,
            height: 370,
            width: 400,
            modal: true,
            buttons: {
                "Change Password": function () {
                    var bValid = true;
                    allFields.removeClass("ui-state-error");

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
                            url: '/UserAccount/ChangePassword',
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
<div class="ui-widget ui-helper-clearfix icons">
    <div class="ui-state-default ui-corner-all" title="Edit Location Data">
        <span id="iconChangePassword" class="ui-icon ui-icon-key"></span>
    </div>
</div>
<div class="cleared"></div>
<div id="passwordDialog-form" title="Change Password">
    <form>
        <fieldset>
            <legend></legend>
            <p class="validateTips">All form fields are required.</p>
            <input type="hidden" id="userNameHdn" value="<%: Model.UserName != null ? Model.UserName : Model.CompanyUserName %>" />
            <div class="editor-label">
                <label for="Change Password" id="lblUserName"><%: Model.UserName != null ? Model.UserName : Model.CompanyUserName %></label>
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
    <div id="message"></div>
</div>

