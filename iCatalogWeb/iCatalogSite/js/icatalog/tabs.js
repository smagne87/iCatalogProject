var _entityPath = "";
var _entityName = "";
var _editPage = "";
var _createPage = "";
var _listPage = "";
var _indexPage = "";

var _listTabName = "";
var _editTabName = "";

var _deleteId = "";
var _deleteItems = "";
var _deleteAllItems = "";
var _moveItems = "";

function initTabs(entityPath, entityName) {
    initPages(entityPath, entityName);
    loadCreate();
}

function initPages(entityPath, entityName, list, addEdit) {
    _entityPath = entityPath;
    _entityName = entityName;
    _editPage = "/Admin/" + _entityPath + "/Edit/";
    _createPage = "/Admin/" + _entityPath + "/Create/";
    _listPage = "/Admin/" + _entityPath + "/Index/";
    _indexPage = "/Admin/" + _entityPath + "/List/";
    _entityName = entityName != undefined ? entityName : _entityName;
    _listTabName = list;
    _editTabName = addEdit;
}

function isCreatePage() {
    if ((location.pathname + "/").toLowerCase().match("^" + _createPage.toLowerCase())) {
        return true;
    }
    return false;
}

function isEditPage() {
    if (location.pathname.toLowerCase().match("^" + _editPage.toLowerCase())) {
        return true;
    }
    return false;
}

function addLoaderInTab(idTab) {
    $('#myTab a[href="#' + idTab + '"]').html('<img src="/img/ajax-loader-tab.gif">');
}

function restoreTextInTab1() {
    $('#myTab a[href="#tab1"]').html(_listTabName);
}

function restoreTextInTab2() {
    $('#myTab a[href="#tab2"]').html(_editTabName);
}

function successTab1(data) {
    $('#tab1').html(data);
    showTab('tab1');
    restoreTextInTab1();
}

function successTab2(data) {
    $('#tab2').html(data);
    restoreTextInTab2();
    restoreTextInTab1();
}

function showTab(idTab) {
    $('#myTab a[href="#' + idTab + '"]').tab('show');
}

function displayDataTab2(data) {
    $('#tab2').html(data);
    showTab('tab2');
    restoreTextInTab2();
}

function cancelPage() {
    loadCreate();
    showTab('tab1');
}

//LIST
function loadList() {
    addLoaderInTab('tab1');
    submitUrl(_listPage, '{}', successTab1);
}

//CREATE
function loadCreate() {
    addLoaderInTab('tab2');
    submitUrl(_createPage, '{}', successTab2);
}

function reloadCreate() {
    addLoaderInTab('tab2');
    submitUrl(_createPage, '{}', onReloadCreateSuccess);
}

function onReloadCreateSuccess(data) {
    $('#tab2').html(data);
    restoreTextInTab2();
    showTab('tab2');
}


function callCreateWithCallBack(jqForm, jqFormSerialized, onCallBackSuccess) {
    addLoaderInTab('tab1');
    CallSubmit(jqForm, jqFormSerialized, onCallBackSuccess, null);
}


function callCreate(jqForm, jqFormSerialized) {
    addLoaderInTab('tab1');
    CallSubmit(jqForm, jqFormSerialized, onCallCreateSuccess, null);
}

function onCallCreateSuccess(data) {
    var json;
    try {
        json = jQuery.parseJSON(data);
        if (json === null && typeof data == 'object') {
            json = data;
        }
        //var json = data;
        if (json.Succeeded == false) {
            displayAlert(json.Message);
        }
        else {
            if (json.RedirectUrl !== "" && json.RedirectUrl !== undefined) {
                //window.location = json.RedirectUrl;
                location = json.RedirectUrl;
            }

            if (isCreatePage()) {
                location = _indexPage;
            }
            else {
                if (json.View) {
                    $('#tab2').html(json.View);
                }
                else {
                    $('#tab2').html(json);
                }
                //fix for nested requests.
                if (location == "") {
                    loadList();
                    restoreTextInTab2();
                }
            }
        }
    } catch (e) {
        $('#tab2').html(data);
        loadList();
        restoreTextInTab2();
    }
}

//EDIT
function editItem(item) {
    addLoaderInTab('tab2');
    submitUrl(_editPage + item, '{}', displayDataTab2);
    return false;
}

function callEdit(jqForm, jqFormSerialized) {
    addLoaderInTab('tab2');
    CallSubmit(jqForm, jqFormSerialized, onCallEditSuccess, null);
}

function onCallEditSuccess(data) {
    if (data.Succeeded == false) {
        displayAlert(data.Message);
    }
    else {
        if (isEditPage()) {
            location = _indexPage;
        }
        else {
            $('#tab2').html(data);
            loadList();
            restoreTextInTab2();
        }
    }
}

//DETAILS
function loadDetails(item) {
    submitUrl("/" + _entityPath + "/Details/" + item, '{}', displayDataTab2);
    return false;
}

//DELETE
function deleteItem(item) {
    _deleteId = item;
    return false;
}


function confirmDelete() {
    if (_deleteId != "")
        submitUrl("/" + _entityPath + "/Delete/" + _deleteId, '{}', loadList);

    _deleteId = "";
    return false;
}

function cancelDelete() {
    _deleteId = "";
    return false;
}

//Bulk DELETE
function selectAll(datatableId) {
    var selectAll = $('#selectAll').is(':checked');

    $('#' + datatableId).find('#selectedItems').attr('checked', selectAll);

    return false;
}


function bulkDelete(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _deleteItems = '&' + $('input:checkbox', oTable.fnGetNodes()).serialize();
//    _deleteItems = utils.qsToObject(_deleteItems);

    return false;
}

function confirmBulkDelete() {
    if (_deleteItems)
        submitUrl("/" + _entityPath + "/BulkDelete/", _deleteItems, loadList);

    _deleteItems = null;
    return false;
}

function selectAllAjax(datatableId) {
    var selectAll = $('#selectAll').is(':checked');
    $('#' + datatableId).find('#selectedItems').each(function () {
        $(this).attr('checked', selectAll);
        var iId = $(this).val();
        var is_in_array = $.inArray(iId, selected);
        if (is_in_array == -1) {
            selected[selected.length] = iId;
        }
        else if (selectAll == false)
        {
            selected = $.grep(selected, function (value) {
                return value != iId;
            });
        } 
    });

    return false;
}

function bulkDeleteAjax(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _deleteItems = '&selectedItems=' + selected.join('&selectedItems=');

    return false;
}


function bulkDeleteAll(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _deleteAllItems = '&' + $('input:checkbox', oTable.fnGetNodes()).serialize();
    //    _deleteItems = utils.qsToObject(_deleteItems);

    return false;
}

function confirmBulkDeleteAll() {
    if (_deleteAllItems)
        submitUrl("/" + _entityPath + "/BulkDeleteAll/", _deleteAllItems, loadList);

    _deleteAllItems = null;
    return false;
}

function bulkMove(datatableId) {
    var oTable = $('#' + datatableId).dataTable();
    _moveItems = '&' + $('input:checkbox', oTable.fnGetNodes()).serialize();

    //    _deleteItems = utils.qsToObject(_deleteItems);

    return false;
}

function confirmBulkMove() {
    if (_moveItems) {
        _moveItems += "&salesForceToMove=" + $("#salesForceToMove").val();
        submitUrl("/" + _entityPath + "/BulkMove/", _moveItems, loadList);
    }
    _moveItems = null;
    return false;
}
