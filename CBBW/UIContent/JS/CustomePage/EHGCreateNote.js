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
    $('#BackBtnActive').val(1);
    EnableSubmitBtn();
};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "ehgHeader_MaterialStatus":
            $('#MaterialStatus').val(value);
            if (value >=0) { isvalid = true; }
            break;
        case "ehgHeader_Instructor":
            if (value > 0) {
                var insname = $('#ehgHeader_Instructor option:selected').text();
                $('#ehgHeader_InstructorName').val(insname);
                $('#Instructor').val(value);
                isvalid = true;
            }
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
            if (value > 0) {
                var empname = $('#AuthorisedEmpNoForManagement option:selected').text();
                $('#AuthorisedEmpNameForManagement').val(empname);                
                isvalid = true;
            }
            break;        
        case "FromdateForMang":
            if (value !='') { isvalid = true; }
            break;
        case "FromTimeForMang":
            if (value !='') { isvalid = true; }
            break;
        case "ToDateForMang":
            if (value != '') { isvalid = true; }
            break;
        case "TADADeniedForManagement":
            if (value != '') { isvalid = true; }
            break;
        case "PurposeOfVisitFoeMang":
            if (value != '') { isvalid = true; }
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
function DateWiseTourDtlClicked() {
    var notenumber = $('#ehgHeader_NoteNumber').val();
    var vehicletype = $('#ehgHeader_VehicleType').val();
    var poallotment = $('#ehgHeader_PurposeOfAllotment').val();
    var matstat = $('#ehgHeader_MaterialStatus').val();
    var instructor = $('#ehgHeader_Instructor').val();
    var authEMpname = $('#DDAuthorisedEmpForWork option:selected').text();
    var InstructorName = $('#ehgHeader_Instructor option:selected').text();
    var DocName = $('#ehgHeader_DocFileName').val();
    //OficeWorkTbl    
    var schrecords = getRecordsFromTableV2('OficeWorkTbl');
    //var x = '{"NoteNumber":"' + notenumber + '","AuthorisedEmpName":"' + authorisedemp + '","PersonDtls":' + schrecords + '}';
    var x = '{"NoteNumber":"' + notenumber
        + '","VehicleType":"' + vehicletype + '","PurposeOfAllotment":"' + poallotment
        + '","MaterialStatus":"' + matstat + '","Instructor":"' + instructor
        + '","AuthorisedEmployeeName":"' + authEMpname
        + '","DocFileName":"' + DocName + '","InstructorName":"' + InstructorName
        + '","PersonDtls":' + schrecords + '}';
    $.ajax({
        method: 'POST',
        url: '/EHG/GetTravelingPersonDetails',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    var url = "/Security/EHG/DateWiseTourDetails";
                    window.location.href = url;                  
                }
                else {
                    Swal.fire({
                        title: 'Error',
                        text: 'Failed To Update Traveling Person Details.',
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
    if (driverno > 0) {
        var drivername = $('#DriverNoForManagement option:selected').text();
        $('#DriverNameForManagement').val(drivername);
        var mUrl = "/EHG/GetDesgCodenName?empID=" + driverno + "&empType=2";
        $.ajax({
            url: mUrl,
            success: function (result) {
                $('#tbl1Designation').html(result);
                $('#DesgCodeNNameForManagement').val(result);
            }
        });
        targetCtrl.isValid();
    }
    else {
        targetCtrl.isInvalid();
    }
    $('#BackBtnActive').val(1);
};
function ValidateCloneRowCtrl() {
    var target = ValidateCloneRowCtrl.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    if (targetid.indexOf('_') >= 0) { targetid = targetid.split('_')[0] }
    var isvalid = validatectrl(targetid, targetCtrl.val());
    if (isvalid) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    EnableAddBtn(tblRow, 'AddBtn');
    EnableDateWiseTourBtn();
    EnableSubmitBtn();
};
function EnableAddBtn(tblRow,addBtnBaseID) {
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id')
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.find('.is-invalid').length > 0)
    { addBtnctrl.makeDisable(); } else { addBtnctrl.makeEnabled(); }
    //alert(rowid + ' - ' + addBtnBaseID+' - '+tblrow.find('.is-invalid').length);
    EnableDateWiseTourBtn();
};
function getDivInvalidCount(mdivID) {
    var x = 0;
    var mDiv = $('#' + mdivID);
    x = mDiv.find('.is-invalid').length;
    //alert(mdivID + ' - ' + x);
    return x;
};
function EnableDateWiseTourBtn() {
    var x = getDivInvalidCount('HdrDiv');
    var y = getDivInvalidCount('for_OfficeWork');
    var DWTBtn = $('#DateWiseTourBtn2');
    //alert((x + y) * 1);
    if ((x+y)*1 > 0) {
        DWTBtn.makeDisable();
    }
    else {
        DWTBtn.makeEnabled();
    }
};
function EnableSubmitBtn() {
    var x = getDivInvalidCount('HdrDiv');
    var y = getDivInvalidCount('for_OfficeWork');
    var z = getDivInvalidCount('for_Management');
    var a = $('#AcceptCmb').val();
    var b = $('#DWSubmitBtnActive').val();
    var c = $('#VASubmitBtnActive').val();
    var p = $('#ehgHeader_VehicleType').val();
    var q = $('#ehgHeader_PurposeOfAllotment').val();
    var IsSubmitActive = false;
    var SubmitBtn = $('#btnSubmit');
    //alert(x + ' - ' + y + ' - ' + z);
    if (a == 1) { IsSubmitActive = true; } else { IsSubmitActive = false; }
    if (IsSubmitActive) {
        IsSubmitActive = false;
        if (x <= 0) {
            if (p == 2) {
                if (y <= 0) {
                    if (b == 1 && c == 1) {
                        IsSubmitActive = true;
                    }                    
                }
            }
            else if (p == 1) {
                if (q == 1) {
                    if (z <= 0) { IsSubmitActive = true; }
                }
                else if (y <= 0) { IsSubmitActive = true; }
            }
        }
    }    
    if (IsSubmitActive) { SubmitBtn.makeEnabled(); } else { SubmitBtn.makeDisable(); }
    
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
async function UpdateAuthorisedPersonForOfficeWorkWithSelectedValue(defaultText,selectedValue) {
    var DDCtrl = $('#DDAuthorisedEmpForWork');
    DDCtrl.empty();
    DDCtrl.append($('<option/>', { value: "-1", text: defaultText }));
    $('.pickPersonName').each(function () {
        var mText = $(this).find('option:selected').text();
        if (mText.length > 0) {
            DDCtrl.append($('<option/>', { value: mText, text: mText }));
        }
    });
    $('.pickPersonNametxt').each(function () {
        var mText = $(this).val();
        if (mText.length > 0) {
            DDCtrl.append($('<option/>', { value: mText, text: mText }));
        }
    });
    DDCtrl.val(selectedValue);
    if (selectedValue.length > 0) { DDCtrl.isValid(); } else { DDCtrl.isInvalid(); }
    alert(selectedValue);
};
function DDAuthorisedEmpForWorkChanged() {
    var targetCtrl = $(DDAuthorisedEmpForWorkChanged.caller.arguments[0].target);
    if (targetCtrl.val().length > 0) {
        $('#ehgHeader_AuthorisedEmployeeName').val(targetCtrl.val());
        //$('#AuthorisedEmpNo').val(targetCtrl.val());
        targetCtrl.isValid();
    } else { targetCtrl.isInvalid(); }
    EnableDateWiseTourBtn();
};
function addOfficeWorkCloneBtnClick() {
    var insrow = addOfficeWorkCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    CloneRow('tbody3', 'tbody4', $(insrow).attr('id') * 1, true, false);
    EnableDateWiseTourBtn();
};
function removeOfficeWorkCloneBtnClick() {
    var tblRow = removeOfficeWorkCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody4');
    UpdateAuthorisedPersonForOfficeWork('Select Authorized Person');
    EnableDateWiseTourBtn();
};
function VehicleTypeChanged() {
    var POACtrl = $('#for_LV');
    var POA2WhCtrl = $('#for_2_wheeler');
    var ForManagementDiv = $('#for_Management');
    var ForOfficeWorkDiv = $('#for_OfficeWork');
    var POADropdown = $('#ehgHeader_PurposeOfAllotment');
    var VehicletypeCtrl = $('#ehgHeader_VehicleType');
    var dwtBtnCtrl = $('#DateWiseTourBtn2');
    var vadBtnCtrl = $('#VADBtn');    
    var selectedvt = VehicletypeCtrl.val();
    $('#VehicleType').val(selectedvt);
    if (selectedvt == 1) {
        POACtrl.removeClass('inVisible');
        POA2WhCtrl.addClass('inVisible');        
        ForOfficeWorkDiv.addClass('inVisible');
        ForManagementDiv.addClass('inVisible');
        //POADropdown.val('');
        POADropdown.isInvalid();
        //dwtBtnCtrl.makeDisable();
        //vadBtnCtrl.makeDisable();
    }
    else if (selectedvt == 2) {        
        POACtrl.addClass('inVisible');
        POA2WhCtrl.removeClass('inVisible');
        ForOfficeWorkDiv.removeClass('inVisible');
        ForManagementDiv.addClass('inVisible');
        POADropdown.clearValidateClass();
        //dwtBtnCtrl.makeEnabled();
        //vadBtnCtrl.makeDisable();
    }
    else {
        POACtrl.addClass('inVisible');
        POA2WhCtrl.addClass('inVisible');
        ForOfficeWorkDiv.addClass('inVisible');
        ForManagementDiv.addClass('inVisible');
        POADropdown.val(''); POADropdown.isInvalid();
        //dwtBtnCtrl.makeDisable();
        //vadBtnCtrl.makeDisable();
    };
    if (selectedvt > 0) {
        VehicletypeCtrl.isValid();
    }
    else { VehicletypeCtrl.isInvalid(); }
    EnableDateWiseTourBtn();
};
function POADropdownChanged() {
    var ForManagementDiv = $('#for_Management');
    var ForOfficeWorkDiv = $('#for_OfficeWork');
    var POADropdown = $('#ehgHeader_PurposeOfAllotment');
    var isUploaded = 0;
    if ($('#ehgHeader_DocFileName').val().length > 1) { isUploaded = 1;}
    var DWTBtn = $('#DateWiseTourBtn2');
    var VABtn = $('#VADBtn');
    var selectedvt = POADropdown.val();
    $('#POA').val(selectedvt);
    $('#ehgHeader_POA2').val(selectedvt);
    //if (isUploaded == 1) {
        if (selectedvt == 1) {
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDiv.removeClass('inVisible');
            //DWTBtn.makeDisable();
            //VABtn.makeDisable();
        }
        else if (selectedvt == 2) {
            ForOfficeWorkDiv.removeClass('inVisible');
            ForManagementDiv.addClass('inVisible');
            //DWTBtn.makeEnabled();
            //VABtn.makeEnabled();
        }
        else {
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDiv.addClass('inVisible');
            //DWTBtn.makeDisable();
            //VABtn.makeDisable();
        };
    //}
    //else {
    //    ForOfficeWorkDiv.addClass('inVisible');
    //    ForManagementDiv.addClass('inVisible');
    //}
    if (selectedvt > 0) { POADropdown.isValid(); } else { POADropdown.isInvalid() }
    EnableDateWiseTourBtn();
};
async function getInitialDataForTravelingPerson() {
    var notenumber = $('#ehgHeader_NoteNumber').val();
    var rowid;
    var authPerson = $('#ehgHeader_AuthorisedEmployeeName').val();
    var persontypeCtrl = $('#DDPersonType');
    var cmbpersonCtrl = $('#cmbDDPersonType');
    var txtpersonCtrl = $('#txtDDPersonType');
    var fromdateCtrl = $('#FromDate');
    var fromtimeCtrl = $('#FromTime');
    var todateCtrl = $('#ToDate');
    var povCtrl = $('#PurposeOfVisit');
    var tadaDeniedCtrl = $('#TaDaDenied');
    var fromdatelblCtrl = $('#lblFromDate');
    var todatelblCtrl = $('#lblToDate');
    var addbtnCtrl = $('#AddBtn');
    var desgCtrl = $('#DesgCodenName');
    var authempCtrl = $('#DDAuthorisedEmpForWork');
    authempCtrl.empty();
    authempCtrl.append($('<option/>', { value: "-1", text: "Select Authorised Employee" }));
    $.ajax({
        url: '/EHG/GetTPDetails',
        method: 'GET',
        data: { NoteNumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                if (index > 0) {                    
                    rowid = CloneRowReturningID('tbody3', 'tbody4', index-1, true, true);
                    persontypeCtrl=$('#DDPersonType_' + rowid);
                    cmbpersonCtrl = $('#cmbDDPersonType_' + rowid);
                    txtpersonCtrl = $('#txtDDPersonType_' + rowid);
                    fromdateCtrl = $('#FromDate_' + rowid);
                    fromtimeCtrl = $('#FromTime_' + rowid);
                    todateCtrl = $('#ToDate_' + rowid);
                    povCtrl = $('#PurposeOfVisit_' + rowid);
                    tadaDeniedCtrl = $('#TaDaDenied_' + rowid);
                    fromdatelblCtrl = $('#lblFromDate_' + rowid);
                    todatelblCtrl = $('#lblToDate_' + rowid);
                    addbtnCtrl = $('#AddBtn_' + rowid);
                    desgCtrl = $('#DesgCodenName_' + rowid);
                }
                persontypeCtrl.val(item.PersonType).isValid();
                txtpersonCtrl.val(item.EmployeeNonName).isValid();
                desgCtrl.html(item.DesignationCodenName);
                fromdateCtrl.val(item.FromDateStr).isValid();
                fromtimeCtrl.val(item.FromTime).isValid();
                todateCtrl.val(item.ToDateStr).isValid();
                povCtrl.val(item.PurposeOfVisit).isValid();
                tadaDeniedCtrl.val(item.TADADenied ? 1 : 0).isValid();
                fromdatelblCtrl.html(item.FromDateStrDisplay);
                todatelblCtrl.html(item.ToDateStrDisplay);                
                authempCtrl.append($('<option/>', { value: item.EmployeeNonName, text: item.EmployeeNonName }));
                authempCtrl.val(authPerson).isValid();
                if (item.IsAuthorised) { authPerson = item.EmployeeNonName; }
                switch (item.PersonType) {
                    case 1:
                        cmbpersonCtrl.removeClass('inVisible').addClass('pickPersonName').isInvalid();
                        txtpersonCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
                        (async function () {
                            const r1 = await getDropDownDataWithSelectedValue(cmbpersonCtrl.attr('id'), 'Select Employee', '/EHG/GetStaffList', item.EmployeeNo);
                        })();
                        break;
                    case 2:
                        cmbpersonCtrl.removeClass('inVisible').addClass('pickPersonName').isInvalid();
                        txtpersonCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
                        (async function () {
                            const r1 = await getDropDownDataWithSelectedValue(cmbpersonCtrl.attr('id'), 'Select Driver', '/EHG/GetDriverList', item.EmployeeNo);
                        })();
                        break;
                    case 3:
                        txtpersonCtrl.removeClass('inVisible').addClass('pickPersonNametxt').isInvalid();
                        cmbpersonCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();                        
                        break;
                    case 4:
                        txtpersonCtrl.removeClass('inVisible').addClass('pickPersonNametxt').isInvalid();
                        cmbpersonCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
                        break;
                    default:
                        txtpersonCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
                        cmbpersonCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
                        break;
                }
                cmbpersonCtrl.isValid();
                addbtnCtrl.makeEnabled();                
            });
        }
    });
};
$(document).ready(function () {
    var VehicletypeCtrl = $('#ehgHeader_VehicleType');
    var POADropdown = $('#ehgHeader_PurposeOfAllotment');
    var acCtrl = $('#AcceptCmb');
    VehicletypeCtrl.change(function () {       
        VehicleTypeChanged();
        $('#BackBtnActive').val(1);
        EnableSubmitBtn();
    });
    POADropdown.change(function () {
        POADropdownChanged();
        $('#BackBtnActive').val(1);
        EnableSubmitBtn();
    });
    $('#btnBack').click(function () {
        var backbtnactive = $('#BackBtnActive').val();
        var backurl = "/Security/EHG/Index";
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
    acCtrl.change(function () {
        if (acCtrl.val() == 1) { acCtrl.isValid(); } else { acCtrl.isInvalid(); }
        EnableSubmitBtn();
    });
});
$(document).ready(function () {
    getDropDownData('DDPersonType', 'Select Type', '/EHG/GetPersonTypes');
});
$(document).ready(function () {
    VehicleTypeChanged();
    POADropdownChanged();
    (async function () {
        const r1 = await getInitialDataForTravelingPerson();
    })();
    var dwtFilled = $('#DWSubmitBtnActive').val();
    var vaFilled = $('#VASubmitBtnActive').val();
    var dwtBtnCtrl = $('#DateWiseTourBtn2');
    var vadBtnCtrl = $('#VADBtn');
    var hdrDiv = $('#HdrDiv');
    var managementDiv = $('#for_Management');
    var officeworkDiv = $('#for_OfficeWork');
    if (dwtFilled == 1) {
        dwtBtnCtrl.makeEnabled(); vadBtnCtrl.makeEnabled();
    } else { dwtBtnCtrl.makeDisable(); vadBtnCtrl.makeDisable(); }    
    if (dwtFilled == 1 || vaFilled == 1) {
        hdrDiv.addClass('sectionB');
        managementDiv.addClass('sectionB');
        officeworkDiv.addClass('sectionB');
        hdrDiv.find('.form-control').each(function () {
            $(this).makeDisable();
        });
        hdrDiv.find('.form-select').each(function () {
            $(this).makeDisable();
        });
        managementDiv.find('.form-control').each(function () {
            $(this).makeDisable();
        });
        managementDiv.find('.form-select').each(function () {
            $(this).makeDisable();
        });
        officeworkDiv.find('.form-control').each(function () {
            $(this).makeDisable();
        });
        officeworkDiv.find('.form-select').each(function () {
            $(this).makeDisable();
        });
    }
    EnableSubmitBtn();
});
