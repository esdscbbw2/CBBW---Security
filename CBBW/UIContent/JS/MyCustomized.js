/**
 * js References
 * multiselect.js,
 * css References
 * style.css,bootstrap-multiselect.css,
 */
//-----------------------------------------------------
/**
 * Ajax Functions
 *
 */
function GetDataFromAjax(url) {
    //alert(url);
    return $.ajax({
        url: url,
        method: "GET",
        dataType: "json"
    });
};
function PostDataInAjax(url, BodyParamInJson) {
    return $.ajax({
        method: 'POST',
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: BodyParamInJson
    });
};
function PostDataInAjaxWithResponseHandleing(url, BodyParamInJson,IsRedirect,RedirectUrl,IsCallback,CallBackFunction) {
    //REsponse should come in "MyAjaxResponse" object.
    $.ajax({
        method: 'POST',
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: BodyParamInJson,
        success: function (data) {
            if (data.bResponseBool) {
                if (IsRedirect) {
                    MyAlertWithRedirection(1, data.sResponseString, RedirectUrl);
                }
                else if (IsCallback) {
                    MyAlertWithCallBack(1, data.sResponseString, CallBackFunction);
                }
                else {
                    MyAlert(1, data.sResponseString);
                }
            }
            else {
                MyAlert(3, data.sResponseString);
            }
            
        },
        error: function (xhr, status, error) {
            // Handle error
            MyAlert(3, 'Error Status: '+status + ' Description: '+error);
        }
    });
};
function HandleResponseOfPostRequest(data, SuccessMessageText, IsRedirect, RedirectUrl, IsCallback, CallBackFunction) {
    //REsponse should come in "MyAjaxResponse" object.
    //public class MyAjaxResponse {
    //    public DateTime dResponseDate { get; set; }
    //    public int iResponseInteger { get; set; }
    //    public bool bResponseBool { get; set; }
    //    public string sResponseString { get; set; }
    //    public string sResponseString2 { get; set; }
    //}
    $(data).each(function (index, item) {
        if (item.bResponseBool == true) {
            if (IsRedirect) {
                MyAlertWithRedirection(1, SuccessMessageText, RedirectUrl);
            }
            else if (IsCallback) {
                MyAlertWithCallBack(1, SuccessMessageText, CallBackFunction);
            }
            else {
                MyAlert(1, SuccessMessageText);
            }
        }
        else {
            MyAlert(3, item.sResponseString);
        }
    });
};
function GetDataFromTable(tableID) {
    //The fields should have an attribute "data-name", Which is the property name of the MVC object
    // "data-name-text" attribute can be used for dropdown or multiselect to get the selected text.
    var schrecords = '';
    var dataname;
    var datavalue;
    var mrecord = '';
    $('#' + tableID + ' tbody tr').each(function () {
        mRow = $(this);
        mRow.find('[data-name]').each(function () {
            that = $(this);
            dataname = that.attr('data-name');
            if (that.hasClass('htmlVal')) {
                datavalue = that.html();
            }
            else if (that.hasClass('CheckVal')) {
                if (that.prop("checked")) { datavalue = "true" } else { datavalue = "false" }
            }
            else { datavalue = that.val(); }
            mrecord = mrecord + '"' + dataname + '":"' + datavalue + '",';
        });
        mRow.find('[data-name-text]').each(function () {
            that = $(this);
            dataname = that.attr('data-name-text');
            thatid = that.attr('id');
            datavalue = $('#' + thatid + ' option:selected').toArray().map(item => item.text).join();
            mrecord = mrecord + '"' + dataname + '":"' + datavalue + '",';
        });
        mrecord = mrecord.replace(/,\s*$/, "");
        schrecords = schrecords + '{' + mrecord + '},';
        mrecord = '';
    });
    schrecords = schrecords.replace(/,\s*$/, "");
    schrecords = '[' + schrecords + ']';
    //alert(schrecords);
    return schrecords;
};
function GetDataFromDivHavingNoTables(divID) {
    //The fields should have an attribute "data-name", Which is the property name of the MVC object
    // "data-name-text" attribute can be used for dropdown or multiselect to get the selected text.
    var schrecords = '';
    var dataname;
    var datavalue;
    var mrecord = '';
    var myDiv = $('#' + divID);
    myDiv.find('[data-name]').each(function () {
        that = $(this);
        dataname = that.attr('data-name');
        if (that.hasClass('htmlVal')) {
            datavalue = that.html();
        }
        else if (that.hasClass('CheckVal')) {
            if (that.prop("checked")) { datavalue = "true" } else { datavalue="false"}
        }
        else { datavalue = that.val(); }
        mrecord = mrecord + '"' + dataname + '":"' + datavalue + '",';
    });
    myDiv.find('[data-name-text]').each(function () {
        that = $(this);
        dataname = that.attr('data-name-text');
        thatid = that.attr('id');
        datavalue = $('#' + thatid + ' option:selected').toArray().map(item => item.text).join();
        mrecord = mrecord + '"' + dataname + '":"' + datavalue + '",';
    });
    mrecord = mrecord.replace(/,\s*$/, "");
    schrecords = schrecords + '{' + mrecord + '}';    
    //alert(schrecords);
    return schrecords;
};
/**
 * Functions related to Input Controlls
 *
 */
