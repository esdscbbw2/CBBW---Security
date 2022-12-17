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
            if (value.length > 4) { isvalid = true; }
            break;
        case "VADetails_OtherVehicleNumber":
            if (value.length > 4) { isvalid = true; }
            break;
    }
    return isvalid;
};
function VehicleNoChanged() {
    var target = VehicleNoChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
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
};
$(document).ready(function () {
    var vbtoCtrl = $('#VADetails_VehicleBelongsTo');
    var ForCVCtrl = $('#for_company_vehicle');
    var ForOVCtrl = $('#for_other_vehicle');
    var CVModelCtrl = $('#for_company_vehicle_model_name');
    var OVModelCtrl = $('#for_other_vehicle_model_name');
    var vnCtrl = $('#VADetails_VehicleNumber');
    var vnoCtrl = $('#VADetails_OtherVehicleNumber');
    vbtoCtrl.change(function () {
        var that = $(this);
        var vehicleBelongsTo = that.val() * 1;
        if (vehicleBelongsTo == 1) {
            ForCVCtrl.removeClass('inVisible');
            vnCtrl.val('').isInvalid();
            vnoCtrl.clearValidateClass();
            ForOVCtrl.addClass('inVisible');
            CVModelCtrl.removeClass('inVisible');
            OVModelCtrl.addClass('inVisible');
            that.isValid();
        }
        else if (vehicleBelongsTo == 2) {
            ForCVCtrl.addClass('inVisible');
            vnCtrl.clearValidateClass();
            ForOVCtrl.removeClass('inVisible');
            vnoCtrl.val('').isInvalid();
            CVModelCtrl.addClass('inVisible');
            OVModelCtrl.removeClass('inVisible');
            that.isValid();
        }
        else {
            ForCVCtrl.addClass('inVisible');
            vnCtrl.clearValidateClass();
            vnoCtrl.clearValidateClass();
            ForOVCtrl.addClass('inVisible');
            CVModelCtrl.addClass('inVisible');
            OVModelCtrl.addClass('inVisible');
            that.isInvalid();
        }
        $('#VABackBtnActive').val(1);
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
});