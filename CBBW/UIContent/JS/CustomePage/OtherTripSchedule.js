$.fn.setValueToOne = function () {
    var that = this;
    that.val('1');
};
$.fn.setValueToZero = function () {
    var that = this;
    that.val('0');
};
function getInitialData() {
    var notenumber = $('#NoteNumber').val();
    $.ajax({
        url: '/CTV/getOTVSChDetailData',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                $('#TripPurpose').val(item.TripPurpose);
                AddBtnVirtualClick(index - 1);
                var fromdatectrl = $('#' + index + '_FromDt');
                var fromtimectrl = $('#' + index + '_Fromtime');
                var fromltctrl = $('#' + index + '_FromLT');
                var toltctrl = $('#' + index + '_ToLT');
                var todtctrl = $('#' + index + '_ToDt');
                var fromlctrl = $('#' + index + '_FromL');

                fromdatectrl.val(item.FromDateStr);
                fromtimectrl.val(item.FromTime).removeClass('is-invalid').addClass('is-valid');
                fromltctrl.val(item.FromCenterTypeCode).removeClass('is-invalid').addClass('is-valid');
                toltctrl.val(item.ToCentreTypeCode).removeClass('is-invalid').addClass('is-valid');
                todtctrl.val(item.ToDateStr).removeClass('is-invalid').addClass('is-valid');

                //// Second Table cloaning
                if (index > 0) { cloneEditRows(index); }
                $('#' + index +'_FromDate2').val(item.FromDateStr);
                $('#' + index +'_FromTime2').val(fromtimectrl.val());
                $('#' + index +'_FromLT2').val(fromltctrl.find('option:selected').text());
                $('#' + index +'_ToLT2').val(toltctrl.find('option:selected').text());
                $('#' + index + '_ToDate2').val(item.ToDateStr);
                $('#' + index + '_Driver2').val(item.DriverCodenName);
                //second table cloaning end
                FillLocationComboVirtually(item.FromCenterTypeCode, index + '_FromL', item.FromCentreCode, index+'_FromL2');
                FillLocationComboVirtually(item.ToCentreTypeCode, index + '_ToL', item.ToCentreCode, index+'_ToL2');
                fromlctrl.removeClass('is-invalid').addClass('is-valid');
                $('#' + index + '_ToL').removeClass('is-invalid').addClass('is-valid');

                
                
                
            });
        }
    });

};
function cloneEditRows(index) {    
    var cloneready = $('#tbody3').find('tr').clone();
    cloneready.find('#0_FromDate2').attr('id', index + '_FromDate2');
    cloneready.find('#0_FromTime2').attr('id', index + '_FromTime2');
    cloneready.find('#0_FromLT2').attr('id', index + '_FromLT2');
    cloneready.find('#0_ToLT2').attr('id', index + '_ToLT2');
    cloneready.find('#0_FromL2').attr('id', index + '_FromL2');
    cloneready.find('#0_ToL2').attr('id', index + '_ToL2');
    cloneready.find('#0_ToDate2').attr('id', index + '_ToDate2');
    cloneready.find('#0_Driver2').attr('id', index + '_Driver2');
    $('#tbody4').append(cloneready);
}
function ClearBtnClick() {
    $('#BackBtnMsg').setValueToOne();
    //$('#TripPurpose').val('');
    $('#tbody2').empty();
    $('.canclear').each(function () {
        $(this).val('').removeClass('is-valid').addClass('is-invalid');
    });
};
function BackButtonClicked() {
    if ($('#BackBtnMsg').val() == 1) {
        Swal.fire({
                    title: 'Confirmation',
                    text: 'Are you sure to go back?',
                    icon: 'question',
                    customClass: 'swal-wide',
                    buttons: {
                        confirm: 'Ok'
                    },
                    confirmButtonColor: '#2527a2',
                }).then(callback);
                function callback(result) {
                    if (result.value) {
                        var url = "/Security/CTV/Create";
                        window.location.href = url;
                    }
                }
    } else {
        var url = "/Security/CTV/Create";
        window.location.href = url;
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
    var trippurpose = $('#TripPurpose').val();
    var vehicleno = $('#VehicleNo').val();
    var notenumber = $('#NoteNumber').val();
    var schrecords = getSchRecords();
    $.ajax({
        method: 'POST',
        url: '/CTV/setOTVSchData',
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
                    var url = "/Security/CTV/Create";
                    window.location.href = url;
                    $('#BackBtnMsg').setValueToZero();
                    //Swal.fire({
                    //    title: 'Confirmation',
                    //    text: 'Data saved successfully.',
                    //    icon: 'success',
                    //    customClass: 'swal-wide',
                    //    buttons: {
                    //        confirm: 'Ok'
                    //    },
                    //    confirmButtonColor: '#2527a2',
                    //}).then(callback);
                    //function callback(result) {
                    //    if (result.value) {
                    //        var url = "/Security/CTV/Create";
                    //        window.location.href = url;
                    //    }
                    //}
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
        var x = '';
        $('#' + rowid + '_ToLT option:selected').each(function () {
            x = x + '_' + $(this).val();
        });
        var xstr = '';
        $('#' + rowid + '_ToLT option:selected').each(function () {
            xstr = xstr + '_' + $(this).text();
        });
        var y = '';
        $('#' + rowid + '_ToL option:selected').each(function () {
            y = y + '_' + $(this).val();
        });
        var ystr = '';
        $('#' + rowid + '_ToL option:selected').each(function () {
            ystr = ystr + '_' + $(this).text();
        });
        schrecords.push({            
            'FromDate': $('#' + rowid + '_FromDt').val(),
            'FromTime': $('#' + rowid + '_Fromtime').val(),
            'FromCentreTypeCode': $('#' + rowid + '_FromLT').val(),
            'FromCentreCode': $('#' + rowid + '_FromL').val(),
            'ToCentreTypeCode': 0,
            'ToCentreCode': 0,
            'ToDate': $('#' + rowid + '_ToDt').val(),
            'DriverCode': $('#DriverCode').val(),
            'DriverName': $('#DriverName').val(),
            'ToCentreTypeCodes': x,
            'ToCentreCodes': y,
            'ToCentreTypeCodesStr': xstr,
            'ToCentreCodesStr': ystr
        });
    });
    return schrecords;
};
function activateSubmitBtn() {
    //alert($('.is-invalid').length);
    var btnSubmit = $('#btnSubmit');
    if ($('.is-invalid').length > 0) {
        btnSubmit.attr('disabled', 'disabled');
    } else {
        btnSubmit.removeAttr('disabled');
    }
};
function AddBtnVirtualClick(insrowid) {
    var maxrows = 0;
    $('#tbody2 tr').each(function () {
        var maxr = $(this).attr('id')
        if (maxr > maxrows) { maxrows = maxr; }
    });
    //alert(maxrows);
    //var rows = $('#tbody2 tr').length + 1;
    //var r = parseInt($('#tbody2 tr:last').attr("id"));
    var r = 0;
    if (maxrows >= 1) { r = maxrows + 1; } else { r = 1; }

    var cloneready = $('#tbody1').find('tr').clone();
    cloneready.attr("id", r);
    //var fromdatectrl = cloneready.find('#0_FromDt');
    var curdt = $('#CurDate').val();
    cloneready.find('#0_FromDt')
        .attr('id', r + '_FromDt')
        .val(curdt)
        .removeClass('is-invalid')
        .addClass('is-valid');
    cloneready.find('#0_Fromtime').attr('id', r + '_Fromtime').val('').removeClass('is-valid').addClass('is-invalid');;
    cloneready.find('#0_FromLT').attr('id', r + '_FromLT').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#0_FromL').attr('id', r + '_FromL').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#0_ToLT').attr('id', r + '_ToLT').removeClass('is-valid').addClass('is-invalid');
    cloneready.find('#B0').remove();
    cloneready.find('.btn-group').remove();
    cloneready.find('#BL0').remove(); 
    cloneready.find('#0_ToL').removeClass('is-invalid').addClass('inVisible');
    cloneready.find('#M_ToL').attr('id', r + '_ToL')
        .removeClass('is-valid inVisible').addClass('is-invalid');
    cloneready.find('#0_ToDt').attr('id', r + '_ToDt').val('').removeClass('is-valid').addClass('is-invalid');
    //cloanready.find('#0_FromtimeDiv').attr('id', r + '_FromtimeDiv')

    var ftime = cloneready.find('#0_FromtimeDiv');
    ftime.datetimepicker({
        useCurrent: false,
        format: "hh:mm A"
    }).on('dp.show', function () {
        if (firstOpen) {
            time = moment().startOf('day');
            firstOpen = false;
        } else {
            time = "01:00 PM"
        }

        $(this).data('DateTimePicker').date(time);
    });

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

    var ltm = $('#' + r + '_ToLT');
    ltm.multiselect({
        templates: {
            button: '<button id="B' + r+'" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
        },
    });
    
    activateSubmitBtn();
};
function addBtnClick() {
    var insrow = addBtnClick.caller.arguments[0].target.closest('.add-row');
    $('#BackBtnMsg').setValueToOne();
    AddBtnVirtualClick($(insrow).attr('id'));

};
function removeBtnClick() {
    var r = removeBtnClick.caller.arguments[0].target.closest('.add-row');
    $('#BackBtnMsg').setValueToOne();
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
    
    if (targetvalue >= 0) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }
    getToDate(rowid);
    //activateSubmitBtn();
};
function ChangeLocationTo() {
    var target = ChangeLocationTo.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    var targetvalue = $(target).val();
    var rowid = $(target.closest('.add-row')).attr("id");

    if (targetvalue != '') {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }
    getToDate(rowid);
    //activateSubmitBtn();
};
function SchTimeChanged() {
    var target = SchTimeChanged.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    var targetvalue = $(target).val();
    if (targetvalue == null || targetvalue=='') {
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
    var vehicleno = $('#VehicleNo').val();
    
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
function FillLocationComboVirtually(locationtypeid, locationcomboid, locationid, editlocationctrlid)
{
    var locationcombo = $('#' + locationcomboid);
    var editlocationctrl = $('#' + editlocationctrlid);
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
            locationcombo.val(locationid);
            editlocationctrl.val(locationcombo.find('option:selected').text());
        }
    });
}
function ChangeLocationType() {
    var target = ChangeLocationType.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    //alert(rowid);
    //var locationctrl
    //var targetid = $(target).attr('id').split("_").pop();
    var targetid = $(target).attr('id');
    targetid = targetid.slice(0, - 1);
    var locationtypeid = $(target).val();
    var locationcombo = $('#' + targetid);
    locationcombo.removeClass('is-valid').addClass('is-invalid');
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
    var todateCtrl = $('#' + rowid + '_ToDt');
    todateCtrl.val('');
    todateCtrl.removeClass('is-valid').addClass('is-invalid');
    //getToDate(rowid);
    //activateSubmitBtn();
};
function ChangeLocationToType() {
    var target = ChangeLocationToType.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    var x = '';
    $('#' + rowid+'_ToLT option:selected').each(function () {
        x = x + '_' + $(this).val();
    });    
    //var targetid = $(target).attr('id').split("_").pop();
    var targetid = $(target).attr('id');
    targetid = targetid.slice(0, - 1);
    var locationtypeid = $(target).val();
    var locationcombo = $('#' + targetid);

    //locationcombo.attr('multiple', 'multiple');
    
    //locationcombo.removeAttr('multiple');

    locationcombo.removeClass('is-valid').addClass('is-invalid');
    //Validations
    if (x == '' || x==null || x=='_-1') {
        $(target).removeClass('is-valid').addClass('is-invalid');
    } else {
        $(target).removeClass('is-invalid').addClass('is-valid');
    }
    $.ajax({
        url: '/CTV/GetToLocationsFromType',
        method: 'GET',
        data: { TypeIDs: x },
        dataType: 'json',
        success: function (data) {            
            locationcombo.empty();
            locationcombo.multiselect('destroy');
            $('#BL' + rowid).remove();
            //locationcombo.append($('<option/>', { value: "-1", text: "Select location" }));
            $(data).each(function (index, item) {
                locationcombo.append($('<option/>', { value: item.ID, text: item.DisplayText }));
            });
            locationcombo.attr('multiple', 'multiple');
            locationcombo.find('.btn-group').remove();
            locationcombo.multiselect({
                templates: {
                    button: '<button id="BL' + rowid+'" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                }, enableFiltering: true,
            });
            //locationcombo.multiselect();
            locationcombo.multiselect('clearSelection');
            locationcombo.multiselect('refresh');
        }
    });
    var todateCtrl = $('#' + rowid + '_ToDt');
    todateCtrl.val('');
    todateCtrl.removeClass('is-valid').addClass('is-invalid');
    //getToDate(rowid);
    //activateSubmitBtn();
};
function getToDate(rowid) {
    //var todate = '';
    $('#BackBtnMsg').setValueToOne();
    var duprec = 0; var mduprec = 0;
    var vehicleno = $('#VehicleNo').val();
    var fromlocationtype = $('#' + rowid + '_FromLT').val();
    var fromlocation = $('#' + rowid + '_FromL').val();
    var tolocationtype = $('#' + rowid + '_ToLT').val();
    var tolocation = $('#' + rowid + '_ToL').val();
    var fromDate = $('#' + rowid + '_FromDt').val();
    var fromTime = $('#' + rowid + '_Fromtime').val();
    var todateCtrl = $('#' + rowid + '_ToDt');
    //--converting comma separeted to _ separated.
    var x = '';
    $('#' + rowid + '_ToLT option:selected').each(function () {
        x = x + '_' + $(this).val();
    });
    var y = '';
    $('#' + rowid + '_ToL option:selected').each(function () {
        y = y + '_' + $(this).val();
    });

    todateCtrl.val('');
    if (fromDate != '') {
        if (fromTime != '') {
            if (fromlocation > 0) {
                if (tolocationtype != '') {                    
                    if (tolocation !='') {
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
                            $('#' + rowid + '_ToL').removeClass('is-valid').addClass('is-invalid');
                        }
                        else {
                            $('.add-row').each(function () {
                                var row = $(this);
                                var rid = row.attr('id');
                                //alert(rid + ' - ' + rowid);
                                if (rowid != rid) {
                                    if ($('#' + rid + '_FromDt').val() == fromDate) {
                                        if ($('#' + rid + '_Fromtime').val() == fromTime) {
                                            mduprec = 1;
                                            if ($('#' + rid + '_FromL').val() == fromlocation) {
                                                if ($('#' + rid + '_ToL').val() == tolocation) {
                                                    duprec = 1;
                                                }
                                            }
                                        }
                                    }
                                };
                            });
                            if (mduprec == 0) {
                                if (duprec == 0) {                                    
                                    $.ajax({
                                        url: '/CTV/GetSchToDate',
                                        method: 'GET',
                                        data: {
                                            Fromdate: fromDate,
                                            FromTime: fromTime,
                                            FromLocation: fromlocation,
                                            ToLocationType: x,
                                            ToLocation: y,
                                            VehicleNo: vehicleno
                                        },
                                        dataType: 'json',
                                        success: function (data) {
                                            $(data).each(function (index, item) {
                                                todateCtrl.val(item.sResponseString);
                                                if (item.bResponseBool == true) {
                                                    todateCtrl.removeClass('is-invalid').addClass('is-valid');
                                                }
                                                else {
                                                    todateCtrl.removeClass('is-valid').addClass('is-invalid');
                                                    Swal.fire({
                                                        title: 'Can not schedule to the destination',
                                                        text: 'The calculated schedule to date is already occupied',
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
                                            activateSubmitBtn();
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
                            } else {
                                todateCtrl.removeClass('is-valid').addClass('is-invalid');
                                Swal.fire({
                                    title: 'Warning!',
                                    text: 'Vehicle cannot be scheduled twice in a particular time.',
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

    //activateSubmitBtn();
    
};
function DisableCombo() {
    $('#CCOth').val(0).removeClass('is-valid').addClass('is-invalid');
    $('#DIC').val(0).removeClass('is-valid').addClass('is-invalid');

};
$(document).ready(function () {
    var maxdt = $('#MaxDate').val();
    var mindt = $('#MinDate').val();
    $('#0_FromDt').attr('max', maxdt).attr('min', mindt);

    var FromLT = $('#0_FromLT');
    var ToLT = $('#0_ToLT');
    var mmtctrl = $('#mmt');
    $.ajax({
        url: '/CTV/GetLocationTypes',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            ToLT.empty();
            ToLT.multiselect('destroy');
            FromLT.append($('<option/>', { value: "-1", text: "Select location type" }));
            //ToLT.append($('<option/>', { value: "-1", text: "Select location type" }));
            $(data).each(function (index, item) {
                FromLT.append($('<option/>', { value: item.ID, text: item.DisplayText }));
                ToLT.append($('<option/>', { value: item.ID, text: item.DisplayText }));
                //$("#0_ToLT").append("<option value='" + item.ID + "'>" + item.DisplayText + "</option>");
            });
            $("#0_ToLT").attr('multiple', 'multiple');
            $("#0_ToLT").multiselect({
                templates: {
                    button: '<button id="B0" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                },
            });
            $("#0_ToLT").multiselect('clearSelection');
            //ToLT.multiselect('refresh');
            //$("#0_ToLT").multiselect('refresh');
            //getInitialData();
        }
    });    
});
//$(function () {
//    $('.datepicker2').datepicker({
//        changeMonth: true,
//        changeYear: true,
//        dateFormat: 'dd-M-yy',
//        minDate: new Date(),
//        maxDate: "+1m",
//        autoclose: true
//    });
//});