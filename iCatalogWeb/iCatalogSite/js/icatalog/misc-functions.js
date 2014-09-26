var utils;

function getParameterByName(name) {
    if (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.search);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    return "";
}

function getParameterFromTextByName(text, name) {
    if (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(text);
        if (results == null)
            return "";
        else
            return results[1];
    }
    return "";
}


function getTextReplaced(source, oldValue, newValue) {
    if (source) {
        return source.replace(oldValue, newValue);
    }
    return "";
}

function getNicEditorVal(id) {
    var nicE = new nicEditors.findEditor(id);
    content = nicE.getContent();

    return content;
}

function htmlEncode(value) {
    if (value) {
        return jQuery('<div />').text(value).html();
    } else {
        return '';
    }
}

function htmlDecode(value) {
    if (value) {
        return $('<div />').html(value).text();
    } else {
        return '';
    }
}

function urlEncode(value) {
    if (value) {
        return encodeURIComponent(value);
    }
    return '';
}

function urlDecode(value) {
    if (value) {
        return decodeURIComponent(value);
    }
    return '';
}

utils = {
    qsToObject: function (qs) {
        var o = {};
        qs.replace(
            new RegExp("([^?=&]+)(=([^&]*))?", "g"),
            function ($0, $1, $2, $3) {
                if (!o[$1]) {
                    o[$1] = $3;
                }
                else {
                    var oldVal = o[$1];
                    if (!isArray(oldVal)) {
                        o[$1] = [];
                        o[$1].push(oldVal);
                    }

                    o[$1].push($3);
                }
            }
            );
        return o;
    },
    objectToQs: function (o) {
        var str = [];
        for (var k in o)
            str.push(k + "=" + o[k]);
        return str.join("&");
    }
}

function isArray(what) {
    return Object.prototype.toString.call(what) === '[object Array]';
}