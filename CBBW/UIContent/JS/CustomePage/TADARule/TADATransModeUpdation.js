function RemoveBtnClicked() {
    var tblRow = RemoveBtnClicked.caller.arguments[0].target.closest('.add-row');
    removeBtnClickFromCloneRow(tblRow, 'tbody2');
    EnableSubmitBtn();
};
function addCloneBtnClick() {
    var insrow = addCloneBtnClick.caller.arguments[0].target.closest('.add-row');
    var insrowid = $(insrow).attr('id');
    //alert(insrowid);
    var addbtn = $('#AddBtn');
    if (insrowid > 0) { addbtn = $('#AddBtn_' + insrowid); }
    var clonerowid = CloneRowReturningIDV2('tbody1', 'tbody2', $(insrow).attr('id') * 1, true, true);
    //addbtn.makeDisable();
};
function PublicTransportChange() {
    var insrow = PublicTransportChange.caller.arguments[0].target.closest('.add-row');
    var rowid = $(insrow).attr('id');
    var cmbCtrl = $('#PTC');    
    if (rowid > 0) {
        cmbCtrl = $('#PTC_' + rowid);        
    }
    var ptcvalue = cmbCtrl.val();
    ClearPTCheckboxes(rowid, ptcvalue);
    if (ptcvalue > 0) { cmbCtrl.isValid(); } else { cmbCtrl.isInvalid(); }
};
function ClearPTCheckboxes(rowid, ptcvalue) {
    var trainDiv = $('#TrainDiv');
    var busDiv = $('#BusDiv');
    var airDiv = $('#AirDiv');
    if (rowid > 0) {
        trainDiv = $('#TrainDiv_' + rowid);
        busDiv = $('#BusDiv_' + rowid);
        airDiv = $('#AirDiv_' + rowid);
    }
    trainDiv.find('.PTCOptions:checked').each(function () {
        $(this).prop('checked', false);;
    });
    busDiv.find('.PTCOptions:checked').each(function () {
        $(this).prop('checked', false);;
    });
    airDiv.find('.PTCOptions:checked').each(function () {
        $(this).prop('checked', false);;
    });
    trainDiv.addClass('inVisible');
    busDiv.addClass('inVisible');
    airDiv.addClass('inVisible');
    if (ptcvalue == 1) {
        trainDiv.removeClass('inVisible');
    }
    else if (ptcvalue == 2) {
        busDiv.removeClass('inVisible');
    }
    else if (ptcvalue == 3) {
        airDiv.removeClass('inVisible');
    }    
};






