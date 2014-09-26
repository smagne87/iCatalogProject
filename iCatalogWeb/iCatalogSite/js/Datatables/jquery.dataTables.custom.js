var oTable;
var currentUrl;
var selected = new Array();
function loadTableContentAjaxAssets(idTable, action, columns) {
    currentUrl = action;
    if (!oTable) {
        oTable = $('#' + idTable).dataTable({
            'bProcessing': true,
            'bServerSide': true,
            'bJQueryUI': true,
            "bAutoWidth": false,
            //"aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, 100]], use to define combobox paging options.
            'sAjaxSource': currentUrl,
            'sPaginationType': 'full_numbers',
            'aoColumns': columns,
            //
            'oLanguage': {
                'sUrl': '/Home/GetGridTranslations'
            },
            'fnInitComplete': function (oSettings, json) {
                $('.dataTables_filter input').attr("id", "txtSearch");
                $('.dataTables_filter input').unbind('keypress keyup');
                $("#" + idTable + "_filter.dataTables_filter label").append($("#btnSearch"));
                $("#btnSearch").removeClass("hide");
                $("#btnSearch").on("click", function () {
                    oTable.fnFilter($("#txtSearch").val());
                });

                $('#selectedItems').removeAttr('checked');

                $('.dataTables_filter input').keypress(function (e) {
                    var keyCode = (window.Event) ? e.which : e.keyCode;
                    switch (keyCode) {
                        case 13:
                            $("#btnSearch").click();
			    return false;
                            break;
                    }
                });
            },
            //
            'fnServerData': function (sSource, aoData, fnCallback) {
                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": currentUrl,
                    "data": aoData,
                    "success": fnCallback
                })
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                $('#' + idTable + ' tbody tr').each(function () {
                    if ($.inArray($(aData[0]).val(), selected) != -1) {
                        //SM: the string replace is because the aData[0] can not be converted to jquery DOM and can not assign the property 'checked'
                        $('td:eq(0)', nRow).html(aData[0].replace('[0]', "checked='checked'"));
                    }
                });
                return nRow;
            },
            "fnDrawCallback": function (oSettings) {
                var id = '#' + idTable + ' tbody tr';
                $(id).each(function () {
                    var iPos = oTable.fnGetPosition(this);
                    if (iPos != null) {
                        var aData = oTable.fnGetData(iPos);
                        if ($.inArray(aData[0], selected) != -1) {
                            //SM: the string replace is because the aData[0] can not be converted to jquery DOM and can not assign the property 'checked'.
                            aData[0] = aData[0].replace("[0]", "checked='checked'");
                        }
                    }
                    $(this).click(function () {
                        var iPos = oTable.fnGetPosition(this);
                        var aData = oTable.fnGetData(iPos);
                        var iId = $(aData[0]).val();
                        is_in_array = $.inArray(iId, selected);
                        if (is_in_array == -1) {
                            selected[selected.length] = iId;
                        }
                        else {
                            selected = $.grep(selected, function (value) {
                                return value != iId;
                            });
                        }
                    });
                });
            }
        });      //.fnSetFilteringDelay(400); //adding delay in keyup before filtering
    }
    else {
        oTable.fnDraw();
    }
}


function loadTableContentAjax(idTable, action, columns) {
    $(document).ready(function () {
        oTable = $('#' + idTable).dataTable({
            'bProcessing': true,
            'bServerSide': true,
            'bJQueryUI': true,
            "bAutoWidth": false,
            //"aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, 100]], use to define combobox paging options.
            'sPaginationType': 'full_numbers',
            'sAjaxSource': action,
            'aoColumns': columns,
            'fnServerData': function (sSource, aoData, fnCallback) {
                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": action,
                    "data": aoData,
                    "success": fnCallback
                })
            },
            'fnInitComplete': function (oSettings, json) {
                $('.dataTables_filter input').attr("id", "txtSearch");
                $('.dataTables_filter input').unbind('keypress keyup');
                $("#" + idTable + "_filter.dataTables_filter label").append($("#btnSearch"));
                $("#btnSearch").removeClass("hide");
                $("#btnSearch").on("click", function () {
                    oTable.fnFilter($("#txtSearch").val());
                });
                $('#selectedItems').removeAttr('checked');

                $('.dataTables_filter input').keypress(function (e) {
                    var keyCode = (window.Event) ? e.which : e.keyCode;
                    switch (keyCode) {
                        case 13:
                            $("#btnSearch").click();
			    return false;
                            break;
                    }
                });
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                $('#' + idTable + ' tbody tr').each(function () {
                    if ($.inArray($(aData[0]).val(), selected) != -1) {
                        //SM: the string replace is because the aData[0] can not be converted to jquery DOM and can not assign the property 'checked'.
                        $('td:eq(0)', nRow).html(aData[0].replace('[0]', "checked='checked'"));
                    }
                });
                return nRow;
            },
            "fnDrawCallback": function (oSettings) {
                var id = '#' + idTable + ' tbody tr';
                $(id).each(function () {
                    var iPos = oTable.fnGetPosition(this);
                    if (iPos != null) {
                        var aData = oTable.fnGetData(iPos);
                        if ($.inArray(aData[0], selected) != -1)
                        {
                            //SM: the string replace is because the aData[0] can not be converted to jquery DOM and can not assign the property 'checked'.
                            aData[0] = aData[0].replace("[0]", "checked='checked'");
                        }
                    }
                    $(this).click(function () {
                        var iPos = oTable.fnGetPosition(this);
                        var aData = oTable.fnGetData(iPos);
                        var iId = $(aData[0]).val();
                        is_in_array = $.inArray(iId, selected);
                        if (is_in_array == -1) {
                            selected[selected.length] = iId;
                        }
                        else {
                            selected = $.grep(selected, function (value) {
                                return value != iId;
                            });
                        }
                    });
                });
            },
            //
            'oLanguage': {
                'sUrl': '/Home/GetGridTranslations'
            }
            //
        }); //.fnSetFilteringDelay(400); //adding delay in keyup before filtering

    });
}

function loadTableContentJS(idTable, columns) {
    $(document).ready(function () {
        $('#' + idTable).dataTable({
            'iDisplayLength': 10,
            'iDisplayStart': 0,
            'sPaginationType': 'full_numbers',
            'bJQueryUI': true,
            'aoColumns': columns,
            'fnInitComplete': function (oSettings, json) {
                $('.dataTables_filter input').attr("id", "txtSearch");
                $('.dataTables_filter input').unbind('keypress keyup');
                $("#" + idTable + "_filter.dataTables_filter label").append($("#btnSearch"));
                $("#btnSearch").removeClass("hide");
                $("#btnSearch").on("click", function () {
                    $('#' + idTable).dataTable().fnFilter($("#txtSearch").val());
                });
                $('#selectedItems').removeAttr('checked');

                $('.dataTables_filter input').keypress(function (e) {
                    var keyCode = (window.Event) ? e.which : e.keyCode;
                    switch (keyCode) {
                        case 13:
                            $("#btnSearch").click();
			    return false;
                            break;
                    }
                });
            }
        }); //.fnSetFilteringDelay(400);
    });
}