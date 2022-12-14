$.fn.makeEnabled = function () {
    var that = this;
    that.removeAttr('disabled');
};
$.fn.makeDisable = function () {
    var that = this;
    that.attr('disabled', 'disabled');
};
$.fn.isInvalid = function () {
    var that = this;
    that.addClass('is-invalid').removeClass('is-valid');
};
$.fn.isValid = function () {
    var that = this;
    that.addClass('is-valid').removeClass('is-invalid');
};
$.fn.makeVisible = function () {
    var that = this;
    that.removeClass('inVisible');
};
$.fn.makeInVisible = function () {
    var that = this;
    that.AddClass('inVisible');
};
$.fn.clearValidateClass = function () {
    var that = this;
    that.removeClass('is-valid').removeClass('is-invalid');
};
$.fn.CustomDateFormat = function () {
    var that = this;
    var parentid = that.attr('id');
    var lblid = 'lbl'+parentid ;
    var dt = this.val();
    var e = dt;
    if (dt.indexOf('/') != -1) {
        var e = dt.split('/').reverse().join('-');
    } else {
        var e = dt.split('-').reverse().join('-');
    }
    $('#' + lblid).html(e);
    //that.addClass('is-valid').removeClass('is-invalid')
};
function CloneRow_Backup(sourceTBody, destinationTBody, rowid, IsRemoveBtn, IsAddBtnEnable) {
    // Source table Body must have a row having (id="0" class="add-row")
    //The controlls should have a class named "alterID";
    // buttons should have class "cloneBtn" - For tooltip functionalities
    //"addBtn" and removeBtn are also used for corrosponding buttons of a row
    //"CustomDateFormatCloneRow" - This class is used for customdatepicker. 
    //If multiselects are in a row then use the class "clonemultiselect" and remove multiple attribute and the classes which are responsible for multiselect creations.
    //Use "htmlVal" class for a controll if the value will be picked from innerhtml.
    //There should be "th" tag which may exclusively used for Serial Number Purpose.
    //alert('CloneRow');
    var maxrows = 0, r = 0;
    var sourcebody = $('#' + sourceTBody);
    var destinationbody = $('#' + destinationTBody);
    $('#' + destinationTBody + ' tr').each(function () {
        var maxr = $(this).attr('id') * 1;
        if (maxr > maxrows) { maxrows = maxr; }
    });
    if (maxrows >= 1) { r = maxrows + 1; } else { r = 1; }//Geting maximum row
    var cloneready = sourcebody.find('tr').clone();
    cloneready.attr("id", r);
    cloneready.find('.alterID').each(function () {
        that = $(this);
        var mID = that.attr('id').split('_');
        var newID = mID[0] + '_' + r;
        that.attr('id', newID);
    });
    cloneready.find('.btn-group').remove();
    cloneready.find('.clonemultiselect').each(function () {
        that = $(this);
        that.multiselect({
            templates: {
                button: '<button type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
            },
        });
        that.multiselect('clearSelection');
        that.multiselect('refresh');
    });
    cloneready.find('.CustomDateFormatCloneRow').each(function () {
        $(this).change(function () {
            $(this).CustomDateFormatCloneRow();
        });
    });
    cloneready.find('.CustomTimeFormatCloneRow').each(function () {
        var firstOpen = true;
        var time;
        $(this).datetimepicker({
            useCurrent: false,
            format: "hh:mm A"
        }).on('dp.show', function () {
            if (firstOpen) {
                time = moment().startOf('day');
                firstOpen = false;
            } else {
                time = "01:00 PM"
            }

            $(this).data('DateTimePicker').date(time);
        });
    });
    cloneready.find('.cloneBtn').each(function () {
        that = $(this);
        that.on('mouseenter', function () {
            $(this).tooltip('show');
        });
        that.on('mouseleave click', function () {
            $(this).tooltip('hide');
        });
    });
    if (IsAddBtnEnable) {
        cloneready.find('.addBtn').makeEnabled();
    }
    else {
        cloneready.find('.addBtn').makeDisable();
    };
    if (IsRemoveBtn) {
        cloneready.find('.removeBtn').removeClass('inVisible');
    }
    else {
        cloneready.find('.removeBtn').addClass('inVisible');
    }
    sourcebody.find('.btn').each(function () {
        that = $(this);
        that.on('mouseleave click', function () {
            $(this).tooltip('hide');
        });
    });
    if (rowid == 0) {
        if (maxrows == 0) {
            destinationbody.append(cloneready);
        } else {
            $(cloneready).insertBefore('#' + destinationTBody + ' tr:first');
        }
    } else {
        $(cloneready).insertAfter('#' + rowid);
    }
    var sl = 2;
    $('#' + destinationTBody + ' th').each(function () {
        $(this).html(sl);
        sl += 1;
    });
};
function CloneRow(sourceTBody, destinationTBody, rowid,IsRemoveBtn,IsAddBtnEnable) {
    // Source table Body must have a row having (id="0" class="add-row")
    //The controlls should have a class named "alterID";
    // buttons should have class "cloneBtn" - For tooltip functionalities
    //"addBtn" and removeBtn are also used for corrosponding buttons of a row
    //"CustomDateFormatCloneRow" - This class is used for customdatepicker. 
   //If multiselects are in a row then use the class "clonemultiselect" and remove multiple attribute and the classes which are responsible for multiselect creations.
    //Use "htmlVal" class for a controll if the value will be picked from innerhtml.
    //There should be "th" tag which may exclusively used for Serial Number Purpose.
    //alert('CloneRow');
    var maxrows = 0, r = 0;
    var sourcebody = $('#' + sourceTBody);
    var destinationbody = $('#' + destinationTBody);
    $('#' + destinationTBody+' tr').each(function () {
        var maxr = $(this).attr('id') * 1;
        if (maxr > maxrows) { maxrows = maxr; }
    });
    if (maxrows >= 1) { r = maxrows + 1; } else { r = 1; }//Geting maximum row
    var cloneready = sourcebody.find('tr').clone();
    cloneready.attr("id", r);
    cloneready.find('.alterID').each(function () {
        that = $(this);
        var mID = that.attr('id').split('_');
        var newID = mID[0] + '_' + r;
        that.attr('id', newID);
        that.val('').isInvalid();
    });
    cloneready.find('.btn-group').remove();
    cloneready.find('.clonemultiselect').each(function () {
        that = $(this);
        that.multiselect({
            templates: {
                button: '<button type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
            },
        });
        that.multiselect('clearSelection');
        that.multiselect('refresh');
    });
    cloneready.find('.CustomDateFormatCloneRow').each(function () {
        $(this).change(function () {
            $(this).CustomDateFormatCloneRow();
        });
    });
    cloneready.find('.CustomTimeFormatCloneRow').each(function () {
        var firstOpen = true;
        var time;
        $(this).datetimepicker({
            useCurrent: false,
            format: "hh:mm A"
        }).on('dp.show', function () {
            if (firstOpen) {
                time = moment().startOf('day');
                firstOpen = false;
            } else {
                time = "01:00 PM"
            }

            $(this).data('DateTimePicker').date(time);
        });
    });
    cloneready.find('.cloneBtn').each(function () {
        that = $(this);
        that.on('mouseenter', function () {
            $(this).tooltip('show');
        });
        that.on('mouseleave click', function () {
            $(this).tooltip('hide');
        });
    });
    cloneready.find('.datelabel').each(function () {
        $(this).html('Select Date');
    });
    cloneready.find('.htmlVal').each(function () {
        $(this).html('');
    });
    if (IsAddBtnEnable) {
        cloneready.find('.addBtn').makeEnabled();
    }
    else {
        cloneready.find('.addBtn').makeDisable();
    };    
    if (IsRemoveBtn) {
        cloneready.find('.removeBtn').removeClass('inVisible');
    }
    else {
        cloneready.find('.removeBtn').addClass('inVisible');
    }
    sourcebody.find('.btn').each(function () {
        that = $(this);
        that.on('mouseleave click', function () {
            $(this).tooltip('hide');
        });
    });
    if (rowid == 0) {
        if (maxrows == 0) {
            destinationbody.append(cloneready);
        } else {
            $(cloneready).insertBefore('#' + destinationTBody+' tr:first');
        }
    } else {
        $(cloneready).insertAfter('#' + rowid);
    }
    var sl = 2;
    $('#' + destinationTBody+' th').each(function () {
        $(this).html(sl);
        sl += 1;
    });
};
function CloneRowReturningID(sourceTBody, destinationTBody, rowid, IsRemoveBtn, IsAddBtnEnable) {
    // Source table Body must have a row having (id="0" class="add-row")
    //The controlls should have a class named "alterID";
    // buttons should have class "cloneBtn" - For tooltip functionalities
    //"addBtn" and removeBtn are also used for corrosponding buttons of a row
    //"CustomDateFormatCloneRow" - This class is used for customdatepicker. 
    //If multiselects are in a row then use the class "clonemultiselect" and remove multiple attribute and the classes which are responsible for multiselect creations.
    //Use "htmlVal" class for a controll if the value will be picked from innerhtml.
    //There should be "th" tag which may exclusively used for Serial Number Purpose.
    //alert('CloneRow');
    var maxrows = 0, r = 0;
    var sourcebody = $('#' + sourceTBody);
    var destinationbody = $('#' + destinationTBody);
    $('#' + destinationTBody + ' tr').each(function () {
        var maxr = $(this).attr('id') * 1;
        if (maxr > maxrows) { maxrows = maxr; }
    });
    if (maxrows >= 1) { r = maxrows + 1; } else { r = 1; }//Geting maximum row
    var cloneready = sourcebody.find('tr').clone();
    cloneready.attr("id", r);
    cloneready.find('.alterID').each(function () {
        that = $(this);
        var mID = that.attr('id').split('_');
        var newID = mID[0] + '_' + r;
        that.attr('id', newID);
        that.val('').isInvalid();
    });
    cloneready.find('.btn-group').remove();
    cloneready.find('.clonemultiselect').each(function () {
        that = $(this);
        that.multiselect({
            templates: {
                button: '<button type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
            },
        });
        that.multiselect('clearSelection');
        that.multiselect('refresh');
    });
    cloneready.find('.CustomDateFormatCloneRow').each(function () {
        $(this).change(function () {
            $(this).CustomDateFormatCloneRow();
        });
    });
    cloneready.find('.CustomTimeFormatCloneRow').each(function () {
        var firstOpen = true;
        var time;
        $(this).datetimepicker({
            useCurrent: false,
            format: "hh:mm A"
        }).on('dp.show', function () {
            if (firstOpen) {
                time = moment().startOf('day');
                firstOpen = false;
            } else {
                time = "01:00 PM"
            }

            $(this).data('DateTimePicker').date(time);
        });
    });
    cloneready.find('.cloneBtn').each(function () {
        that = $(this);
        that.on('mouseenter', function () {
            $(this).tooltip('show');
        });
        that.on('mouseleave click', function () {
            $(this).tooltip('hide');
        });
    });
    cloneready.find('.datelabel').each(function () {
        $(this).html('Select Date');
    });
    cloneready.find('.htmlVal').each(function () {
        $(this).html('');
    });
    if (IsAddBtnEnable) {
        cloneready.find('.addBtn').makeEnabled();
    }
    else {
        cloneready.find('.addBtn').makeDisable();
    };
    if (IsRemoveBtn) {
        cloneready.find('.removeBtn').removeClass('inVisible');
    }
    else {
        cloneready.find('.removeBtn').addClass('inVisible');
    }
    sourcebody.find('.btn').each(function () {
        that = $(this);
        that.on('mouseleave click', function () {
            $(this).tooltip('hide');
        });
    });
    if (rowid == 0) {
        if (maxrows == 0) {
            destinationbody.append(cloneready);
        } else {
            $(cloneready).insertBefore('#' + destinationTBody + ' tr:first');
        }
    } else {
        $(cloneready).insertAfter('#' + rowid);
    }
    var sl = 2;
    $('#' + destinationTBody + ' th').each(function () {
        $(this).html(sl);
        sl += 1;
    });
    return r;
};
async function getMultiselectData(multiselectID,dataSourceURL) {
    var multiselectCtrl = $('#' + multiselectID);
    $.ajax({
        url: dataSourceURL,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            multiselectCtrl.empty();
            multiselectCtrl.multiselect('destroy');
            //ToLT.append($('<option/>', { value: "-1", text: "Select location type" }));
            $(data).each(function (index, item) {                
                multiselectCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
            });
            multiselectCtrl.attr('multiple', 'multiple');
            multiselectCtrl.multiselect({
                templates: {
                    button: '<button id="B0" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                },
            });
            multiselectCtrl.multiselect('clearSelection');
            multiselectCtrl.multiselect('refresh');
        }
    });
};
async function getMultiselectDataWithSelectedValues(multiselectID, dataSourceURL, commaSeparatedSelectedValues) {
    var multiselectCtrl = $('#' + multiselectID);
    var i = commaSeparatedSelectedValues.indexOf(',');
    $.ajax({
        url: dataSourceURL,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            multiselectCtrl.empty();
            multiselectCtrl.multiselect('destroy');
            //ToLT.append($('<option/>', { value: "-1", text: "Select location type" }));
            $(data).each(function (index, item) {
                multiselectCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
            });
            multiselectCtrl.attr('multiple', 'multiple');
            multiselectCtrl.multiselect({
                templates: {
                    button: '<button id="B0" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                },
            });
            multiselectCtrl.multiselect('clearSelection');
            if (i > 0) {
                locationcombo.val(locationid.split(','));
            } else {
                locationcombo.val(locationid);
            }
            multiselectCtrl.multiselect('refresh');
        }
    });
};
function ChangeCashCadingSourceInCloaning(destinationCtrlID,datasourceURL) {
    var target = ChangeCashCadingSourceInCloaning.caller.arguments[0].target;
    var targetCtrl = $(target);
    var targetid = targetCtrl.attr('id');
    //alert(targetid);
    var rowid = $(target.closest('.add-row')).attr("id");
    var i = targetid.indexOf('_');
    if (i >= 0) { destinationCtrlID = destinationCtrlID + '_' + rowid; }
    var x = '';
    $('#' + targetid + ' option:selected').each(function () {
        x = x + '_' + $(this).val();
    });
    datasourceURL = datasourceURL + x;
    //alert(datasourceURL);
    (async function () {
        const r1 = await getMultiselectData(destinationCtrlID, datasourceURL);
    })();
    if (targetCtrl.val().length > 0) { targetCtrl.isValid(); } else { targetCtrl.isInvalid();}
};
async function getDropDownData(DropDownID,defaultText, dataSourceURL) {
    var DropdownCtrl = $('#' + DropDownID);
    $.ajax({
        url: dataSourceURL,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            DropdownCtrl.empty();
            DropdownCtrl.append($('<option/>', { value: "-1", text: defaultText }));
            $(data).each(function (index, item) {
                DropdownCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
            });            
        }
    });
};
async function getDropDownDataWithSelectedValue(DropDownID, defaultText, dataSourceURL,selectedValue) {
    var DropdownCtrl = $('#' + DropDownID);
    $.ajax({
        url: dataSourceURL,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            DropdownCtrl.empty();
            DropdownCtrl.append($('<option/>', { value: "-1", text: defaultText }));
            $(data).each(function (index, item) {
                DropdownCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
            });
            DropdownCtrl.val(selectedValue);
        }
    });
};
function getRecordsFromTable(tableName) {
    //The fields should have an attribute "data-name", Which is the property name of the MVC object
    var schrecords='';
    var dataname;
    var datavalue;
    var mrecord='';
    $('#' + tableName+' tbody tr').each(function () {
        mRow = $(this);
        mRow.find('[data-name]').each(function () {
            that = $(this);
            dataname = that.attr('data-name');
            if (that.hasClass('htmlVal')) {
                datavalue = that.html();
            }
            else { datavalue = that.val(); }
            mrecord = mrecord+ '"' + dataname + '":"' + datavalue+'",';
        });
        mrecord = mrecord.replace(/,\s*$/, "");
        schrecords = schrecords +'{'+ mrecord+'},';
        mrecord = '';
    });
    schrecords = schrecords.replace(/,\s*$/, "");
    schrecords = '[' + schrecords + ']';
    return schrecords;
};
function getRecordsFromTableV2(tableName) {
    //The fields should have an attribute "data-name", Which is the property name of the MVC object
    var schrecords = '';
    var dataname;
    var datavalue;
    var mrecord = '';
    $('#' + tableName + ' tbody tr').each(function () {
        mRow = $(this);
        mRow.find('[data-name]').each(function () {
            that = $(this);
            dataname = that.attr('data-name');
            if (that.hasClass('htmlVal')) {
                datavalue = that.html();
            }
            else { datavalue = that.val(); }
            mrecord = mrecord + '"' + dataname + '":"' + datavalue + '",';
        });
        mRow.find('[data-name-text]').each(function () {
            that = $(this);
            dataname = that.attr('data-name-text');
            thatid = that.attr('id');
            datavalue = $('#' + thatid+' option:selected').toArray().map(item => item.text).join();
            mrecord = mrecord + '"' + dataname + '":"' + datavalue + '",';
        });
        mrecord = mrecord.replace(/,\s*$/, "");
        schrecords = schrecords + '{' + mrecord + '},';
        mrecord = '';
    });
    schrecords = schrecords.replace(/,\s*$/, "");
    schrecords = '[' + schrecords + ']';
    return schrecords;
};
function removeBtnClickFromCloneRow(r,destinationTBody) {
    //var r = removeBtnClickFromCloneRow.caller.arguments[0].target.closest('.add-row');
    if ($(r).attr("id") == 0) {
    } else {
        r.remove();
    };
    var sl = 2;
    $('#' + destinationTBody+' th').each(function () {
        $(this).html(sl);
        sl += 1;
    });
}
function BackButtonClicked() {
    $.ajax({
        url: "/Security/Common/BackButtonClicked",
        success: function (result) { window.location.href = result; }
    });
};
function CustomDateChange(firstDate, addDays, DisplaySeparator) {
    first_date = new Date(firstDate);
    output_f = new Date(first_date.setDate(first_date.getDate() + addDays)).toISOString().split('.');
    output_s = output_f[0].split('T');
    //$('#second_date').val(output_s[0]);
    //$('#datetime').val(output_f[0]);
    var result = output_s[0];
    var e = result;
    if (result.indexOf('/') != -1) {
        e = result.split('/').reverse().join(DisplaySeparator);
    } else {
        e = result.split('-').reverse().join(DisplaySeparator);
    }
    return e;
}
function EnableAddBtnInCloneRow(tblRow, addBtnBaseID) {
    var tblrow = $(tblRow);
    var rowid = tblrow.attr('id')
    if (rowid != 0) { addBtnBaseID = addBtnBaseID + '_' + rowid; }
    var addBtnctrl = $('#' + addBtnBaseID);
    if (tblrow.find('.is-invalid').length > 0) { addBtnctrl.makeDisable(); } else { addBtnctrl.makeEnabled(); }
    
};