function refreshDropdown(data, myCtrlID, IsIDString, DefaultText) {
    var myCtrl = $("#" + myCtrlID);
    myCtrl.empty(); // Clear existing options
    myCtrl.append($('<option/>', { value: "", text: DefaultText })); // Adding Default Text
    $.each(data, function (index, item) {
        if (IsIDString) {
            myCtrl.append($('<option/>', { value: item.IDStr, text: item.DisplayText }));
        } else {
            myCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
        }
    });
    myCtrl.isInvalidCtrl();
}
function assignValueToDropdown(myCtrlID, value) {
    // Assign the specified value to the dropdown    
    if (value != '') { $("#" + myCtrlID).val(value).isValid(); }
}
function refreshMultiselect(data, myCtrlID, IsIDString) {
    var myCtrl = $("#" + myCtrlID);
    myCtrl.empty();
    myCtrl.multiselect('destroy');
    //myCtrl.append($('<option/>', { value: "", text: DefaultText })); // Adding Default Text
    $.each(data, function (index, item) {
        if (IsIDString) {
            myCtrl.append($('<option/>', { value: item.IDStr, text: item.DisplayText }));
        }
        else { myCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText })); }
    });
    myCtrl.attr('multiple', 'multiple');
    myCtrl.multiselect({
        templates: {
            button: '<button id="B0" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
        },
    });
    myCtrl.multiselect('clearSelection');
    myCtrl.multiselect('refresh');
    myCtrl.isInvalidCtrl();
}
function assignValueToMultiSelect(myCtrlID, value) {
    if (value != '') {
        xCtrl = $("#" + myCtrlID);
        var i = value.indexOf(',');
        if (i >= 0) {
            xCtrl.val(value.split(','));
        } else {
            xCtrl.val(value);
        }

        xCtrl.multiselect('refresh');
        xCtrl.isValid();
    }
}
function FillCashCadingDropDown(myCtrlID, url, IsIDString, DefaultText) {
    var myCtrl = $('#' + myCtrlID);
    $.ajax({
        url: url,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            myCtrl.empty();
            myCtrl.append($('<option/>', { value: "-1", text: DefaultText }));
            $(data).each(function (index, item) {
                if (IsIDString) {
                    myCtrl.append($('<option/>', { value: item.IDStr, text: item.DisplayText }));
                } else {
                    myCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
                }
            });
        }
    });
};
function FillCashCadingMultiSelect(myCtrlID, url, IsIDString) {
    var myCtrl = $('#' + myCtrlID);
    $.ajax({
        url: url,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            myCtrl.empty();
            myCtrl.multiselect('destroy');
            $(data).each(function (index, item) {
                if (IsIDString) {
                    myCtrl.append($('<option/>', { value: item.IDStr, text: item.DisplayText }));
                }
                else { myCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText })); }
            });
            myCtrl.attr('multiple', 'multiple');
            myCtrl.multiselect({
                templates: {
                    button: '<button id="B0" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                },
            });
            myCtrl.multiselect('clearSelection');
            myCtrl.multiselect('refresh');
        }
    });
};
/**
 * Some Useful functions
 *
 */
