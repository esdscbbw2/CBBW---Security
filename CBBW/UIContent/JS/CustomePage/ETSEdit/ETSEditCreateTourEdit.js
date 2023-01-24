function ValidateEditDateCtrl() {
    var targetCtrl = $(ValidateEditDateCtrl.caller.arguments[0].target);
    if (targetCtrl.val() != '') { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
};
function ValidatePurposeOfEdit() {
    var poeCtrl = $('#PurposeOfEdit');
    if (poeCtrl.val() != '') {
        if (WordCount(poeCtrl.val()) <= 20) {
            poeCtrl.isValid();
        } else { poeCtrl.isInvalid(); }
    } else { poeCtrl.isInvalid(); }
};
function EditTagChanged() {
    var editTagCtrl = $('#EditTag');
    var editTag = editTagCtrl.val();
    if (editTag > 0) {
        editTagCtrl.isValid();
        if (editTag == 1) {
            togleDiv('tour_cancel');
        } else if (editTag == 2) {
            togleDiv('other_edit');
        } else if (editTag == 3) {
            //if ($('#IsExtensionAllowed').val() == 1) {
                togleDiv('tour_extension');
            //}
            //else {
            //    Swal.fire({
            //        title: 'Error',
            //        text: 'Extension Of Tour Can Be Allowed After Commencement & Before Completion.',
            //        icon: 'error',
            //        customClass: 'swal-wide',
            //        buttons: {
            //            confirm: 'Ok'
            //        },
            //        confirmButtonColor: '#2527a2',
            //    });
            //}            
        }
    }
    else { editTagCtrl.isInvalid(); }
    
};
function togleDiv(divID) {
    var tourCancelDiv = $('#tour_cancel');
    var tourExtDiv = $('#tour_extension');
    var tourOtherDiv = $('#other_edit');
    tourCancelDiv.addClass('inVisible');
    tourExtDiv.addClass('inVisible');
    tourOtherDiv.addClass('inVisible');
    $('#' + divID).removeClass('inVisible');
};
function CRTourCategoryChangedReUsable(targetCtrl, tblRowid) {
    var CtrlMulti = 'CRCenterCodeMulti';
    var CtrlDD = 'CRCenterCodeDD';
    if (tblRowid > 0) {
        CtrlMulti = 'CRCenterCodeMulti_' + tblRowid;
        CtrlDD = 'CRCenterCodeDD_' + tblRowid;
    }
    var CatCodes = targetCtrl.val();
    var mValid = true;
    var mText = 'NA';
    if (CatCodes == 6) { mText = '13 / Nizambad'; }
    if (CatCodes.indexOf('1') >= 0 || CatCodes.indexOf('2') >= 0 || CatCodes.indexOf('3') >= 0) {
        if (CatCodes.indexOf('4') >= 0 || CatCodes.indexOf('5') >= 0 || CatCodes.indexOf('6') >= 0) {
            mValid = false;
        }
        else {
            toggleCentreDiv('CRCOMultiDiv', tblRowid, '');
            toggleBranchDiv('CRBranchCodeText', tblRowid, 'NA');
            getMultiselectData(CtrlMulti, '/Security/ETS/GetLocationsFromTypes?TypeIDs=' + CatCodes);
        }
    } else {
        if (CatCodes.length > 1) {
            mValid = false;
        }
        else {
            if (CatCodes == 4) {
                toggleCentreDiv('CRCODDDiv', tblRowid, '');
                toggleBranchDiv('CRBRMultiDiv', tblRowid, '');
                getDropDownData(CtrlDD, 'Select Center Code', '/Security/ETS/GetLocationsFromType?TypeID=' + CatCodes);
            } else {
                toggleCentreDiv('CRCenterCodeText', tblRowid, mText);
                toggleBranchDiv('CRBranchCodeText', tblRowid, 'NA');
            }
        }
    }
    if (!mValid) {
        targetCtrl.multiselect('clearSelection');
        toggleCentreDiv('CRCenterCodeText', tblRowid, 'NA');
        toggleBranchDiv('CRBranchCodeText', tblRowid, 'NA');
        Swal.fire({
            title: 'Error',
            text: 'Invalid Combination Of Tour Category. Only Centre Visit,Branch & Centre Visit,Others Can Be Combined Together.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
        targetCtrl.isInvalid();
    }
    else { targetCtrl.isValid(); }
};
function CRTourCategoryChanged() {
    var target = CRTourCategoryChanged.caller.arguments[0].target;
    var tblRow = $(target.closest('.add-row'));
    var tblRowid = tblRow.attr('id');
    var targetCtrl = $(target);
    CRTourCategoryChangedReUsable(targetCtrl, tblRowid);
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function CRCenterCodeDDChangedReUsable(targetCtrl, tblRowid) {
    var Ctrl1 = 'CRBranchCodeMulti';
    var Ctrl2 = $('#CRCenterCodeMulti');
    var mVal = targetCtrl.val();
    if (tblRowid > 0) {
        Ctrl1 = 'CRBranchCodeMulti_' + tblRowid;
        Ctrl2 = $('#CRCenterCodeMulti' + tblRowid);
    }
    (async function () {
        const r1 = await getMultiselectData(Ctrl1, '/Security/ETS/getBranchType?CenterId=' + mVal);
    })();
    
    if (mVal > 0) {
        targetCtrl.isValid();
        Ctrl2.isValid();
    } else { targetCtrl.isInvalid(); }
};
function CRCenterCodeDDChanged() {
    var target = CRCenterCodeDDChanged.caller.arguments[0].target;
    var tblRow = $(target.closest('.add-row'));
    var tblRowid = tblRow.attr('id');
    var targetCtrl = $(target);
    CRCenterCodeDDChangedReUsable(targetCtrl, tblRowid);
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function CRCenterCodeMultiChangedReUsable(targetCtrl, tblRowid) {
    var Ctrl1 = $('#CRCenterCodeDD');
    if (tblRowid > 0) { Ctrl1 = $('#CRCenterCodeDD_' + tblRowid);}
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
        Ctrl1.isValid();
    } else { targetCtrl.isInvalid(); }
};
function CRCenterCodeMultiChanged() {
    var target = CRCenterCodeMultiChanged.caller.arguments[0].target;
    var tblRow = $(target.closest('.add-row'));
    var tblRowid = tblRow.attr('id');
    var targetCtrl = $(target);
    CRCenterCodeMultiChangedReUsable(targetCtrl, tblRowid);
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function ToDateChanged() {
    var target = ToDateChanged.caller.arguments[0].target;
    var tblRow = $(target.closest('.add-row'));
    var tblRowid = tblRow.attr('id');
    var targetCtrl = $(target);
    if (targetCtrl.val() != '') { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function CRBranchCodeMultiChanged() {
    var target = CRBranchCodeMultiChanged.caller.arguments[0].target;
    var tblRow = $(target.closest('.add-row'));
    var tblRowid = tblRow.attr('id');
    var targetCtrl = $(target);
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
    } else { targetCtrl.isInvalid(); }
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function toggleCentreDiv(divID, rowID,divText) {
    var ccTextCtrl = $('#CRCenterCodeText');
    var ccMulCtrl = $('#CRCOMultiDiv');
    var ccDDCtrl = $('#CRCODDDiv');
    var mCTRL = $('#' + divID );
    if (rowID > 0) {
        ccTextCtrl = $('#CRCenterCodeText_' + rowID);
        ccMulCtrl = $('#CRCOMultiDiv_' + rowID);
        ccDDCtrl = $('#CRCODDDiv_' + rowID);
        mCTRL = $('#' + divID + '_' + rowID);
    }
    //alert(divID + ' - ' + rowID+' - ' +mCTRL.attr('id'));
    ccTextCtrl.addClass('inVisible');
    ccMulCtrl.addClass('inVisible');
    ccDDCtrl.addClass('inVisible');
    mCTRL.removeClass('inVisible');
    ccTextCtrl.html(divText);
};
function toggleBranchDiv(divID, rowID,divText) {
    var ccTextCtrl = $('#CRBranchCodeText');
    var ccMulCtrl = $('#CRBRMultiDiv');
    var Ctrl1 = $('#CRBranchCodeMulti');
    var mCTRL = $('#' + divID);
    if (rowID > 0) {
        ccTextCtrl = $('#CRBranchCodeText_' + rowID);
        ccMulCtrl = $('#CRBRMultiDiv_' + rowID);
        Ctrl1 = $('#CRBranchCodeMulti_' + rowID);
        mCTRL = $('#' + divID + '_' + rowID);
    }
    ccTextCtrl.addClass('inVisible');
    ccMulCtrl.addClass('inVisible');
    mCTRL.removeClass('inVisible');
    ccTextCtrl.html(divText);
    if (divID == 'CRBRMultiDiv') { Ctrl1.isInvalid(); } else { Ctrl1.isValid(); }
};
function EnableSubmitBtn() {

};
function RemoveBtnClicked() {
    var tblRow = RemoveBtnClicked.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody2');
    EnableSubmitBtn();
};
function addCloneBtnClick() {
    var insrow = addCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    var insrowid = $(insrow).attr('id');
    var addbtn = $('#AddBtn');
    if (insrowid > 0) { addbtn = $('#AddBtn_' + insrowid); }
    var clonerowid = CloneRowReturningID('tbody1', 'tbody2', $(insrow).attr('id') * 1, true, false);
    var preToDate = $(insrow).find('.todt').val();
    var curFromDate = CustomDateChange(preToDate, 1, '/');
    $('#FromDateLbl_' + clonerowid).html(curFromDate);
    $('#ToDate_' + clonerowid).isInvalid();
    $('#CRTourCategory_' + clonerowid).isInvalid();
    $('#CRCenterCodeMulti_' + clonerowid).isInvalid();
    $('#CRCenterCodeDD_' + clonerowid).isInvalid();
    $('#CRBranchCodeMulti_' + clonerowid).isInvalid();
    addbtn.makeDisable();
};
$(document).ready(function () {
    (async function () {
        const r1 = await getMultiselectData('CRTourCategory', '/ETSEdit/GetTourCategories');
    })();
});