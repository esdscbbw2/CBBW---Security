$(document).ready(function () {
    $('#NoteNumber').change(function () {
        Notenumberchanged($(this).val());
    });

    Notenumberchanged($('#NoteNumber').val());
    var btnDisplays = $("#btnDisplay").val();
    var dateDetails = $('#dateDetails');
    var TravDetails = $('#TravDetails');

    if (btnDisplays == 1) {
        $('#NoteNumber').makeDisable();
        UnLockSection(TravDetails.attr('id'));
        LockSection(dateDetails.attr('id'));
    } else {

        $('#NoteNumber').makeEnabled();
        LockSection(dateDetails.attr('id'));
        LockSection(TravDetails.attr('id'));
    }
});
function Notenumberchanged(notenumber) {
    var noteCtrl = $('#NoteNumber');
    if (notenumber != '') { noteCtrl.isValid(); } else { noteCtrl.isInvalid(); }
    $('#tbody2').empty();
    $.ajax({
        url: '/ETS/GetETSHdrDetails',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                $('#CenterCodeName').val(item.etsHeader.CenterCodeName);
                $('#AttachFile').val(item.etsHeader.AttachFile);
                $('#EntryDate').val(item.etsHeader.EntryDateDisplay);
                $('#EntryTime').val(item.etsHeader.EntryTime);


                if (item.TourCatstatus) {
                    $('#OtherPlace').val('Yes')
                } else {
                    $('#OtherPlace').val('-')
                }
               
                DisplayTPDetails(item.PersonDtls);
               
                if ($('#CenterCodeName').val() != "") { $('#btnTravDetails').makeEnabled();}
                
            });
        }
    });
};
function DisplayTPDetails(data) {
    $('#tbody2').empty();
    for (var item in data) {
        var rowdata = data[item];
        var PersonTypeName = $('#PersonTypeName');
        var EmployeeNonName = $('#EmployeeNonName');
        var DesignationCodenName = $('#DesignationCodenName');
        var EligibleVehicleTypeName = $('#EligibleVehicleTypeName');
        var TADADenieds = $('#TADADenieds');
        if (item > 0) {
            CloneRowWithNoControls('tbody1', 'tbody2', item);
            PersonTypeName = $('#PersonTypeName_' + item);
            EmployeeNonName = $('#EmployeeNonName_' + item);
            DesignationCodenName = $('#DesignationCodenName_' + item);
            EligibleVehicleTypeName = $('#EligibleVehicleTypeName_' + item);
            TADADenieds = $('#TADADenieds_' + item);  
        }
        PersonTypeName.html(rowdata.PersonTypeName);
        EmployeeNonName.html(rowdata.EmployeeNonName);
        DesignationCodenName.html(rowdata.DesignationCodenName);
        EligibleVehicleTypeName.html(rowdata.EligibleVehicleTypeName);
        if (rowdata.TADADenieds) { TADADenieds.html('Yes'); } else { TADADenieds.html('No'); }  
    }
}
$(document).ready(function () {
    $('#btnViewDoc').click(function () {
        var docfilename = $('#AttachFile').val();
        var filepath = "/Upload/Forms/" + docfilename;
        if (docfilename.length > 2) { OpenWindow(filepath); }
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
        $(target).isValidCtrl();
    } else {
        $(target).isInvalidCtrl();
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
    var dateDetails = $('#dateDetails');

    switch (targetid) {
        case "APPRej":
            isvalid = validatectrl_YesNoCombo(value);
           
            if (isvalid) {
                $('.content').removeClass('border-red').addClass('border-green');
                UnLockSection(dateDetails.attr('id'));

            }
            else {
                $('.content').removeClass('border-green').addClass('border-red');
                LockSection(dateDetails.attr('id'));
                dateDetails.val('').isInvalid();
            }
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
        url: '/ETS/ETSApproveNote',
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
                            var url = "/Security/ETS/ETSNoteApproveList";
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