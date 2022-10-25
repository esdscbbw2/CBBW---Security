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
};
function getToDate(rowid) {
    var todate = '';
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
                                });
                            }
                        });
                    }
                }
            }
        }
    }  
    todateCtrl.val(todate);
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