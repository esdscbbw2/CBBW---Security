function ActivateSubmitButton() {
    var btnSubmit = $('#SubmitBtn');
    var isenabled = false;
    if ($('#btnIndividualEdit').val() == 1 || $('#btnTourEdit').val() == 1)
    {
        if ($('#MainDiv').find('.is-invalid').length <= 0) { isenabled = true; }
    }
    if (isenabled) { btnSubmit.makeEnabled(); } else { btnSubmit.makeDisable();}
};
function AsPerPolicyOptionChanged() {
    var targetCtrl = $(AsPerPolicyOptionChanged.caller.arguments[0].target);
    if (targetCtrl.val() == 1) { targetCtrl.isValid(); } else { targetCtrl.isInvalid(); }
    ActivateSubmitButton();
};
function NotenumberChanged() {
    var TEBtn = $('#TourEditBtn');
    var IEBtn = $('#IndEditBtn');
    var notenumberCtrl = $('#NoteNumber');
    var notenumber = notenumberCtrl.val();
    var lblNoteDesc = $('#lblNoteDesc');
    var notetype = notenumber.substring(7, 10);
    if (notetype == 'EHG') {
        lblNoteDesc.html('Ref. Employee’s Travelling  Details & Vehicle Allotment (By HG)  –  ENTRY Note No.');
    }
    else if (notetype == 'EZB') {
        lblNoteDesc.html('Ref. Employees Travelling  Schedule Details – ENTRY (FOR NZB STAFF) Note No.');
    }
    else if (notetype == 'EMN') {
        lblNoteDesc.html('Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS RECORDED AT NZB) Note No.');
    }
    else { lblNoteDesc.html('Ref. Employees Travelling  Schedule Details – ENTRY (FOR MFG. CENTERS) Note No.'); }
    if (notenumber != '') { notenumberCtrl.isValid(); } else { notenumberCtrl.isInvalid(); }
    $.ajax({
        url: '/ETSEdit/GetNoteInfo',
        method: 'GET',
        data: { NoteNumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                $('#NoteEntryDate').val(item.EntryDateDisplay);
                $('#NoteEntryTime').val(item.EntryTime);
                $('#CentreCodenName').val(item.CenterName);
                $('#TripPurpose').val(item.POAText);
                $('#EPTourStatus').val(item.EPTourText);
                $('#AppStat').val(item.IsApprovedDisplay);
                $('#AppDateTime').val(item.AppDateTimeDisplay);
                $('#AppReason').val(item.NotAppReason);
                $('#ratStat').val(item.IsRatifiedDisplay);
                $('#RatDT').val(item.RetDateTimeDisplay);
                $('#ratReason').val(item.RetReason);
                $('#IndEditDiv').html(item.IsIndividualEdit);
                $('#IsCancelled').val(item.IsCancelled);
                if (item.POA == 1) {
                    TEBtn.makeEnabled();
                    IEBtn.makeDisable();
                }
                else {
                    if (item.IsIndividualEdit == 1) {
                        TEBtn.makeDisable();
                        IEBtn.makeEnabled();
                    }
                    else {
                        if ($('#btnIndividualEdit').val() == 1) {
                            TEBtn.makeDisable();
                            IEBtn.makeEnabled();
                        }
                        else if ($('#btnTourEdit').val() == 1) {
                            TEBtn.makeEnabled();
                            IEBtn.makeDisable();
                        }
                        else {
                            TEBtn.makeEnabled();
                            IEBtn.makeEnabled();
                        }
                    }
                }                
            });
        }
    });
    $('#backbtnactive').val(1);
    ActivateSubmitButton();
};
$(document).ready(function () {
    NotenumberChanged();
    $('#btnBack').click(function () {
        var backbtnactive = $('#BackBtnActive').val();
        var backurl = "/Security/ETSEdit/Index";
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