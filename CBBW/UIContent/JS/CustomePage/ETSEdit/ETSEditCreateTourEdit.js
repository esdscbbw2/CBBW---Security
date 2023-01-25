﻿function ValidateEditDateCtrl() {
    var targetCtrl = $(ValidateEditDateCtrl.caller.arguments[0].target);
    var tblRowid = targetCtrl.attr('id').split('_')[1];
    var ctrl1 = $('#EditTagDiv_' + tblRowid);
    if (targetCtrl.val() != '') {
        targetCtrl.isValid(); ctrl1.html(1);
    } else { targetCtrl.isInvalid(); ctrl1.html(0); }
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
function CRTourCategoryChangedReUsable(targetCtrl, tblRowid,mTag) {
    var CtrlMulti = mTag+'CenterCodeMulti';
    var CtrlDD = mTag+'CenterCodeDD';
    if (tblRowid > 0) {
        CtrlMulti = mTag+'CenterCodeMulti_' + tblRowid;
        CtrlDD = mTag+'CenterCodeDD_' + tblRowid;
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
            toggleCentreDiv(mTag + 'COMultiDiv', tblRowid, '', mTag);
            toggleBranchDiv(mTag + 'BranchCodeText', tblRowid, 'NA', mTag);
            getMultiselectData(CtrlMulti, '/Security/ETS/GetLocationsFromTypes?TypeIDs=' + CatCodes);
        }
    } else {
        if (CatCodes.length > 1) {
            mValid = false;
        }
        else {
            if (CatCodes == 4) {
                toggleCentreDiv(mTag + 'CODDDiv', tblRowid, '', mTag);
                toggleBranchDiv(mTag + 'BRMultiDiv', tblRowid, '', mTag);
                getDropDownData(CtrlDD, 'Select Center Code', '/Security/ETS/GetLocationsFromType?TypeID=' + CatCodes);
            } else {
                toggleCentreDiv(mTag + 'CenterCodeText', tblRowid, mText, mTag);
                toggleBranchDiv(mTag + 'BranchCodeText', tblRowid, 'NA', mTag);
            }
        }
    }
    if (!mValid) {
        targetCtrl.multiselect('clearSelection');
        toggleCentreDiv(mTag + 'CenterCodeText', tblRowid, 'NA', mTag);
        toggleBranchDiv(mTag + 'BranchCodeText', tblRowid, 'NA', mTag);
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
    CRTourCategoryChangedReUsable(targetCtrl, tblRowid,'CR');
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function OETourCategoryChanged() {
    var target = OETourCategoryChanged.caller.arguments[0].target;
    //var tblRow = $(target.closest('.add-row'));    
    var targetCtrl = $(target);
    var tblRowid = targetCtrl.attr('id').split('_')[1];
    CRTourCategoryChangedReUsable(targetCtrl, tblRowid, 'OE');
    var EditTagCtrl = $('#EditTagDivOE_' + tblRowid);
    if (targetCtrl.val() != '') { EditTagCtrl.html(1); } else { EditTagCtrl.html(0); }
};
function CRCenterCodeDDChangedReUsable(targetCtrl, tblRowid,mTag) {
    var Ctrl1 = mTag+'BranchCodeMulti';
    var Ctrl2 = $('#' + mTag+'CenterCodeMulti');
    var mVal = targetCtrl.val();
    if (tblRowid > 0) {
        Ctrl1 = mTag+'BranchCodeMulti_' + tblRowid;
        Ctrl2 = $('#' + mTag+'CenterCodeMulti' + tblRowid);
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
    CRCenterCodeDDChangedReUsable(targetCtrl, tblRowid,'CR');
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function OECenterCodeDDChanged() {
    var target = OECenterCodeDDChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
    var tblRowid = targetCtrl.attr('id').split('_')[1];
    CRCenterCodeDDChangedReUsable(targetCtrl, tblRowid,'OE');
};
function CRCenterCodeMultiChangedReUsable(targetCtrl, tblRowid,mTag) {
    var Ctrl1 = $('#' + mTag+'CenterCodeDD');
    if (tblRowid > 0) { Ctrl1 = $('#' + mTag+'CenterCodeDD_' + tblRowid);}
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
    CRCenterCodeMultiChangedReUsable(targetCtrl, tblRowid,'CR');
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
};
function OECenterCodeMultiChanged() {
    var target = OECenterCodeMultiChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
    var tblRowid = targetCtrl.attr('id').split('_')[1];
    CRCenterCodeMultiChangedReUsable(targetCtrl, tblRowid,'OE');
};
function ToDateChanged() {
    var target = ToDateChanged.caller.arguments[0].target;
    var tblRow = $(target.closest('.add-row'));
    var tblRowid = tblRow.attr('id');
    var targetCtrl = $(target);
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
        //var lbltodtCtrl = $('#lblToDate');
        //var crtodtCtrl = $('#CRTodate');
        //if (tblRowid > 0) {
        //    lbltodtCtrl = $('#lblToDate_' + tblRowid);
        //    crtodtCtrl = $('#CRTodate_' + tblRowid);
        //}
        //crtodtCtrl.html(lbltodtCtrl.val());
    } else { targetCtrl.isInvalid(); }
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
function OEBranchCodeMultiChanged() {
    var target = OEBranchCodeMultiChanged.caller.arguments[0].target;
    var targetCtrl = $(target);
    if (targetCtrl.val() != '') {
        targetCtrl.isValid();
    } else { targetCtrl.isInvalid(); }
};
function toggleCentreDiv(divID, rowID, divText, mTag) {
    var ccTextCtrl = $('#' + mTag+'CenterCodeText');
    var ccMulCtrl = $('#' + mTag+'COMultiDiv');
    var ccDDCtrl = $('#' + mTag+'CODDDiv');
    var mCTRL = $('#' + divID);
    var Ctrl1 = $('#' + mTag + 'CenterCodeMulti');
    var Ctrl2 = $('#' + mTag + 'CenterCodeDD');
    if (rowID > 0) {
        ccTextCtrl = $('#' + mTag+'CenterCodeText_' + rowID);
        ccMulCtrl = $('#' + mTag+'COMultiDiv_' + rowID);
        ccDDCtrl = $('#' + mTag+'CODDDiv_' + rowID);
        mCTRL = $('#' + divID + '_' + rowID);
        Ctrl1 = $('#' + mTag + 'CenterCodeMulti_' + rowID);
        Ctrl2 = $('#' + mTag + 'CenterCodeDD_' + rowID);
    }
    //alert(divID + ' - ' + rowID+' - ' +mCTRL.attr('id'));
    ccTextCtrl.addClass('inVisible');
    ccMulCtrl.addClass('inVisible');
    ccDDCtrl.addClass('inVisible');
    mCTRL.removeClass('inVisible');
    ccTextCtrl.html(divText);
    if (divID == 'OECOMultiDiv' || divID == 'CRCOMultiDiv') {
        Ctrl1.isInvalid();
    } else { Ctrl1.isValid(); }
    if (divID == 'OECODDDiv' || divID == 'CRCODDDiv') {
        Ctrl2.isInvalid();
    } else { Ctrl2.isValid(); }
};
function toggleBranchDiv(divID, rowID, divText, mTag) {
    var ccTextCtrl = $('#' + mTag+'BranchCodeText');
    var ccMulCtrl = $('#' + mTag+'BRMultiDiv');
    var Ctrl1 = $('#' + mTag+'BranchCodeMulti');
    var mCTRL = $('#' + divID);
    if (rowID > 0) {
        ccTextCtrl = $('#' + mTag+'BranchCodeText_' + rowID);
        ccMulCtrl = $('#' + mTag+'BRMultiDiv_' + rowID);
        Ctrl1 = $('#' + mTag+'BranchCodeMulti_' + rowID);
        mCTRL = $('#' + divID + '_' + rowID);
    }
    ccTextCtrl.addClass('inVisible');
    ccMulCtrl.addClass('inVisible');
    mCTRL.removeClass('inVisible');
    ccTextCtrl.html(divText);
    if (divID == mTag+'BRMultiDiv') { Ctrl1.isInvalid(); } else { Ctrl1.isValid(); }
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
    var maxSourceid = ($('#MaxSourceID').val() * 1) + clonerowid;
    $('#SourceIDDiv_' + clonerowid).html(maxSourceid);
    $('#CREditTagDiv_' + clonerowid).html(1);
    //$('#CRTodate_' + clonerowid).html('-');
};
function btnSubmitClicked() {
    var editTag = $('#EditTag').val();
    var editPurpose = $('#PurposeOfEdit').val();
    var notenumber = $('#NoteNumber').val();
    var tblRecords = '';
    if (editTag == 1) {
        tblRecords = getRecordsFromTableV2('tblTourCancel');
    } else if (editTag == 2) {
        tblRecords = getRecordsFromTableV2('tblOtherEdit');
    } else if (editTag == 3) {
        tblRecords = getRecordsFromTableV2('tblTourExtension');
    }
    var x = '{"NoteNumber":"' + notenumber
        + '","EditTag":"' + editTag
        + '","ReasonForEdit":"' + editPurpose
        + '","DWTDetails":'
        + tblRecords + '}';
    alert(tblRecords);
    $.ajax({
        method: 'POST',
        url: '/ETSEdit/SetDWTForTourEdit',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    var url = "/Security/ETSEdit/Create";
                    window.location.href = url;
                } else {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Failed To Save Date Wise Tour Details.',
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
$(document).ready(function () {
    (async function () {
        const r1 = await getMultiselectData('CRTourCategory', '/ETSEdit/GetTourCategories');
    })();
});