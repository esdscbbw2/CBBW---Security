﻿function VisiblePersonRow(personid) {
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
function MakeButtonsEnable(BtnID) {
    var TPDBtn = $('#TPDBtn');
    var TDBtn = $('#TDBtn');
    var VADBtn = $('#VADBtn');
    var InnerPageBtn = $('#InnerPageBtn');
    var EnableBtn = $('#' + BtnID);
    if (BtnID == 'ALL') {
        TPDBtn.makeEnabled();
        TDBtn.makeEnabled();
        VADBtn.makeEnabled();
        InnerPageBtn.makeEnabled();
    }
    else if (BtnID == 'NONE') {
        TPDBtn.makeDisable();
        TDBtn.makeDisable();
        VADBtn.makeDisable();
        InnerPageBtn.makeDisable();
    }
    else {
        EnableBtn.makeEnabled();
    }
};
function NotenumberChanged(mVal) {    
    var notenumberCtrl = $('#NoteNumber');
    var notenumber = notenumberCtrl.val();
    var notetype = notenumber.substring(7, 10);
    $('#lblNoteDesc').html(GetNoteDescription(notetype));
    if (notenumber != '') {
        notenumberCtrl.isValid();
        MakeButtonsEnable('ALL');
    } else { notenumberCtrl.isInvalid(); MakeButtonsEnable('NONE'); }
    $.ajax({
        url: '/EntryII/GetNoteInfo',
        method: 'GET',
        data: { NoteNumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                $('#CentreCodenName').val(item.CenterName);
                $('#EPTourDesc').val(item.EPTourText);
                $('#AppStat').val(item.IsApprovedDisplay);
                $('#AppDateTime').val(item.AppDateTimeDisplay);
                $('#AppReason').val(item.NotAppReason);
                $('#ratStat').val(item.IsRatifiedDisplay);
                $('#RatDT').val(item.RetDateTimeDisplay);
                $('#ratReason').val(item.RetReason);                
            });
        }
    });
    $('#IsBackButtonActive').val(1);
    //EnableSubmitBtn();
    //$('#VASubmitBtnActive').val();
    //$('#IsVABtnEnabled').val();
    //if (mVal == 1) { $('#IsVABtnEnabled').val(0); $('#VABtn').makeDisable(); }
};
function VATBtnClicked() {
    var notenumber = $('#NoteNumber').val();
    if (notenumber != '') {
        var iDiv = $('#VAModalDiv');
        var modalDiv = $('#VAModal');
        var dataSourceURL = '/EntryII/VAView?NoteNumber=' + notenumber;
        $.ajax({
            url: dataSourceURL,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                iDiv.html(result);
                modalDiv.modal('show');
            },
            error: function (xhr, status) {

            }
        })
    }
    else {
        Swal.fire({
            title: 'Error',
            text: 'Select A Reference Note To View Vehicle Allotment Details.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }
};
function TDBtnClicked() {
    var notenumber = $('#NoteNumber').val();
    if (notenumber != '') {
        var iDiv = $('#TDModalDiv');
        var modalDiv = $('#TDModal');
        var dataSourceURL = '/EntryII/TDView?NoteNumber=' + notenumber;
        $.ajax({
            url: dataSourceURL,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                iDiv.html(result);
                modalDiv.modal('show');
            },
            error: function (xhr, status) {

            }
        })
    }
    else {
        Swal.fire({
            title: 'Error',
            text: 'Select A Reference Note To View Travelling Details.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }
};
function TPDBtnClicked() {
    var notenumber = $('#NoteNumber').val();
    if (notenumber!='') {
        var iDiv = $('#TPDModalDiv');
        var modalDiv = $('#TPDModal');
        var dataSourceURL = '/EntryI/TPView?NoteNumber=' + notenumber;
        $.ajax({
            url: dataSourceURL,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                iDiv.html(result);
                modalDiv.modal('show');
            },
            error: function (xhr, status) {

            }
        })
    }
    else {
        Swal.fire({
            title: 'Error',
            text: 'Select A Reference Note To View Travelling Person Details.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }
};
$(document).ready(function () {
    $('#btnBack').click(function () {
        var backbtnactive = $('#IsBackButtonActive').val();
        var backurl = "/Security/EntryII/Index";
        if (backbtnactive == 1) {
            Swal.fire({
                title: 'Confirmation',
                text: "Are You Sure Want to Go Back?",
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
                    window.location.href = backurl;
                }
            }
        }
        else {
            window.location.href = backurl;
        }
    });
    NotenumberChanged(0);

});
$(document).ready(function () {
    var defPerson = $('#DefaultPersonID').val();
    $('#' + defPerson).attr('checked', true);
    VisiblePersonRow(defPerson);
});

function RFIDChanged() {
    alert($('#ORFID').val());
};