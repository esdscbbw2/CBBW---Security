function EnableBlockedControlls() {
    if ($('#DWTActive').val() == 1 && $('#VAActive').val() == 1) {
        $('#AppOption2').makeEnabled();
        $('#AppStatus').makeEnabled();
    } else {
        $('#AppStatus').makeDisable();
        $('#AppOption2').makeDisable();
    }
};
function EnableSubmitBtn() {
    var btnsubmit = $('#btnSubmit');
    var isenable = false;
    if ($('.is-invalid').length == 0) {
        if ($('#DWTActive').val() == 1 && $('#VAActive').val() == 1) { isenable = true; }
    }
    if (isenable) { btnsubmit.makeEnabled(); } else { btnsubmit.makeDisable(); }
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
    $('#BackBtnActive').val(1);
    EnableSubmitBtn();
};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "AppOption2":
            if (value ==1) { isvalid = true; }
            break;
        case "AppStatus":
            if (value >= 0) {
                var mCtrl = $('#ReasonForDisApproval');
                if (value == 1) { mCtrl.val('').makeDisable(); mCtrl.clearValidateClass(); }
                else { mCtrl.val('').makeEnabled(); mCtrl.isInvalid(); }
                isvalid = true;
            }
            break;
        case "ReasonForDisApproval":
            if (value !='') { isvalid = true; }
            break;
    }
    return isvalid;
};
function DisplayTPDetails(data) {
    for (var item in data) {
        var rowdata = data[item];
        var persontype = $('#PersonTypeTD');
        var ENoTD = $('#ENoTD');
        var DesgTD = $('#DesgTD');
        var schFromDateTD = $('#schFromDateTD');
        var schFromTimeTD = $('#schFromTimeTD');
        var ActTourOutdtTD = $('#ActTourOutdtTD');
        var ActTourOutTimeTD = $('#ActTourOutTimeTD');
        var schToDateTD = $('#schToDateTD');
        var povCtrl = $('#povCtrl');
        var TADADeniedTD = $('#TADADeniedTD');
        var RTInDateTD = $('#RTInDateTD');
        var RTITimeTD = $('#RTITimeTD');
        var ATInDateTD = $('#ATInDateTD');
        var ATITimeTD = $('#ATITimeTD');
        var StatusTD = $('#StatusTD');
        if (item > 0) {
            CloneRowWithNoControls('tbody1', 'tbody2', item);
            persontype = $('#PersonTypeTD_' + item);
            ENoTD = $('#ENoTD_' + item);
            DesgTD = $('#DesgTD_' + item);
            schFromDateTD = $('#schFromDateTD_' + item);
            schFromTimeTD = $('#schFromTimeTD_' + item);
            ActTourOutdtTD = $('#ActTourOutdtTD_' + item);
            ActTourOutTimeTD = $('#ActTourOutTimeTD_' + item);
            schToDateTD = $('#schToDateTD_' + item);
            povCtrl = $('#povCtrl_' + item);
            TADADeniedTD = $('#TADADeniedTD_' + item);
            RTInDateTD = $('#RTInDateTD_' + item);
            RTITimeTD = $('#RTITimeTD_' + item);
            ATInDateTD = $('#ATInDateTD_' + item);
            ATITimeTD = $('#ATITimeTD_' + item);
            StatusTD = $('#StatusTD_' + item);
        }
        persontype.html(rowdata.PersonTypeText);
        ENoTD.html(rowdata.EmployeeNonName);
        DesgTD.html(rowdata.DesignationCodenName);
        schFromDateTD.html(rowdata.FromDateStrDisplay);
        schFromTimeTD.html(rowdata.FromTime);
        ActTourOutdtTD.html(rowdata.ActualTourOutDateDisplay);
        ActTourOutTimeTD.html(rowdata.ActualTourOutTime);
        schToDateTD.html(rowdata.ToDateStrDisplay);
        povCtrl.html(rowdata.PurposeOfVisit);
        TADADeniedTD.html(rowdata.TADADenied);
        RTInDateTD.html(rowdata.RequiredTourInDateDisplay);
        RTITimeTD.html(rowdata.RequiredTourInTime);
        ATInDateTD.html(rowdata.ActualTourInDateDisplay);
        ATITimeTD.html(rowdata.ActualTourInTime);
        StatusTD.html(rowdata.TourStatusText);
        //povCtrl.attr('data-bs-original-title', rowdata.PurposeOfVisit);
    }
}
function NoteNumberChanged(notenumber) {
    var noteCtrl = $('#NoteNumber');
    if (notenumber != '') { noteCtrl.isValid(); } else { noteCtrl.isInvalid(); }
    $('#tbody2').empty();
    $.ajax({
        url: '/EHG/GetNoteHdrTPD',
        method: 'GET',
        data: { NoteNumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                var matstat = 'No';
                if (item.Header.MaterialStatus == 1) { matstat = 'Yes'; }
                $('#CentreCodenNameInput').val(item.Header.CenterCodenName);
                $('#VehicleTypeInput').val(item.Header.VehicleTypeText);
                $('#POAInput').val(item.Header.POAText);
                $('#MaterialStatInput').val(matstat);
                $('#InstructorInput').val(item.Header.InstructorName);
                $('#InitiatorInput').val(item.Header.InitiatorCodenName);
                $('#AuthorizeEmpInput').val(item.Header.AuthorisedEmployeeName);
                if (item.Header.PurposeOfAllotment == 1) {
                    $('#TPDHeader').html('Travelling Person Details: For Management');
                    $('.OffSpecial').each(function () {
                        $(this).addClass('inVisible');
                    });
                    $('.mngSpecial').each(function () {
                        $(this).removeClass('inVisible');
                    });
                    $('#btnsDiv').addClass('inVisible');
                    $('#AppOption2').makeEnabled();
                    $('#AppStatus').makeEnabled();
                    $('#DWTActive').val(1);
                    $('#VAActive').val(1);
                }
                else {
                    $('#TPDHeader').html('Travelling Person Details: For Office Work');
                    $('.OffSpecial').each(function () {
                        $(this).removeClass('inVisible');
                    });
                    $('.mngSpecial').each(function () {
                        $(this).addClass('inVisible');
                    });
                    $('#btnsDiv').removeClass('inVisible');
                }                
                DisplayTPDetails(item.TPDetails);
            });
        }
    });
    $('#BackBtnActive').val(1);
};
$(document).ready(function () {
    $('#btnBack').click(function () {
        var backbtnactive = $('#BackBtnActive').val();
        var backurl = "/Security/EHG/NoteApproveList";
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
    var notenumberctrl= $('#NoteNumber');
    notenumberctrl.change(function () {
        NoteNumberChanged($(this).val());        
    });
    if (notenumberctrl.val() != '') { NoteNumberChanged(notenumberctrl.val()); }
    EnableBlockedControlls();
});