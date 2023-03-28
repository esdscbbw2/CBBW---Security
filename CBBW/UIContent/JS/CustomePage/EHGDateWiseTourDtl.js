
function EnableAddBtnInCloneRowIfOnlyLastV2(tblRow, addBtnBaseID) {
    //If The Add Button Is Exist In The Last Row Then Only Enable 
    var mTodate = $('#TodateStr').val();
    var tDateCtrl = $('#ToDate');
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id');
    if (rowid != 0) {
        addBtnBaseID = addBtnBaseID + '_' + rowid;
        tDateCtrl = $('#ToDate_' + rowid);
    }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.is(":last-child")) {
        if (tblrow.find('.is-invalid').length > 0) {
            addBtnctrl.makeDisable();
        } else {
            if (mTodate == tDateCtrl.val()) {
                addBtnctrl.makeDisable();
            } else { addBtnctrl.makeEnabled(); }
        }
    }
    else { addBtnctrl.makeDisable(); }

    //alert(tblrow.find('.is-invalid').length);
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
    var clonerowid = CloneRowReturningID('tbody1', 'tbody2', $(insrow).attr('id') * 1, false, false);
    var preToDate = $(insrow).find('.todt').val();
    var curFromDate = CustomDateChange(preToDate, 1, '/');
    $('#FromDateLbl_' + clonerowid).html(curFromDate);
    $('#btnSubmit').makeDisable();
    $('#CenterCode_' + clonerowid).isInvalid();
    $('#ToDate_' + clonerowid).isInvalid();
    addbtn.tooltip('hide');
    addbtn.makeDisable();
};
function ValidateCloneRowCtrl() {
    var target = ValidateCloneRowCtrl.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    if (targetid.indexOf('_') >= 0) { targetid = targetid.split('_')[0] }
    var isvalid = validatectrl(targetid, targetCtrl.val(), $(tblRow).attr('id'));
    if (isvalid) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }    
    $('#DWBackBtnActive').val(1);
    if (targetid == 'ToDate') {
        RestRowsDeleted('table1', $(tblRow).attr('id'));
        var todate = new Date($('#TodateStr').val());
        var preToDate = $(tblRow).find('.todt').val();
        var calculatedFromdate = new Date(ChangeDateFormat(CustomDateChange(preToDate, 1, '-')));
        if (todate <= calculatedFromdate) {
            $(tblRow).find('.addBtn').makeDisable();
        }
    }
    EnableAddBtnInCloneRowIfOnlyLastV2(tblRow, 'AddBtn');
    EnableSubmitBtn();
};
function validatectrl(targetid, value,rowid) {
    var isvalid = false;
    switch (targetid) {
        case "ToDate":
            if (value != '') {
                var fromdateCtrl = $('#FromDateLbl');
                if (rowid > 0) { fromdateCtrl = $('#FromDateLbl_' + rowid); }
                if (CompareDate(fromdateCtrl.html(), 0, value, 1)) {
                    isvalid = true;
                }
                else {
                    Swal.fire({
                        title: 'Invalid Date Range!',
                        text: 'To Date Must Be Greater Or Equal To From Date.',
                        icon: 'error',
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    });
                    isvalid = false;
                }
            }
            break;        
        case "CenterCode":
            if (value != '') {
                if ($('#ehgHeader_CenterCode').val() == value) {
                    Swal.fire({
                        title: 'Invalid Centre Code!',
                        text: 'Cannot Use Screen Initiated Centre Code',
                        icon: 'error',
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    });
                }
                else { isvalid = true; }                
            }
            break;
        case "PurposeOfVisitFoeMang":
            if (value != '') { isvalid = true; }
            break;
    }
    return isvalid;
};
async function getInitialData() {
    var notenumber = $('#ehgHeader_NoteNumber').val();
    var rowid;    
    var fromdateCtrl = $('#FromDateLbl');
    var todateCtrl = $('#ToDate');
    var lbltodateCtrl = $('#lblToDate');
    var tourcatCtrl = $('#TourCategory');
    var centrecodeCtrl = $('#CenterCode');
    var addbtnCtrl = $('#AddBtn');
    $.ajax({
        url: '/EHG/GetDWTDetails',
        method: 'GET',
        data: { NoteNumber: notenumber, isActive:0 },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                if (index > 0) {
                    rowid = CloneRowReturningID('tbody1', 'tbody2', index - 1, true, false);
                    fromdateCtrl = $('#FromDateLbl_' + rowid);
                    todateCtrl = $('#ToDate_' + rowid);
                    lbltodateCtrl = $('#lblToDate_' + rowid);
                    tourcatCtrl = $('#TourCategory_' + rowid);
                    centrecodeCtrl = $('#CenterCode_' + rowid);
                    addbtnCtrl = $('#AddBtn_' + rowid) ;
                }
                fromdateCtrl.html(item.FromDateStrDisplay);
                todateCtrl.val(item.ToDateStr).isValid();
                lbltodateCtrl.html(item.ToDateStrDisplay);
                if (item.TourCatCodes.indexOf(',') > 0) {
                    tourcatCtrl.val(item.TourCatCodes.split(',')).multiselect('refresh');
                }
                else {
                    tourcatCtrl.val(item.TourCatCodes).multiselect('refresh');
                }
                tourcatCtrl.isValid();                
                (async function () {
                    const r1 = await getMultiselectDataWithSelectedValues(centrecodeCtrl.attr('id'), '/Security/EHG/GetTourLocations?CategoryIDs=', item.CenterCodes);
                })();
                centrecodeCtrl.isValid();
                addbtnCtrl.makeDisable();
            });
        }
    });
};
function EnableSubmitBtn() {
    var mEnable = false;
    var todate = $('#TodateStr').val();
    var SubmitBtn = $('#btnSubmit');
    var x = getDivInvalidCount('HdrDiv');
    $('.todt').each(function () {
        if ($(this).val() == todate) { mEnable = true; }
    });    
    if (x <= 0) {       
        if (mEnable) {
            SubmitBtn.makeEnabled();
        }
        else {
            SubmitBtn.makeDisable();
        }
    }
    else { SubmitBtn.makeDisable(); }
};
$(document).ready(function () {
    $('#btnBack').click(function () {
        var backbtnactive = $('#DWBackBtnActive').val();
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
$(document).ready(function () {
    (async function () {
        const r1 = await getMultiselectData('TourCategory', '/EHG/GetTourCategories');
    })();
});
$(document).ready(function () {
    $('#FromDateLbl').html($('#FromdateStrForDisplay').val());
    (async function () {
        const r1 = await getInitialData();
    })();
    
});
