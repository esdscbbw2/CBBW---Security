$(document).ready(function () {
    $('#NoteNumber').change(function () {
        Notenumberchanged($(this).val());
    });
   
});

function Notenumberchanged(notenumber) {
    var noteCtrl = $('#NoteNumber');
  
    if (notenumber != '') {
        $.ajax({
            url: '/BIL/GetTADABillGenerationDataReport',
            method: 'GET',
            data: { Notenumber: notenumber },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    $('#PersonTypetxt').val(item.PersonTypetxt);
                    $('#EmployeeCodeName').val(item.EmployeeCodeName);
                    $('#DesigCodeName').val(item.DesigCodeName);
                    $('#CenterCodeName').val(item.CenterCodeName);
                    $('#TourFromDateNTime').val(item.TourFromDateNTime);
                    $('#TourToDateNTime').val(item.TourToDateNTime);
                    $('#NoOfDays').val(item.NoOfDays);

                    $('#TAAllowance').html(item.ATAAmount);
                    $('#DAAllowance').html(item.DAAmount);
                    $('#LocalAllowance').html(item.ALocAmount);
                    $('#LodingAmount').html(item.ALodAmount);
                    $('#NetAmount').html(item.NetAmount);
                    
                    (async function () {
                        const r1 = await GetTPDetails(notenumber, item.EmployeeNo);
                    })();

                });
            }
        });
    } 
};

async function GetTPDetails(notenumber, EmployeeNo) {
    // var notenumber = $('#NoteNumber').val();
    var TPDetailsDiv = $('#TPDiv');
    TPDetailsDiv.addClass('inVisible');
    var dataSourceURL = '/BIL/GetReport?EmployeeNo=' + EmployeeNo+'&NoteNumber=' + notenumber;
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