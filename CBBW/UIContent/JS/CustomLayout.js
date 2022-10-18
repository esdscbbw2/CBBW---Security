$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
$(document).ready(function () {
    $('.example-getting-started').multiselect({
        templates: {
            button: '<button type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
        },
    });
});
$(function () {
    $('.datepicker1').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-M-yy',
        minDate: new Date(),
        maxDate: "+1m",
        autoclose: true
    });
});

$(document).ready(function () {
    $(".only-numeric").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode

        if (!(keyCode >= 48 && keyCode <= 57)) {
            $(".error").css("display", "inline");
            return false;
        } else {
            $(".error").css("display", "none");
        }
    });
    $(".only-decimal").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        //alert(keyCode);
        if (!(keyCode >= 48 && keyCode <= 57)) {
            if (keyCode != 46) { return false; }
            //$(".error").css("display", "inline");                    
        } else {
            //$(".error").css("display", "none");
        }
    });
});