function GetCommonValues(CommaSeparatedString1, CommaSeparatedString2) {
    var commonElements = [];
    var a1 = []; var a2 = [];
    if (CommaSeparatedString1.indexOf(',') >= 0) {
        a1 = CommaSeparatedString1.split(',');
    } else { a1.push(CommaSeparatedString1); }
    if (CommaSeparatedString2.indexOf(',') >= 0) {
        a2 = CommaSeparatedString2.split(',');
    } else { a2.push(CommaSeparatedString2); }
    $.each(a1, function (index, value) {
        if ($.inArray(value, a2) !== -1) {
            commonElements.push(value);
        }
    });
    return commonElements;
};
function GetCommonValuesFromArray(CommaSeparatedString1, CommaSeparatedString2) {
    var commonElements = [];
    $.each(CommaSeparatedString1, function (index, value) {
        if ($.inArray(value, CommaSeparatedString2) !== -1) {
            commonElements.push(value);
        }
    });
    return commonElements;
};
/**
 * Some Function Chaining
 *
 */
$.fn.isInvalid = function () {
    var that = this;
    that.addClass('is-invalid valid').removeClass('is-valid');
};
$.fn.isValid = function () {
    var that = this;
    that.addClass('is-valid valid').removeClass('is-invalid');
};
$.fn.removeValidation = function () {
    var that = this;
    that.removeClass('is-valid is-invalid');
};
$.fn.makeEnable = function () {
    var that = this;
    that.removeAttr('disabled');
    that.removeClass('nodrop');
};
$.fn.makeDisable = function () {
    var that = this;
    that.attr('disabled', 'disabled');
    that.addClass('nodrop');
};
$.fn.makeVisible = function () {
    var that = this;
    that.removeClass('inVisible');
};
$.fn.makeInvisible = function () {
    var that = this;
    that.AddClass('inVisible');
};
$.fn.isRed = function () {
    var that = this;
    that.addClass('border-red').removeClass('border-green');
};
$.fn.isGreen = function () {
    var that = this;
    that.addClass('border-green').removeClass('border-red');
};
/**
 * Validations
 *
 */
function isOnlyDigits(value) {
    var regex = /^[0-9]+$/;
    return regex.test(value);
};
function isDecimalNumber(value) {
    var regex = /^\d+(\.\d+)?$/;
    return regex.test(value);
}
function isOnlyAlphabates(value) {
    var regex = /^[a-zA-Z]+$/;
    return regex.test(value);
}
function isOnlyAlphabatesWithSpace(value) {
    var regex = /^[a-zA-Z\s]+$/;
    return regex.test(value);
}
function isAlphaNumeric(value) {
    var regex = /^[a-zA-Z0-9]+$/;
    return regex.test(value);
}
function isAlphaNumericWithSpace(value) {
    var regex = /^[a-zA-Z0-9\s]+$/;
    return regex.test(value);
}
function isValidEmailID(value) {
    var regex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
    return regex.test(value);
}
function isValidContactNumber(value) {
    var regex = /^\d{10}$/;
    return regex.test(value);
}
function isAlphabateWithMaxLimit(value, maxLimitInteger) {
    if (value.length > maxLimitInteger) { return false; }
    else {
        return value.match(/^[a-zA-Z]+$/);
    }
};
function isAlphaNumericWithMaxLimit(value, maxLimitInteger) {
    if (value.length > maxLimitInteger) { return false; }
    else {
        return value.match(/^[a-zA-Z0-9]+$/);
    }
};
function isSpaceAlphabateWithMaxLimit(value, maxLimitInteger) {
    if (value.length > maxLimitInteger) { return false; }
    else {
        return value.match(/^[a-zA-Z\s]+$/);
    }
};
function isSpaceAlphaNumericWithMaxLimit(value, maxLimitInteger) {
    if (value.length > maxLimitInteger) { return false; }
    else {
        return value.match(/^[a-zA-Z0-9\s]+$/);
    }
};
function validatePassword(value) {
    //(?=.* [a - z]) Contains at least one lowercase letter
    //(?=.* [A - Z]) Contains at least one uppercase letter
    //(?=.*\d) Contains at least one digit
    //(?=.* [@$!%*?&]) Contains at least one special character
    //[A - Za - z\d@$!%*?&]{ 6,} Contains at least 6 characters and only includes uppercase

    var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,}$/;
    return regex.test(value);
}
function isValidMaxWordCount(value, MaxNoOfWordsRequired) {
    var wordcount = $.trim(value).split(" ").length;
    if (wordcount <= MaxNoOfWordsRequired) { return true; } else { return false; }
}
function CompareStringArrayForCommonValue(CommaSeparatedString1, CommaSeparatedString2) {
    var commonElements = [];
    $.each(CommaSeparatedString1, function (index, value) {
        if ($.inArray(value, CommaSeparatedString2) !== -1) {
            commonElements.push(value);
        }
    });
    if (commonElements.length > 0) {
        return true;
    } else { return false; }
};
function CompareStringForCommonValue(CommaSeparatedString1, CommaSeparatedString2) {
    var commonElements = [];
    var a1 = []; var a2 = [];
    if (CommaSeparatedString1.indexOf(',') >= 0) {
        a1 = CommaSeparatedString1.split(',');
    } else { a1.push(CommaSeparatedString1); }
    if (CommaSeparatedString2.indexOf(',') >= 0) {
        a2 = CommaSeparatedString2.split(',');
    } else { a2.push(CommaSeparatedString2); }
    $.each(a1, function (index, value) {
        if ($.inArray(value, a2) !== -1) {
            commonElements.push(value);
        }
    });
    if (commonElements.length > 0) {
        return true;
    } else { return false; }
};
/**
 * Date Time
 *
 */

