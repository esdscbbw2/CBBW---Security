$(document).ready(function () {
    $('#EmployeeNo').isInvalid();
    $('#RefNoteNumber').isInvalid();
    $('#RefEntryDate').val('');
    var selectedvalue = 0;
    $('#RefNoteNumber').change(function () {

        Notenumberchanged($(this).val(), selectedvalue);
        $('#SubmitBtn').makeDisable();
    });
    var btnDisplays = $('#SubmitCount').val();
    
    if (btnDisplays == 1) {
        if ($('#EmpNo').val() != 0) {
        Notenumberchanged($('#RefNoteNumber').val(), $('#EmpNo').val());
            ChangeEmployee($('#EmpNo').val());
        }
       // $('#Save').makeEnabled();
       // $('#RefNoteNumber').makeDisable();
       // $('#EmployeeNo').makeDisable();

    } else {
        //$('#Save').makeDisable();
      // $('#RefNoteNumber').makeEnabled();
       // $('#EmployeeNo').makeEnabled();
        
    }
});
function Notenumberchanged(notenumber, selectedvalue) {
   
    var noteCtrl = $('#RefNoteNumber');
    $('#EmployeeNo').isInvalid();
    
    if (notenumber != '') {
        noteCtrl.isValid();
        $.ajax({
            url: '/BIL/GetNoteHdr',
            method: 'GET',
            data: { Notenumber: notenumber },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    $('#RefEntryDatestr').val(item.RefEntryDatestr);
                    $('#RefEntryDate').val(item.RefEntryDatestr);
                    $('#RefEntryTime').val(item.RefEntryTime);
                    (async function () {
                        const r1 = await GetEmployeeList(notenumber, selectedvalue);
                    })();
                });
            }
        });
    } else { noteCtrl.isInvalid(); }
};
async function GetEmployeeList(notenumber, selectedvalue) {
    getDropDownDataWithSelectedValueWithColor('EmployeeNo', 'Select Employee', '/Security/BIL/GetEmployeeList?NoteNumber=' + notenumber, selectedvalue);

};
$('#EmployeeNo').change(function () {
    $('#EmployeeCodeName').val($("#EmployeeNo option:selected").text())
    ChangeEmployee($(this).val());
    $('#SubmitBtn').makeDisable();
});
function ChangeEmployee(EmployeeNo) {
    $('#EmpNo').val(EmployeeNo);
    var noteCtrl = $('#EmployeeNo');
    var NoteNumber = $('#RefNoteNumber').val();
    
    if (EmployeeNo != '' && EmployeeNo != '-1') {
        var Active = GetInActiveInDD('EmployeeNo');
       // alert(Active);
        if (Active) {
            $('#SubmitBtn').makeEnabled();
        }
        noteCtrl.isValid();
        $.ajax({
            url: '/BIL/GetTAdARuleData',
            method: 'GET',
            data: { EmployeeNumber: EmployeeNo, NoteNumber: NoteNumber },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                   
                    var date = new Date(parseInt(item.ActualTourInDate.substr(6)));
                    
                    if (date.getFullYear() != 0001) {
                        $('#NoteNumber').val(item.NoteNumber);
                       
                    if (item.status == 1) {
                        $('#PersonTypetxt').val(item.PersonType);
                        $('#DesigCodeName').val(item.DesginationCodeName);
                        $('#DAAmount').val(item.DAAmount);
                        $('#DADeducted').val(item.DADeducted);
                        $('#EDAllowance').val(item.EAmount);
                        $('#TourFromDateNTime').val(item.ActualTourInDatestr);
                        $('#TourToDateNTime').val(item.ActualTourOutDatestr);
                        $('#TourFromDate').val(item.ActualTourInDatestr);
                        $('#TourToDate').val(item.ActualTourOutDatestr);
                        $('#NoOfDays').val(item.TotalNoOfDays);
                        $('#PurposeOfVisit').val(item.PurposeOfVisit);
                        $('#TotalExpenses').val(item.EAmount);
                        $('#Lodging').attr('max', item.MaxLodgingExp);
                        $('#LocalConveyance').attr('max', item.MaxLocalConv);
                        $('#TAAmount').val(0);
                        $('#LocalConveyance').val(0);
                        $('#Lodging').val(0);
                    }
                    else if (item.status == 2) {
                        
                        $('#PersonTypetxt').val(item.PersonType);
                        $('#DesigCodeName').val(item.DesginationCodeName);
                        $('#DAAmount').val(item.DAAmount);
                        $('#DADeducted').val(item.DADeducted);
                        $('#EDAllowance').val(item.EAmount);
                        $('#TourFromDateNTime').val(item.ActualTourInDatestr);
                        $('#TourToDateNTime').val(item.ActualTourOutDatestr);
                        $('#TourFromDate').val(item.ActualTourInDatestr);
                        $('#TourToDate').val(item.ActualTourOutDatestr);
                        $('#NoOfDays').val(item.TotalNoOfDays);
                        $('#PurposeOfVisit').val(item.PurposeOfVisit);
                        $('#TotalExpenses').val(item.TotalExpenses);
                        $('#TAAmount').val(item.TAAmount);
                        $('#LocalConveyance').val(item.LocalConveyance);
                        $('#Lodging').val(item.Lodging);
                    }
                        $('#Deducted').makeEnabled();
                    }
                });
            }
        });


    } else { noteCtrl.isInvalid(); }
}
function TotalExpance() {
    debugger;
    var target = TotalExpance.caller.arguments[0].target;
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    var TotalExpenses = $('#TotalExpenses').val();
    if (targetCtrl.val() != 0 && targetCtrl.val() != "") {
    var Total = parseInt(TotalExpenses) + parseInt(targetCtrl.val());
        $('#TotalExpenses').val(Total);
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

};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "Policy":
            isvalid = validatectrl_YesNoCombo(value);
            if (isvalid) { $('#Policy').removeClass('border-red').addClass('border-green'); }
           
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

    if (value > 0) {
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
    var SubmitBtn = $('#Save');
    if (z <= 0 && btn == 1) {

        SubmitBtn.makeEnabled();
    }
   
};
function GetInActiveInDD(DDId) {
    var IsActive = false;
    var TotalOptions = 0;
    var Options = 0;
     Options = $('#' + DDId + '>option').length;
     SelectedOption = 0;
     TotalOptions = Options - 1;
    $('#' + DDId + '>option').each(function (){
        if ($(this).hasClass('Active')) { SelectedOption = SelectedOption + 1; }
    });
   // alert(SelectedOption + '==' + TotalOptions);

    if (SelectedOption == TotalOptions) {
        IsActive = true;
    }
    return IsActive;
    

}
function TPDBtnClicked() {
    var notenumber = $('#NoteNumber').val();
    var EmployeeNo = $('#EmployeeNo').val();
    var NoteNumber = $('#RefNoteNumber').val();
    $('#SubmitCount').val(1);
    if (notenumber != '') {
        var iDiv = $('#TPDModalDiv');
        var modalDiv = $('#TPDModal');
        var dataSourceURL = '/BIL/VMDeductionsFromDA?EmployeeNo=' + EmployeeNo + '&NoteNumber=' + NoteNumber;
        $.ajax({
            url: dataSourceURL,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                iDiv.html(result);
                modalDiv.modal('show');
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
