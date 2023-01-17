function ValidateControlCtrl() {
    var target = ValidateControlCtrl.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var isvalid = validatectrl(targetid, $(target).val());
    if (isvalid) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }

    EnableSubmitBtnActive();

};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "VehicleAlloc":
            isvalid = validatectrl_YesNoCombo(value);
            break;
        case "EligibleVeh":
            isvalid = validatectrl_YesNoCombo(value);
            break;
        case "ReasonVehicleProvided":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "VehicleTypeProvided":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "ReasonVehicleProvided":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "EmployeeNonName":
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
function EnableSubmitBtnActive() {
    var x = getDivInvalidCount('TravDetails');
    var y = getDivInvalidCount('dateDetails');
    var DWTBtn = $('#btnSubmit');
    //alert(x); alert(y);
    if ((x + y) * 1 > 0) {
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

function SaveDataTravClicked() {
    var VehicleTypeProvided = $('#VehicleTypeProvided').val();
    var ReasonVehicleProvided = $('#ReasonVehicleProvided').val();
    var NoteNumber = $('#NoteNumber').val();
    var EmployeeNonName = $('#EmployeeNonName').val();

    var x = '{"VehicleTypeProvided":"' + VehicleTypeProvided + '","NoteNumber":"' + NoteNumber + '","ReasonVehicleProvided":"' + ReasonVehicleProvided + '","EmployeeNonName":"' + EmployeeNonName + '"}';
    $.ajax({
        method: 'POST',
        url: '/ETS/SetApprovalTravDetails',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
               
                if (item.bResponseBool == true) {

                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Travelling Approval Details Update Successfully.',
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
                            var url = "/Security/ETS/ETSApproveNote?NoteNumber=" + NoteNumber;
                            window.location.href = url;
                        }
                    }
                }
                else {
                    Swal.fire({
                        title: 'Error',
                        text: 'Failed To Update Traveling Details.',
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