/**
 * Sweet Alerts
 *
 */
function MyAlert(MessageType, MessageText) {
    //MessageType 0-Information Alert, 1-Success Alert,2-Confirmation Alert,
    //MessageType  3- Eror Alert. 4- Validation Failed Alert,5-Warning Alert,

    switch (MessageType) {
        case 1:
            MySuccessAlert(MessageText, '');
            break;
        case 3:
            MyErrorAlert(MessageText, '');
            break;
        case 4:
            MyValidationFailedAlert(MessageText, '');
            break;
        case 5:
            MyWarningAlert(MessageText, '');
            break;        
        default:
            MyInformationAlert(MessageText, '');
            break;
    }
};
function MyAlertWithCallBack(MessageType, MessageText, callBackFunctionName) {
    //MessageType 0-Information Alert, 1-Success Alert,2-Confirmation Alert,
    //MessageType  3- Eror Alert. 4- Validation Failed Alert,5-Warning Alert,
    //debugger;
    switch (MessageType) {
        case 1:
            MySuccessAlert(MessageText, callBackFunctionName);
            break;
        case 2:
            MyConfirmationAlert(MessageText, callBackFunctionName);
            break;
        case 3:
            MyErrorAlert(MessageText, callBackFunctionName);
            break;
        case 4:
            MyValidationFailedAlert(MessageText, callBackFunctionName);
            break;
        case 5:
            MyWarningAlert(MessageText, callBackFunctionName);
            break;
        case 6:
            MyConfirmationAlertV2(MessageText, callBackFunctionName);
            break;
        case 7:
            MyConfirmationCancelAlert(MessageText, callBackFunctionName);
            break;
        default:
            MyInformationAlert(MessageText, callBackFunctionName);
            break;
    }
};
function MyAlertWithRedirection(MessageType, MessageText, RedirectUrl) {
    //MessageType 0-Information Alert, 1-Success Alert,2-Confirmation Alert,
    //MessageType  3- Eror Alert. 4- Validation Failed Alert,5-Warning Alert,
    switch (MessageType) {
        case 1:
            MySuccessAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 2:
            MyConfirmationAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 3:
            MyErrorAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 4:
            MyValidationFailedAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 5:
            MyWarningAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 7:
            MyConfirmationAlertWithRedirectionOnCancel(MessageText, RedirectUrl);
            break;
        default:
            MyInformationAlertWithRedirection(MessageText, RedirectUrl);
            break;
    }
};
function MyConfirmationAlertWithCallBacks(MessageText, OkCallback, CancelCallback) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(function (result) {
        if (result.isConfirmed) {
            if (OkCallback != '' && OkCallback != 'NA' && typeof OkCallback === 'function') {
                OkCallback();
            }
        } else {
            if (CancelCallback != '' && CancelCallback != 'NA' && typeof CancelCallback === 'function') {
                CancelCallback();
            }
        }
    });
};
function MyConfirmationAlertWithRedirections(MessageText, OkRedirectUrl, CancelRedirectUrl) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(function (result) {
        if (result.isConfirmed) {
            window.location.href = OkRedirectUrl;
        }
        else {
            window.location.href = CancelRedirectUrl;
        }
    });
};
//Helping functions recomended not call directly from outside.
function MySuccessAlert(MessageText, callback) {
    Swal.fire({
        title: 'Success',
        text: MessageText,
        icon: 'success',
        customClass: 'swal-wide my-success',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(function (result) {
        if (result.isConfirmed) {
            if (callback != '' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MyInformationAlert(MessageText, callback) {
    Swal.fire({
        title: 'Information',
        text: MessageText,
        icon: 'info',
        customClass: 'swal-wide my-info',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(function (result) {
        if (result.isConfirmed) {
            if (callback != '' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MyValidationFailedAlert(MessageText, callback) {
    Swal.fire({
        title: 'Validation Failed',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(function (result) {
        if (result.isConfirmed) {
            if (callback != '' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MyErrorAlert(MessageText, callback) {
    Swal.fire({
        title: 'Error Occurred',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(function (result) {
        if (result.isConfirmed) {
            if (callback != '' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MyConfirmationAlert(MessageText, callback) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(function (result) {
        if (result.isConfirmed) {
            if (callback != '' && callback != 'NA' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MyConfirmationAlertV2(MessageText, callback) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: false,
    }).then(function (result) {
        if (result.isConfirmed) {
            if (callback != '' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MyConfirmationCancelAlert(MessageText, callback) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: false,
    }).then(function (result) {
        if (!result.isConfirmed) {
            if (callback != '' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MyWarningAlert(MessageText, callback) {
    Swal.fire({
        title: 'Warning',
        text: MessageText,
        icon: 'warning',
        customClass: 'swal-wide my-warning',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(function (result) {
        if (result.isConfirmed) {
            if (callback != '' && typeof callback === 'function') {
                callback();
            }
        }
    });
};
function MySuccessAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Success',
        text: MessageText,
        icon: 'success',
        customClass: 'swal-wide my-success',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyInformationAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Information',
        text: MessageText,
        icon: 'info',
        customClass: 'swal-wide my-info',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyValidationFailedAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Validation Failed',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyErrorAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Error Occurred',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyConfirmationAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyConfirmationAlertWithRedirectionOnCancel(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(callback);
    function callback(result) {
        if (result.value) {

        }
        else {
            window.location.href = RedirectUrl;
        }
    }
};
function MyWarningAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Warning',
        text: MessageText,
        icon: 'warning',
        customClass: 'swal-wide my-warning',
        confirmButtonText: "Proceed",
        cancelButtonText: "Stop",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};


/**
 * Auto Apply Classes
 * 1. For Multi Select: ApplyMultiSelect/ApplyMultiSelectWithSelectAll 
 */
$(document).ready(function () {    
    $('.ApplyMultiSelectWithSelectAll').each(function () {
        that = $(this);
        that.prop('multiple', 'multiple');
        that.multiselect({
            templates: {
                button: '<button type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
            },
            includeSelectAllOption: true,
            selectAllName: 'select-all-name'
        });
        that.multiselect('clearSelection');
        that.multiselect('refresh');
    });
    $('.ApplyMultiSelect').each(function () {
        that = $(this);
        that.prop('multiple', 'multiple');
        that.multiselect({
            templates: {
                button: '<button type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
            },
        });
        that.multiselect('clearSelection');
        that.multiselect('refresh');
    });
    $('.indDate').on('change', function () {
        selectedDate=$(this).val();
        var formattedDate = new Date(selectedDate).toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });
        $('#Input1').val(formattedDate);
        $(this).val(formattedDate);

        var inputField = document.getElementById("Date1");
        var selectedDate = inputField.value;
        var formattedDate = new Date(selectedDate).toLocaleDateString('en-GB', { day: '2-digit', month: '2-digit', year: 'numeric' });

        inputField.value = formattedDate;
    });
});