﻿$.fn.makeEnabled = function () {
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
    EnableDateWiseTourBtn();
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
            //alert(isvalid);
            break;
        case "FromTime":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "ToDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "PurposeOfVisit":
            if (value.length > 1 && WordCount(value) <= 200) {
                if (IsAlphaNumericWithSpace(value)) {
                    isvalid = true;
                }                
            }
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
            if (value != '') {
                if ($('#lblFromdateForMang').html() == 'Select Date') { isvalid = false; }
                else {
                    isvalid = true;
                    var maxdays = $('#MaxDaysOfTourForEmp').val();
                    var mtdt = CustomDateChangeV2(value, maxdays);
                    $('#ToDateForMang').attr('max', mtdt).attr('min', value);
                    $('#ToDateForMang').val('').isInvalid();
                    $('#lblToDateForMang').html('Select Date');
                }
                $('#ActualTOutDtForMang').html(ChangeDateFormat(value));
            }
            break;
        case "FromTimeForMang":
            if (value != '') {
                isvalid = true;
                $('#ActualTOutTimeForMang').html(value);
            }
            break;
        case "ToDateForMang":
            if (value != '') {
                var fromdate = $('#FromdateForMang').val();
                isvalid = CompareDateV2(fromdate, 0, value, 0);
                $('#ReTInDtForMang').html(ChangeDateFormat(value));
                if (!isvalid) { $('#ToDateForMang').prop('title', 'To Date Should Be Latter Than From Date'); }
            }
            break;
        case "TADADeniedForManagement":
            if (value != -1) { isvalid = true; }
            break;
        case "PurposeOfVisitFoeMang":
            if (value.length > 1 && WordCount(value) <= 200) {
                if (IsAlphaNumericWithSpace(value)) {
                    isvalid = true;
                }
            }
            break;
        case "DriverNoForManagement":
            if (value >= 1) { isvalid = true; }
            break;
    }
    
    if (targetid == 'AuthorisedEmpNoForManagement' && isvalid) {
        LockDiv('sManagementDiv');
        //$('#AuthorisedEmpNoForManagement').removeAttr('disabled');
    }
    else if (targetid == 'AuthorisedEmpNoForManagement' && !isvalid) {
        UnLockDiv('sManagementDiv');
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
function getDesgnCode(rowid, empCode) {
    var actualempcode=0
    if ($.isNumeric(empCode)) { actualempcode = empCode; }
    //alert(rowid + ' - ' + empCode + ' - ' + actualempcode);
    var desgCtrl = $('#DesgCodenName');
    var persontypeCtrl = $('#DDPersonType');
    if (rowid != 0) {
        desgCtrl = $('#DesgCodenName_' + rowid);
        persontypeCtrl = $('#DDPersonType_' + rowid);
    }    
    var mValue = persontypeCtrl.val()*1;
    if (mValue<=4 && mValue>0) {
        var mUrl = "/EHG/GetDesgCodenName?empID=" + actualempcode + "&empType=" + mValue;
        $.ajax({
            url: mUrl,
            success: function (result) { desgCtrl.html(result); }
        });
        //desgCtrl.html('qwe');
    }
    else {
        desgCtrl.html('');
    }
};
function DDPersonTypeChanged() {
    var target = DDPersonTypeChanged.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var docname = $('#ehgHeader_DocFileName').val();
    if (docname != '') {        
        var targetid = targetCtrl.attr('id');
        var mValue = targetCtrl.val();
        var cmbCtrl = $('#cmb' + targetid);
        var txtCtrl = $('#txt' + targetid);
        //alert(mValue);
        //$('#ehgHeader_PersonType').val(mValue);
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
    } else {
        targetCtrl.val('').isInvalid();
        Swal.fire({
            title: 'Error',
            text: 'No Documents Uploaded Yet.So Can Not Proceed Further.',
            icon: 'question',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }

    
};
function DDPickPersonChanged(x) {
    var target = DDPickPersonChanged.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var mIndex = $(tblRow).attr('id');
    var targetCtrl = $(target);
    var mValue = targetCtrl.val();
    //Check for Duplicate Person
    var dstat = 0;
    $('.xPerson').each(function () {        
        if (mValue != '' && $(this).val() == mValue) { dstat +=1; }
        //alert(mValue + ' - ' + $(this).val() + ' - ' + dstat);
    });
    //Check for Duplicate Person - end
    if (x == 1) {
        if (mValue.length <= 20 && mValue.length > 0)
        { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    }
    else if (mValue > 0) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    if (dstat>1) {
        targetCtrl.val('');
        targetCtrl.isInvalid();
        Swal.fire({
            title: 'Data Duplicacy Error',
            text: 'Person You Have Selected Is Already Taken.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }
    else {
        getDesgnCode(mIndex, mValue);
        EnableAddBtn(tblRow, 'AddBtn');
        UpdateAuthorisedPersonForOfficeWork('Select Authorized Person');
    }    
};
function DriverNoForManagementChanged() {
    var target = DriverNoForManagementChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
    var docname = $('#ehgHeader_DocFileName').val();
    if (docname != '') {        
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
    } else {
        targetCtrl.val('').isInvalid();
        Swal.fire({
            title: 'Error',
            text: 'No Documents Uploaded Yet.So Can Not Proceed Further.',
            icon: 'question',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }
    EnableSubmitBtn();
};
function ValidateCloneRowCtrl() {
    var target = ValidateCloneRowCtrl.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    var fdtCtrl = $('#FromDate');
    var tdtCtrl = $('#ToDate');
    var lbltdtCtrl = $('#lblToDate');
    if (targetid.indexOf('_') >= 0) {
        targetid = targetid.split('_')[0];
        fdtCtrl = $('#FromDate_' + $(tblRow).attr('id'));
        tdtCtrl = $('#ToDate_' + $(tblRow).attr('id'));
        lbltdtCtrl = $('#lblToDate_' + $(tblRow).attr('id'));
    }
    var isvalid = validatectrl(targetid, targetCtrl.val());
    //for todate>fromdate validation
    if (targetid == 'ToDate') {
        isvalid = CompareDateV2(fdtCtrl.val(), 0, targetCtrl.val(), 0);
    }
    
    if (targetid == 'FromDate' && isvalid) {
        var maxdays = $('#MaxDaysOfTourForEmp').val();
        var mtdt = CustomDateChangeV2(targetCtrl.val(), maxdays);
        tdtCtrl.attr('max', mtdt).attr('min', targetCtrl.val());
        tdtCtrl.val('').isInvalid();
        lbltdtCtrl.html('Select Date');
    }
    if (isvalid) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    EnableAddBtn(tblRow, 'AddBtn');
    EnableDateWiseTourBtn();
    EnableSubmitBtn();    
};
function EnableAddBtn(tblRow, addBtnBaseID) {
    var authempCtrl=$('#DDAuthorisedEmpForWork');
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id')
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.find('.is-invalid').length > 0) {
        addBtnctrl.makeDisable();
        authempCtrl.makeDisable();
    }
    else {
        addBtnctrl.makeEnabled();
        authempCtrl.makeEnabled();
    }
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
    var vehtypeCtrl = $('#ehgHeader_VehicleType');
    var poaCtrl = $('#ehgHeader_PurposeOfAllotment');
    var p = vehtypeCtrl.val();
    var q = poaCtrl.val();
    ForManagementDivRemoveInValidStatus();
    if (p == 2) { poaCtrl.removeClass('is-invalid'); }    
    var x = getDivInvalidCount('HdrDiv');
    var y = getDivInvalidCount('for_OfficeWork');
    var z = getDivInvalidCount('for_Management');
    var a = $('#AcceptCmb').val();
    var b = $('#DWSubmitBtnActive').val();
    var c = $('#VASubmitBtnActive').val();    
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
                else if (y <= 0) {
                    if (b == 1 && c == 1) {
                        IsSubmitActive = true;
                    }
                    //IsSubmitActive = true;
                }
            }
        }
    }    
    if (IsSubmitActive) {        
        SubmitBtn.makeEnabled();
    } else { SubmitBtn.makeDisable(); }    
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
    if (targetCtrl.val() !='-1' && targetCtrl.val().length > 0) {
        $('#ehgHeader_AuthorisedEmployeeName').val(targetCtrl.val());
        //$('#AuthorisedEmpNo').val(targetCtrl.val());
        targetCtrl.isValid();
        LockDiv('sOfficeworkDiv');
        //targetCtrl.removeAttr('disabled');
    }
    else {
        targetCtrl.isInvalid();
        UnLockDiv('sOfficeworkDiv');
    }
    EnableDateWiseTourBtn();
};
function addOfficeWorkCloneBtnClick() {
    //var mtargetCtrl = $(addOfficeWorkCloneBtnClick.caller.arguments[0].target);
    var insrow = addOfficeWorkCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    var sRowid = $(insrow).attr('id');
    var frmdtlblSource = $('#lblFromDate');
    var btnAdd = $('#AddBtn');
    if (sRowid > 0) {
        frmdtlblSource = $('#lblFromDate_' + sRowid);
        btnAdd = $('#AddBtn_' + sRowid);
    }
    var rowid=CloneRowReturningID('tbody3', 'tbody4', $(insrow).attr('id') * 1, true, false);
    $('#PurposeOfVisit_' + rowid).val('');
    $('#cmbDDPersonType_' + rowid).empty();
    $('#ToDate_' + rowid).val('');
    //$('#FromDate_' + rowid).val('');
    $('#lblFromDate_' + rowid).html(frmdtlblSource.html());
    $('#FromTime_' + rowid).val('');
    $('#txtDDPersonType_' + rowid).val('');
    $('#FromDate_' + rowid).isValid();
    $('#FromDate_' + rowid).attr('disabled', 'disabled');
    $('#DDAuthorisedEmpForWork').attr('disabled', 'disabled');
    btnAdd.attr('disabled', 'disabled');
    EnableDateWiseTourBtn();
};
function removeOfficeWorkCloneBtnClick() {
    var tblRow = removeOfficeWorkCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody4');
    UpdateAuthorisedPersonForOfficeWork('Select Authorized Person');
    EnableDateWiseTourBtn();
    $('#DDAuthorisedEmpForWork').removeAttr('disabled');
    $('#OficeWorkTbl tr:last').find('button').each(function () {
        $(this).makeEnabled();
    });
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
        POADropdown.isInvalid();
        
        POA2WhCtrl.addClass('inVisible');        
        ForOfficeWorkDiv.addClass('inVisible');
        ForManagementDiv.addClass('inVisible');
        //POADropdown.val('');        
        //dwtBtnCtrl.makeDisable();
        //vadBtnCtrl.makeDisable();
    }
    else if (selectedvt == 2) {
        POA2WhCtrl.removeClass('inVisible');
        ForOfficeWorkDiv.removeClass('inVisible');
        POADropdown.removeClass('is-invalid').removeClass('is-valid');
        ForManagementDiv.addClass('inVisible');
        POACtrl.addClass('inVisible');
        //dwtBtnCtrl.makeEnabled();
        //vadBtnCtrl.makeDisable();
    }
    else {
        POADropdown.val(''); POADropdown.isInvalid();
        POACtrl.addClass('inVisible');
        POA2WhCtrl.addClass('inVisible');
        ForOfficeWorkDiv.addClass('inVisible');
        ForManagementDiv.addClass('inVisible');
        
        //dwtBtnCtrl.makeDisable();
        //vadBtnCtrl.makeDisable();
    };
    if (selectedvt > 0) {
        VehicletypeCtrl.isValid();
    }
    else { VehicletypeCtrl.isInvalid(); }
    EnableDateWiseTourBtn();
};
function ForManagementDivRemoveInValidStatus() {
    //if ($('#for_Management').hasClass('inVisible')) {
    //} else {
    var vehtypeCtrl = $('#ehgHeader_VehicleType');
    var poaCtrl = $('#ehgHeader_PurposeOfAllotment');
    var p = vehtypeCtrl.val();
    var q = poaCtrl.val();
    if (p == 1 && q == 1) {
        var Ctrl1 = $('#DriverNoForManagement');
        var ctrl2 = $('#FromdateForMang');
        var ctrl3 = $('#FromTimeForMang');
        var ctrl4 = $('#ToDateForMang');
        var ctrl5 = $('#PurposeOfVisitFoeMang');
        var ctrl6 = $('#TADADeniedForManagement');
        var ctrl7=$('#AuthorisedEmpNoForManagement');
        var isvalid = validatectrl('DriverNoForManagement', Ctrl1.val());
        if (isvalid) {
            Ctrl1.isValid();
            isvalid = validatectrl('FromdateForMang', ctrl2.val());
            if (isvalid) {
                ctrl2.isValid();
                isvalid = validatectrl('FromTimeForMang', ctrl3.val());
                if (isvalid) {
                    ctrl3.isValid();
                    isvalid = validatectrl('ToDateForMang', ctrl4.val());
                    if (isvalid) {
                        ctrl4.isValid();
                        isvalid = validatectrl('PurposeOfVisitFoeMang', ctrl5.val());
                        if (isvalid) {
                            ctrl5.isValid();
                            isvalid = validatectrl('TADADeniedForManagement', ctrl6.val());
                            if (isvalid) {
                                ctrl6.isValid();
                                isvalid = validatectrl('AuthorisedEmpNoForManagement', ctrl7.val());
                                if (isvalid) {
                                    ctrl7.isValid();
                                }
                                else { ctrl7.isInvalid(); }
                            }
                            else { ctrl6.isInvalid(); }
                        }
                        else { ctrl5.isInvalid(); }
                    }
                    else { ctrl4.isInvalid(); }
                }
                else { ctrl3.isInvalid(); }
            }
            else { ctrl2.isInvalid(); }
        } else { Ctrl1.isInvalid() }
    } else {
        $('#DriverNoForManagement').removeClass('is-invalid');
        $('#FromdateForMang').removeClass('is-invalid');
        $('#FromTimeForMang').removeClass('is-invalid');
        $('#ToDateForMang').removeClass('is-invalid');
        $('#PurposeOfVisitFoeMang').removeClass('is-invalid');
        $('#TADADeniedForManagement').removeClass('is-invalid');
        $('#AuthorisedEmpNoForManagement').removeClass('is-invalid');
    }
        
    //}
    
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
            ForManagementDivRemoveInValidStatus();
            ForManagementDiv.addClass('inVisible');

            //DWTBtn.makeEnabled();
            //VABtn.makeEnabled();
        }
        else {            
            ForOfficeWorkDiv.addClass('inVisible');
            ForManagementDivRemoveInValidStatus();
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
                    rowid = CloneRowReturningID('tbody3', 'tbody4', index-1, true, false);
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
                        txtpersonCtrl.removeClass('inVisible').addClass('pickPersonNametxt');
                        cmbpersonCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();                        
                        break;
                    case 4:
                        txtpersonCtrl.removeClass('inVisible').addClass('pickPersonNametxt');
                        cmbpersonCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
                        break;
                    default:
                        txtpersonCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
                        cmbpersonCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
                        break;
                }
                cmbpersonCtrl.isValid();                
                EnableDateWiseTourBtn();
            });
            addbtnCtrl.makeEnabled();
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
    //getDropDownData('DDPersonType', 'Select Type', '/EHG/GetPersonTypes');
    var maxdt = $('#MaxFromDate').val();
    var mindt = $('#MinFromDate').val();
    $('#FromDate').attr('max', maxdt).attr('min', mindt);
    $('#ToDate').attr('min', mindt);
});
$(document).ready(function () {
    VehicleTypeChanged();
    POADropdownChanged();
    (async function () {
        const r1 = await getInitialDataForTravelingPerson();
    })();
    var matstat = $('#ehgHeader_MaterialStatus');
    if (matstat.val() >= 0) { matstat.isValid(); } else { matstat.isInvalid(); }
    var instCtrl = $('#ehgHeader_Instructor');
    if (instCtrl.val() > 0) { instCtrl.isValid(); } else { instCtrl.isInvalid(); }
    var dwtFilled = $('#DWSubmitBtnActive').val();
    var vaFilled = $('#VASubmitBtnActive').val();
    var dwtBtnCtrl = $('#DateWiseTourBtn2');
    var vadBtnCtrl = $('#VADBtn');
    var hdrDiv = $('#HdrDiv');
    var managementDiv = $('#for_Management');
    var officeworkDiv = $('#for_OfficeWork');
    if (dwtFilled == 1) {
        dwtBtnCtrl.makeEnabled();
        vadBtnCtrl.makeEnabled();
    } else {
        //alert('ok');
        //dwtBtnCtrl.makeEnabled();
        vadBtnCtrl.makeDisable();
    }
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
    var POAValue = $('#ehgHeader_VehicleType').val();
    if (POAValue == 2) { officeworkDiv.removeClass('inVisible'); }
    EnableSubmitBtn();
    
});
