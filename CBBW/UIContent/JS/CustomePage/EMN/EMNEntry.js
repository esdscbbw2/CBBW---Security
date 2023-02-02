﻿function AddClonebtn() {
    var insrow = AddClonebtn.caller.arguments[0].target.closest('.add-row');
    var insrowid = $(insrow).attr('id');
    var addbtn = $('#AddBtn');
    if (insrowid > 0) { addbtn = $('#AddBtn_' + insrowid); }

    var rowid = CloneRowReturningID('tbody1', 'tbody2', $(insrow).attr('id') * 1, true, false);
    $('#cmbDDPersonType_' + rowid).empty();
    $('#txtDDPersonType_' + rowid).val('').addClass('is-invalid');
    addbtn.makeDisable();
    EnableTavPersonBtn();
    EnableTravellingBtn();
}
function removeClonebtn() {
    var tblRow = removeClonebtn.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody2');
    EnableTavPersonBtn();
};
function EnableTravellingBtn() {
    var x = getDivInvalidCount('HdrDiv');
    var y = getDivInvalidCount('TravellingPerson');
    var DWTBtn = $('#VADBtn');
    var Btnsubmit = $('#Btnsubmit').val();
  
    if (Btnsubmit == 1) { DWTBtn.makeEnabled(); }
    if ((x + y) * 1 > 0) {
        DWTBtn.makeDisable();
       
    }
    else {
        DWTBtn.makeEnabled();
      
    }
};
function EnableTavPersonBtn() {
  
    var y = getDivInvalidCount('TravellingPerson');
    var SaveTP = $('#SaveTP');
    if (y * 1 > 0) {
        SaveTP.makeDisable();
    }
    else {
        SaveTP.makeEnabled();
    }
};
function DDPersonTypeChanged() {
    var target = DDPersonTypeChanged.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var docname = $('#emnHeader_AttachFile').val();
    if (docname != '') {
    var targetindexid = target.closest('.add-row');
    var targetid = targetCtrl.attr('id');
    var mValue = targetCtrl.val();
        var selectvalue = 0;
        var Empval = "";
        GetDDPersonTypeChanged(targetid, mValue, selectvalue, Empval);
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
function GetDDPersonTypeChanged(targetid, mValue, selectedvalue, Empval) {
    var cmbCtrl = $('#cmb' + targetid);
    var txtCtrl = $('#txt' + targetid);
    switch (mValue*1) {
        case 1:
            cmbCtrl.removeClass('inVisible').addClass('pickPersonName').isInvalid();
            txtCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
            getDropDownDataWithSelectedValue('cmb' + targetid, 'Select Employee', '/EHG/GetStaffList', selectedvalue);
            //getDropDownData('cmb' + targetid, 'Select Employee', '/EHG/GetStaffList');
            break;
        case 2:
            cmbCtrl.removeClass('inVisible').addClass('pickPersonName').isInvalid();
            txtCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
            getDropDownDataWithSelectedValue('cmb' + targetid, 'Select Driver', '/EHG/GetDriverList', selectedvalue);
            //getDropDownData('cmb' + targetid, 'Select Driver', '/EHG/GetDriverList');
            break;
        case 3:
            txtCtrl.removeClass('inVisible').addClass('pickPersonNametxt').isInvalid();
            cmbCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
            txtCtrl.val(Empval);
            break;
        case 4:
            txtCtrl.removeClass('inVisible').addClass('pickPersonNametxt').isInvalid();
            cmbCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
            txtCtrl.val(Empval);
            break;
        default:
            txtCtrl.addClass('inVisible').removeClass('pickPersonNametxt').clearValidateClass();
            cmbCtrl.addClass('inVisible').removeClass('pickPersonName').clearValidateClass();
            break;
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
        if (mValue != '' && $(this).val() == mValue) { dstat += 1; }
        //alert(mValue + ' - ' + $(this).val() + ' - ' + dstat);
    });

    if (x == 1) {
        mValue = mValue.length;
    }
    if (mValue > 0) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }

   
    if (x == 1) { GetVehicleEligibility(mIndex, 0); } else { GetVehicleEligibility(mIndex, mValue);}

    if (dstat > 1) {
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
    } else {
        getDesgnCode(mIndex, mValue);
        EnableAddBtn(tblRow, 'AddBtn');
    }
   
};
$(document).ready(function () {
    var NoteNumber = $('#emnHeader_NoteNumber').val();
    if ($('#Btnsubmit').val() == 0) {
        GetAllCenterCodeList();
    } else if ($('#Btnsubmit').val() == 1) {
        GetSavedCenterCodeList(NoteNumber);
    }
   
    (async function () {
        const r1 = await getDropDownDataWithSelectedValue('DDPersonType', 'select Person Type', '/Security/ETS/GetPersonTypes', 0);;
    })();
    EnableTravellingBtn();
  
});
async function GetAllCenterCodeList() {
    (async function () {
        const r2 = await getDropDownDataWithSelectedValue('CenterCN', 'select Center Code', '/Security/EMN/GetCenterCodeList', 0);
    })();
};
async function GetSavedCenterCodeList(NoteNumber) {

    (async function () {
        const r3 = await getDropDownDataWithSelectedValue('CenterCN', 'select Center Code', '/Security/EMN/getCenterCodeListFromTravellingPersonNotActive?NoteNumber='+NoteNumber, 0);
    })();
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
    //$('#BackBtnActive').val(1);
    EnableSubmitBtn();
    
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
   
  // EnableTavPersonBtn();
   
};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "TaDaDenied":
            isvalid = validatectrl_YesNoCombo(value);
            
            break;
        case "otherplace":
            isvalid = validatectrl_YesOrNo(value);
            break;
        case "carryLaptop":
            isvalid = validatectrl_YesOrNo(value);
            break;
        case "Policy":
            isvalid = validatectrl_YesOrNo(value);
        case "CenterCN":
            isvalid = validatectrl_ValidatestringLength(value);
            break;
    }
    return isvalid;
};
function validatectrl_ValidateLength(value) {
    if (value.length > 0) {
        return true;
    } else { return false; }
};
function validatectrl_ValidatestringLength(value) {
    if (value != "-1") {
        return true;
    } else { return false; }
};
function validatectrl_YesNoCombo(value) {

    if (value * 1 >= 0) {
        return true;
    } else { return false; }
}
function validatectrl_YesOrNo(value) {
    
    if (value * 1 ==0) {
        return true;
    } else { return false; }
}
function EnableAddBtn(tblRow, addBtnBaseID) {
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id')
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.find('.is-invalid').length > 0) { addBtnctrl.makeDisable(); } else { addBtnctrl.makeEnabled(); }
    //alert(rowid + ' - ' + addBtnBaseID+' - '+tblrow.find('.is-invalid').length);
    EnableTavPersonBtn();
};
function EnableSubmitBtn() {
   // var x = getDivInvalidCount('HdrDiv');
  //  var y = getDivInvalidCount('TravellingPerson');
    var z = getDivInvalidCount('Questions');
    var btn = $('#Btnsubmit').val();
    var SubmitBtn = $('#btnSubmited');
   // alert(z + ' - ' + btn);
   
    if (z <= 0 && btn==1) {
        SubmitBtn.makeEnabled();
        //alert(btn);
    }
};
function getDivInvalidCount(mdivID) {
    var x = 0;
    var mDiv = $('#' + mdivID);
    x = mDiv.find('.is-invalid').length;
    //alert(mdivID + ' - ' + x);
    return x;
};
function getDesgnCode(rowid, empCode) {
    var actualempcode = 0
    if ($.isNumeric(empCode)) { actualempcode = empCode; }
    var desgCtrl = $('#DesgCodenName');
    var persontypeCtrl = $('#DDPersonType');
    if (rowid != 0) {
        desgCtrl = $('#DesgCodenName_' + rowid);
        persontypeCtrl = $('#DDPersonType_' + rowid);
    }
    var mValue = persontypeCtrl.val();
    if (mValue <= 4 && mValue > 0) {
       
        var mUrl = "/ETS/GetDesgCodenName?empID=" + actualempcode + "&empType=" + mValue;
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
function GetVehicleEligibility(rowid, empCode) {
    var actualempcode = 0
    if ($.isNumeric(empCode)) { actualempcode = empCode; }
    var VTypeCtrl = $('#EgblVehicleType');
    var typeCtrlname = $('#EgblVehicleTypeName');
    var persontypeCtrl = $('#DDPersonType');
    if (rowid != 0) {
        VTypeCtrl = $('#EgblVehicleType_' + rowid);
        typeCtrlname = $('#EgblVehicleTypeName_' + rowid);
        persontypeCtrl = $('#DDPersonType_' + rowid);
    }
    var mValue = persontypeCtrl.val() * 1;
    if (mValue <= 4 && mValue > 0)  {
        var mUrl = "/ETS/GetVehicleEligibility?EmployeeNumber=" + actualempcode;
        $.ajax({
            url: mUrl,
            success: function (result) {
                typeCtrlname.html(result.DisplayText);
                VTypeCtrl.html(result.ID);
            }
        });
        
    }
    else {
        typeCtrlname.html('');
        VTypeCtrl.html('');
    }
};
function TravellingPersonBtnSave() {
    var notenumber = $('#emnHeader_NoteNumber').val();
    var DocName = $('#emnHeader_AttachFile').val();
    var CenterCodeName = $('#emnHeader_CenterCodeName').val();
    var CenterCN = $("#CenterCN option:selected").val();
    var CenterCNtxt=$("#CenterCN option:selected").text();
    var Btnsubmit = $('#Btnsubmit').val();
    if (Btnsubmit == null) { Btnsubmit = 0;}
  //  alert(Btnsubmit);
    //OficeWorkTbl    
    var schrecords = getRecordsFromTableV2('Traveltbl');
    var x = '{"NoteNumber":"' + notenumber + '","AttachFile":"' + DocName + '","CenterCodeName":"' + CenterCodeName + '","CenterCode":"' + CenterCN + '","CenterCodetxt":"' + CenterCNtxt + '","Btnsubmit":"' + Btnsubmit + '","PersonDtls":' + schrecords + '}';
    $.ajax({
        method: 'POST',
        url: '/EMN/SetTravelingPersonDetails',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Travelling Person Details Save Successfully.',
                        setTimeout: 5000,
                        icon: 'success',
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    });
                    EnableTravellingBtn();
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
function SaveFinalSubmit() {
    var notenumber = $('#emnHeader_NoteNumber').val();
    var DocName = $('#emnHeader_AttachFile').val();
    var CenterCodeName = $('#emnHeader_CenterCodeName').val();
  
    var x = '{"NoteNumber":"' + notenumber + '","AttachFile":"' + DocName + '","CenterCodeName":"' + CenterCodeName + '"}';
   // alert(x);
    $.ajax({
        method: 'POST',
        url: '/EMN/Create',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    var url = "/Security/EMN/Index";
                   
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Data saved successfully.',
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
                            var url = "/Security/EMN/Index"
                            window.location.href = url;
                        }
                    }
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
}
$('#btnBack').click(function () {
    var backbtnactive = $('#BackBtnActive').val(1);
    var backurl = "/Security/ETS/Index";
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
$(document).ready(function () {
    (async function () {
      //  const r1 = await getInitialData();
    })();

});
async function getInitialData(CenterCode) {
    $("#tbody2").empty();
    var rowid = 0;
    var TaDa;
    var NoteNumber = $('#emnHeader_NoteNumber').val();
    var DDPersonType = $('#DDPersonType');
    var cmbDDPersonType = $('#cmbDDPersonType');
    var txtDDPersonType = $('#txtDDPersonType');
    var DesgCodenName = $('#DesgCodenName');
    var EgblVehicleType = $('#EgblVehicleType');
    var EgblVehicleTypeName = $('#EgblVehicleTypeName');
    var TaDaDenied = $('#TaDaDenied');
    var AddBtn = $('#AddBtn');
    $.ajax({
        url: '/EMN/GetTraveelingPersonReverseData?NoteNumber=' + NoteNumber+'&CenterCode='+CenterCode,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
          
            $(data).each(function (index, item) {
               
                    if (item.PersonDtls.length!=0) {
                    $(item.PersonDtls).each(function (indexs, Peritem) {
                        if (indexs > 0) {
                            rowid = CloneRowReturningID('tbody1', 'tbody2', indexs - 1, true, false);
                            DDPersonType = $('#DDPersonType_' + rowid);
                            cmbDDPersonType = $('#cmbDDPersonType_' + rowid);
                            txtDDPersonType = $('#txtDDPersonType_' + rowid);
                            DesgCodenName = $('#DesgCodenName_' + rowid);
                            EgblVehicleType = $('#EgblVehicleType_' + rowid);
                            EgblVehicleTypeName = $('#EgblVehicleTypeName_' + rowid);
                            TaDaDenied = $('#TaDaDenied_' + rowid);
                            AddBtn = $('#AddBtn_' + rowid);
                        }

                        getDropDownDataWithSelectedValue(DDPersonType.attr('id'), 'select Person Type', '/Security/EHG/GetPersonTypes', Peritem.PersonType);
                        DDPersonType.val(Peritem.PersonType).isValid();
                        GetDDPersonTypeChanged(DDPersonType.attr('id'), Peritem.PersonType, Peritem.EmployeeNo, Peritem.EmployeeNonName)
                        cmbDDPersonType.isValid();
                        txtDDPersonType.isValid();
                        //alert(Peritem.EmployeeNonName);
                        getDesgnCode(rowid, Peritem.EmployeeNo);

                        GetVehicleEligibility(rowid, Peritem.EmployeeNo);
                        if (Peritem.TADADenieds == true) { TaDa = 1 } else { TaDa = 0 };
                        TaDaDenied.val(TaDa).isValid();
                        AddBtn.makeDisable();
                        EnableTravellingBtn();
                        EnableTavPersonBtn();
                    });
                    }else {
                        EmptyTPTable();
                        EnableTavPersonBtn();
                        EnableTravellingBtn();

                    }
                if ($('#Btnsubmit').val() == 1) {
                    
                    $("#Traveltbl").find("input,button,textarea,select").attr("disabled", "disabled");
                    $('#SaveTP').makeDisable();

                }
            });
        }
    });
};
$('#btnClear').click(function () {
    var selectvalue = 0;
    var Empval = "";
    var mValue = 0;
    var DDPersonType = $('#DDPersonType');
    var TaDaDenied = $('#TaDaDenied');
    $('#etsHeader_AttachFile').val('');
    $('#otherplace').val('');
    $('#carryLaptop').val('');
    $('#Policy').val('');
    DDPersonType.val('').isInvalid();
    GetDDPersonTypeChanged(DDPersonType.attr('id'), mValue, selectvalue, Empval);
    getDesgnCode(0, 0);
    GetVehicleEligibility(0,0);
    TaDaDenied.val('').isInvalid();
    $("#tbody2").empty();
    EnableTravellingBtn();

});

function CenterCNChanged() {
    var target = CenterCNChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
    var mValue = targetCtrl.val();
    var Btnsubmit = $('#Btnsubmit').val();
    
    getInitialData(mValue);
   // EnableTravellingBtn();
  

};
function EmptyTPTable() {
      var selectvalue = 0;
    var Empval = "";
    var mValue = 0;
    var DDPersonType = $('#DDPersonType');
    var TaDaDenied = $('#TaDaDenied');
    DDPersonType.val('').isInvalid();
    GetDDPersonTypeChanged(DDPersonType.attr('id'), mValue, selectvalue, Empval);
    getDesgnCode(0, 0);
    GetVehicleEligibility(0, 0);
    TaDaDenied.val('').isInvalid();
    $("#tbody2").empty();
}


