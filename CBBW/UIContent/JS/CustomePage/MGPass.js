$(document).ready(function () {
    $('#NoteNo').change(function () {    
        
        Notenumberchanged($(this).val());
    });
    Notenumberchanged($('#NoteNo').val());
});
function Notenumberchanged(notenumber) {
    var that = $('#NoteNo');
    $.ajax({
        url: '/CTV/getDataFromNote',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                $('#CenterCode').val(item.CentreCodenName);
                $('#FortheMonthnYear').val(item.FortheMonthnYear);
                //$('#txtFromDate').val(item.FromDateStr);
                //$('#txtToDate').val(item.ToDateStr);
                 $('#VehicleNo').val(item.Vehicleno);
                 $('#VehicleType').val(item.VehicleType);
                 $('#ModelName').val(item.ModelName);
                //$('#txtDriverNonName').val(item.DriverNonName);
                //if (item.ApprovalFor == 0 || item.ApprovalFor == 2) { osbtn.removeAttr('disabled') }
                //else if (item.ApprovalFor == 1) { lsbtn.removeAttr('disabled') }
               // that.isValid();
                that.addClass('is-valid').removeClass('is-invalid');
                ActivateOutbtn(notenumber);

            });
        }
    });
};

function ActivateOutbtn(notenumber) {
   
    var btnOut = $('#btnCMOD');
    var btnIn = $('#btnCMID'); 
    $.ajax({
        url: '/MaterialGatePass/CheckAvailableNoteNoforOut',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
           
            if (data.length > 0) {
                $(data).each(function (index, item) {
                    //btnOut.removeAttr('disabled');
                    //btnIn.removeAttr('disabled');
                    //Comment for Testing out details only 
                    if (item.OutActive == false && item.InActive == false) {
                        btnOut.removeAttr('disabled');
                        btnIn.attr('disabled', 'disabled');
                    } else if (item.OutActive == true && item.InActive == false) {
                        btnIn.removeAttr('disabled');
                        btnOut.attr('disabled', 'disabled');       
                    } else if (item.OutActive == true && item.InActive == true) {
                       // btnOut.removeAttr('disabled');
                        btnOut.attr('disabled', 'disabled');
                        btnIn.attr('disabled', 'disabled');
                    }
                    else {
                        btnOut.removeAttr('disabled');
                        btnIn.attr('disabled', 'disabled');
                    }
                });
            } else {
                      btnOut.removeAttr('disabled');
                      btnIn.attr('disabled', 'disabled');
                 }

            }
        
    });
};
function btnClearClicked() {
    debugger;
    $('.canclear').each(function () {
        $(this).val('');
    });
    //$('#submitConfirmation').removeClass('is-valid').addClass('is-invalid');
    //$('#Vehicleno').removeClass('is-valid').addClass('is-invalid');
    //$('#divErrorLVS').addClass('inVisible');
    //$('#divErrorOTS').addClass('inVisible');
    //$('#divError').addClass('inVisible');
    //$('#btnLVT').attr('disabled', 'disabled');
    //$('#btnOVT').attr('disabled', 'disabled');
    //var noteno = $('#NoteNo').val();
    //$.ajax({
    //    url: '/CTV/RemoveNoteDetails',
    //    method: 'GET',
    //    data: { NoteNumber: noteno },
    //    dataType: 'json',
    //    success: function (data) {
    //        $(data).each(function (index, item) {

    //        });
    //    }
    //});
};

function activateSubmitBtn() {
    //alert($('.is-invalid').length);
    //isOtherPlaceButtonEnabled();
    
    var issubmit = $('#ISSubmitActive').val();
    
    var btnSubmit = $('#btnSubmit');
    if ($('.is-invalid').length > 0) {
        btnSubmit.attr('disabled', 'disabled');
    } else {
        if (issubmit == 1) {
            btnSubmit.removeAttr('disabled');
        } else {
            btnSubmit.attr('disabled', 'disabled');
        }
    }
};
function ValidateControl() {
    var target = ValidateControl.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var targetvalue = $(target).val();
    var isvalid = validatectrl(targetid, targetvalue);
   
    if (isvalid) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }

    activateSubmitBtn();
};

function validatectrl(targetid, mvalue) {
    var isvalid = false;
    if (targetid == 'vehicledriver' || targetid == 'driversame') {
        if (mvalue.length > 0) { isvalid = true; }
    }
    return isvalid;
};