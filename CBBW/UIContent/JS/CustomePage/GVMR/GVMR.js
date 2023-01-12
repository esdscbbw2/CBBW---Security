$('#NoteNo').change(function () {
   
    ExistingRecord($(this).val());
    Notenumberchanged($(this).val());
    
});

function ExistingRecord(notenumber) {
    var that = $('#NoteNo');
    $.ajax({
        url: '/GVMR/GetGVMRDetailsForView',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
            debugger;
            $(data).each(function (index, item) {
                $('#tbody3').removeClass('inVisible');
                if (index > 0) {
                    var cloneready = $('#tbody3').find('tr').clone();
                    cloneready.attr("id", index);
                    cloneready.find('td').find('#0_LocationNameD').attr('id', index + '_LocationNameD');
                    cloneready.find('td').find('#0_ActualInRFIDCardD').attr('id', index + '_ActualInRFIDCardD');
                    cloneready.find('td').find('#0_ActualTripInDateD').attr('id', index + '_ActualTripInDateD');
                    cloneready.find('td').find('#0_ActualTripInTimeD').attr('id', index + '_ActualTripInTimeD');
                    cloneready.find('td').find('#0_ActualTripInKMD').attr('id', index + '_ActualTripInKMD');
                    cloneready.find('td').find('#0_ActualOutRFIDCardD').attr('id', index + '_ActualOutRFIDCardD');
                    cloneready.find('td').find('#0_ActualTripOutDateD').attr('id', index + '_ActualTripOutDateD');
                    cloneready.find('td').find('#0_ActualTripOutTimeD').attr('id', index + '_ActualTripOutTimeD');
                    cloneready.find('td').find('#0_ActualTripOutKMD').attr('id', index + '_ActualTripOutKMD');
                    cloneready.find('td').find('#0_RemarkD').attr('id', index + '_RemarkD');
                   
                    $('#tbody4').append(cloneready);
                }
                $("#" + index + "_LocationNameD").html(item.LocationName);
                $("#" + index + "_ActualInRFIDCardD").html(item.ActualInRFIDCard);
                $("#" + index + "_ActualTripInDateD").html(item.ActualTripInDateDisplay);
                $("#" + index + "_ActualTripInTimeD").html(item.ActualTripInTime);
                $("#" + index + "_ActualTripInKMD").html(item.ActualTripInKM);
                $("#" + index + "_ActualOutRFIDCardD").html(item.ActualOutRFIDCard);
                $("#" + index + "_ActualTripOutDateD").html(item.ActualTripOutDateDisplay);
                $("#" + index + "_ActualTripOutTimeD").html(item.ActualTripOutTime);
                $("#" + index + "_ActualTripOutKMD").html(item.ActualTripOutKM);
                $("#" + index + "_RemarkD").html(item.Remark);
               
            });
           
        }
    });
};

