$("#EmployeeNumber").on("change", function () {
    var desg = $("#EmployeeNumber option:selected").attr('data-desc');
    var myCtrl = $(this);
    if (myCtrl.val() > 0) {
        $('#EmpDesg').val(desg);
        myCtrl.isValid();
    } else {
        $('#EmpDesg').val('');
        myCtrl.isInvalid();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#UserName").on("keyup", function () {
    var myCtrl = $(this);
    if (isAlphanumeric(myCtrl.val())) {
        myCtrl.isValid();
    } else {
        myCtrl.isInvalid();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#NewPassword").on("keyup", function () {
    var myCtrl = $(this);
    if (validatePassword(myCtrl.val())) {
        myCtrl.isValid();
    } else {
        myCtrl.isInvalid();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#CnfPassword").on("keyup", function () {
    var myCtrl = $(this);
    if (validatePassword(myCtrl.val())) {
        myCtrl.isValid();
    } else {
        myCtrl.isInvalid();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv');
});
$("#CnfPassword").on("blur", function () {
    var myCtrl = $(this);
    var newpassword = $('#NewPassword').val();
    if (newpassword == myCtrl.val()) {
        myCtrl.isValid();
    }
    else {
        MyAlert(4, 'Password Confirmation Failed.')
        myCtrl.isInvalid();
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
