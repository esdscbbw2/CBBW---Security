$(function () {
    $("#PublicTransport").change(function () {
        MakevalueReset();
        var PTval = $('option:selected', this).val();
        var Typeval = $("#PersonType").val();
        if (PTval == '0') {
            $('.forYes').hide();
            $('.forNo').show();
            $("#VehicleType").removeClass('is-valid').addClass('is-invalid');
            $("#ReasonVehicleReq").removeClass('is-valid').addClass('is-invalid');
            getDropDownData('VehicleType', 'Select Type', '/ETS/GetVehicleTypes?TypeVal=' + Typeval);
        } else {
            $('.forYes').show();
            $('.forNo').hide();
            $("#VehicleType").removeClass('is-invalid').addClass('is-valid');
            $("#ReasonVehicleReq").removeClass('is-invalid').addClass('is-valid');
           
        }
       
        GetTourCategory(PTval);
        //getMultiselectData('TourCategory', '/ETS/GetTourCategories?PTval='+PTval);
        
    });
});
function MakevalueReset() {
    $("#SchFromDate").val('').isInvalid();
    $("#SchFromTime").val('').isInvalid();
    $("#SchTourToDate").val('').isInvalid();
    $("#SchTourToDate,#SchToDate").makeDisable();
 
   
};
function fireSweetAlert(evt) {

    if (evt == "1") {
        Swal.fire({
            title: 'Information Message',
            text: "Employee Vehicle Selection Is Beyond The Eligiblity. Are You Sure To Proceed ?",
            icon: 'success',
            cancelButtonClass: 'btn-cancel',
            cancelButtonText: 'Cancel',
            confirmButtonText: 'Ok',
            confirmButtonColor: '#2527a2',
            showCancelButton: true,

        })
    }
}
$(document).ready(function () {
    $('.forYes').hide();
    $('.forNo').hide();
    $('#SchTourToDate').makeDisable();
    $('#SchToDate').makeDisable();
});
function GetTourCategory(PTval) {
    getMultiselectData('TourCategory', '/ETS/GetTourCategories?PTval=' + PTval);
  //  alert(PTval);
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
    if (targetid == 'VehicleType') {
        fireSweetAlert($(target).val());
    }
    
    if (targetid == "SchFromDate") { Datechange($(target).val());}
    if (targetid == "SchTourToDate") { SetDatechange($(target).val()); }
    
    $('#SchToDate').val('').isInvalid();
    ClearallDropdownData(0);
    $("#tbody2").empty();

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

    var todate = new Date($('#SchTourToDate').val());
    var preToDate = $(tblRow).find('.todt').val();
    var calculatedFromdate = new Date(ChangeDateFormat(CustomDateChange(preToDate, 1, '-')));
    if (todate <= calculatedFromdate) {
        $(tblRow).find('.addBtn').makeDisable();
    } 

    EnableSubmitBtn();
};
function validatectrl(targetid, value) {
    var isvalid = false;

    switch (targetid) {
        
        case "ReasonVehicleReq":
            isvalid = validatectrl_ValidateLength(value);
              break;
        case "PublicTransport":
            isvalid = validatectrl_YesNoCombo(value);
            break;
            
        case "VehicleType":
            isvalid = validatectrl_YesNoCombo(value);
            break;
        case "SchFromDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "SchFromTime":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "SchTourToDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "dateTour_SchToDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "dateTour_BranchCodeName":
            if (value > 0) {
                var insname = $('#dateTour_BranchCodeName option:selected').text();
                $('#dateTour_BranchCodeName').val(insname);
                
                isvalid = true;
            }
            break;
        case "SchToDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "TourCategory":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "CenterCodeName":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "BranchCodeName":
            isvalid = validatectrl_ValidateLength(value);
            break;
    }
   
    return isvalid;
};
function validatectrl_YesNoCombo(value) {

    if (value * 1 >= 0) {
        return true;
    } else { return false; }
};
function validatectrl_ValidateLength(value) {
    if (value.length > 0) {
        return true;
    } else { return false; }
};
function EnableSubmitBtn() {
    var x = getDivInvalidCount('TravDetails');
    var y = getDivInvalidCount('dateDetails');
    var DWTBtn = $('#btnSubmits');
   // alert(x); alert(y);
    if ((x + y) * 1 > 0) {
        DWTBtn.makeDisable();
    }
    else {
        DWTBtn.makeEnabled();
    }
};
function AddClonebtn() {
    var insrow = AddClonebtn.caller.arguments[0].target.closest('.add-row');
    var insrowid = $(insrow).attr('id');
    var addbtn = $('#AddBtn');
    if (insrowid > 0) { addbtn = $('#AddBtn_' + insrowid); }

    var rowid = CloneRowReturningID('tbody1', 'tbody2', $(insrow).attr('id') * 1, true, false);
    var preToDate = $(insrow).find('.todt').val();
    var curFromDate = CustomDateChange(preToDate,0, '/');
    $('#DDSchFromDate_' + rowid).html(curFromDate);
    var SchTourToDate = $('#SchTourToDate').val();
    $('#SchToDate_' + rowid).attr('max', SchTourToDate);
    $('#SchToDate_' + rowid).attr('min', preToDate);
    $('#SchToDate_' + rowid).val('');
    ClearallDropdownData(rowid);
    $('#btnSubmits').makeDisable();
    
    $('#TourCategory_' + rowid).isInvalid();
  
    addbtn.makeDisable();
    //EnableSubmitBtn();
    
};
function EnableAddBtn(tblRow, addBtnBaseID) {
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id')
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.find('.is-invalid').length > 0) { addBtnctrl.makeDisable(); } else { addBtnctrl.makeEnabled(); }
   
};
function ClearallDropdownData(rowid) {
    var ccnamectrls = 'CenterCodeName';
    var bnamectrls = 'BranchCodeName';
    var CCNADiv = 'CentcodeNAdiv';
    var BBNADiv = 'BCNAdiv';
    var BCCtrlDiv = 'BCctrldiv';
    var Cctrldiv = 'CenterDiv';
    var dTourCategory ='TourCategory';

    if (rowid > 0) {
        ccnamectrls = 'CenterCodeName_' + rowid;
        bnamectrls = 'BranchCodeName_' + rowid;
        CCNADiv = 'CentcodeNAdiv_' + rowid;
        BBNADiv = 'BCNAdiv_' + rowid;
        BCCtrlDiv = 'BCctrldiv_' + rowid;
        Cctrldiv = 'CenterDiv_' + rowid;
        dTourCategory = 'TourCategory_' + rowid;
    }

    $('#' + BCCtrlDiv).addClass('inVisible');
    $('#' + Cctrldiv).addClass('inVisible');
    $('#' + CCNADiv).removeClass('inVisible');
    $('#' + BBNADiv).removeClass('inVisible');
    $('#' + CCNADiv).html('NA');
    $('#' + BBNADiv).html('NA');
    $('#' + dTourCategory).multiselect('clearSelection');
    RemoveAllDataFromDropdown(ccnamectrls, bnamectrls);
   
};
function RemoveAllDataFromDropdown(CenterCode, BranchCode) {
   
    getMultiselectData(CenterCode, '/Security/ETS/GetLocationsFromTypes?TypeIDs=' + 0);
    getDropDownData(CenterCode, 'select Center Code', '/Security/ETS/GetLocationsFromType?TypeID=' + 0);
    getMultiselectData(BranchCode, '/Security/ETS/getBranchType?CenterId=' + 0);

}
function removeClonebtn() {
    var tblRow = removeClonebtn.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody2');

};
function Datechange(evt) {
    var CDate = ChangeDateFormat(evt);
    $('.ddSFDate').html(CDate);
    $('#SchTourToDate').makeEnabled();
    $('#SchTourToDate').val('').addClass('is-invalid');
    $('#SchTourToDate').attr('min', evt);
   
};
function SetDatechange(evt) {
    $('#SchToDate').makeEnabled();
    $('#SchToDate').val('').addClass('is-invalid');
    var  SchFromDate=$('#SchFromDate').val();
    $('#SchToDate').attr('min', SchFromDate);
    $('#SchToDate').attr('max', evt);
};
function GetCenterCode() {
    var target = GetCenterCode.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var targetval = $(target).val();
    var targetCtrl = $(target);
    //alert(targetid +'-'+ targetval);
    var rowid = $(target.closest('.add-row')).attr("id");
    var CCnamectrl = 'CenterCodeName';
    var CCCtrldiv = 'CenterDiv';
    var CCNaDiv = 'CentcodeNAdiv';

    var BCnamectrl = 'BranchCodeName';
    var BCCtrlDiv = 'BCctrldiv';
    var BCNaDiv = 'BCNAdiv';


    if (rowid > 0) {
        CCnamectrl = 'CenterCodeName_' + rowid;
        CCCtrldiv = 'CenterDiv_' + rowid;
        CCNaDiv = 'CentcodeNAdiv_' + rowid;

        BCnamectrl = 'BranchCodeName_' + rowid;
        BCCtrlDiv = 'BCctrldiv_' + rowid;
        BCNaDiv = 'BCNAdiv_' + rowid;
    }
    $('#' + CCnamectrl).isValid();
    $('#' + BCnamectrl).isValid();
    $('#' + CCCtrldiv).addClass('inVisible');
    $('#' + BCCtrlDiv).addClass('inVisible');
    $('#' + CCNaDiv).html('').addClass('inVisible');
    $('#' + BCNaDiv).html('').addClass('inVisible');
    var x = '';
    $('#' + targetid + ' option:selected').each(function () {
        x = x + '_' + $(this).val();
    });
    if (targetval == "4") {
        $('#' + CCCtrldiv).removeClass('inVisible');
        $('#' + BCCtrlDiv).removeClass('inVisible');
        $('#' + CCnamectrl).removeAttr('multiple');
        $('#' + CCnamectrl).multiselect('destroy');
        $('#' + CCnamectrl).isInvalid();
        getDropDownData(CCnamectrl, 'select Center Code', '/Security/ETS/GetLocationsFromType?TypeID=' + 4);
    } else if (targetval == "5") {
        $('#' + CCNaDiv).removeClass('inVisible').html('NA');
        $('#' + BCNaDiv).removeClass('inVisible').html('NA');
    } else if (targetval == "3" || targetval == "2" || targetval == "1"|| targetval == "1,3" || targetval == "2,3" || targetval == "3,5") {
        $('#' + CCCtrldiv).removeClass('inVisible');
        $('#' + BCNaDiv).removeClass('inVisible').html('NA');
        $('#' + CCnamectrl).isInvalid();
        getMultiselectData(CCnamectrl, '/Security/ETS/GetLocationsFromTypes?TypeIDs=' + x);

    } else {
        $('#' + CCNaDiv).removeClass('inVisible').html('NA');
        $('#' + BCNaDiv).removeClass('inVisible').html('NA');
    }
    toggleGroupv(target,targetid, targetval);
    
};
function BranchChanges() {

    var target = BranchChanges.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var targetval = $(target).val();
    var rowid = $(target.closest('.add-row')).attr("id");
    debugger;
    var bbnamectrl = 'BranchCodeName';
    var Tournamectrl = 'TourCategory';
    if (rowid > 0) {
        Tournamectrl = 'TourCategory_' + rowid;
        bbnamectrl = 'BranchCodeName_' + rowid;
    }
    var CCCTournamectrl = $('#' + Tournamectrl).val();
    if (CCCTournamectrl == "4") {
        $('#' + bbnamectrl).isInvalid();
        getMultiselectData(bbnamectrl, '/Security/ETS/getBranchType?CenterId=' + targetval);
    }
  
};
function SaveDataClicked() {
    var PersonType = $('#PersonType').val();
    var NoteNumber = $('#NoteNumber').val();
    var AttachFile = $('#AttachFile').val();    
    var TravD = getRecordsFromTableV2('TravdtlTbl');
    var Tourwise = getRecordsFromTableV2('TourWisetbl');
    var x = '{"PersonType":"' + PersonType + '","NoteNumber":"' + NoteNumber + '","AttachFile":"' + AttachFile + '","TravellingDetails":' + TravD + ',"dateTour":' + Tourwise + '}';
    //alert(x);
    $.ajax({
        method: 'POST',
        url: '/ETS/SetTravNTourDetails',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) { 
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Travelling & Date Wise Tour Details Saved Successfully.',
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
                            var url = "/Security/ETS/Create";
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

};
function toggleGroupv(target,multictrlId, value) {
    var idsval = "";
    var rowid = $(target.closest('.add-row')).attr("id");
    var ccnamectrl = 'CenterCodeName';
    var BBnamectrl = 'BranchCodeName';
    if (rowid > 0) {
        ccnamectrl = 'CenterCodeName_' + rowid;
        BBnamectrl = 'BranchCodeName_' + rowid; 
    }
    debugger;
    if (value == "1,2") {
        WarringMsg();
        $('#' + multictrlId).multiselect('clearSelection');
        RemoveAllDataFromDropdown(ccnamectrl, BBnamectrl);
    } else if (value == "1,5") {
        WarringMsg();
        $('#' + multictrlId).multiselect('clearSelection');
        RemoveAllDataFromDropdown(ccnamectrl, BBnamectrl);
    } else if (value == "2,5") {
        WarringMsg();
        $('#' + multictrlId).multiselect('clearSelection');
        RemoveAllDataFromDropdown(ccnamectrl, BBnamectrl);
    } 



   
    
};
function WarringMsg() {
    Swal.fire({
        title: 'Error',
        text: 'This selection not allowed, Please change selected values.',
        icon: 'question',
        customClass: 'swal-wide',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    });
};