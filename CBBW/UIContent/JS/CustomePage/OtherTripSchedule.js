function ClearBtnClick() {
    //$('#TripPurpose').val('');
    $('#tbody2').empty();
    $('.canclear').each(function () {
        $(this).val('').removeClass('is-valid').addClass('is-invalid');
    });
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

    activateSubmitBtn();

};
function validatectrl(targetid, value) {
    var isvalid = false;
    switch (targetid) {
        case "TripPurpose":
            if (value.length >1) { isvalid = true; }
            break;
        case "CCOth":
            if (value == 1) { isvalid = true; }
            break;
        case "DIC":
            if (value == 1) { isvalid = true; }
            break;
        
    }
    return isvalid;
};
function SaveData() {
    var trippurpose = 'Inspection and others';
    var vehicleno = 'ABC';
    var notenumber = 'XYZ';
    var schrecords = getSchRecords();
    $.ajax({
        method: 'POST',
        url: '/CTV/getOTVSchData',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            VehicleNumber: vehicleno,
            TripPurpose: trippurpose,
            NoteNumber: notenumber,
            OTSchList: schrecords
        }),
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Data saved successfully.',
                        icon: 'success',
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    }).then(callback);
                    function callback(result) {
                        if (result.value) {
                            var url = "/CTV/OtherTrip";
                            window.location.href = url;
                        }
                    }
                } else {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Failed to save data.',
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
function getSchRecords() {
    var schrecords = [];
    $('.add-row').each(function () {
        var rowid = $(this).attr('id');
        schrecords.push({
            'FromDate': $('#' + rowid + '_FromDt').val(),
            'FromTime': $('#' + rowid + '_Fromtime').val(),
            'FromCentreTypeCode': $('#' + rowid + '_FromLT').val(),
            'FromCentreCode': $('#' + rowid + '_FromL').val(),
            'ToCentreTypeCode': $('#' + rowid + '_ToLT').val(),
            'ToCentreCode': $('#' + rowid + '_ToL').val(),
            'ToDate': $('#' + rowid + '_ToDt').val()
        });
    });
    return schrecords;
};
function activateSubmitBtn() {
    var btnSubmit = $('#btnSubmit');
    if ($('.is-invalid').length > 0) {
        btnSubmit.attr('disabled', 'disabled');
    } else {
        btnSubmit.removeAttr('disabled');
    }
};
function addBtnClick() {
    var insrow = addBtnClick.caller.arguments[0].target.closest('.add-row');
    //getting Max Row 
    var maxrows = 0;
    $('#tbody2 tr').each(function () {
        var maxr=$(this).attr('id')
        if (maxr > maxrows) { maxrows = maxr; }
    });
    //alert(maxrows);
    //var rows = $('#tbody2 tr').length + 1;
    //var r = parseInt($('#tbody2 tr:last').attr("id"));
    var r = 0;
    if (maxrows >= 1) { r = maxrows + 1; } else { r = 1; }
    
    var cloneready = $('#tbody1').find('tr').clone();
    cloneready.attr("id", r);
    cloneready.find('#0_FromDt').attr('id', r + '_FromDt').val('').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#0_Fromtime').attr('id', r + '_Fromtime').val('').removeClass('is-valid').addClass('is-invalid');;
    cloneready.find('#0_FromLT').attr('id', r + '_FromLT').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#0_FromL').attr('id', r + '_FromL').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#0_ToLT').attr('id', r + '_ToLT').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#0_ToL').attr('id', r + '_ToL').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#0_ToDt').attr('id', r + '_ToDt').val('').removeClass('is-valid').addClass('is-invalid');
    
    var addbtn = cloneready.find('#0_AddBtn');
    addbtn.attr('id', r + '_AddBtn');
    addbtn.on('mouseenter', function () {
        $(this).tooltip('show');
    });
    addbtn.on('mouseleave click', function () {
        $(this).tooltip('hide');
    });
    
    var deletebtn = cloneready.find('#0_DeleteBtn');
    deletebtn.attr('id', r + '_DeleteBtn').removeClass("inVisible");
    deletebtn.on('mouseenter', function () {
        $(this).tooltip('show');
    });
    deletebtn.on('mouseleave click', function () {
        $(this).tooltip('hide');
    });
    
    var insrowid = $(insrow).attr('id');
    //alert(insrowid + ' - ' + maxrows);
    if (insrowid == 0) {
        if (maxrows == 0) {
            $('#tbody2').append(cloneready);
        } else {
            $(cloneready).insertBefore('#tbody2 tr:first');
        }       
    } else {
        $(cloneready).insertAfter('#' + insrowid);
    }
    
    $('#0_AddBtn').on('mouseleave click', function () {
        $(this).tooltip('hide');
    });

    var sl = 2;
    $('#tbody2 th').each(function () {
        $(this).html(sl);
        sl += 1;
    });

    activateSubmitBtn();
};
function removeBtnClick() {
    var r = removeBtnClick.caller.arguments[0].target.closest('.add-row');
    if ($(r).attr("id") == 0) {
    } else {
        r.remove();
    };
    var sl = 2;
    $('#tbody2 th').each(function () {
        $(this).html(sl);
        sl += 1;
    });
    activateSubmitBtn();
    return false;
}
function ChangeLocation() {
    var target = ChangeLocation.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var targetvalue = $(target).val();
    var rowid = $(target.closest('.add-row')).attr("id");
    getToDate(rowid);
    if (targetvalue >= 0) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }

    activateSubmitBtn();
};
function SchTimeChanged() {
    var target = SchTimeChanged.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    var targetvalue = $(target).val();
    if (targetvalue == null) {
        $(target).removeClass('is-valid').addClass('is-invalid');
    } else {
        $(target).removeClass('is-invalid').addClass('is-valid');
    }

    getToDate(rowid);
};
function SchDateChanged() {
    var target = SchDateChanged.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    var datevalue = $(target).val();
    var vehicleno = '';
    
    if (datevalue == null) {
        $(target).removeClass('is-valid').addClass('is-invalid');
    } else {
        $.ajax({
            url: '/CTV/CheckSchDateAvl',
            method: 'GET',
            data: {
                VehicleNo: vehicleno,
                ScheduleDate: datevalue
            },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    if (item.bResponseBool) {
                        $(target).removeClass('is-invalid').addClass('is-valid');
                    }
                    else {
                        $(target).removeClass('is-valid').addClass('is-invalid');
                        Swal.fire({
                            title: 'Date Availability',
                            text: item.sResponseString,
                            icon: 'warning',
                            customClass: 'swal-wide',
                            buttons: {
                                //cancel: 'Cancel',
                                confirm: 'Ok'
                            },
                            //cancelButtonClass: 'btn-cancel',
                            confirmButtonColor: '#2527a2',
                        });
                    }
                });
                getToDate(rowid);
            }
        });
    }    
};
function ChangeLocationType() {
    var target = ChangeLocationType.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    //var targetid = $(target).attr('id').split("_").pop();
    var targetid = $(target).attr('id');
    targetid = targetid.slice(0, - 1);
    var locationtypeid = $(target).val();
    var locationcombo = $('#' + targetid);
    //Validations
    if (locationtypeid >= 0) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }
    $.ajax({
        url: '/CTV/GetLocationsFromType',
        method: 'GET',
        data: { TypeID: locationtypeid },
        dataType: 'json',
        success: function (data) {
            locationcombo.empty();
            locationcombo.append($('<option/>', { value: "-1", text: "Select location" }));
            $(data).each(function (index, item) {
                locationcombo.append($('<option/>', { value: item.ID, text: item.DisplayText }));                
            });
        }
    });

    getToDate(rowid);
    activateSubmitBtn();
};
function getToDate(rowid) {
    //var todate = '';
    var duprec = 0;
    var fromlocationtype = $('#' + rowid + '_FromLT').val();
    var fromlocation = $('#' + rowid + '_FromL').val();
    var tolocationtype = $('#' + rowid + '_ToLT').val();
    var tolocation = $('#' + rowid + '_ToL').val();
    var fromDate = $('#' + rowid + '_FromDt').val();
    var fromTime = $('#' + rowid + '_Fromtime').val();
    var todateCtrl = $('#' + rowid + '_ToDt');
    todateCtrl.val('');
    if (fromDate != '') {
        if (fromTime != '') {
            if (fromlocation > 0) {
                if (tolocationtype > 0) {
                    if (tolocation > 0) {
                        if (fromlocation == tolocation) {
                            Swal.fire({
                                title: 'Warning!',
                                text: 'Source and destination must be different.',
                                icon: 'warning',
                                customClass: 'swal-wide',
                                buttons: {
                                    //cancel: 'Cancel',
                                    confirm: 'Ok'
                                },
                                //cancelButtonClass: 'btn-cancel',
                                confirmButtonColor: '#2527a2',
                            });
                            todateCtrl.removeClass('is-valid').addClass('is-invalid');
                        }
                        else {
                            $('.add-row').each(function () {
                                var row = $(this);
                                var rid = row.attr('id');
                                //alert(rid + ' - ' + rowid);
                                if (rowid != rid) {
                                    if ($('#' + rid + '_FromDt').val() == fromDate) {
                                        if ($('#' + rid + '_Fromtime').val() == fromTime) {
                                            if ($('#' + rid + '_FromL').val() == fromlocation) {
                                                if ($('#' + rid + '_ToL').val() == tolocation) {
                                                    duprec = 1;
                                                }
                                            }
                                        }
                                    }
                                };
                            });
                            if (duprec == 0) {
                                $.ajax({
                                    url: '/CTV/GetSchToDate',
                                    method: 'GET',
                                    data: {
                                        Fromdate: fromDate,
                                        FromTime: fromTime,
                                        FromLocation: fromlocation,
                                        ToLocationType: tolocationtype,
                                        ToLocation: tolocation
                                    },
                                    dataType: 'json',
                                    success: function (data) {
                                        $(data).each(function (index, item) {
                                            todateCtrl.val(item.sResponseString);
                                            todateCtrl.removeClass('is-invalid').addClass('is-valid');
                                        });
                                    }
                                });
                            } else {
                                todateCtrl.removeClass('is-valid').addClass('is-invalid');
                                Swal.fire({
                                    title: 'Warning!',
                                    text: 'Duplicate record found.',
                                    icon: 'warning',
                                    customClass: 'swal-wide',
                                    buttons: {
                                        //cancel: 'Cancel',
                                        confirm: 'Ok'
                                    },
                                    //cancelButtonClass: 'btn-cancel',
                                    confirmButtonColor: '#2527a2',
                                });
                            }
                        }
                        
                    }
                }
            }
        }
    }

    activateSubmitBtn();
    //DisableCombo();
    //todateCtrl.val(todate);
    //alert(todate);
    //if (todate == '') {
    //    todateCtrl.removeClass('is-valid').addClass('is-invalid');
    //} else {
    //    todateCtrl.removeClass('is-invalid').addClass('is-valid')
    //}
};
function DisableCombo() {
    $('#CCOth').val(0).removeClass('is-valid').addClass('is-invalid');
    $('#DIC').val(0).removeClass('is-valid').addClass('is-invalid');

};
$(document).ready(function () {
    var FromLT = $('#0_FromLT');
    var ToLT = $('#0_ToLT');
    $.ajax({
        url: '/CTV/GetLocationTypes',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            FromLT.append($('<option/>', { value: "-1", text: "Select location type" }));
            ToLT.append($('<option/>', { value: "-1", text: "Select location type" }));
            $(data).each(function (index, item) {
                FromLT.append($('<option/>', { value: item.ID, text: item.DisplayText }));
                ToLT.append($('<option/>', { value: item.ID, text: item.DisplayText }));
            });
        }
    });

    
});