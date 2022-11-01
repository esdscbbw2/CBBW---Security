$.fn.isInvalid = function () {
    var that = this;
    that.addClass('is-invalid').removeClass('is-valid')
};
$.fn.isValid = function () {
    var that = this;
    that.addClass('is-valid').removeClass('is-invalid')
};
$(document).ready(function () {
    
    $('#NoteNo').change(function () {
        var that= $(this);        
        var notenumber = that.val();
        alert(notenumber);
        $.ajax({
            url: '/CTV/getDataFromNote',
            method: 'GET',
            data: { Notenumber: notenumber},
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    $('#txtCenterCodenName').val(item.CentreCodenName);
                    $('#txtForTheMonthYear').val(item.FortheMonthnYear);
                    $('#txtFromDate').val(item.FromDate);
                    $('#txtToDate').val(item.ToDate);
                    $('#txtVehicleNo').val(item.Vehicleno);
                    $('#txtVehicleType').val(item.VehicleType);
                    $('#txtModelName').val(item.ModelName);
                    $('#txtDriverNonName').val(item.DriverNonName);
                });

            }
        });
    });
});