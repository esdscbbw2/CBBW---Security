﻿function RFIDInChanged() {
    var targetCtrl = $('#RFIDCardIn');
    var divCtrl = $('#VTourInTimeCtrl');
    var timCtrl = $('#TourInTimeCtrl');
    var RFIDDate = $('#SchToDate').val();
    var RFIDNo = targetCtrl.val();
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
        $('#RFIDInDiv').html(RFIDNo);
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
        $('#RFIDInDiv').html('NA');
    }
};
function RFIDOutChanged() {
    var targetCtrl = $('#RFIDCardOut');
    var divCtrl = $('#VTourOutTimeCtrl');
    var timCtrl = $('#TourOutTimeCtrl');
    var RFIDDate = $('#SchFromDate').val();
    var RFIDNo = targetCtrl.val();
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
        $('#RFIDOutDiv').html(RFIDNo);
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
        $('#RFIDOutDiv').html('NA');
    }
};
function TimeInCtrlBlured() {
    var targetCtrl = $(TimeInCtrlBlured.caller.arguments[0].target);
    var timerid = targetCtrl.attr('id');
    var datadivid = 'V' + timerid;
    $('#' + datadivid).html(targetCtrl.val());
    //alert(targetCtrl.val());
    if (targetCtrl.val() != '') { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
}
function VisiblePersonRow(personid) {
    $('#TPDtlTable tbody tr').each(function () {
        $(this).addClass('inVisible');
    });
    $('#M_' + personid).each(function () {
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
function GetOutDCDetails(vehicleno, fromdate, todate) {
    var iDiv = $('#RefDCOutDiv');
    var dataSourceURL = '/EntryII/GetRefDCPartialOut?VehicleNo=' + vehicleno + '&FromDate=' + fromdate + '&ToDate=' + todate ;
    $.ajax({
        url: dataSourceURL,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            iDiv.html(result);
            //GetInDCDetails(vehicleno, fromdate, todate);
        },
        error: function (xhr, status) {

        }
    })
};
function GetInDCDetails(vehicleno, fromdate, todate) {
    var iDiv = $('#RefDCInDiv');
    var dataSourceURL = '/EntryII/GetRefDCPartialIn?VehicleNo=' + vehicleno + '&FromDate=' + fromdate + '&ToDate=' + todate ;
    $.ajax({
        url: dataSourceURL,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            iDiv.html(result);
        },
        error: function (xhr, status) {

        }
    })
};
$(document).ready(function () {
    var vehicleno = $('#mVehicleNo').html();
    var fromdate = $('#mFromDate').html();
    var todate = $('#mToDate').html();
    GetOutDCDetails(vehicleno, fromdate, todate);
    GetInDCDetails(vehicleno, fromdate, todate);
    //alert(vehicleno + ' ' + fromdate + ' ' + todate);
});
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
        var tblVDtls = '';
        tblPersonRecords = getRecordsFromTableV2('TPTable');
        tblDateWiseRecords = getRecordsFromTableV2('TPDtlTable');
        tblVDtls = getRecordsFromTableV2('VDTable');
        var x = '{"NoteNumber":"' + notenumber
            + '","DateWiseDetails":' + tblDateWiseRecords
            + ',"VDetails":' + tblVDtls + ',"TPersons":'
            + tblPersonRecords + '}';
        //alert(tblDateWiseRecords);
        $.ajax({
            method: 'POST',
            url: '/EntryII/SaveMLOutIn',
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
                                var url = "/Security/EntryII/MLCreate";
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