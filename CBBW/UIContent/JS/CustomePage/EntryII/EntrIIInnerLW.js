function RFIDInChanged() {
    var targetCtrl = $('#RFIDCardIn');
    var divCtrl = $('#VTourInTimeCtrl');
    var timCtrl = $('#TourInTimeCtrl');
    var RFIDDate = $('#SchFromDate').val();
    var RFIDNo = targetCtrl.val();
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
        divCtrl.removeClass('inVisible');
        timCtrl.addClass('inVisible');
        $.ajax({
            url: '/EntryII/GetRFIDPunchTime',
            method: 'GET',
            data: { RFIDNumber: RFIDNo, PunchDate: RFIDDate },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    divCtrl.html(item.PunchInStr);
                });
            }
        });
    }
    else {
        //targetCtrl.isInvalid();
        divCtrl.addClass('inVisible');
        timCtrl.removeClass('inVisible');
    }
};
function RFIDOutChanged() {
    var targetCtrl = $('#RFIDCardOut');
    var divCtrl = $('#VTourOutTimeCtrl');
    var timCtrl = $('#TourOutTimeCtrl');
    var RFIDDate = $('#SchToDate').val();
    var RFIDNo = targetCtrl.val();
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
        divCtrl.removeClass('inVisible');
        timCtrl.addClass('inVisible');
        $.ajax({
            url: '/EntryII/GetRFIDPunchTime',
            method: 'GET',
            data: { RFIDNumber: RFIDNo, PunchDate: RFIDDate },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    divCtrl.html(item.PunchOutStr);
                });
            }
        });
    }
    else {
        //targetCtrl.isInvalid();
        divCtrl.addClass('inVisible');
        timCtrl.removeClass('inVisible');
    }
};
function TimeInCtrlBlured(){
    var targetCtrl = $(TimeInCtrlBlured.caller.arguments[0].target);
    var timerid = targetCtrl.attr('id');
    var datadivid = 'V' + timerid;
    $('#' + datadivid).html(targetCtrl.val());
    //alert(targetCtrl.val());
    if (targetCtrl.val() != '') { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
}
function LNfireSweetAlert() {
    //alert('Ok');
    Swal.fire({
        title: 'Driver Extra Punching',
        text: "Enter Late Night Punch For Driver",
        icon: 'success',
        html: `<div style="text-align:left;">
                            <div class="form-group">
                                <label class="swal-label"></label>
                                <input id="LNP" type="text" class="form-control timePicker"
                                                           onblur="ATIBlured()"
                                                           placeholder="Select Time">
                            </div>
                        </div>`,
        cancelButtonClass: 'btn-cancel',
        cancelButtonText: 'Cancel',
        confirmButtonText: 'Submit',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(callback);
    function callback(result) {
        if (result.value) {
            var punchtime = $('#LNP').val();
        }
    }
}
function VisiblePersonRow(personid) {
    $('#TPDtlTable tbody tr').each(function () {
        $(this).addClass('inVisible');
    });
    $('.M_' + personid).each(function () {
        $(this).removeClass('inVisible');
    });
    $('#TPTable tbody tr').each(function () {
        $(this).removeClass('selected-row');
    });
    $('#tr_' + personid).each(function () {
        $(this).addClass('selected-row');
    });
};
function TPSelected() {
    var targetCtrl = $(TPSelected.caller.arguments[0].target);
    VisiblePersonRow(targetCtrl.attr('id'));
};
function ATIBlured(mvalue)
{
    var targetCtrl = $(ATIBlured.caller.arguments[0].target);
    var timerid = targetCtrl.attr('id');
    var datadivid = 'V' + timerid;
    $('#' + datadivid).html(targetCtrl.val());
    //LNfireSweetAlert();
    //alert(mvalue);
}
function ATIBluredEM() {
    var targetCtrl = $(ATIBluredEM.caller.arguments[0].target);
    var timerid = targetCtrl.attr('id');
    var datadivid = 'V' + timerid;
    $('#' + datadivid).html(targetCtrl.val());
    //alert(targetCtrl.val());
};
function ATIBluredLN() {
    var targetCtrl = $(ATIBluredLN.caller.arguments[0].target);
    var timerid = targetCtrl.attr('id');
    var datadivid = 'V' + timerid;
    $('#' + datadivid).html(targetCtrl.val());
};
$(document).ready(function () {
    var defPerson = $('#DefaultPersonID').val();
    $('#' + defPerson).attr('checked', true);
    VisiblePersonRow(defPerson);
    RFIDInChanged();
    RFIDOutChanged();
});
$(document).ready(function () {
    $('#btnSubmit').click(function () {
        var notenumber = $('#NoteNumber').val();
        var tblPersonRecords = '';
        var tblDateWiseRecords = '';
        tblPersonRecords = getRecordsFromTableV2('TPTable');
        tblDateWiseRecords = getRecordsFromTableV2('TPDtlTable');
        var x = '{"NoteNumber":"' + notenumber
            + '","DateWiseDetails":' + tblDateWiseRecords
            + ',"TPersons":'
            + tblPersonRecords + '}';
        //alert(tblDateWiseRecords);
        $.ajax({
            method: 'POST',
            url: '/EntryII/SaveLWOutIn',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: x,
            success: function (data) {
                $(data).each(function (index, item) {
                    if (item.bResponseBool == true) {
                        Swal.fire({
                            title: 'Confirmation',
                            text: 'Data Saved Successfully.',
                            icon: 'success',
                            customClass: 'swal-wide',
                            buttons: {
                                confirm: 'Ok'
                            },
                            confirmButtonColor: '#2527a2',
                        }).then(callback);
                        function callback(result) {
                            if (result.value) {
                                var url = "/Security/EntryII/LWCreate";
                                window.location.href = url;
                            }
                        }
                    } else {
                        Swal.fire({
                            title: 'Confirmation',
                            text: 'Failed To Save Date Wise Tour Details.',
                            icon: 'question',
                            customClass: 'swal-wide',
                            buttons: {
                                confirm: 'Ok'
                            },
                            confirmButtonColor: '#2527a2',
                        });
                    }
                });
            },
        });
    });
});