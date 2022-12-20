function RemoveBtnClicked() {
    var tblRow = RemoveBtnClicked.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody2');
    EnableSubmitBtn();
};
function addCloneBtnClick() {
    var insrow = addCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    var clonerowid = CloneRowReturningID('tbody1', 'tbody2', $(insrow).attr('id') * 1, true, false);
    var preToDate = $(insrow).find('#ToDate').val();
    var curFromDate = CustomDateChange(preToDate, 1, '/');
    $('#FromDateLbl_' + clonerowid).html(curFromDate);
    $('#btnSubmit').makeDisable();
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
                    rowid = CloneRowReturningID('tbody1', 'tbody2', index - 1, true, true);
                    fromdateCtrl = $('#FromDateLbl_' + rowid);
                    todateCtrl = $('#ToDate_' + rowid);
                    lbltodateCtrl = $('#lblToDate_' + rowid);
                    tourcatCtrl = $('#TourCategory_' + rowid);
                    centrecodeCtrl = $('#CenterCode_' + rowid);
                    addbtnCtrl = $('#AddBtn_') + rowid;
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
                //alert(centrecodeCtrl.attr('id'));
                (async function () {
                    const r1 = await getMultiselectDataWithSelectedValues(centrecodeCtrl.attr('id'), '/Security/EHG/GetTourLocations?CategoryID=' + item.TourCatCodes, item.CenterCodes);
                })();
                addbtnCtrl.makeEnabled();
            });
        }
    });
};
function EnableSubmitBtn() {
    var x = getDivInvalidCount('HdrDiv');
    var SubmitBtn = $('#btnSubmit');
    if (x <= 0) { SubmitBtn.makeEnabled(); } else { SubmitBtn.makeDisable(); }
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
