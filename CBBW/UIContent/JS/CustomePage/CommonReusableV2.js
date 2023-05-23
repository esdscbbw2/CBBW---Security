﻿//Section - Input Controll Functionalities
function FillCashCadingDropDown(myCtrlID, url, IsIDString,DefaultText) {
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
                    myCtrl.append($('<option/>', { value: item.IDStr, text: item.DisplayText}));
                } else {
                    myCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
                }
            });
        }
    });
};
function FillCashCadingMultiSelect(myCtrlID, url,IsIDString) {
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
                    myCtrl.append($('<option/>', { value: item.IDStr, text: item.DisplayText }));}
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
//Section - Common Button Functionality
function ClearBtnClicked(pageurl) {
    if ($('#IsBackDenied').val() == 1) {
        MyAlertWithRedirection(5, 'All Recently Unsaved Data Will Be Lost.', pageurl);
    }
    else {
        window.location.href = pageurl;
    }
};
function SubmitBtnStatus(SubmitBtnID, ControlDivID) {
    var SubmitBtn = $('#' + SubmitBtnID);
    var x = DivInvalidCount(ControlDivID);
    if (x == 0) { SubmitBtn.makeEnable(); } else { SubmitBtn.makeDisable() }
    $('#IsBackDenied').val(1);
};
function BackBtnClicked() {
    $.ajax({
        url: "/Security/Common/BackButtonClicked",
        success: function (result) { window.location.href = result; }
    });
};
function NormalBackBtnClicked(pageurl) {
    if ($('#IsBackDenied').val() == 1) {
        MyAlertWithRedirection(2, 'Are You Sure Want to Go Back?', pageurl)
    }
    else {
        window.location.href = pageurl;
    }
};
//Section - Table Row Cloaning
function RowAddButtonStatus(btnID) {
    var myCtrl = $('#' + btnID);
    var row = myCtrl.closest('tr');
    var invalidcount = row.find('.is-invalid').length;
    if (invalidcount > 0) { myCtrl.makeDisable(); } else { myCtrl.makeEnable();}
};
function RowSpanRemoveBtnClicked() {
    var $row = $(RowSpanRemoveBtnClicked.caller.arguments[0].target.closest('.add-row'));
    var nextrow = $row.next();
    var ind = 0;
    $row.find('td').each(function (index, element) {
        var $cell = $(element);
        var rowspan = $cell.attr('rowspan');
        if (rowspan > 1) {
            $cell.attr('rowspan', rowspan - 1);
            var $secondCell = nextrow.find('td:eq(' + ind + ')');
            $secondCell.before($cell);
            ind += 1;
            $row.remove();
        } else {
            $cell.remove();
        }
    });
};
function RowRemoveBtnClicked(destinationTBody) {
    var r = $(RowRemoveBtnClicked.caller.arguments[0].target.closest('.add-row'));
    r.find('.btn').tooltip('hide');
    if (r.attr("id") == 0) {
    } else {
        r.remove();
    };
    var sl = 2;
    $('#' + destinationTBody + ' th').each(function () {
        $(this).html(sl);
        sl += 1;
    });    
};
function RowRemoveBtnWithEffectSubmitClicked(destinationTBody, SubmitBtnID, CtrlContainerID) {
    var r = $(RowRemoveBtnWithEffectSubmitClicked.caller.arguments[0].target.closest('.add-row'));
    r.find('.btn').tooltip('hide');
    if (r.attr("id") == 0) {
    } else {
        r.remove();
    };
    var sl = 2;
    $('#' + destinationTBody + ' th').each(function () {
        $(this).html(sl);
        sl += 1;
    });
    SubmitBtnStatus(SubmitBtnID, CtrlContainerID);
};
function RowAddBtnClick(sourceTBody, destinationTBody, IsRemoveBtn, IsAddBtn, IsAddBtnEnable) {
    var insrow = $(RowAddBtnClick.caller.arguments[0].target.closest('.add-row'));
    var insrowid = insrow.attr('id');
    var clonerowid = TableRowCloaning(sourceTBody, destinationTBody, insrowid, IsRemoveBtn, IsAddBtn, IsAddBtnEnable);
    return clonerowid;
};
function RowAddBtnWithEffectSubmitClicked(sourceTBody, destinationTBody, IsRemoveBtn, IsAddBtn, IsAddBtnEnable, SubmitBtnID, CtrlContainerID) {
    var insrowid = $(RowAddBtnWithEffectSubmitClicked.caller.arguments[0].target.closest('.add-row')).attr('id');
    var r = TableRowCloaning(sourceTBody, destinationTBody, insrowid, IsRemoveBtn, IsAddBtn, IsAddBtnEnable);
    if (r > 0) { SubmitBtnStatus(SubmitBtnID, CtrlContainerID);}
};
function TableRowCloaning(sourceTBody, destinationTBody, rowid, IsRemoveBtn, IsAddBtn, IsAddBtnEnable) {
    //New - 1. Checkbox Label for attribute functionality autogenerated 2. InvisibleTag introduced
    // Source table Body must have a row having (id="0" class="add-row")
    //The controlls should have a class named "alterID";
    // buttons should have class "cloneBtn" - For tooltip functionalities
    //"addBtn" and removeBtn are also used for corrosponding buttons of a row
    //"CustomDateFormatCloneRow" - This class is used for customdatepicker. 
    //If multiselects are in a row then use the class "clonemultiselect" and remove multiple attribute and the classes which are responsible for multiselect creations.
    //Use "htmlVal" class for a controll if the value will be picked from innerhtml.
    //There should be "th" tag which may exclusively used for Serial Number Purpose.
    // "inVisibleTag" & "inValidTag" use this class to make a controll invalid or invisible when cloaning.
    //alert('CloneRow');
    var maxrows = 0, r = 0;
    var sourcebody = $('#' + sourceTBody);
    var destinationbody = $('#' + destinationTBody);
    $('#' + destinationTBody + ' tr').each(function () {
        var maxr = $(this).attr('id') * 1;
        if (maxr > maxrows) { maxrows = maxr; }
    });
    if (maxrows >= 1) { r = maxrows + 1; } else { r = 1; }//Geting maximum row
    var cloneready = sourcebody.find('tr').clone();
    cloneready.attr("id", r);
    cloneready.find('.alterID').each(function () {
        that = $(this);
        var mID = that.attr('id').split('_');
        var newID = mID[0] + '_' + r;
        that.attr('id', newID);
        //that.val('').isInvalid();
    });
    cloneready.find('.form-check-label').each(function () {
        that = $(this);
        var mID = that.attr('id');
        that.attr('for', 'opt' + mID)
    });
    cloneready.find('.inValidTag').each(function () {
        that = $(this);
        that.val('');
        that.isInvalid();
    });
    cloneready.find('.inVisibleTag').each(function () {
        that = $(this);
        //that.val('');
        that.addClass('inVisible');
    });
    cloneready.find('.btn-group').remove();
    cloneready.find('.ApplyMultiSelectWithSelectAll').each(function () {
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
    cloneready.find('.ApplyMultiSelect').each(function () {
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
    cloneready.find('input[type="date"]').each(function () {
        $(this).ApplyCustomDateFormat();
    });
    cloneready.find('.CustomTimeFormatCloneRow').each(function () {
        var firstOpen = true;
        var time;
        $(this).datetimepicker({
            useCurrent: false,
            format: "hh:mm A"
        }).on('dp.show', function () {
            if (firstOpen) {
                time = moment().startOf('day');
                firstOpen = false;
            } else {
                time = "01:00 PM"
            }

            $(this).data('DateTimePicker').date(time);
        });
    });
    cloneready.find('.cloneBtn').each(function () {
        that = $(this);
        that.on('mouseenter', function () {
            $(this).tooltip('show');
        });
        that.on('click mouseleave', function () {
            $(this).tooltip('hide');
        });
    });
    cloneready.find('.datelabel').each(function () {
        $(this).html('Select Date');
    });
    cloneready.find('.htmlVal').each(function () {
        $(this).html('');
    });
    cloneready.find('.addBtn').each(function () {
        var that = $(this);
        if (IsAddBtn) {
            that.removeClass('inVisible');
            if (IsAddBtnEnable) { that.removeAttr('disabled'); } else { that.attr('disabled', 'disabled'); }
        } else {
            that.addClass('inVisible');
        }
    });
    cloneready.find('.removeBtn').each(function () {
        var that = $(this);
        if (IsRemoveBtn) {
            that.removeClass('inVisible');
        } else { that.addClass('inVisible'); }
    });
    sourcebody.find('.btn').each(function () {
        that = $(this);
        that.on('mouseenter', function () {
            $(this).tooltip('show');
        });
        that.on('mouseleave click', function () {
            $(this).tooltip('hide');
        });
    });
    cloneready.find('.form-control').each(function () {
        $(this).focus(function () {
            $(this).tooltip('show');
        });
    });
    cloneready.find('.EntrynDisabledForEntry').each(function () {
        $(this).EntrynDisabledForEntry();
    });
    if (rowid == 0) {
        if (maxrows == 0) {
            destinationbody.append(cloneready);
        } else {
            $(cloneready).insertBefore('#' + destinationTBody + ' tr:first');
        }
    } else {
        $(cloneready).insertAfter('#' + rowid);
    }
    var sl = 2;
    $('#' + destinationTBody + ' th').each(function () {
        $(this).html(sl);
        sl += 1;
    });
    return r;
};
//Section - Date Time Functions
function GetCurrentTime() {
    var currentDate = new Date();
    var currentHour = currentDate.getHours();
    var currentMinute = currentDate.getMinutes();
    alert(('0' + currentHour).slice(-2) + ':' + ('0' + currentMinute).slice(-2));
    return ('0' + currentHour).slice(-2) + ':' + ('0' + currentMinute).slice(-2);
}
function IsValidTimeSelected(InputDate, InputTime) {
    //InputDate='2023-05-22' InputTime='20:22'
    var targetDateTime = new Date(InputDate + ' ' + InputTime);
    var currentDateTime = new Date();
    if (targetDateTime.getTime() >= currentDateTime.getTime() - 59000) {
        return true;
    } else {
        return false;
    }
};
function IsCurrentDate(InputDate) {
    //InputDate='2023-05-22'
    var targetDate = new Date(InputDate);
    var currentDate = new Date();
    if (targetDate.setHours(0, 0, 0, 0) === currentDate.setHours(0, 0, 0, 0)) {
        return true;
    } else {
        return false;
    }
};
$.fn.ApplyCustomDateFormat = function () {
    var that = this;
    var parentid = that.attr('id');
    var lblid = 'lbl' + parentid;
    var dt = this.val();
    var e = dt;
    if (dt.indexOf('/') != -1) {
        var e = dt.split('/').reverse().join('/');
    }
    else {
        var e = dt.split('-').reverse().join('/');
    }
    if (dt != '') { $('#' + lblid).html(e); } else { $('#' + lblid).html('Select A Date'); }
    
    //that.addClass('is-valid').removeClass('is-invalid')
};
$.fn.DisableDates = function () {
    var that = this;
    var disabledDates = ["2023-05-21", "2023-05-25", "2023-05-29"];
    that.datepicker({
        beforeShowDay: function (date) {
            var formattedDate = $.datepicker.formatDate("yy-mm-dd", date);
            return [disabledDates.indexOf(formattedDate) === -1];
        }
    });
};
function DisableDatesInCalendar(disabledDates, CtrlID) {
    var dateInput = document.getElementById(CtrlID);
    dateInput.addEventListener("input", function () {
        var selectedDate = this.value;
        // Check if the selected date is in the array of disabled dates
        if (disabledDates.includes(selectedDate)) {
            this.value = ""; // Clear the input value
            MyAlert(4, 'Local Trip Schedule Is Done On This Date. Please Select Another Date.')
            //alert("This date is disabled. Please select another date.");
        }
    });
};
function GetDisableDates() {    
    return $('#Slots_OccupiedSlots').val().split(',');
    //$.each(slots, function (index, value) {
    //    dates.push(value);
    //});

    //var dates = [];
    //dates.push("2023-05-25");
    //dates.push("2023-05-28");
    //dates.push("2023-05-31");
    //return dates;
};
//Section - Helper Functions
function DivInvalidCount(mdivID) {
    var x = 0;
    var mDiv = $('#' + mdivID);
    x = mDiv.find('.is-invalid').length;
    return x;
};
function WordCount(value) {
    return $.trim(value).split(" ").length;
};
function ExtractLocationCodeFromTypeLocationCodes(locCodes) {
    locCodes='2-1,2-2,3-4'
};
//Section - Style Functions
function LockSection(id) {
    $('#' + id).find('input').each(function () {
        $(this).attr('disabled', 'disabled');
    });
    $('#' + id).find('select').each(function () {
        $(this).attr('disabled', 'disabled');
    });
    $('#' + id).find('textarea').each(function () {
        $(this).attr('disabled', 'disabled');
    });
    $('#' + id).find('button').each(function () {
        $(this).attr('disabled', 'disabled');
    });
    $('#' + id).find('.EntrynDisabledForEntry').each(function () {
        $(this).EntrynDisabledForEntry();
    });
    $('#' + id).find('.EntryDoneDisableMode').each(function () {
        $(this).EntryDoneDisableMode();
    });
    $('#' + id).addClass('sectionB');    
};
function UnLockSection(id) {
    $('#' + id).find('input').each(function () {
        $(this).removeAttr('disabled');
    });
    $('#' + id).find('select').each(function () {
        $(this).removeAttr('disabled');
    });
    $('#' + id).find('textarea').each(function () {
        $(this).removeAttr('disabled');
    });
    $('#' + id).find('button').each(function () {
        $(this).removeAttr('disabled');
    });
    $('#' + id).find('.EntrynDisabledForEntry').each(function () {
        $(this).EntrynDisabledForEntry();
    });
    $('#' + id).removeClass('sectionB');
};
function LockControl(id) {
    var myCtrl = $('#' + id);
    myCtrl.attr('disabled', 'disabled');
    if (myCtrl.hasClass('EntrynDisabledForEntry')) { myCtrl.EntrynDisabledForEntry(); }
};
function UnLockControl(id) {
    var myCtrl = $('#' + id);
    myCtrl.removeAttr('disabled');
    if (myCtrl.hasClass('EntrynDisabledForEntry')) { myCtrl.EntrynEnableForEntry(); }
};
$.fn.EntrynDisabledForEntry = function () {
    var that = this;
    that.val('');
    that.attr('disabled', 'disabled')
        .addClass('bg-blue border-red nodrop');
};
$.fn.EntrynEnableForEntry = function () {
    var that = this;
    that.removeAttr('disabled')
        .addClass('is-invalid')
        .removeClass('bg-blue border-blue is-valid nodrop');
    that.val('');
};
$.fn.EntryDoneDisableMode = function () {
    var that = this;
    that.attr('disabled', 'disabled').addClass('bg-blue border-green');
};
$.fn.BtnDisableAfterEntry = function () {
    var that = this;
    that.attr('disabled', 'disabled').addClass('border-green');
};
//Section Start - Validations
function validatePassword(password) {
    //(?=.* [a - z]) Contains at least one lowercase letter
    //(?=.* [A - Z]) Contains at least one uppercase letter
    //(?=.*\d) Contains at least one digit
    //(?=.* [@$!%*?&]) Contains at least one special character
    //[A - Za - z\d@$!%*?&]{ 6,} Contains at least 6 characters and only includes uppercase

    var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,}$/;
    return regex.test(password);
}
function isAlphanumeric(input) {
    var regex = /^[a-zA-Z0-9]+$/;
    return regex.test(input);
}
function isAlphanumericWithSpace(input) {
    var regex = /^[a-zA-Z0-9\s]+$/;
    return regex.test(input);
}
function isValidContactNumber(contact) {
    var regex = /^\d{10}$/;
    return regex.test(contact);
}
function isValidMaxWordCount(value, MaxNoOfWordsRequired) {
    var wordcount = $.trim(value).split(" ").length;
    if (wordcount <= MaxNoOfWordsRequired) { return true; } else { return false;}
}
function isDecimalNumber(input) {
    var regex = /^\d+(\.\d+)?$/;
    return regex.test(input);
}
function isNumeric(input) {
    var regex = /^[0-9]+$/;
    return regex.test(input);
}
//Section Start - Runtime Classess
$.fn.isInvalid = function () {
    var that = this;
    that.addClass('is-invalid valid').removeClass('is-valid');
};
$.fn.isValid = function () {
    var that = this;
    that.addClass('is-valid valid').removeClass('is-invalid');
};
$.fn.makeEnable = function () {
    var that = this;
    that.removeAttr('disabled');
};
$.fn.makeDisable = function () {
    var that = this;
    that.attr('disabled', 'disabled');
};
$.fn.makeVisible = function () {
    var that = this;
    that.removeClass('inVisible');
};
$.fn.makeInVisible = function () {
    var that = this;
    that.AddClass('inVisible');
};
//Section Start- SweetAlert Templates
function MyAlert(MessageType, MessageText) {
    //MessageType 0-Information Alert, 1-Success Alert,2-Confirmation Alert,
    //MessageType  3- Eror Alert. 4- Validation Failed Alert,5-Warning Alert,
   
    var IsOK = false;
    switch (MessageType) {
        case 1:
            IsOK = MySuccessAlert(MessageText);
            break;
        case 2:
            IsOK = MyConfirmationAlert(MessageText);
            break;
        case 3:
            IsOK = MyErrorAlert(MessageText);
            break;
        case 4:
            IsOK = MyValidationFailedAlert(MessageText);
            break;
        case 5:
            IsOK = MyWarningAlert(MessageText);
            break;
        default:
            IsOK = MyInformationAlert(MessageText);
    }
    return IsOK;
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
        default:
            MyInformationAlertWithRedirection(MessageText, RedirectUrl);
    }
};
function MySuccessAlert(MessageText) {
    var IsOK = false;
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
            IsOK=true;
        }
    }
    return IsOK;
};
function MyInformationAlert(MessageText) {
    var IsOK = false;
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
            IsOK = true;
        }
    }
    return IsOK;
};
function MyValidationFailedAlert(MessageText) {
    var IsOK = false;
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
            IsOK = true;
        }
    }
    return IsOK;
};
function MyErrorAlert(MessageText) {
    var IsOK = false;
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
            IsOK = true;
        }
    }
    return IsOK;
};
function MyConfirmationAlert(MessageText) {
    var IsOK = false;
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
            IsOK = true;
        }
    }
    return IsOK;
};
function MyWarningAlert(MessageText) {
    var IsOK = false;
    Swal.fire({
        title: 'Warning',
        text: MessageText,
        icon: 'warning',
        customClass: 'swal-wide my-warning',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            IsOK = true;
        }
    }
    return IsOK;
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
//Section End- SweetAlert Templates
$(document).ready(function () {
    $('.form-control').focus(function () {
        $(this).tooltip('show');
    });
    $('input[type="date"]').each(function () {
        $(this).on("change", function () {
            $(this).ApplyCustomDateFormat();
        });        
    });
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
    $('.timePickerPreDateCtrl').each(function () {
        var myDtCtrl = $('#' + $(this).attr('id').split('-')[1]);
        $(this).datetimepicker({
            format: "hh:mm A"
        }).on("dp.show", function (e) {
            time = "01:00 PM"
        }).on("dp.change", function (e) {
            var myDate = myDtCtrl.val();
            $(this).isValid();
            if (myDate != '') {
                if (!IsValidTimeSelected(myDate, $(this).val())) {
                    $(this).isInvalid();
                    MyAlert(4, "Selected Time Should Be Greater Than The Current Time.");
                }                
            }
        });
    });
    $('.EntrynDisabledForEntry').each(function () {
        $(this).EntrynDisabledForEntry();
    });
});
//dummy functions

var setMin = function (currentDateTime) {
    this.setOptions({
        minDate: '-1970/01/02'
    });
    this.setOptions({
        minTime: 0
    });
};

