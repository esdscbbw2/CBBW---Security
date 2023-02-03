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
function GetEmployeeList(notenumber) {
    (async function () {
        const r2 = await getDropDownDataWithSelectedValue('CenterCN', 'All Centers', '/Security/EMN/getCenterCodeListFromTravellingPerson?NoteNumber=' + notenumber, '-1');
    })();
}
function Notenumberchanged(notenumber) {
    var noteCtrl = $('#NoteNumber');
    if (notenumber != '') { noteCtrl.isValid(); } else { noteCtrl.isInvalid(); }
    TPTableClear();
    $.ajax({
        url: '/EMN/GetEMNHdrDetails',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                $('#CenterCodeName').val(item.emnHeader.CenterCodeName);
                $('#AttachFile').val(item.emnHeader.AttachFile);
               // if ($('#CenterCodeName').val() != "") { $('#btnTravDetails').makeEnabled();}
                getTravellingPersonData('-1');
            });
        }
    });
    GetEmployeeList(notenumber);
};
function TPTableClear() {
    $("#tbody2").empty();
    var DDPersonType = $('#DDPersonType');
    var EmployeeNo = $('#EmployeeNo');
    var DesgCodenName = $('#DesgCodenName');
    var EgblVehicleTypeName = $('#EgblVehicleTypeName');
    var TaDaDenied = $('#TaDaDenied');
    DDPersonType.html('');
    EmployeeNo.html('');
    DesgCodenName.html('');
    EgblVehicleTypeName.html('');
    TaDaDenied.html('');

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
        url: '/EMN/EMNApproveNote',
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
                            var url = "/Security/EMN/EMNNoteApproveList";
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
function CenterCNChanged() {
    var target = CenterCNChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
    var mValue = targetCtrl.val();
    getTravellingPersonData(mValue);
};
async function getTravellingPersonData(CenterCode) {
    TPTableClear();
    $('#btnTravDetails').makeDisable();
    var rowid = 0;
    var TaDa;
    var NoteNumber = $('#NoteNumber').val();
    var DDPersonType = $('#DDPersonType');
    var EmployeeNo = $('#EmployeeNo');
    var DesgCodenName = $('#DesgCodenName');
    var EgblVehicleTypeName = $('#EgblVehicleTypeName');
    var TaDaDenied = $('#TaDaDenied');

    $.ajax({
        url: '/EMN/GetTravellingPersonForEMN?NoteNumber=' + NoteNumber + '&CenterCode=' + CenterCode,
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

                    }
                    DDPersonType.html(items.PersonTypeName);
                    EmployeeNo.html(items.EmployeeNonName);
                    DesgCodenName.html(items.DesignationCodenName);
                    EgblVehicleTypeName.html(items.EligibleVehicleTypeName);

                    if (items.TADADenieds == true) { TaDa = 'Yes' } else { TaDa = 'No' }
                    TaDaDenied.html(TaDa);
                });
                $('#btnTravDetails').makeEnabled();
            }

        }
    });
};