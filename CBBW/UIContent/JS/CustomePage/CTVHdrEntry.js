$(document).ready(function () {
    var vehiclenodropdown = $('#Vehicleno');
    var errorDiv = $('#divError');
    var btnLS = $('#btnLVT');
    var btnOS = $('#btnOVT');
    vehiclenodropdown.change(function () {
        var vno = $(this).val();        
        $.ajax({
            url: '/CTV/GetVehicleInfo',
            method: 'GET',
            data: { VehicleNo: vno },
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    if (item.IsSuccess) {
                        errorDiv.addClass('inVisible');
                        vehiclenodropdown.removeClass('is-invalid').addClass('is-valid');
                    } else {
                        $('#lblError').html(item.Msg);
                        errorDiv.removeClass('inVisible');
                        vehiclenodropdown.removeClass('is-valid').addClass('is-invalid');
                    }
                    if (item.LocalTripRecords > 0) {
                        btnLS.removeAttr('disabled');
                        btnOS.attr('disabled','disabled');
                    }
                    else {
                        btnLS.attr('disabled','disabled');
                        btnOS.removeAttr('disabled');
                    }                    
                    $('#VehicleTypeDisplay').val(item.VehicleType);
                    $('#VehicleType').val(item.VehicleType);
                    $('#ModelName').val(item.ModelName);
                    $('#ModelNameDisplay').val(item.ModelName);
                    $('#DriverNonName').val(item.DriverNonName);
                    $('#DriverNonNameDisplay').val(item.DriverNonName);
                });
            }
        });
    });
});