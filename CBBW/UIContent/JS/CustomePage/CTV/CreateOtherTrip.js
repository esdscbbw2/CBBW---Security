function GetUserClickSequence(value,ctrlID) {
    var string1 = $('#' + ctrlID).html();
    var string2 = value;

    var arr1 = string1.split(',');
    var arr2 = string2.split(',');

    var updatedArr1 = arr1.filter(function (value) {
        return !arr2.includes(value);
    });

    var updatedString1 = updatedArr1.join(',');
    $('#' + ctrlID).html(updatedString1);
});

};
function GetAvaialbleSlots() {
    var x = $('#Slots_AvailableSlots').val();
    var datesArray = x.split(",");
    var twoDimensionalArray = [];
    for (var i = 0; i < datesArray.length; i += 2) {
        var subArray = [datesArray[i], datesArray[i + 1]];
        twoDimensionalArray.push(subArray);
    }
    return twoDimensionalArray;
};
function CheckDatesAvailableOrNot(FromDate,ToDate) {
    var isBetween = false;
    date1 = new Date(FromDate);
    date2 = new Date(ToDate);
    twoDimensionalArray = GetAvaialbleSlots();
    $.each(twoDimensionalArray, function (index, row) {
        var min = new Date(row[0]);
        var max = new Date(row[1]);
        if (date1 >= min && date1 <= max && date2 >= min && date2 <= max) {
            isBetween = true;
            return false; // Exit the loop early if found
        }
    });
    return isBetween;
};
function GetToDate(ID) {
    var mybtnCtrl = $('#' + ID);
    var myToDateCtrl = $('#ToDate_' + ID.split('_')[1]);
    var row = mybtnCtrl.closest('tr');
    var fromDate = row.find('.disableDates').val();
    var fromloctype = row.find('.fromloctype').val();
    var fromloc = row.find('.fromloc').val();
    var toloc = row.find('.toloc').val();
    if (fromDate != '' && fromloc != '' && fromloctype != '' && toloc != null) {
        $.ajax({
            url: '/CTV2/GetToDate?FromDate=' + fromDate + '&FromLocationType=' + fromloctype + '&FromLocation=' + fromloc + '&ToLocation=' + toloc,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    myToDateCtrl.val(item.sResponseString);
                    if (item.bResponseBool == true) {
                        myToDateCtrl.isValid();
                    }
                    else {
                        myToDateCtrl.isInvalid();
                        MyAlert(3, 'Error Calculating Schedule To Date.');
                    }
                });
            }
        });
    }
    
};
function SettingSlotForFromDate() {
    var CtrlID = 'FromDate_0';
    DisableDatesInCalendar(GetDisableDates(), CtrlID);
};
function ChangeToLocationType() {
    var myCtrl = $(ChangeToLocationType.caller.arguments[0].target);
    var myCashcadingID = 'ToLocation_' + myCtrl.attr('id').split('_')[1];
    var myCashcadingCtrl = $('#' + myCashcadingID);
    if (myCtrl.val() != '') {        
        var url = '/CTV2/GetToLocationsFromTypes?TypeIDs=' + myCtrl.val();
        FillCashCadingMultiSelect(myCashcadingID, url,true);
        myCtrl.isValid();
        myCashcadingCtrl.EntrynEnableForEntry();        
    } else { myCtrl.isInvalid(); myCashcadingCtrl.EntrynDisabledForEntry(); }
    var addBtnID = 'AddBtn_' + myCtrl.attr('id').split('_')[1];
    GetToDate(myCtrl.attr('id'));
    RowAddButtonStatus(addBtnID);
    //SubmitBtnStatus('btnSubmit', 'HdrDiv');
};
function ChangeFromLocationType() {
    var myCtrl = $(ChangeFromLocationType.caller.arguments[0].target);
    var myCashcadingID = 'FromLocation_' + myCtrl.attr('id').split('_')[1];
    var myCashcadingCtrl = $('#' + myCashcadingID);
    if (myCtrl.val() != '') {        
        var url = '/CTV2/GetToLocationsFromTypes?TypeIDs=' + myCtrl.val();
        FillCashCadingDropDown(myCashcadingID, url, false,'Select Location');
        myCtrl.isValid();
        myCashcadingCtrl.EntrynEnableForEntry();
    }
    else { myCtrl.isInvalid(); myCashcadingCtrl.EntrynDisabledForEntry(); }
    var addBtnID = 'AddBtn_' + myCtrl.attr('id').split('_')[1];
    RowAddButtonStatus(addBtnID);
    //SubmitBtnStatus('btnSubmit', 'HdrDiv');
};
function ChangeFromLocation() {
    var myCtrl = $(ChangeFromLocation.caller.arguments[0].target);
    var myNextCtrl =$('#ToLocationType_' + myCtrl.attr('id').split('_')[1]);
    if (myCtrl.val() != '') {
        myCtrl.isValid();
        myNextCtrl.EntrynEnableForEntry();
    }
    else { myCtrl.isInvalid(); myNextCtrl.EntrynDisabledForEntry(); }
    var addBtnID = 'AddBtn_' + myCtrl.attr('id').split('_')[1];
    GetToDate(myCtrl.attr('id'));
    RowAddButtonStatus(addBtnID);
};
function ChangeToLocation() {
    var myCtrl = $(ChangeToLocation.caller.arguments[0].target);
    if (myCtrl.val() != '') { myCtrl.isValid(); } else { myCtrl.isInvalid(); }
    var addBtnID = 'AddBtn_' + myCtrl.attr('id').split('_')[1];
    GetToDate(myCtrl.attr('id'));
    RowAddButtonStatus(addBtnID);
    GetUserClickSequence(myCtrl.val(),'trlDiv');
};
function ValidateTripPurpose() {
    var myCtrl = $(ValidateTripPurpose.caller.arguments[0].target);
    LockSection('tblOTVAdd');
    if (myCtrl.val() != '') {
        if (isValidMaxWordCount(myCtrl.val(), 100)) {
            myCtrl.isValid();
            UnLockSection('tblOTVAdd');
        } else { myCtrl.isInvalid(); }
    } else { myCtrl.isInvalid(); }
    SubmitBtnStatus('btnSubmit','HdrDiv');
};
function FromDateChanged() {
    var myCtrl = $(FromDateChanged.caller.arguments[0].target);    
    var myNextCtrl = $('#FromLocationType_' + myCtrl.attr('id').split('_')[1]);
    myCtrl.ApplyCustomDateFormat();
    var myTimeCtrl = $('#' + 'time-' + myCtrl.attr('id'));    
    if (myCtrl.val() != '') {
        myCtrl.isValid();
        myTimeCtrl.EntrynEnableForEntry();        
    }
    else { myCtrl.isInvalid(); myTimeCtrl.EntrynDisabledForEntry(); }
    myNextCtrl.EntrynDisabledForEntry();
    var addBtnID = 'AddBtn_' + myCtrl.attr('id').split('_')[1];
    GetToDate(myCtrl.attr('id'));
    RowAddButtonStatus(addBtnID);
    //SubmitBtnStatus('btnSubmit', 'HdrDiv');
};
function AddBtnClicked(sourceTBody, destinationTBody, IsRemoveBtn, IsAddBtn, IsAddBtnEnable, SubmitBtnID, CtrlContainerID) {
    var insrowid = $(AddBtnClicked.caller.arguments[0].target.closest('.add-row')).attr('id');
    var r = TableRowCloaning(sourceTBody, destinationTBody, insrowid, IsRemoveBtn, IsAddBtn, IsAddBtnEnable);
    if (r > 0) { SubmitBtnStatus(SubmitBtnID, CtrlContainerID); }
    var disableDates = GetDisableDates();
    $('.disableDates').each(function () {
        DisableDatesInCalendar(disableDates, $(this).attr('id'));
    });
    $('.timePickerPreDateCtrlV2').each(function () {
        var myDtCtrl = $('#' + $(this).attr('id').split('-')[1]);
        var myNextCtrl = $('#FromLocationType_' + $(this).attr('id').split('_')[1]);
        $(this).datetimepicker({
            format: "hh:mm A"
        }).on("dp.show", function (e) {
            time = "01:00 PM"
        }).on("dp.change", function (e) {
            var myDate = myDtCtrl.val();
            $(this).isValid();
            myNextCtrl.EntrynEnableForEntry();
            if (myDate != '') {
                if (!IsValidTimeSelected(myDate, $(this).val())) {
                    $(this).isInvalid();
                    myNextCtrl.EntrynDisabledForEntry();
                    MyAlert(4, "Selected Time Should Be Greater Than The Current Time.");
                }
            }
        });
    });
};
$(document).ready(function () {
    SettingSlotForFromDate();
    LockSection('tblOTVAdd');
    $('.timePickerPreDateCtrlV2').each(function () {
        var myDtCtrl = $('#' + $(this).attr('id').split('-')[1]);
        var myNextCtrl = $('#FromLocationType_' + $(this).attr('id').split('_')[1]);
        $(this).datetimepicker({
            format: "hh:mm A"
        }).on("dp.show", function (e) {
            time = "01:00 PM"
        }).on("dp.change", function (e) {
            var myDate = myDtCtrl.val();
            $(this).isValid();
            myNextCtrl.EntrynEnableForEntry();
            if (myDate != '') {
                if (!IsValidTimeSelected(myDate, $(this).val())) {
                    $(this).isInvalid();
                    myNextCtrl.EntrynDisabledForEntry();
                    MyAlert(4, "Selected Time Should Be Greater Than The Current Time.");
                }
            }
        });
    });
});
//Dummy Functions

