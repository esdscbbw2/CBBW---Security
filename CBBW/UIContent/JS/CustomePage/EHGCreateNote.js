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
$.fn.clearValidateClass = function () {
    var that = this;
    that.removeClass('is-valid').removeClass('is-invalid');
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
        case "FromDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "FromTime":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "ToDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "PurposeOfVisit":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "TaDaDenied":
            isvalid = validatectrl_YesNoCombo(value);
            break;
        case "AuthorisedEmpNoForManagement":
            if (value > 0) { isvalid = true; }
            break;
        case "DriverNoForManagement":
            if (value > 0) { isvalid = true; }
            break;
    }
    return isvalid;
};
function validatectrl_ValidateLength(value) {
    if (value.length > 0) {
        return true;
    } else { return false;}
}
function validatectrl_YesNoCombo(value) {
    if (value*1 >= 0) {
        return true;
    } else { return false; }
}
function getDesgnCode(rowid,empCode) {
    var desgCtrl = $('#DesgCodenName');
    var persontypeCtrl = $('#DDPersonType');
    if (rowid != 0) {
        desgCtrl = $('#DesgCodenName_' + rowid);
        persontypeCtrl = $('#DDPersonType_' + rowid);
    }    
    var mValue = persontypeCtrl.val();
    if (mValue == 1 || mValue == 2) {
        var mUrl = "/EHG/GetDesgCodenName?empID=" + empCode + "&empType=" + mValue;
        $.ajax({
            url: mUrl,
            success: function (result) { desgCtrl.html(result); }
        });
        desgCtrl.html('qwe');
    }
    else {
        desgCtrl.html('');
    }
};
function DDPersonTypeChanged() {
    var target = DDPersonTypeChanged.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    //var targetindexid = target.closest('.add-row');
    var targetid = targetCtrl.attr('id');
    var mValue = targetCtrl.val();
    var cmbCtrl = $('#cmb' + targetid);
    var txtCtrl = $('#txt' + targetid);    
    switch (mValue) {
        case '1':
            cmbCtrl.removeClass('inVisible').addClass('pickPersonName').isInvalid();
            txtCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
            getDropDownData('cmb' + targetid, 'Select Employee', '/EHG/GetStaffList');
            break;
        case '2':
            cmbCtrl.removeClass('inVisible').addClass('pickPersonName').isInvalid();
            txtCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
            getDropDownData('cmb' + targetid, 'Select Driver', '/EHG/GetDriverList');
            break;
        case '3':
            txtCtrl.removeClass('inVisible').addClass('pickPersonNametxt').isInvalid();
            cmbCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
            
            break;
        case '4':
            txtCtrl.removeClass('inVisible').addClass('pickPersonNametxt').isInvalid();
            cmbCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
            break;
        default:
            txtCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
            cmbCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
            break;
    }
    if (mValue > 0) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    EnableAddBtn(tblRow, 'AddBtn');
};
function DDPickPersonChanged(x) {
    UpdateAuthorisedPersonForOfficeWork('Select Authorized Person');
    var target = DDPickPersonChanged.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var mIndex = $(tblRow).attr('id');
    var targetCtrl = $(target);
    var mValue = targetCtrl.val();
    if (x == 1) {
        mValue = mValue.length;
    }
    if (mValue > 0) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    getDesgnCode(mIndex, mValue);
    EnableAddBtn(tblRow, 'AddBtn');
};
function DriverNoForManagementChanged() {
    var target = DriverNoForManagementChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
    var driverno = targetCtrl.val();
    alert(driverno);
    if (driverno > 0) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    var mUrl = "/EHG/GetDesgCodenName?empID=" + driverno + "&empType=2" ;
    $.ajax({
        url: mUrl,
        success: function (result) {$('#tbl1Designation').html(result);}
    });  
    
};
function ValidateCloneRowCtrl() {
    var target = ValidateCloneRowCtrl.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    if (targetid.indexOf('_') >= 0) { targetid = targetid.split('_')[0] }
    var isvalid = validatectrl(targetid, targetCtrl.val());
    if (isvalid) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    EnableAddBtn(tblRow,'AddBtn');
};
function EnableAddBtn(tblRow,addBtnBaseID) {
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id')
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.find('.is-invalid').length > 0)
    { addBtnctrl.makeDisable(); } else { addBtnctrl.makeEnabled();}
};
function UpdateAuthorisedPersonForOfficeWork(defaultText) {
    var DDCtrl = $('#DDAuthorisedEmpForWork');
    DDCtrl.empty();
    DDCtrl.append($('<option/>', { value: "-1", text: defaultText }));
    $('.pickPersonName').each(function () {
        var mText = $(this).find('option:selected').text();
        if (mText.length > 0)
        {
            DDCtrl.append($('<option/>', { value: mText, text: mText }));
        }
    });
    $('.pickPersonNametxt').each(function () {
        var mText = $(this).val();
        if (mText.length > 0) {
            DDCtrl.append($('<option/>', { value: mText, text: mText }));
        }
    });
};
function addOfficeWorkCloneBtnClick() {
    var insrow = addOfficeWorkCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    CloneRow('tbody3', 'tbody4', $(insrow).attr('id') * 1, true, false);
};
function removeOfficeWorkCloneBtnClick() {
    var tblRow = removeOfficeWorkCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody4');
    UpdateAuthorisedPersonForOfficeWork('Select Authorized Person');
};
$(document).ready(function () {
    var VehicletypeCtrl = $('#ehgHeader_VehicleType');
    var POACtrl = $('#for_LV');
    var POA2WhCtrl = $('#for_2_wheeler');
    var ForManagementDiv = $('#for_Management');
    var ForOfficeWorkDiv = $('#for_OfficeWork');
    var POADropdown = $('#ehgHeader_PurposeOfAllotment');
    var DWTBtn = $('#DateWiseTourBtn');
    var VABtn = $('#VehicleAllotBtn');
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
            DWTBtn.makeDisable();
            VABtn.makeDisable();
        }
        else if (selectedvt == 2) {
            ForOfficeWorkDiv.removeClass('inVisible');
            ForManagementDiv.addClass('inVisible');
            DWTBtn.makeEnabled();
            VABtn.makeEnabled();
        } else {
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDiv.addClass('inVisible');
            DWTBtn.makeDisable();
            VABtn.makeDisable();
        };
        if (selectedvt > 0) { POADropdown.isValid(); } else { POADropdown.isInvalid() }
    });
});
$(document).ready(function () {
    getDropDownData('DDPersonType', 'Select Type', '/EHG/GetPersonTypes');
});
