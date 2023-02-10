$(document).ready(function () {
    $('#NoteNumber').change(function () {
        Notenumberchanged($(this).val());
    });

    Notenumberchanged($('#NoteNumber').val());

    var btnDisplays = $("#btnDisplay").val();
    if (btnDisplays == 1) {
        $('#NoteNumber').makeDisable();
    } else {
        $('#NoteNumber').makeEnabled();
    }
    

});

function Notenumberchanged(notenumber) {
    var noteCtrl = $('#NoteNumber');
    if (notenumber != '') { noteCtrl.isValid(); } else { noteCtrl.isInvalid(); }
    TPTableClear();
    if (noteCtrl.val() != "") {
        $.ajax({
            url: '/EMC/GetEMCHdrDetails',
            method: 'GET',
            data: { Notenumber: notenumber },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    $('#CenterCodeName').val(item.emnHeader.CenterCodeName);
                    $('#AttachFile').val(item.emnHeader.AttachFile);
                    if (item.emnHeader.IsEPTour == true) {
                        $('#IsEPTour').val('Yes');

                    } else if (item.emnHeader.IsEPTour == false) {
                        $('#IsEPTour').val('No');
                    } else {
                        $('#IsEPTour').val('-');
                    }
                    $('#btnTravDetails').makeEnabled();

                });
                getTravellingPersonData();
            }
        });
    } else {
        $('#btnTravDetails').makeDisable();
    }
};
function TPTableClear() {
    $("#tbody2").empty();
    var DDPersonType = $('#DDPersonType');
    var EmployeeNo = $('#EmployeeNo');
    var DesgCodenName = $('#DesgCodenName');
    var EgblVehicleTypeName = $('#EgblVehicleTypeName');
    var TaDaDenied = $('#TaDaDenied');
    var EPNoteNumber = $('#EPNoteNumber');
    var NoteDate = $('#NoteDate');

    DDPersonType.html('');
    EmployeeNo.html('');
    DesgCodenName.html('');
    EgblVehicleTypeName.html('');
    TaDaDenied.html('');
    EPNoteNumber.html('');
    NoteDate.html('');
    $('#CenterCodeName').val('');
    $('#AttachFile').val('');
    $('#IsEPTour').val('');

};
$(document).ready(function () {
    $('#btnViewDoc').click(function () {
        var docfilename = $('#AttachFile').val();
        var filepath = "/Upload/Forms/" + docfilename;
        if (docfilename.length > 2) { window.open(filepath); }
        else {
            Swal.fire({
                title: 'Information',
                text: "No Document Found For This Note",
                icon: 'warning',
                customClass: 'swal-wide',
                buttons: {
                    //cancel: 'Cancel',
                    confirm: 'Ok'
                },
                //cancelButtonClass: 'btn-cancel',
                confirmButtonColor: '#2527a2',
            });
        }
    });
})
function ValidateControl() {
    var target = ValidateControl.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var isvalid = validatectrl(targetid, $(target).val());
    if (isvalid) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }

    if (targetid == 'IsApprove') {
        if ($(target).val() == 1) {
            $('#ApproveReason').makeDisable();
            $('#ApproveReason').val('');
            $('#ApproveReason').removeClass('is-invalid').removeClass('is-valid');
        } else {
            $('#ApproveReason').makeEnabled()
            $('#ApproveReason').isInvalid();
        }
    }
    EnableSubmitBtn();

};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "APPRej":
            isvalid = validatectrl_YesNoCombo(value);
            break;
        case "IsApprove":
            isvalid = validatectrl_YesNoComboApproval(value);
            break;
        case "ApproveReason":
            isvalid = validatectrl_ValidateLength(value);
            break;
       
    }

    return isvalid;
};
function validatectrl_ValidateLength(value) {
    if (value.length > 0) {
        return true;
    } else { return false; }
};
function validatectrl_YesNoCombo(value) {

    if (value * 1 > 0) {
        return true;
    } else { return false; }
};
function validatectrl_YesNoComboApproval(value) {

    if (value * 1 >= 0) {
        return true;
    } else { return false; }
};
function EnableSubmitBtn() {
    var x = getDivInvalidCount('TravDetails');
    var y = getDivInvalidCount('dateDetails');
    var DWTBtn = $('#btnSubmit');
    var btnDisplay= $('#btnDisplay').val();
    
    
    if ((x + y) * 1 > 0 || btnDisplay==0) {
        DWTBtn.makeDisable();
    }
    else {
        DWTBtn.makeEnabled();
    }
};
function Buttonclear() {
    $('.clear').val('');
    $('.clear').isInvalid();
}
function SaveDataClicked() {
    var NoteNumber = $('#NoteNumber').val();
    var IsApprove = $('#IsApprove').val();
    var ApproveReason = $('#ApproveReason').val();
    var x = '{"NoteNumber":"' + NoteNumber + '","IsApprove":"' + IsApprove + '","ApproveReason":"' + ApproveReason + '"}';
    $.ajax({
        method: 'POST',
        url: '/EMC/EMCApproveNote',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
               
                if (item.bResponseBool == true) {

                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Approval Process Saved Successfully.',
                        setTimeout: 5000,
                        icon: 'success',
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    }).then(callback);
                    function callback(result) {
                        if (result.value) {
                            var url = "/Security/EMC/EMCNoteApproveList";
                            window.location.href = url;
                        }
                    }
                }
                else {
                    Swal.fire({
                        title: 'Error',
                        text: 'Failed To Update Approval Process.',
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

};
async function getTravellingPersonData() {
   // TPTableClear();
    $('#btnTravDetails').makeDisable();
    var rowid = 0;
    var TaDa;
    var NoteNumber = $('#NoteNumber').val();
    var DDPersonType = $('#DDPersonType');
    var EmployeeNo = $('#EmployeeNo');
    var DesgCodenName = $('#DesgCodenName');
    var EgblVehicleTypeName = $('#EgblVehicleTypeName');
    var TaDaDenied = $('#TaDaDenied');
    var EPNoteNumber = $('#EPNoteNumber');
    var NoteDate = $('#NoteDate');

    $.ajax({
        url: '/EMC/GetTravellingPersonForEMC?NoteNumber=' + NoteNumber,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            var Datalenght = Object.keys(data).length;
            if (Datalenght > 0) {
                $(data).each(function (indexs, items) {
                    if (indexs > 0) {

                        rowid = CloneRowWithNoControlsReturningID('tbody1', 'tbody2', indexs - 1)
                        DDPersonType = $('#DDPersonType_' + rowid);
                        EmployeeNo = $('#EmployeeNo_' + rowid);
                        DesgCodenName = $('#DesgCodenName_' + rowid);
                        EgblVehicleTypeName = $('#EgblVehicleTypeName_' + rowid);
                        TaDaDenied = $('#TaDaDenied_' + rowid);
                        EPNoteNumber = $('#EPNoteNumber_' + rowid);
                        NoteDate = $('#NoteDate_' + rowid);

                    }
                    DDPersonType.html(items.PersonTypeName);
                    EmployeeNo.html(items.EmployeeNonName);
                    DesgCodenName.html(items.DesignationCodenName);
                    EgblVehicleTypeName.html(items.EligibleVehicleTypeName);
                    if ($.trim(items.EPNoteNumber) == "NA") { EPNoteNumber.html(items.EPNoteNumber); } else {
                        EPNoteNumber.html(items.EPNoteNumber);
                        NoteDate.html(items.NoteDatestr);
                    }
                    if (items.TADADenieds == true) { TaDa = 'Yes' } else { TaDa = 'No' }
                    TaDaDenied.html(TaDa);
                });
                $('#btnTravDetails').makeEnabled();
            }

        }
    });
};