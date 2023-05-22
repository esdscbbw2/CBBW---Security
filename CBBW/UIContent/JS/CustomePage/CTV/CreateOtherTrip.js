function ChangeToLocationType() {
    var myCtrl = $(ChangeToLocationType.caller.arguments[0].target);
    if (myCtrl.val() != '') {
        var myCashcadingID = 'ToLocation_' + myCtrl.attr('id').split('_')[1];
        var url = '/CTV2/GetToLocationsFromTypes?TypeIDs=' + myCtrl.val();
        FillCashCadingMultiSelect(myCashcadingID, url,true);
        myCtrl.isValid();
    } else { myCtrl.isInvalid(); }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
};
function ChangeFromLocationType() {
    var myCtrl = $(ChangeFromLocationType.caller.arguments[0].target);
    if (myCtrl.val() != '') {
        var myCashcadingID = 'FromLocation_' + myCtrl.attr('id').split('_')[1];
        var url = '/CTV2/GetToLocationsFromTypes?TypeIDs=' + myCtrl.val();
        FillCashCadingDropDown(myCashcadingID, url, false,'Select Location');
        myCtrl.isValid();
    }
    else { myCtrl.isInvalid(); }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
};
function ValidateTripPurpose() {
    var myCtrl = $(ValidateTripPurpose.caller.arguments[0].target);
    if (myCtrl.val() != '') {
        if (isValidMaxWordCount(myCtrl.val(), 100)) { myCtrl.isValid(); } else { myCtrl.isInvalid(); }
    } else { myCtrl.isInvalid(); }
    SubmitBtnStatus('btnSubmit','HdrDiv');
};
function FromDateChanged() {
    var myCtrl = $(FromDateChanged.caller.arguments[0].target);
    myCtrl.ApplyCustomDateFormat();
    var myTimeCtrl = $('#' + 'time-' + myCtrl.attr('id'));
    myTimeCtrl.val('').isInvalid();
    if (myCtrl.val() != '') { myCtrl.isValid(); } else { myCtrl.isInvalid(); }
    //Changing the time controll
    
};
function AddBtnClicked(sourceTBody, destinationTBody, IsRemoveBtn, IsAddBtn, IsAddBtnEnable, SubmitBtnID, CtrlContainerID) {
    var insrowid = $(AddBtnClicked.caller.arguments[0].target.closest('.add-row')).attr('id');
    var r = TableRowCloaning(sourceTBody, destinationTBody, insrowid, IsRemoveBtn, IsAddBtn, IsAddBtnEnable);
    if (r > 0) { SubmitBtnStatus(SubmitBtnID, CtrlContainerID); }
    var disableDates = GetDisableDates();
    $('.disableDates').each(function () {
        DisableDatesInCalendar(disableDates, $(this).attr('id'));
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
};
$(document).ready(function () {
    SettingSlotForFromDate();
});
//Dummy Functions
function SettingSlotForFromDate() { //Dummy Code
    var disabledDates = ["2023-05-25", "2023-05-28", "2023-05-31"]; // Array of disabled dates
    var CtrlID = 'FromDate_0';
    DisableDatesInCalendar(disabledDates, CtrlID);
};
