function ValidateControl() {
    var target = ValidateControl.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var isvalid = validatectrl(targetid, $(target).val());
    if (isvalid) {
        $(target).removeClass('is-invalid').addClass('is-valid');
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
            break;
        case "VADetails_OtherVehicleModelName":
            if (value.length > 0) { isvalid = true; }
            break;
        case "VADetails_OtherVehicleNumber":
            if (IsAlphaNumeric(value)) {
                if (value.length == 10) {
                    isvalid = true;
                    var modelnameCtrl = $('#VADetails_OtherVehicleModelName');
                    modelnameCtrl.val('NA')
                    //modelnameCtrl.clearValidateClass()
                    //modelnameCtrl.makeDisable();
                }
            }            
            break;
    }
    return isvalid;
};

function VehicleNoChanged() {
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
                    /*multiselectCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));*/
                    Modelnamectrl.val(item.ModelName);
                    Modelnamectrl2.val(item.ModelName);
                });
            }
        });
        targetCtrl.isValid();
    } else { targetCtrl.isInvalid(); }
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
    var vehicleBelongsTo = vbtoCtrl.val() * 1;
    if (vehicleBelongsTo == 1) {
        ForCVCtrl.removeClass('inVisible');
        vnCtrl.isInvalid();
        //vnCtrl.val('');
        vnoCtrl.clearValidateClass();
        modelNameCtrl.clearValidateClass();
        ForOVCtrl.addClass('inVisible');
        CVModelCtrl.removeClass('inVisible');
        OVModelCtrl.addClass('inVisible');
        vbtoCtrl.isValid();
    }
    else if (vehicleBelongsTo == 2) {
        ForCVCtrl.addClass('inVisible');
        vnCtrl.clearValidateClass();
        ForOVCtrl.removeClass('inVisible');
        vnoCtrl.isInvalid();
        modelNameCtrl.isInvalid();
        //vnoCtrl.val('')
        CVModelCtrl.addClass('inVisible');
        OVModelCtrl.removeClass('inVisible');
        vbtoCtrl.isValid();
    }
    else {
        ForCVCtrl.addClass('inVisible');
        vnCtrl.clearValidateClass();
        vnoCtrl.clearValidateClass();
        ForOVCtrl.addClass('inVisible');
        CVModelCtrl.addClass('inVisible');
        OVModelCtrl.addClass('inVisible');
        vbtoCtrl.isInvalid();
    }    
    if (mVal == '1') {
        VehicleNoChanged();
    } else { validatectrl('VADetails_OtherVehicleNumber', vnoCtrl.val()); }
    //EnableSubmitBtn();
};
function EnableSubmitBtn() {
    var x = getDivInvalidCount('HdrDiv');
    var SubmitBtn = $('#btnSubmit');    
    if (x<=0) { SubmitBtn.makeEnabled(); } else { SubmitBtn.makeDisable(); }
};
$(document).ready(function () {
    var vbtoCtrl = $('#VADetails_VehicleBelongsTo');    
    vbtoCtrl.change(function () {
        VehicleBelongsToChanged(0);        
        $('#VABackBtnActive').val(1);
        EnableSubmitBtn();
    });
    $('#btnBack').click(function () {
        var backbtnactive = $('#VABackBtnActive').val();
        var backurl = "/Security/EHG/Create";
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
    //Checking Initial Data
    var matStatusCtrl = $('#VADetails_MaterialStatus');
    var driverCtrl = $('#VADetails_DriverNumber');
    if (matStatusCtrl.val() >= 0) { matStatusCtrl.isValid(); } else { matStatusCtrl.isInvalid(); }
    if (driverCtrl.val().length > 0) { driverCtrl.isValid(); } else { driverCtrl.isInvalid(); }
    if (vbtoCtrl.val() > 0) { vbtoCtrl.isValid(); VehicleBelongsToChanged(1);} else { vbtoCtrl.isInvalid(); }
});
