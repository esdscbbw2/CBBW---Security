$(document).ready(function () {
    var vehiclenodropdown = $('#Vehicleno');
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
                        $('#divError').addClass('inVisible');
                        vehiclenodropdown.removeClass('is-invalid').addClass('is-valid');
                    } else {
                        $('#lblError').html(item.Msg);
                        $('#divError').removeClass('inVisible');
                        vehiclenodropdown.removeClass('is-valid').addClass('is-invalid');
                    }
                    $('#VehicleType').val(item.VehicleType);
                    $('#ModelName').val(item.ModelName);
                    $('#DriverNonName').val(item.DriverNonName);
                });
            }
        });
    });
});