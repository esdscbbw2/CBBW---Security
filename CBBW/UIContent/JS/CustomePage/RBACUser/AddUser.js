$("#EmployeeNumber").on("change", function () {
    var desg = $("#EmployeeNumber option:selected").attr('data-desg');
    var myCtrl = $(this);
    if (myCtrl.val() > 0) {
        $('#EmpDesg').val(desg);
        myCtrl.isValidCtrl();
    } else {
        $('#EmpDesg').val('');
        $("#UserName").val('').addClass('is-invalid');
        myCtrl.isInvalidCtrl();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#UserName").on("keyup", function () {
    var myCtrl = $(this);
    if (isAlphanumeric(myCtrl.val())) {
        var url = '/UserManagement/ValidateUserName?UserName=' + $(this).val();
        GetDataFromAjax(url).done(function (data) {
            if (data) { myCtrl.isValidCtrl(); }
            else {
                myCtrl.isInvalidCtrl();
                $('#NewPassword').val('');
                MyAlert(4, 'User Name Already Exist');
            }
        });        
    } else {
        myCtrl.isInvalidCtrl();
        $('#NewPassword').val('');
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#NewPassword").on("keyup", function () {
    var myCtrl = $(this);
    if (validatePassword(myCtrl.val())) {
        myCtrl.isValidCtrl();
    } else {
        myCtrl.isInvalidCtrl();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#CnfPassword").on("keyup", function () {
    var myCtrl = $(this);
    if (validatePassword(myCtrl.val())) {
        myCtrl.isValidCtrl();
    } else {
        myCtrl.isInvalidCtrl();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#CnfPassword").on("blur", function () {
    var myCtrl = $(this);
    var newpassword = $('#NewPassword').val();
    if (newpassword == myCtrl.val()) {
        myCtrl.isValidCtrl();
    }
    else {
        MyAlert(4, 'Password Confirmation Failed.')
        myCtrl.isInvalidCtrl();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#EffectiveDate").on("change", function () {
    var myCtrl = $(this);
    if (myCtrl.val() !='') {
        myCtrl.isValid();
    } else {
        myCtrl.isInvalid();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
function RolesChanged() {
    var myCtrl = $(RolesChanged.caller.arguments[0].target);
    if (myCtrl.val() != '') { myCtrl.isValid(); } else { myCtrl.isInvalid(); }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
};
function CentreChanged() {
    var myCtrl = $(CentreChanged.caller.arguments[0].target);
    if (myCtrl.val() != '') { myCtrl.isValid(); } else { myCtrl.isInvalid(); }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
};
