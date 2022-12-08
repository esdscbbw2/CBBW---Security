$.fn.makeEnabled = function () {
    var that = this;
    that.removeAttr('disabled');
};
$.fn.makeDisable = function () {
    var that = this;
    that.attr('disabled', 'disabled');
};
$.fn.isInvalid = function () {
    var that = this;
    that.addClass('is-invalid').removeClass('is-valid');
};
$.fn.isValid = function () {
    var that = this;
    that.addClass('is-valid').removeClass('is-invalid');
};
function ValidateControl() {
    var target = ValidateControl.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var isvalid = validatectrl(targetid, $(target).val());
    if (isvalid) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }
};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "ehgHeader_MaterialStatus":
            if (value >=0) { isvalid = true; }
            break;
        case "ehgHeader_Instructor":
            if (value >0) { isvalid = true; }
            break;

    }
    return isvalid;
};
$(document).ready(function () {
    var VehicletypeCtrl = $('#ehgHeader_VehicleType');
    var POACtrl = $('#for_LV');
    var POA2WhCtrl = $('#for_2_wheeler');
    var ForManagementDiv = $('#for_Management');
    var ForOfficeWorkDiv = $('#for_OfficeWork');
    var POADropdown = $('#ehgHeader_PurposeOfAllotment');
    
    VehicletypeCtrl.change(function () {
        var selectedvt = $(this).val();        
        if (selectedvt == 1) {
            POACtrl.removeClass('inVisible');
            POA2WhCtrl.addClass('inVisible');
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDiv.addClass('inVisible');
        }
        else if (selectedvt == 2) {
            POACtrl.addClass('inVisible');
            POA2WhCtrl.removeClass('inVisible');
            ForOfficeWorkDiv.removeClass('inVisible');
            ForManagementDiv.addClass('inVisible');
        } else {
            POACtrl.addClass('inVisible');
            POA2WhCtrl.addClass('inVisible');
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDiv.addClass('inVisible');
        };
        if (selectedvt > 0) {
            VehicletypeCtrl.isValid();
            POADropdown.val('').isInvalid();
        } else { VehicletypeCtrl.isInvalid() }
    });
    POADropdown.change(function () {
        var selectedvt = $(this).val();
        if (selectedvt == 1) {
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDiv.removeClass('inVisible');
        }
        else if (selectedvt == 2) {
            ForOfficeWorkDiv.removeClass('inVisible');
            ForManagementDiv.addClass('inVisible');
        } else {
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDiv.addClass('inVisible');
        };
        if (selectedvt > 0) { POADropdown.isValid(); } else { POADropdown.isInvalid() }
    });
});
