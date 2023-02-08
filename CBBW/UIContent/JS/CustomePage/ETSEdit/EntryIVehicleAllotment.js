function Option1Changed() {
    var OptCtrl = $('#Option1');
    var OptDiv = $('#Option1Div');
    if (OptCtrl.val() == 1) {
        OptCtrl.isValid(); OptDiv.isGreen();
    } else {
        OptCtrl.isInvalid(); OptDiv.isRed();
    }
    EnableSubmitBtn();
};
function ValidateControl() {
    var target = ValidateControl.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var isvalid = validatectrl(targetid, $(target).val());
    if (isvalid) {
        $(target).removeClass('is-invalid').addClass('is-valid');
        $('#IsBtn').val(1);
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }
    $('#VABackBtnActive').val(1);
    EnableSubmitBtn();
};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "VADetails_MaterialStatus":
            if (value >= 0) { isvalid = true; }
            break;
        case "VADetails_DriverNumber":
            if (value >= 0) {
                var x = $('#VADetails_DriverNumber option:selected').text();
                $('#VADetails_DriverName').val(x);
                isvalid = true;
            }
            //alert(value+' - '+isvalid);
            break;
        case "VADetails_OtherVehicleModelName":
            if (value.length > 0) { isvalid = true; }
            break;
        case "VADetails_OtherVehicleNumber":
            if (IsAlphaNumeric(value)) {
                if (value.length == 10) {
                    isvalid = true;
                    var modelnameCtrl = $('#VADetails_OtherVehicleModelName');
                    modelnameCtrl.val('NA');
                    modelnameCtrl.clearValidateClass()
                    //modelnameCtrl.makeDisable();
                    $('#VADetails_OtherVehicleNumber').isValid();
                    $('#VADetails_VehicleNumber').clearValidateClass();
                    //alert(value);
                }
            }
            break;
    }
    return isvalid;
};
function VehicleNoChanged(value) {
    var targetCtrl = $('#VADetails_VehicleNumber');
    var Modelnamectrl = $('#ModelName');
    var Modelnamectrl2 = $('#VADetails_ModelName');
    var vno = targetCtrl.val();
    if (vno.length > 4) {
        var dataSourceURL = '/EHG/GetVehicleBasicInfo?VehicleNumber=' + vno;
        $.ajax({
            url: dataSourceURL,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    Modelnamectrl.val(item.ModelName);
                    Modelnamectrl2.val(item.ModelName);
                });
            }
        });
        targetCtrl.isValid();
    } else { targetCtrl.isInvalid(); }
    $('#IsBtn').val(value);
    EnableSubmitBtn();
};
function VehicleBelongsToChanged(mVal) {
    var vbtoCtrl = $('#VADetails_VehicleBelongsTo');
    var ForCVCtrl = $('#for_company_vehicle');
    var ForOVCtrl = $('#for_other_vehicle');
    var CVModelCtrl = $('#for_company_vehicle_model_name');
    var OVModelCtrl = $('#for_other_vehicle_model_name');
    var vnCtrl = $('#VADetails_VehicleNumber');
    var vnoCtrl = $('#VADetails_OtherVehicleNumber');
    var modelNameCtrl = $('#VADetails_OtherVehicleModelName');
    var drivernameCtrl = $('#mDriverName');
    if (drivernameCtrl.val().length > 2) {
        $('#VADetails_DriverName').val(drivernameCtrl.val());
        drivernameCtrl.isValid();
    }
    else { drivernameCtrl.isInvalid();}
    var vehicleBelongsTo = vbtoCtrl.val() * 1;
    //alert(drivernameCtrl.val());
    if (vehicleBelongsTo == 1) {
        ForCVCtrl.removeClass('inVisible');
        vnCtrl.isInvalid();
        //vnCtrl.val('');
        vnoCtrl.clearValidateClass();
        modelNameCtrl.clearValidateClass();
        CVModelCtrl.removeClass('inVisible');
        vbtoCtrl.isValid();
        vnoCtrl.val('');
        ForOVCtrl.addClass('inVisible');
        OVModelCtrl.addClass('inVisible');
    }
    else if (vehicleBelongsTo == 2) {
        ForOVCtrl.removeClass('inVisible');
        OVModelCtrl.removeClass('inVisible');
        vnCtrl.clearValidateClass();
        vnoCtrl.isInvalid();
        modelNameCtrl.isInvalid();
        //vnoCtrl.val('');        
        vbtoCtrl.isValid();
        ForCVCtrl.addClass('inVisible');
        CVModelCtrl.addClass('inVisible');
        validatectrl('VADetails_OtherVehicleNumber', vnoCtrl.val());
    }
    else {
        vbtoCtrl.isInvalid();
        vnCtrl.clearValidateClass();
        vnoCtrl.clearValidateClass();
        ForCVCtrl.addClass('inVisible');
        ForOVCtrl.addClass('inVisible');
        CVModelCtrl.addClass('inVisible');
        OVModelCtrl.addClass('inVisible');
    }
    if (mVal == '1') {
        VehicleNoChanged(0);
    } else {
        validatectrl('VADetails_OtherVehicleNumber', vnoCtrl.val());
    }
    EnableSubmitBtn();
};
function EnableSubmitBtn() {
    var x = getDivInvalidCount('HdrDiv') + getDivInvalidCount('Option1Div');
    var SubmitBtn = $('#btnSubmit');
    if (x <= 0 && $('#IsBtn').val() == 1) {
        SubmitBtn.makeEnabled();
    } else { SubmitBtn.makeDisable(); }
};
$(document).ready(function () {
    var vbtoCtrl = $('#VADetails_VehicleBelongsTo');
    vbtoCtrl.change(function () {
        VehicleBelongsToChanged(0);
        $('#VABackBtnActive').val(1);
        EnableSubmitBtn();
    });    
    //Checking Initial Data
    var matStatusCtrl = $('#VADetails_MaterialStatus');
    var driverCtrl = $('#mDriverName');
    if (matStatusCtrl.val() >= 0) { matStatusCtrl.isValid(); } else { matStatusCtrl.isInvalid(); }
    if (driverCtrl.val().length >= 0) { driverCtrl.isValid(); } else { driverCtrl.isInvalid(); }
    if (vbtoCtrl.val() > 0) {
        vbtoCtrl.isValid(); VehicleBelongsToChanged(1);
        if (vbtoCtrl.val() == 1) {
            $('#VADetails_OtherVehicleNumber').clearValidateClass();
        } else { $('#VADetails_VehicleNumber').clearValidateClass();}
    } else { vbtoCtrl.isInvalid(); }

    $('#IsBtn').val(0);
});
$(document).ready(function () {
    $('#btnClear').click(function () {
        $('#VADetails_MaterialStatus').val(-1).isInvalid();
        $('#VADetails_VehicleBelongsTo').val(0).isInvalid();
        VehicleBelongsToChanged(0);
        //$('#mDriverName').val('').isInvalid();
    });
    $('#btnBack').click(function () {
        var backbtnactive = $('#VABackBtnActive').val();
        //alert(backbtnactive);
        var backurl = "/Security/EntryI/Create";
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
});