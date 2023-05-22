$("#VehicleNumber").on("change", function () {
    var myCtrl = $(this);
    if (myCtrl.val() != '') {
        var errorDiv = $('#divError');
        var btnLS = $('#btnLVT');
        var btnOS = $('#btnOVT');
        var errorDivLVS = $('#divErrorLVS');
        var errorDivOTS = $('#divErrorOTS');
        var ErrlblLVS = $('#lblErrorLVS');
        var ErrlblOTS = $('#lblErrorOTS');
        errorDivLVS.addClass('inVisible');
        errorDivOTS.addClass('inVisible');
        $.ajax({
            url: '/CTV2/GetVehicleInfo',
            method: 'GET',
            data: { VehicleNumber: myCtrl.val() },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    if (item.IsSuccess) {
                        errorDiv.removeClass('show');
                        myCtrl.isValid();
                        if (item.IsActive) {
                            if (item.LocalTripRecords > 0) {
                                btnLS.makeEnable();
                                if ($('#IsOTSActivated').val() == 0) {
                                    btnOS.makeDisable();
                                }
                                else {
                                    if (item.IsSlotAvbl) {
                                        btnOS.makeEnable();
                                    }
                                    else {
                                        btnOS.makeDisable();
                                        errorDivOTS.removeClass('inVisible');
                                        ErrlblOTS.html('Local Vehicle Schedule Was Scheduled For All The Dates, So Other Trip Schedule Cannot Be Enabled');
                                        MyAlert(5, 'Local Vehicle Schedule Was Scheduled For All The Dates, So "Other Trip Schedule - Date Wise Vehicle Trip Details Entry" Button Cannot Be Enabled');
                                    }
                                }
                            }
                            else {
                                if (item.IsSlotAvbl) {
                                    btnOS.makeEnable();
                                }
                                else {
                                    btnOS.makeDisable();
                                    errorDivOTS.removeClass('inVisible');
                                    ErrlblOTS.html('Local Vehicle Schedule Was Scheduled For All The Dates, So Other Trip Schedule Cannot Be Enabled');
                                    MyAlert(5, 'Local Vehicle Schedule Was Scheduled For All The Dates, So "Other Trip Schedule - Date Wise Vehicle Trip Details Entry" Button Cannot Be Enabled');
                                }
                                btnLS.makeDisable();
                                errorDivLVS.removeClass('inVisible');
                                ErrlblLVS.html('Local Vehicle Schedule Was Not Updated, So Button Cannot Be Enabled');
                                MyAlert(5, 'Local Vehicle Schedule Was Not Updated, So "Local Vehicle Schedule - Date Wise Vehicle Trip Details Entry" Button Cannot Be Enabled');
                            }
                        }
                        else {
                            $('#lblError').html('Vehicle is not active');
                            errorDiv.addClass('show');
                            myCtrl.isInvalid();
                            btnOS.makeDisable();
                            btnLS.makeDisable();
                            MyAlert(5, 'Vehicle Is Not Active');
                        }
                    }
                    else {
                        $('#lblError').html(item.Msg);
                        errorDiv.addClass('show');
                        myCtrl.isInvalid();
                        btnOS.makeDisable();
                        btnLS.makeDisable();
                        MyAlert(5, item.Msg);
                    }
                    $('#cVehicleType').val(item.VehicleType);
                    $('#cModelName').val(item.ModelName);
                    $('#cDriverNumberName').val(item.DriverNonName);
                });
                myCtrl.isValid();
            } 
        });
    } else {
        myCtrl.isInvalid();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});

$(document).ready(function () {
    LockSection('Section3');
    //UnLockSection('Section3');
});