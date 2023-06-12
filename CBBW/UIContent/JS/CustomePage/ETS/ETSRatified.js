$(document).ready(function () {
    $('#NoteNumber').change(function () {
        Notenumberchanged($(this).val());
    });

    Notenumberchanged($('#NoteNumber').val());
    var btnDisplays = $("#btnDisplay").val();
    var OtherPdiv = $('#OtherPdiv');
    var DivRati = $('#DivRati');
    if (btnDisplays == 1) {
        $('#NoteNumber').makeDisable();
        UnLockSection(OtherPdiv.attr('id'));
        LockSection(DivRati.attr('id'));
    } else {
        $('#NoteNumber').makeEnabled();
        LockSection(OtherPdiv.attr('id'));
        LockSection(DivRati.attr('id'));
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
                var approv = item.etsHeader.IsApproved == true ? "Yes" : "No"
                if (notenumber == null || notenumber == "") {
                    $('#IsApproved').val('-');
                    $('#ApprovedDateTime').val('-');
                    $('#ApprovedReason').val('-');
                } else {
                    $('#IsApproved').val(approv);
                    $('#ApprovedDateTime').val(item.etsHeader.ApproveDatestr + " " + item.etsHeader.ApproveTime);
                    $('#ApprovedReason').val(item.etsHeader.ApprovedReason);
                }
                if (item.TourCatstatus) {
                    $('#OtherPlace').val('Yes')
                } else {
                    $('#OtherPlace').val('-')
                }
                DisplayTPDetails(item.PersonDtls);

                if ($('#CenterCodeName').val() != "") { $('#btnTravDetails').makeEnabled(); }

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
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }
    if (targetid == 'IsRatified') {
        if ($(target).val() == 1) {
            $('#RatifiedReason').makeDisable();
            $('#RatifiedReason').val('');
            $('#RatifiedReason').removeClass('is-invalid').removeClass('is-valid');
        } else {
            $('#RatifiedReason').makeEnabled()
            $('#RatifiedReason').isInvalid();
        }
    }
    EnableSubmitBtn();

};
function validatectrl(targetid, value) {
    var isvalid = false;
    var DivRati = $('#DivRati');
    switch (targetid) {
        case "OtherP":
            isvalid = validatectrl_YesNoCombo(value);
            if (isvalid) {
                $('.content').removeClass('border-red').addClass('border-green');
                UnLockSection(DivRati.attr('id'));

            }
            else {
                $('.content').removeClass('border-green').addClass('border-red');
                LockSection(DivRati.attr('id'));
                $('#IsRatified').val('').isInvalid();
            }
            break;
        case "IsRatified":
            isvalid = validatectrl_YesNoComboApproval(value);
            break;
        case "RatifiedReason":
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
    var btnDisplay = $('#btnDisplay').val();

    // alert(x); alert(y);
    if ((x + y) * 1 > 0 || btnDisplay == 0) {
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
    var IsRatified = $('#IsRatified').val();
    var RatifiedReason = $('#RatifiedReason').val();
    var x = '{"NoteNumber":"' + NoteNumber + '","IsRatified":"' + IsRatified + '","RatifiedReason":"' + RatifiedReason + '"}';
    $.ajax({
        method: 'POST',
        url: '/ETS/RTFNCreate',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {

                if (item.bResponseBool == true) {

                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Ratification Process Saved Successfully.',
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
                            var url = "/Security/ETS/RTFNIndex";
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