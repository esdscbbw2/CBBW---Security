﻿function AddClonebtn() {
    var insrow = AddClonebtn.caller.arguments[0].target.closest('.add-row');
    var insrowid = $(insrow).attr('id');
    var addbtn = $('#AddBtn');
    var checklist = $('#CheckList');
    var EmployeeCodes = $('#EmployeeCodes');
    var RefNoteNumbers = $('#RefNoteNumbers');
    var CenterCodeName = $('#CenterCodeName');
    var PersonTypetxt = $('#PersonTypetxt');
    var EmployeeCodeName = $('#EmployeeCodeName');
    var DesigCodeName = $('#DesigCodeName');
    var TourFromDate = $('#TourFromDate');
    var TourToDate = $('#TourToDate');
    var NoOfDays = $('#NoOfDays');
    var PurposeOfVisit = $('#PurposeOfVisit');
    var rowid = CloneRowReturningID('tbody1', 'tbody2', $(insrow).attr('id') * 1, true, false);

    if (rowid > 0) {
        addbtn = $('#AddBtn_' + rowid);
        checklist = $('#CheckList_' + rowid);
        EmployeeCodes = $('#EmployeeCodes_' + rowid);
        RefNoteNumbers = $('#RefNoteNumbers_' + rowid);
        CenterCodeName = $('#CenterCodeName_' + rowid);
        PersonTypetxt = $('#PersonTypetxt_' + rowid);
        EmployeeCodeName = $('#EmployeeCodeName_' + rowid);
        DesigCodeName = $('#DesigCodeName_' + rowid);
        TourFromDate = $('#TourFromDate_' + rowid);
        TourToDate = $('#TourToDate_' + rowid);
        NoOfDays = $('#NoOfDays_' + rowid);
        PurposeOfVisit = $('#PurposeOfVisit_' + rowid);
    }
    EmployeeCodes.html('');
    RefNoteNumbers.html('');
    CenterCodeName.html('');
    PersonTypetxt.html('');
    EmployeeCodeName.html('');
    DesigCodeName.html('');
    TourFromDate.html('');
    TourToDate.html('');
    NoOfDays.html('');
    PurposeOfVisit.html('');


    checklist.prop('checked', false);
    checklist.removeClass('selected-row');
    $('.CheckList').prop('checked', false);
    $('#ExpensesDetailsDiv').addClass('inVisible');
    addbtn.makeDisable();
    checklist.makeDisable();

}
function removeClonebtn() {
    var tblRow = removeClonebtn.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody2');
    EnableAddBtn(tblRow, 'AddBtn', 'CheckList');
    $('#ExpensesDetailsDiv').addClass('inVisible');

};
function EnableAddBtn(tblRow, addBtnBaseID, CheckId) {
    debugger;
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id')
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; CheckId = CheckId + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    var CheckId = $('#' + CheckId);
    if (tblrow.find('.is-invalid').length > 0) { addBtnctrl.makeDisable(); CheckId.makeDisable(); } else { addBtnctrl.makeEnabled(); CheckId.makeEnabled(); }


};
$(document).ready(function () {
    (async function () {
        const r1 = await getDropDownDataWithSelectedValueWithColor('NoteNumbers', 'select NoteNumber', '/Security/BIL/GetBILPaymentNoteNumberList', 0);;
    })();
});
function VisibleRows() {
    $('#SubmitCount').val(0);
    $('#BtnSubmit').makeDisable();
    $('#SubmitBtn').makeDisable();
    var ExpensesDetailsDiv = $('#ExpensesDetailsDiv');
    var rowid = 0;
    var targetCtrl = $(VisibleRows.caller.arguments[0].target);
    var insrow = targetCtrl.closest('.add-row');
    rowid = $(insrow).attr('id');
    var EmployeeCode = $('#EmployeeCodes');
    var RefNoteNumber = $('#RefNoteNumbers');
    var NoteNo = $('#NoteNumbers');
    if (rowid > 0) {
        NoteNo = $('#NoteNumbers_' + rowid);
        EmployeeCode = $('#EmployeeCodes_' + rowid);
        RefNoteNumber = $('#RefNoteNumbers_' + rowid);
    }


    $('#NoteNumber').val($.trim(NoteNo.val()));
    $('#EmployeeNo').val(EmployeeCode.html());
    $('#RefNoteNumber').val($.trim(RefNoteNumber.html()));
    ExpensesDetailsDiv.addClass('inVisible');

    if ($.trim(NoteNo.val()) != '' && $.trim(NoteNo.val()) != '-1') {
        var dataSourceURL = '/BIL/PaymentExpensesDetails?NoteNumber=' + $.trim(NoteNo.val());
        $.ajax({
            url: dataSourceURL,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                ExpensesDetailsDiv.removeClass('inVisible');
                ExpensesDetailsDiv.html(result);
            },
            error: function (xhr, status) {
                ExpensesDetailsDiv.html(xhr.responseText);
            }
        })
    } else {
        Swal.fire({
            title: 'Error',
            text: 'Please Select Bill No.',
            icon: 'question',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });

    }
};
$('#btnBacks').click(function () {
    var url = "/Security/BIL/PaymentIndex";
    if ($('#NoteNumber').val() != "") {
        Swal.fire({
            title: 'Confirmation Message',
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

                window.location.href = url;
            }

        }
    } else {

        window.location.href = url;
    };
});
$('#clrbtn').click(function () {
    var url = "/Security/BIL/PaymentCreate";
    window.location.href = url;
});
$(document).ready(function () {
    $('#btnTourRule').click(function () {
        var url = "/Security/TourRule/ViewRedirection?CBUID=1";
        window.open(url);
    });
});
function ChangeNoteNumber() {
    $('#ExpensesDetailsDiv').addClass('inVisible');
    var rowid = 0;
    var targetCtrl = $(ChangeNoteNumber.caller.arguments[0].target);
    var insrow = targetCtrl.closest('.add-row');
    rowid = $(insrow).attr('id');
    var mID = targetCtrl.attr('id');
    $('.CheckList').prop('checked', false);
    $('.CheckList').removeClass('selected-row');
    var TargetVal = $(targetCtrl).val();

    var EmployeeCode = $('#EmployeeCodes');
    var RefNoteNumber = $('#RefNoteNumbers');
    var CenterCodeName = $('#CenterCodeName');
    var PersonTypetxt = $('#PersonTypetxt');
    var EmployeeCodeName = $('#EmployeeCodeName');
    var DesigCodeName = $('#DesigCodeName');
    var TourFromDate = $('#TourFromDate');
    var TourToDate = $('#TourToDate');
    var NoOfDays = $('#NoOfDays');
    var PurposeOfVisit = $('#PurposeOfVisit');

    var dstat = 0;
    $('.xPerson').each(function () {
        if (TargetVal != '' && $(this).val() == TargetVal) { dstat += 1; }
        //alert(mValue + ' - ' + $(this).val() + ' - ' + dstat);
    });
    if (dstat > 1) {

        $(targetCtrl).val('');
        $(targetCtrl).isInvalid();
        Swal.fire({
            title: 'Data Duplicacy Error',
            text: 'Bill No You Have Selected Is Already Taken.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    } else {
        $.ajax({
            url: '/BIL/GetTADABillGenerationDataHdr',
            method: 'GET',
            data: { NoteNumber: TargetVal },
            dataType: 'json',
            success: function (data) {

                $(data).each(function (index, item) {

                    if (rowid > 0) {
                        EmployeeCode = $('#EmployeeCodes_' + rowid);
                        RefNoteNumber = $('#RefNoteNumbers_' + rowid);
                        CenterCodeName = $('#CenterCodeName_' + rowid);
                        PersonTypetxt = $('#PersonTypetxt_' + rowid);
                        EmployeeCodeName = $('#EmployeeCodeName_' + rowid);
                        DesigCodeName = $('#DesigCodeName_' + rowid);
                        TourFromDate = $('#TourFromDate_' + rowid);
                        TourToDate = $('#TourToDate_' + rowid);
                        NoOfDays = $('#NoOfDays_' + rowid);
                        PurposeOfVisit = $('#PurposeOfVisit_' + rowid);

                    }

                    EmployeeCode.html(item.EmployeeNo);
                    RefNoteNumber.html(item.RefNoteNumber);
                    CenterCodeName.html(item.CenterCodeName);
                    PersonTypetxt.html(item.PersonTypetxt);
                    EmployeeCodeName.html(item.EmployeeCodeName);
                    DesigCodeName.html(item.DesigCodeName);
                    TourFromDate.html(item.TourFromDateNTime + "-" + item.TourFromTime);
                    TourToDate.html(item.TourToDateNTime + "-" + item.TourToTime);
                    NoOfDays.html(item.NoOfDays);
                    PurposeOfVisit.html(item.PurposeOfVisit);


                });
            },
            error: function (xhr, status) {
                alert(xhr.responseText);
            }
        });
    }


};
function TotalExpance() {
    var target = TotalExpance.caller.arguments[0].target;
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    var TotalExpenses = $('#ETotalAmount').val();
    if (targetCtrl.val() != 0 && targetCtrl.val() != "") {
        var Total = parseInt(TotalExpenses) + parseInt(targetCtrl.val());
        $('#ETotalAmount').val(Total);
    }
};
function TPDBtnClicked() {
    $('#DFAbtnSubmit').makeDisable();
    var notenumber = $('#NoteNumber').val();
    var EmployeeNo = $('#EmployeeNo').val();
    var NoteNumber = $('#RefNoteNumber').val();
    var ETotalAmount = $('#ETotalAmount').val();
    if (notenumber != '') {
        var iDiv = $('#TPDModalDiv');
        var modalDiv = $('#TPDModal');
        var dataSourceURL = '/BIL/VPytmDeductionsFromDA?EmployeeNo=' + EmployeeNo + '&RefNoteNumber=' + NoteNumber + '&NoteNumber=' + notenumber + '&ETotalAmount=' + ETotalAmount;
        $.ajax({
            url: dataSourceURL,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                iDiv.html(result);
                modalDiv.modal('show');
                var Deptcode = $('#TadabollGen_DeptCode');
                var RequisitionNo = $('#TadabollGen_RequisitionNo');
                var RequisitionDate = $('#TadabollGen_RequisitionDate');
                var PreparedEmpNo = $('#PreparedEmpNo');
                var PreparedEmpNoDD = $('#TadabollGen_PreparedEmpNo');
                var RequisitionAmt = $('#TadabollGen_RequisitionAmt');
                var Remark = $('#TadabollGen_Remark');
                var Datestr = $('#TadabollGen_RequisitionDatestr');
                Deptcode.val() > 0 ? Deptcode.isValid() : Deptcode.isInvalid();
                RequisitionNo.val() > 0 ? RequisitionNo.isValid() : RequisitionNo.isInvalid();
                if (Datestr.val() != '01/01/0001') {
                    $('#TadabollGen_RequisitionDatelbl').html(Datestr.val());
                    $('#TadabollGen_RequisitionDate').val($('#TadabollGen_RequisitionDatestrDisplay').val());
                }
                Datestr.val() != '01/01/0001'? RequisitionDate.isValid() : RequisitionDate.isInvalid();
                if (PreparedEmpNo.val() > 0) {
                    GetEmp(Deptcode.val(), PreparedEmpNo.val());
                }
                PreparedEmpNo.val() > 0 ? PreparedEmpNoDD.isValid() : PreparedEmpNoDD.isInvalid();
                RequisitionAmt.val() > 0 ? RequisitionAmt.isValid() : RequisitionAmt.isInvalid();
                RequisitionAmt.attr('min', 0);
                RequisitionAmt.attr('max', ETotalAmount * 1);
                EnableSubmitBtn();
            },
            error: function (xhr, status) {

            }
        })
    }
    else {
        Swal.fire({
            title: 'Error',
            text: 'Select A NoteNumber To Deductions From DA Detail.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }
};
function GetEmp(Deptid, employeeno) {
    getDropDownDataWithSelectedValueWithColor('TadabollGen_PreparedEmpNo', 'Select Employee', '/Security/BIL/GetDeptWiseEmployee?DeptId=' + Deptid, employeeno);
};
function TDBtnClicked() {
    var notenumber = $('#NoteNumber').val();
    var EmployeeNo = $('#EmployeeNo').val();
    var NoteNumber = $('#RefNoteNumber').val();
    var SubmitCount = $('#SubmitCount');
    var submitval = SubmitCount.val();
    if (notenumber != '') {
        var iDiv = $('#TDModalDiv');
        var modalDiv = $('#TDModal');
        var dataSourceURL = '/BIL/PytmTravellingDetails?EmployeeNo=' + EmployeeNo + '&RefNoteNumber=' + NoteNumber + '&NoteNumber=' + notenumber;
        $.ajax({
            url: dataSourceURL,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                submitval = submitval + 1;
                SubmitCount.val(submitval);
                iDiv.html(result);
                modalDiv.modal('show');

            },
            error: function (xhr, status) {

            }
        })
    }
    else {
        Swal.fire({
            title: 'Error',
            text: 'Select A NoteNumber To Deductions From DA Detail.',
            icon: 'error',
            customClass: 'swal-wide',
            buttons: {
                confirm: 'Ok'
            },
            confirmButtonColor: '#2527a2',
        });
    }
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


    EnableSubmitBtn();
    EnablDeductionDAeSavebtn();

};
function ValidateCloneRowCtrl() {
    debugger;
    var target = ValidateCloneRowCtrl.caller.arguments[0].target;
    var tblRow = target.closest('.add-row');
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    if (targetid.indexOf('_') >= 0) { targetid = targetid.split('_')[0] }
    var isvalid = validatectrl(targetid, targetCtrl.val());
    if (isvalid) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    EnableAddBtn(tblRow, 'AddBtn', 'CheckList');

};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "NoteNumbers":
            isvalid = validatectrl_ValidatestringLength(value);
            break;

        case "EReamrk":
            isvalid = validatectrl_ValidatestringLength(value);
            break;

        case "TadabollGen_DeptCode":
            isvalid = validatectrl_ValidatestringLength(value);
            break;
        case "TadabollGen_RequisitionNo":
            if (value >= 0 && value.length <= 5) {
                isvalid = true;
            }
            break;
        case "TadabollGen_RequisitionDate":
            isvalid = validatectrl_ValidateLength(value);
            break;
        case "TadabollGen_PreparedEmpNo":
            isvalid = validatectrl_ValidatestringLength(value);
            break;
        case "TadabollGen_RequisitionAmt":
            //isvalid = validatectrl_ValidateLength(value);
            var ETotalAmount = $('#TadabollGen_ETotalAmount').val() * 1;
            if ((value * 1) <= ETotalAmount) {
                isvalid = true;
            } else {
                isvalid = false;
            }

            break;
        case "TadabollGen_Remark":
            if (value.length > 1 && WordCount(value) <= 50) { isvalid = true; $(targetid).off('keypress');}
            else {
               
                targetid.preventTypying();
            }
            break;
        case "Checked":
            isvalid = validatectrl_ValidatestringLength(value);
            if (isvalid) {
                $('.content').removeClass('border-red').addClass('border-green');
            }

            break;

    }
   
    return isvalid;
};
function validatectrl_YesNoComboApproval(value) {

    if (value * 1 >= 0) {
        return true;
    } else { return false; }
};
function validatectrl_ValidatestringLength(value) {
    if (value != "-1") {
        return true;
    } else { return false; }
};
function validatectrl_ValidateLength(value) {
    debugger;
    if (value.length > 0) {
        return true;
    } else { return false; }
};
function validatectrl_YesNoCombo(value) {

    if (value * 1 > 0) {
        return true;
    } else { return false; }
};
function EnableSubmitBtn() {
    var z = getDivInvalidCount('AllData');
    var btn = $('#SubmitCount').val();
    var SubmitBtn = $('#SubmitBtn');
    if (z <= 0 && btn >= 2) {
        SubmitBtn.makeEnabled();
    }
};
function SaveDetails() {
    var notenumber = $('#NoteNumber').val();
    var EEDAmount = $('#EEDAmount').val();
    var ETAAmount = $('#ETAAmount').val();
    var ELocAmount = $('#ELocAmount').val();
    var ELodAmount = $('#ELodAmount').val();
    var ETotalAmount = $('#ETotalAmount').val();
    var EReamrk = $('#EReamrk').val();
    var status = 3;

    var x = '{"NoteNumber":"' + notenumber + '","EEDAmount":"' + EEDAmount + '","ETAAmount":"' + ETAAmount + '","ELocAmount":"' + ELocAmount + '","ELodAmount":"' + ELodAmount + '","ETotalAmount":"' + ETotalAmount + '","EReamrk":"' + EReamrk + '","status":"' + status + '"}';
    $.ajax({
        method: 'POST',
        url: '/BIL/PaymentCreate',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    $('#SubmitCount').val(0);

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
                            $('#BtnSubmit').makeEnabled();
                            //var url = "/Security/EMN/Index"
                            //window.location.href = url;
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
function FinalSubmit() {
    var status = 2;
    var schrecords = getRecordsFromTableV2('BILTable');
    var x = '{"status":"' + status + '","NoteList":' + schrecords + '}';

    $.ajax({
        method: 'POST',
        url: '/BIL/PaymentCreate',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Bill No Payment Process saved successfully.',
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
                            var url = "/Security/BIL/PaymentIndex";
                            window.location.href = url;
                        }
                    }
                }
                else {
                    Swal.fire({
                        title: 'Error',
                        text: 'Failed To Update Payment Details.',
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
function SaveRequisitionDetails() {
    var notenumber = $('#TadabollGen_NoteNumber').val();
    var RefNoteNumber = $('#TadabollGen_RefNoteNumber').val();
    var DeptCode = $('#TadabollGen_DeptCode').val();
    var RequisitionNo = $('#TadabollGen_RequisitionNo').val();
    var RequisitionDate = $('#TadabollGen_RequisitionDate').val();
    var PreparedEmpNo = $('#TadabollGen_PreparedEmpNo').val();
    var RequisitionAmt = $('#TadabollGen_RequisitionAmt').val();
    var Remark = $('#TadabollGen_Remark').val();
    var status = 1;
    var SubmitCount = $('#SubmitCount');
    var submitval = SubmitCount.val();
    var x = '{"NoteNumber":"' + notenumber + '","RefNoteNumber":"' + RefNoteNumber + '","DeptCode":"' + DeptCode + '","RequisitionNo":"' + RequisitionNo + '","RequisitionDate":"' + RequisitionDate + '","PreparedEmpNo":"' + PreparedEmpNo + '","RequisitionAmt":"' + RequisitionAmt + '","Remark":"' + Remark + '","status":"' + status + '"}';

    $.ajax({
        method: 'POST',
        url: '/BIL/SetDeductionFromData',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    submitval = submitval + 1;
                    SubmitCount.val(submitval);

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
                            $("#TPDModal").modal('hide');

                            //var url = "/Security/EMN/Index"
                            //window.location.href = url;
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
function EnablDeductionDAeSavebtn() {
    var z = getDivInvalidCount('DADection');
    var btn = $('#SubmitCount').val();
    var SubmitBtn = $('#DFAbtnSubmit');
    if ((z * 1) <= 0) {
        SubmitBtn.makeEnabled();
    } else {
        SubmitBtn.makeDisable();
    }
}
function decrementQty(e) {
    debugger;
    var value = e.parentElement.parentElement.firstElementChild.value;
    value = value.trim().replace(/\,/g, '');
    value = isNaN(value) ? 1 : value;
    value--;

    value = value < 0 ? 0 : value;
    e.parentElement.parentElement.firstElementChild.value = value;
    TotalExp();
}
function AEDincrementQty() {
    var target = $(AEDincrementQty.caller.arguments[0].target);
    var mid = target.attr('id');
    var maxvalue = $('#' + mid + 'Max').html() * 1;
    var txtcTrl = $('#' + mid + 'Amount');
    var mValue = txtcTrl.val() * 1;
    //alert(mValue + '' + maxvalue);
    mValue += 1;
    mValue = mValue > maxvalue ? maxvalue : mValue;
    txtcTrl.val(mValue);
    TotalExp();
};
function TotalExp() {
    var edamt = $('#EEDAmount').val() * 1;
    var atamt = $('#ETAAmount').val() * 1;
    var alamt = $('#ELocAmount').val() * 1;
    var aloamt = $('#ELodAmount').val() * 1;
    var total = edamt + atamt + alamt + aloamt;
    var Atotal = isNaN(total) ? 0 : total;
    $('#ETotalAmount').val(Atotal);
};
//function isNumber(evt) {
//    var target = isNumber.caller.arguments[0].target;
//    var targetCtrl = $(target).val() * 1;
//    var ETotalAmount = $('#TadabollGen_ETotalAmount').val()*1;
//    if (targetCtrl > ETotalAmount) {
//        $('#TadabollGen_RequisitionAmt').preventTypying();
//    } else {
//        $('#TadabollGen_RequisitionAmt').off('keypress');
//    }
   
//}
$('.numbers').keypress(function (e) {
    if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
});
function keypressCountWord(e) {
    var target = keypressCountWord.caller.arguments[0].target;
    var targetCtrl = $(target).val();
    if (targetCtrl.length > 1 && WordCount(targetCtrl) >= 50) {
        $(target).preventTypying();
    } else {
        $(target).off('keypress');
    }
}








