
function VisiblePersonRow(personid) {
    $('#TPDtlTable tbody tr').each(function () {
        $(this).addClass('inVisible');
    });
    $('.M_' + personid).each(function () {
        $(this).removeClass('inVisible');
    });
    $('#TPTable tbody tr').each(function () {
        $(this).removeClass('selected-row');
    });
    $('#tr_' + personid).each(function () {
        $(this).addClass('selected-row');
    });
};
function TPSelected() {
    var targetCtrl = $(TPSelected.caller.arguments[0].target);
    VisiblePersonRow(targetCtrl.attr('id'));
};
function ATIBlured()
{
    var targetCtrl = $(ATIBlured.caller.arguments[0].target);
    var timerid = targetCtrl.attr('id');
    var datadivid = 'V' + timerid;
    $('#' + datadivid).html(targetCtrl.val());
}
$(document).ready(function () {
    var defPerson = $('#DefaultPersonID').val();
    $('#' + defPerson).attr('checked', true);
    VisiblePersonRow(defPerson);
});
$(document).ready(function () {
    $('#btnSubmit').click(function () {
        var notenumber = $('#NoteNumber').val();
        var tblPersonRecords = '';
        var tblDateWiseRecords = '';
        tblPersonRecords = getRecordsFromTableV2('TPTable');
        tblDateWiseRecords = getRecordsFromTableV2('TPDtlTable');
        var x = '{"NoteNumber":"' + notenumber
            + '","DateWiseDetails":' + tblDateWiseRecords
            + ',"TPersons":'
            + tblPersonRecords + '}';
        //alert(tblDateWiseRecords);
        $.ajax({
            method: 'POST',
            url: '/EntryII/SaveLWOutIn',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: x,
            success: function (data) {
                $(data).each(function (index, item) {
                    if (item.bResponseBool == true) {
                        Swal.fire({
                            title: 'Confirmation',
                            text: 'Data Saved Successfully.',
                            icon: 'success',
                            customClass: 'swal-wide',
                            buttons: {
                                confirm: 'Ok'
                            },
                            confirmButtonColor: '#2527a2',
                        }).then(callback);
                        function callback(result) {
                            if (result.value) {
                                var url = "/Security/EntryII/LWCreate";
                                window.location.href = url;
                            }
                        }
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
    });
});