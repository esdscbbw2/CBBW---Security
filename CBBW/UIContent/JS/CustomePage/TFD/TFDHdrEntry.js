$(document).ready(function () {
    $('#tfdHdr_EntEntryDate').val('');
    $('#tfdHdr_EntEntryTime').val('');
    $('#tfdHdr_TourFromDate').val('');
    $('#tfdHdr_TourToDate').val('');
 
    $('#NoteNumber').change(function () {
        Notenumberchanged($(this).val());
    });
    Notenumberchanged($('#NoteNumber').val());
    var btnDisplays = $('#submitcount').val();
    if (btnDisplays == 1) {
        $('#NoteNumber').makeDisable();
    } else {
        $('#NoteNumber').makeEnabled();
    }
});
function Notenumberchanged(notenumber) {
    $('#TourFB').makeDisable();
    var noteCtrl = $('#NoteNumber');
    var selectedvalue = 0;
    if (notenumber != '') { noteCtrl.isValid(); 
    $.ajax({
        url: '/TFD/GetTFDHeaderData',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                $('#tfdHdr_EntEntryDate').val(item.tfdHdr.EntEntryDatestr);
                $('#tfdHdr_EntEntryTime').val(item.tfdHdr.EntEntryTime);
                $('#tfdHdr_TourFromDate').val(item.tfdHdr.TourFromDatestr);
                $('#tfdHdr_TourToDate').val(item.tfdHdr.TourToDatestr);
                $('#tfdHdr_PurposeOfVisit').val(item.tfdHdr.PurposeOfVisit);
                $('#TourFB').makeEnabled();
            });
        }
    });
        (async function () {
            const r1 = await GetEmployeeList(notenumber, selectedvalue);
        })();
        (async function () {
            const r2 = await GetTPDetails(notenumber);
        })();
       
    } else { noteCtrl.isInvalid(); }
};
async function GetEmployeeList(notenumber, selectedvalue) {
   getDropDownDataWithSelectedValue('EmployeeNo', 'Select Employee', '/Security/TFD/GetENTAuthEmployeeList?NoteNumber=' + notenumber, selectedvalue );
 
}
async function GetTPDetails(notenumber) {
   // var notenumber = $('#NoteNumber').val();
    var TPDetailsDiv = $('#TPDiv');
    var dataSourceURL = '/TFD/TPView?NoteNumber=' + notenumber;
    $.ajax({
        url: dataSourceURL,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            TPDetailsDiv.removeClass('inVisible');
            TPDetailsDiv.html(result);
        },
        error: function (xhr, status) {
            TPDetailsDiv.html(xhr.responseText);
        }
    })
};
function FinalSavedata() {
    var EntEntryDate= $('#tfdHdr_EntEntryDate').val();
    var EntEntryTime=$('#tfdHdr_EntEntryTime').val();
    var TourFromDate=$('#tfdHdr_TourFromDate').val();
    var TourToDate=$('#tfdHdr_TourToDate').val();
    var PurposeOfVisit=$('#tfdHdr_PurposeOfVisit').val();
    var AuthEmployeeCode = $('#EmployeeNo').val();
    var AuthEmployeeName = $('#EmployeeNo option:selected').text();
    var NoteNumber=$('#tfdHdr_NoteNumber').val();
    var RefNoteNumber = $('#NoteNumber').val();

    var x = '{"NoteNumber":"' + NoteNumber + '","RefNoteNumber":"' + RefNoteNumber + '","EntEntryDate":"' + EntEntryDate + '","EntEntryTime": "' + EntEntryTime + '" ,"TourFromDate": "' + TourFromDate + '","TourToDate": "' + TourToDate + '", "PurposeOfVisit": "' + PurposeOfVisit + '", "AuthEmployeeCode": "' + AuthEmployeeCode + '", "AuthEmployeeName": "' + AuthEmployeeName + '"}';
    $.ajax({
        method: 'POST',
        url: '/TFD/Create',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: x,
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'TFD Data Saved Successfully.',
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
                            var url = "/Security/TFD/Index";
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
        case "EmployeeNo":
            isvalid = validatectrl_ValidatestringLength(value);
            break;
        case "Required":
            isvalid = validatectrl_ValidateLength(value);
            break;
            
    }
    return isvalid;
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
function EnableSubmitBtn() {
    var z = getDivInvalidCount('AllDiv');
    var btn = $('#submitcount').val();
    var SubmitBtn = $('#SubmitBtn');
    if (z <= 0 && btn == 1) {
        SubmitBtn.makeEnabled();
    }
};



