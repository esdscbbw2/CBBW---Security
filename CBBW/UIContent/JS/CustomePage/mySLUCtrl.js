/* Sequential Lock Unlock Controlls Inside A Container - For CBBW New Requirement
 * Follow The Instruction
 * 1. Container Should Have "SLUContainer" Class
 * 2. Controlls Should Have "SLUCtrl" Class
 * 3. Controlls Should Have "NOSLU" Class To Always Remain In Disabled State.
 * 4. "SLUBtn" Class For Buttons "SLUBtnOK" Class Is Used To Determine The Data Saved Inside The Inner Screen Or Not.
 * 6. "SLUStatement" Class For Div To Show Green Or Red Borders
 * 7. "SLUSection" Class Is For Sequentially Locking Unlocking Sections.
 * 8. Every Element Using SLU Classes Must Have Unique IDs
 */
function SLUNextCtrl(myCtrlID) {
    var myCtrl = $('#' + myCtrlID);
    var myDiv = myCtrl.closest('.SLUContainer');
    const inputControls = myDiv.find('.SLUCtrl');
    const currentIndex = inputControls.index(myCtrl);
    if (currentIndex < inputControls.length - 1) {
        if (inputControls.eq(currentIndex + 1).is(":visible")) {
            UnLockSLUCtrl(inputControls.eq(currentIndex + 1).attr('id'));
        } else {
            SLUNextCtrl(inputControls.eq(currentIndex + 1).attr('id'));
        }
    }
    else {
        var mySection = myCtrl.closest('.SLUSection');
        SLUNextSection(mySection.attr('id'));
    }
};
function SLUNextSection(mySectionID) {    
    var mySection = $('#' + mySectionID);
    if (mySection.hasClass('SLUStatement')) { MakeSLUSectionGreen(mySectionID); }
    var invalidcount = mySection.find('.is-invalid').length;
    var btns = mySection.find('.SLUBtn').length;
    var okbtns = mySection.find('.SLUBtnOK').length;
    if (!mySection.is(":visible")) { invalidcount = 0; btns = okbtns; }
    var allSections = $('.SLUSection');
    var currentSecIndex = allSections.index(mySection);
    if (invalidcount <= 0 && btns == okbtns) {        
        if (currentSecIndex < allSections.length - 1) {
            if (allSections.eq(currentSecIndex + 1).is(":visible")) {
                UnLockSLUSection(allSections.eq(currentSecIndex + 1).attr('id'));
                if (allSections.eq(currentSecIndex + 1).hasClass('SLUStatement')) {
                    MakeSLUSectionGreen(allSections.eq(currentSecIndex + 1).attr('id'));
                }
            }
            else {
                SLUNextSection(allSections.eq(currentSecIndex + 1).attr('id'));
            }
        }
    }    
};
$(document).ready(function () {
    $('.SLUCtrl').focus(function () {
        LockNextSLUCtrls($(this).attr('id'));
    });    
});
function LockNextSLUCtrls(myCtrlID) {
    var myCtrl = $('#' + myCtrlID);
    var myDiv = myCtrl.closest('.SLUContainer');
    var mySection = myCtrl.closest('.SLUSection');
    const inputControls = myDiv.find('.SLUCtrl');
    const currentIndex = inputControls.index(myCtrl);
    if (currentIndex < inputControls.length - 1) {
        if (inputControls.eq(currentIndex + 1).is(":visible")) {
            LockSLUCtrl(inputControls.eq(currentIndex + 1).attr('id'));
            LockNextSLUSection(mySection.attr('id'));
        }
        LockNextSLUCtrls(inputControls.eq(currentIndex + 1).attr('id'));
    }
};
function LockNextSLUSection(id) {
    var mySection = $('#' + id);
    var allSections = $('.SLUSection');
    var currentSecIndex = allSections.index(mySection);
    if (currentSecIndex < allSections.length - 1) {
        if (allSections.eq(currentSecIndex + 1).is(":visible")) {
            LockSLUSection(allSections.eq(currentSecIndex + 1).attr('id'));
        }
        LockNextSLUSection(allSections.eq(currentSecIndex + 1).attr('id'));
    }
};
function UnLockSLUSection(id) {
    var mySection = $('#' + id);
    mySection.find('.SLUCtrl').each(function () {
        UnLockSLUCtrl($(this).attr('id'));
    });
};
function LockSLUSection(id) {
    var mySection = $('#' + id);
    mySection.find('.SLUCtrl').each(function () {
        LockSLUCtrl($(this).attr('id'));
    });
};
function LockSLUCtrl(id) {
    var myCtrl = $('#' + id);
    myCtrl.attr('disabled', 'disabled');
    myCtrl.addClass('nodrop');
    if (myCtrl.hasClass('EntrynDisabledForEntry')) { myCtrl.EntrynDisabledForEntry(); }
    //For Multiselect
    myCtrl.find('.btn-default').each(function () {
        $(this).addClass('nodrop disabled bg-blue');
    });
};
function UnLockSLUCtrl(id) {
    var myCtrl = $('#' + id);
    myCtrl.removeAttr('disabled');
    myCtrl.removeClass('nodrop');
    if (myCtrl.hasClass('EntrynDisabledForEntry')) { myCtrl.EntrynEnableForEntry(); }
    //For Multiselect
    myCtrl.find('.btn-default').each(function () {
        $(this).removeClass('nodrop disabled bg-blue');
        $(this).removeAttr('disabled');
    });
};
$.fn.EntrynDisabledForEntry = function () {
    var that = this;
    that.val('');
    that.attr('disabled', 'disabled')
        .addClass('bg-blue border-red nodrop is-invalid')
        .removeClass('is-valid');
};
$.fn.EntrynEnableForEntry = function () {
    var that = this;
    that.removeAttr('disabled')
        .addClass('is-invalid')
        .removeClass('bg-blue border-blue is-valid nodrop');
    that.val('');
};
$.fn.isSLURed = function () {
    var that = this;
    that.addClass('border-red').removeClass('border-green');
};
$.fn.isSLUGreen = function () {
    var that = this;
    that.addClass('border-green').removeClass('border-red');
};
function MakeSLUSectionGreen(divID) {    
    var mDiv = $('#' + divID);
    x = mDiv.find('.is-invalid').length;
    if (x > 0) { mDiv.isSLURed(); } else { mDiv.isSLUGreen(); }
};