function Notenumberchanged(notenumber) {
    var that = $('#NoteNo');
    $.ajax({
        url: '/GVMR/GetGVMRDetails',
        method: 'GET',
        data: { Notenumber: notenumber },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                
                $('#CenterCode').val(item.LocationName);
                $('#VehicleNo').val(item.VehicleNo);
                $('#VehicleType').val(item.VehicleType);
                $('#ModelName').val(item.ModelName);
                var DriverNameCode = item.DriverNo + "/" + item.DriverName;
                $('#DriverName').val(DriverNameCode);
                that.addClass('is-valid').removeClass('is-invalid');
                
                if (index > 0) {
                    var cloneready = $('#tbody1').find('tr').clone();
                    cloneready.attr("id", index);
                    cloneready.find('td').find('#0_LocationName').attr('id', index + '_LocationName');
                    cloneready.find('td').find('#0_Gvmrid').attr('id', index + '_Gvmrid');
                    cloneready.find('td').find('#0_ActualInRFIDCard').attr('id', index + '_ActualInRFIDCard');
                    cloneready.find('td').find('#0_FromDateVal').attr('id', index + '_FromDateVal');
                    cloneready.find('td').find('#0_ToDateVal').attr('id', index + '_ToDateVal');
                    cloneready.find('td').find('#0_ActualTripInDate').attr('id', index + '_ActualTripInDate');
                    cloneready.find('td').find('#0_ActualTripInTime').attr('id', index + '_ActualTripInTime');
                    cloneready.find('td').find('#0_ActualTripInKM').attr('id', index + '_ActualTripInKM');
                    cloneready.find('td').find('#0_ActualOutRFIDCard').attr('id', index + '_ActualOutRFIDCard');
                    cloneready.find('td').find('#0_ActualTripOutDate').attr('id', index + '_ActualTripOutDate');
                    cloneready.find('td').find('#0_ActualTripOutTime').attr('id', index + '_ActualTripOutTime');
                    cloneready.find('td').find('#0_ActualTripOutKM').attr('id', index + '_ActualTripOutKM');
                    cloneready.find('td').find('#0_Remark').attr('id', index + '_Remark');
                    
                    $('#tbody2').append(cloneready);
                }
                $("#" + index + "_LocationName").val(item.LocationName);
                $("#" + index + "_Gvmrid").val(item.Gvmrid);
                $("#" + index + "_ActualInRFIDCard").val();
                $("#" + index + "_FromDateVal").val(item.SchFromDateDisplay);
                $("#" + index + "_ToDateVal").val(item.ToDateVal);
                $("#" + index + "_ActualTripInDate").val();
                $("#" + index + "_ActualTripInTime").val();
                $("#" + index + "_ActualTripInKM").val(item.ActualTripInKM);
                $("#" + index + "_ActualOutRFIDCard").val();
                $("#" + index + "_ActualTripOutDate").val();
                $("#" + index + "_ActualTripOutTime").val();
                $("#" + index + "_ActualTripOutKM").val(item.ActualTripOutKM);
                $("#" + index + "_Remark").val();
                GetRFIDCardNos(index);
            });
            $('#in_out_details').removeClass('invisible');
        }
    });
};
function GetRFIDCardNos(index) {
    var FRIDData = $('#' + index + '_ActualInRFIDCard');
    var FRIDDataout = $('#' + index + '_ActualOutRFIDCard');
    $.ajax({
        url: '/MaterialGatePass/GetRFIdCards',
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            FRIDData.append($('<option/>', { value: "-1", text: "Select RFID" }));
            $(data).each(function (index, item) {
                FRIDData.append($('<option/>', { value: item.RFIDCardNo, text: item.RFIDCardNo }));
            });
            FRIDData.append($('<option/>', { value: "NA", text: "NA" }));

            FRIDDataout.append($('<option/>', { value: "-1", text: "Select RFID" }));
            $(data).each(function (index, item) {
                FRIDDataout.append($('<option/>', { value: item.RFIDCardNo, text: item.RFIDCardNo }));
            });
            FRIDDataout.append($('<option/>', { value: "NA", text: "NA" }));
            //DisplayData();
        }
    });

};
function GetActualDate() {
    var target = GetActualDate.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    var _val = $('#' + rowid + '_ActualInRFIDCard option:selected').val();
    var tripdatein = $('#' + rowid + '_ActualTripInDate');
    var fromDateVal = $('#' + rowid + '_FromDateVal').val();
    var toDateVal = $('#' + rowid + '_ToDateVal').val();
   
    if (_val == 'NA') {
        
        tripdatein.datepicker("destroy");
        tripdatein.removeClass('ActualTripIDates').addClass('ActualInDates');
        tripdatein.val('');
        tripdatein.removeClass('is-valid').addClass('is-invalid');
        $('.ActualInDates').datepicker({
            dateFormat: 'dd/mm/yy',
            autoclose: true,
            todayHighlight: true,
            minDate: fromDateVal,
            maxDate: toDateVal
        });
    } else {
        tripdatein.datepicker("destroy");
        tripdatein.removeClass('ActualInDates').addClass('ActualTripIDates');
        tripdatein.val('');
        tripdatein.removeClass('is-valid').addClass('is-invalid');
        
        DatePicker();
    }
    
};
function GetDateUsingRFID() {
    var target = GetDateUsingRFID.caller.arguments[0].target;
    var rowids = $(target.closest('.add-row')).attr("id");
    var _vals = $('#' + rowids + '_ActualOutRFIDCard option:selected').val();
    var tripdateout = $('#' + rowids + '_ActualTripOutDate');
    var fromDateVals = $('#' + rowids + '_FromDateVal').val();
    var toDateVals = $('#' + rowids + '_ToDateVal').val();
    if (_vals == 'NA') {
        tripdateout.datepicker("destroy");
        tripdateout.removeClass('ActualTripIDates').addClass('ActualInDates');
        tripdateout.val('');
        tripdateout.removeClass('is-valid').addClass('is-invalid');
        $('.ActualInDates').datepicker({
            dateFormat: 'dd/mm/yy',
            autoclose: true,
            todayHighlight: true,
            minDate: fromDateVals,
            maxDate: toDateVals
        });
    } else {
        tripdateout.datepicker("destroy");
        tripdateout.removeClass('ActualInDates').addClass('ActualTripIDates');
        tripdateout.val('');
        tripdateout.removeClass('is-valid').addClass('is-invalid');
        DatePicker();
    }

};
function DatePicker() {
    var minDate = new Date();
    var maxDate = new Date();
    $('.ActualTripIDates').datepicker({
        dateFormat: 'dd/mm/yy',
        autoclose: true,
        todayHighlight: true,
        minDate: minDate,
        maxDate: maxDate
    });

};
function ValidateControls() {
    var target = ValidateControls.caller.arguments[0].target;
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

    if (value == "" || value == null) { isvalid = false; } else { isvalid = true }
    activateSubmitBtn();
    return isvalid;

};
function activateSubmitBtn() {

    var btnSubmit = $('#btnSubmit');
    if ($('.is-invalid').length > 0) {
        btnSubmit.attr('disabled', 'disabled');
    } else {
        btnSubmit.removeAttr('disabled');
    }
};
function getCurrentRecords() {
    var Currrecords = [];
    $('.add-row').each(function () {
        var rowid = $(this).attr('id');
        if (rowid >= 0) {
            Currrecords.push({
                'Gvmrid': $('#' + rowid + '_Gvmrid').val(),
                'NoteNo': $('#NoteNo').val(),
                'ActualInRFIDCard': $('#' + rowid + '_ActualInRFIDCard').val(),
                'ActualTripInDate': $('#' + rowid + '_ActualTripInDate').val(),
                'ActualTripInTime': $('#' + rowid + '_ActualTripInTime').val(),
                'ActualTripInKM': $('#' + rowid + '_ActualTripInKM').val(),
                'ActualOutRFIDCard': $('#' + rowid + '_ActualOutRFIDCard').val(),
                'ActualTripOutDate': $('#' + rowid + '_ActualTripOutDate').val(),
                'ActualTripOutTime': $('#' + rowid + '_ActualTripOutTime').val(),
                'ActualTripOutKM': $('#' + rowid + '_ActualTripOutKM').val(),
                'Remark': $('#' + rowid + '_Remark').val()
            });
        }
    });
    return Currrecords;
};
function SaveData() {
   
    var currentrecord = getCurrentRecords();
    //alert(JSON.stringify(currentrecord));
   
    $.ajax({
        method: 'POST',
        url: '/Security/GVMR/Create',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            gvmrdatasave: currentrecord
        }),
        success: function (data) {
            $(data).each(function (index, item) {
                if (item.bResponseBool == true) {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Data saved successfully.',
                        icon: 'success',
                        setTimeout:5000,
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    }).then(callback);
                    function callback(result) {
                        if (result.value) {
                            var url = "/Security/GVMR/Index";
                            window.location.href = url;
                        }
                    };
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

//function btnClearClicked() {

//    $('.canclear').each(function () {
//        $(this).val('');
//        //$(this).removeClass('is-valid').addClass('is-invalid');
//    });

//};