function addBtnClick() {
    var rows = $('#tbody2 tr').length + 1;
    var r = parseInt($('#tbody2 tr:last').attr("id"));
    if (r >= 1) { r = r + 1; } else { r = 1; }
    var cloneready = $('#tbody1').find('tr').clone();
    cloneready.attr("id", r);
    cloneready.find('#0_C').attr('id', r + '_C');
    var classdiv = cloneready.find('#0_D');
    classdiv.empty();
    classdiv.attr('id', r + '_D');
    var classdivr = cloneready.find('#0_DR');
    classdivr.val("");
    classdivr.attr('id', r + '_DR');
    var addbtn = cloneready.find('#0_addbtn');
    addbtn.attr('id', r + '_addbtn');
    addbtn.on('mouseenter', function () {
        $(this).tooltip('show');
    });
    addbtn.on('mouseleave click', function () {
        $(this).tooltip('hide');
    });
    var delbtn = cloneready.find('#0_delbtn');
    delbtn.attr('id', r + '_delbtn').removeClass("hideControl");
    delbtn.on('mouseenter', function () {
        $(this).tooltip('show');
    });
    delbtn.on('mouseleave click', function () {
        $(this).tooltip('hide');
    });
    //cloneready.find('.hideControl').removeClass("hideControl");

    if (rows < 3) {
        $('#tbody2').append(cloneready);
    }
    if (rows >= 2) {
        $('.addbtn').attr("disabled", "disabled");
        //sweet alert here
    }
    $('#0_addbtn').on('mouseleave click', function () {
        $(this).tooltip('hide');
    });

}
function removeBtnClick() {
    var r = removeBtnClick.caller.arguments[0].target.closest('.add-row');
    if ($(r).attr("id") == 0) {
    } else {
        r.remove();
        $('.addbtn').removeAttr("disabled", "disabled");
    };
    return false;
}
function GetTransportTypes() {
    var cmbType = $('#0_C');
    $.ajax({
        url: '/TADARules/GetPublicTransTypes',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            cmbType.append($('<option/>', { value: "-1", text: "select type" }));
            $(data).each(function (index, item) {
                cmbType.append($('<option/>', { value: item.ID, text: item.DisplayText }));
            });

            GetInitialData();
        }
    });
}
function comboChangeContext() {
    var target = comboChangeContext.caller.arguments[0].target;
    var rowid = $(target).closest('.add-row').attr("id");
    var value1 = $(target).val();
    var msg = 0;
    $('.dycom').each(function () {
        if ($(this).attr("id") != rowid + '_C') {
            if ($(this).val() == value1 && value1 > 0) {
                msg = 1;
            }
        }
    });
    var rowid = $(target).closest('.add-row').attr("id");
    var transportid = $(target).val();
    if (msg == 1) {
        $(target).val('-1');
        comboChangeContextChild(rowid, "-1");
        Swal.fire({
            title: 'Information Message',
            text: "You Have Selected Duplicate Transport Type",
            icon: 'warning',
            customClass: 'swal-wide',
            //buttons: {
            //    cancel: 'No',
            //    confirm: 'Yes'
            //},
            confirmButtonText: "OK",
            //cancelButtonText: "No",
            cancelButtonClass: 'btn-cancel',
            confirmButtonColor: '#2527a2',
            //showCancelButton: true,
        })
        //swal.fire('Duplicate transport type');
    } else {
        comboChangeContextChild(rowid, transportid);
    }

};
function comboChangeContextChild(rowid, transportid) {
    var classdiv = $('#' + rowid + '_D');
    var classtext = $('#' + rowid + '_DR');
    var r = '';
    var chk = '';
    $.ajax({
        url: '/TADARules/GetClassTypes',
        method: 'GET',
        data: { TypeID: transportid },
        dataType: 'json',
        success: function (data) {
            classdiv.empty();
            $(data).each(function (index, item) {
                if (item.IsSelected) {
                    //chk = 'checked="checked"';
                    chk = '';
                    r = '';
                } else {
                    chk = '';
                }
                classdiv.append('<input type="checkbox" class="form-check-input pointer" onclick="cbOptionClick()" name="' + item.DisplayText + '" value="' + item.ID + '" ' + chk + ' />&nbsp;&nbsp;<label class="form-check-label">' + item.DisplayText + '</label><br/>');
            });
            if (r == '') {
                classtext.val('no options selected');
            } else {
                classtext.val(r.slice(0, -2));
            }
            getallPubtranIds();
            validateSubmit();
        }
    });
};
function comboChangeContextChildForFirstTime(rowid, transportid) {
    var classdiv = $('#' + rowid + '_D');
    var classtext = $('#' + rowid + '_DR');
    var r = '';
    var chk = '';
    $.ajax({
        url: '/TADARules/GetClassTypes',
        method: 'GET',
        data: { TypeID: transportid },
        dataType: 'json',
        success: function (data) {
            classdiv.empty();
            $(data).each(function (index, item) {
                if (item.IsSelected) {
                    chk = 'checked="checked"';
                    r = r + item.DisplayText + ', ';
                } else {
                    chk = '';
                }
                classdiv.append('<input type="checkbox" class="form-check-input pointer" onclick="cbOptionClick()" name="' + item.DisplayText + '" value="' + item.ID + '" ' + chk + ' />&nbsp;&nbsp;<label class="form-check-label">' + item.DisplayText + '</label><br/>');
            });
            if (r == '') {
                classtext.val('no options selected');
            } else {
                classtext.val(r.slice(0, -2));
            }
            getallPubtranIds();
            validateSubmit();
        }
    });
};
function cbOptionClick() {
    var target = cbOptionClick.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    var classtext = $('#' + rowid + '_DR');
    var chkboxes = $('#' + rowid + '_D input[type="checkbox"]:checked');
    var r = '';
    if (chkboxes.length > 0) {
        chkboxes.each(function () {
            r += $(this).attr("name") + ', ';
        });
        r = r.slice(0, -2);
        $('#IsBtnSubmit').val("1");
    }
    else {
        r = "No options selected";
    }
    classtext.val(r);
    getallPubtranIds();
    validateSubmit();
}
function GetInitialData() {
    $.ajax({
        url: '/TADARules/GetInitialPTData',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            var r = '';
            $(data).each(function (index, item) {
                if (index == 0) {
                    comboChangeContextChildForFirstTime(index, item.TypeID);
                    $('#' + index + '_C').val(item.TypeID);
                } else {
                    addBtnClick();
                    comboChangeContextChildForFirstTime(index, item.TypeID);
                    $('#' + index + '_C').val(item.TypeID);
                }
                r = r + '#' + index + '_C' + " value = " + item.TypeID + "<br/>"
            });
        }
    });
}

function getmySelectedCheckBoxText(checkboxContainer) {
    var r = "";
    var chkboxes = $('#' + checkboxContainer + ' input[type="checkbox"]:checked');
    if (chkboxes.length > 0) {
        chkboxes.each(function () {
            r += $('#T' + $(this).attr("id")).attr("value") + ", ";
        });
        r = r.slice(0, -2);
    }
    else {
        r = "No options selected";
    }
    return r;
}
function getallmySelectedCheckBoxText() {
    var x = getmySelectedCheckBoxText("divmChkBox");
    $('#divChkBoxResult').val(x);
}
function getallPubtranIds() {
    var r = '';
    var chkboxes = $('#Pubtran input[type="checkbox"]:checked');
    if (chkboxes.length > 0) {
        chkboxes.each(function () {
            r += $(this).val() + ",";
        });
        r = r.slice(0, -1);
    }
    $('#SelectedpubTranOptions').val(r);
}
function validateToEnableSubmit() {
    var btnvalid = false;
    var pubtrans = $('#Pubtran input[type="checkbox"]:checked');
    var comptranms = $('#divmChkBox input[type="checkbox"]:checked');
    if (pubtrans.length > 0) {
        if (comptranms.length > 0) {
            if ($('#IsBtnSubmit').val() == 1) {
                btnvalid = true;
            }
        }
    }
    return btnvalid;
}
function validateSubmit() {
    var btnsubmit = $('#btnSubmit');
    if (validateToEnableSubmit()) {
        btnsubmit.removeAttr("disabled");
    } else { btnsubmit.attr("disabled", "disabled"); }
};
function btnClearClicked() {
    $('#Pubtran input[type="checkbox"]:checked').each(function () {
        $(this).prop("checked", false);
    });
    $('.pubtrantxt').val('');
    $('#divmChkBox input[type="checkbox"]:checked').each(function () {
        $(this).prop("checked", false);
    });
    $('#divChkBoxResult').val('');
    validateSubmit();
};
