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
    }
};
$(document).ready(function () {
    var vbtoCtrl = $('#VADetails_VehicleBelongsTo');
    var ForCVCtrl = $('#for_company_vehicle');
    var ForOVCtrl = $('#for_other_vehicle');
    var CVModelCtrl = $('#for_company_vehicle_model_name');
    var OVModelCtrl = $('#for_other_vehicle_model_name');
    var vnCtrl = $('#VADetails_VehicleNumber');    
    vbtoCtrl.change(function () {
        var that = $(this);
        var vehicleBelongsTo = that.val() * 1;
        if (vehicleBelongsTo == 1) {
            ForCVCtrl.removeClass('inVisible');
            vnCtrl.val('').isInvalid();
            ForOVCtrl.addClass('inVisible');
            CVModelCtrl.removeClass('inVisible');
            OVModelCtrl.addClass('inVisible');
            that.isValid();
        }
        else if (vehicleBelongsTo == 2) {
            ForCVCtrl.addClass('inVisible');
            vnCtrl.clearValidateClass();
            ForOVCtrl.removeClass('inVisible');
            CVModelCtrl.addClass('inVisible');
            OVModelCtrl.removeClass('inVisible');
            that.isValid();
        }
        else {
            ForCVCtrl.addClass('inVisible');
            vnCtrl.clearValidateClass();
            ForOVCtrl.addClass('inVisible');
            CVModelCtrl.addClass('inVisible');
            OVModelCtrl.addClass('inVisible');
            that.isInvalid();
        }
        
    });
    
});