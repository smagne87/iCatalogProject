var totalFilesToUpload = 0;
var totalFilesToUpload = 0;

function loadUploadify(idUploadify, url, fileExt, fileMaxSize, onSuccess, onFailed) {
    if (!fileExt)
        fileExt = '*.jpeg;*.jpg;*.gif;*.png';
    if (!fileMaxSize)
        fileMaxSize = 0;

    var lastHdIdUploadify = 'hd_' + idUploadify;
    $('#' + idUploadify).uploadify({
        'swf': '/js/Uploadify/uploadify.swf',
        //'uploader': '/UploadBrandHandler.ashx',
        'fileSizeLimit': fileMaxSize,
        'uploader': url,
        'fileTypeExts': fileExt,
        'cancelImg': 'uploadify-cancel.png',
        'buttonText': 'Examinar',
        'auto': false,
        'multi': false,
        'queueSizeLimit': 1,
        'buttonImage': null,
        'onUploadComplete': function (file) {
            //            alert('The file ' + file.name + ' finished processing.');
        },
        'onQueueComplete': function (queueData) {
            //            alert(queueData.uploadsSuccessful + ' files were successfully uploaded.');
        },
        'onUploadSuccess': function (file, data, response) {
            //            alert('The file ' + file.name + ' was successfully uploaded with a response of ' + response + ':' + data);
            totalFilesToUpload--;

            if (totalFilesToUpload == 0)
                onSuccess(data);
        },
        'onUploadError': function (file, errorCode, errorMsg, errorString) {
            if (onFailed) {
                onFailed(errorString);
            }
            else {
                onSubmitFailed(errorString);
            }
            //            alert('The file ' + file.name + ' could not be uploaded: ' + errorString);
        },
        'onSelect': function (file) {
            // Update selected so we know they have selected a file
            totalFilesToUpload++;
            $("#" + lastHdIdUploadify).val('1');
        },
        'onCancel': function (event, ID, fileObj, data) {
            // Update selected so we know they have no file selected
            totalFilesToUpload--;
            $("#" + lastHdIdUploadify).val('');
        }
    });
}

function isFileSelected(id) {
    if ($("#" + 'hd_' + id).val().length > 0) {
        return true;
    }
    else {
        return false;
    }
}

function uploadFile(idFU, field, countUploads, jqForm, formData, auth, aspSessionId) {
    //converting serialized data to json
    formData += '&field=' + field + '&countUploads=' + countUploads + '&ASPSESSID=' + aspSessionId + '&AUTHID=' + auth;

    var qsAsObject = utils.qsToObject(formData);

    $(idFU).uploadify('settings', 'formData', qsAsObject);
    $(idFU).uploadify('upload');
}