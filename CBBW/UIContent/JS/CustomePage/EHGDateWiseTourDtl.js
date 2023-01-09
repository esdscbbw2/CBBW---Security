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
    $('#btnSubmit').makeDisable();
    $('#CenterCode_' + clonerowid).isInvalid();
    addbtn.makeDisable();
};
function ValidateCloneRowCtrl() {
    var target = ValidateCloneRowCtrl.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    if (targetid.indexOf('_') >= 0) { targetid = targetid.split('_')[0] }
    var isvalid = validatectrl(targetid, targetCtrl.val());
    if (isvalid) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    EnableAddBtnInCloneRow(tblRow, 'AddBtn');
    $('#DWBackBtnActive').val(1);
    EnableSubmitBtn();
    
    var todate = new Date($('#TodateStr').val());
    var preToDate = $(tblRow).find('.todt').val();
    var calculatedFromdate =new Date(ChangeDateFormat(CustomDateChange(preToDate, 1, '-')));
    //alert(todate+' - '+calculatedFromdate + ' - ' + todate);
    //var rowid = $('#tblRow').attr('id');
    //if (rowid > 0) { addbtn = $('#AddBtn_' + rowid); }
    if (todate <= calculatedFromdate) {
        $(tblRow).find('.addBtn').makeDisable();
    }
    
};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "ToDate":
            if (value !='') { isvalid = true; }
            break;        
        case "CenterCode":
            if (value != '') { isvalid = true; }
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
                    const r1 = await getMultiselectDataWithSelectedValues(centrecodeCtrl.attr('id'), '/Security/CTV/GetToLocationsFromType?TypeIDs=2&m=0', item.CenterCodes);
                })();                
                addbtnCtrl.makeDisable();
            });
        }
    });
};
function EnableSubmitBtn() {
    var mEnable = false;
    var todate = $('#TodateStr').val();
    $('.todt').each(function () {
        if ($(this).val() == todate) { mEnable = true; }
        //alert(todate + ' - ' + mEnable + ' - ' + $(this).val());
    });
    var x = getDivInvalidCount('HdrDiv');
    //alert(x);
    var SubmitBtn = $('#btnSubmit');
    if (x <= 0) {       
        if (mEnable) { SubmitBtn.makeEnabled(); } else { SubmitBtn.makeDisable(); }        
    } else { SubmitBtn.makeDisable(); }
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
