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
    if (myCtrl.val() != '') {
        myCtrl.isValidCtrl();        
    } else { myCtrl.isInvalidCtrl(); }
    EnableAddBtn(myCtrl.attr('id').split('_')[1],'AddBtn');
};
function LocationTypeChanged() {
    var myCtrl = $(LocationTypeChanged.caller.arguments[0].target);
    var rowid = myCtrl.attr('id').split('_')[1];
    var myCashcadingID = 'LocationCode_' + rowid;
    if (myCtrl.val() != '') {
        var url = '/Security/CTV2/GetToLocationsFromTypes?TypeIDs=' + myCtrl.val();
        GetDataFromAjax(url).done(function (data) {
            refreshMultiselect(data, myCashcadingID, false);
        });
        myCtrl.isValidCtrl();
    } else { myCtrl.isInvalidCtrl(); }
    EnableAddBtn(myCtrl.attr('id').split('_')[1], 'AddBtn');
};
function LocationCodeChanged() {
    var myCtrl = $(LocationCodeChanged.caller.arguments[0].target);
    if (myCtrl.val() != '') { myCtrl.isValidCtrl(); } else { myCtrl.isInvalidCtrl(); }
    EnableAddBtn(myCtrl.attr('id').split('_')[1], 'AddBtn');
};
function FromDateChanged() {
    var myCtrl = $(FromDateChanged.caller.arguments[0].target);
    if (myCtrl.val() != '') { myCtrl.isValidCtrl(); } else { myCtrl.isInvalidCtrl(); }
    EnableAddBtn(myCtrl.attr('id').split('_')[1], 'AddBtn');
};
function ToDateChanged() {
    var myCtrl = $(ToDateChanged.caller.arguments[0].target);
    if (myCtrl.val() != '') { myCtrl.isValidCtrl(); } else { myCtrl.isInvalidCtrl(); }
    EnableAddBtn(myCtrl.attr('id').split('_')[1], 'AddBtn');
};
function CloneRowAddBtnClick() {
    var insrow = CloneRowAddBtnClick.caller.arguments[0].target.closest('.add-row');
    var myCtrl = $(CloneRowAddBtnClick.caller.arguments[0].target);
    var sRowid = $(insrow).attr('id');
    var cRowid = TableRowCloaning('tbody1', 'tbody2', sRowid, true, true, false);
    $('#tblSection').removeClass('sectionB');
    myCtrl.tooltip('hide');
};
function EnableAddBtn(rowid, addBtnBaseID) {
    var tblrow = $('#' + rowid);
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.find('.is-invalid').length > 0) {
        addBtnctrl.makeSLUDisable();
    }
    else {
        addBtnctrl.makeSLUEnable();
    }
    SubmitBtnStatus('btnSubmit', 'HdrDiv1');
};