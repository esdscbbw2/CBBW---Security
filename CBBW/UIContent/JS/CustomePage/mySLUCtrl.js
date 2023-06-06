/* Sequential Lock Unlock Controlls Inside A Container - For CBBW New Requirement
 * Follow The Instruction
 * 1. Container Should Have "SLUContainer" Class
 * 2. Controlls Should Have "SLUCtrl" Class
 * 3. Controlls Should Have "NOSLU" Class To Always Remain In Disabled State.
 * 4. "SLUBtn" Class For Buttons "SLUBtnOK" Class Is Used To Determine The Data Saved Inside The Inner Screen Or Not.
 * 6. "SLUStatement" Class For Div To Show Green Or Red Borders
 * 7. "SLUSection" Class Is For Sequentially Locking Unlocking Sections.
 * 8. Every Element Using SLU Classes Must Have Unique IDs
 * 9. "approvalForm" Class For Div & "ApproveCtrl" For Approve Status To Show Approval Patern.
 */
//////////
function SLUValid(myCtrlID) {
    SLUNextCtrl(myCtrlID); //Unlock the next visible control.
    var mySection = $('#' + myCtrlID).closest('.SLUSection'); //Geting the section of the control.
    var mySectionID = mySection.attr('id');
    
    var nextSectionID = GetNextSLUSectionID(mySectionID); //Getting next section id of myCtrl
    if (nextSectionID != 'NA') {
        if (IsValidSection(mySectionID)) { //Check Elligibility of current section to lock/unlock the next section
            UnLockSLUSection(nextSectionID);//unlock next section
        }
        else {
            LockSLUSection(nextSectionID);//lock next section 
        }
    } else { IsValidSection(mySectionID);}
};
function SLUInvalid(myCtrlID) {
    LockNextCtrlsInContainer(myCtrlID);
    var mySection = $('#' + myCtrlID).closest('.SLUSection'); //Geting the section of the control.
    var mySectionID = mySection.attr('id');
    var nextSectionID = GetNextSLUSectionID(mySectionID);
    LockSLUSection(nextSectionID);//lock next section
};
function SLUNextCtrl(myCtrlID) {
    var myCtrl = $('#' + myCtrlID);
    var myDiv = myCtrl.closest('.SLUContainer');
    const inputControls = myDiv.find('.SLUCtrl');
    const currentIndex = inputControls.index(myCtrl);
    if (currentIndex < inputControls.length - 1) {
        if (inputControls.eq(currentIndex + 1).is(":visible")) {
            UnLockSLUCtrl(inputControls.eq(currentIndex + 1).attr('id'));
        }
        else {
            SLUNextCtrl(inputControls.eq(currentIndex + 1).attr('id'));
        }
    }
};
function LockNextCtrlsInContainer(myCtrlID) {
    var myCtrl = $('#' + myCtrlID);
    var myDiv = myCtrl.closest('.SLUContainer');
    const inputControls = myDiv.find('.SLUCtrl');
    const currentIndex = inputControls.index(myCtrl);
    if (currentIndex < inputControls.length - 1) {
        if (inputControls.eq(currentIndex + 1).is(":visible")) {
            LockSLUCtrl(inputControls.eq(currentIndex + 1).attr('id'));
        }
        LockNextCtrlsInContainer(inputControls.eq(currentIndex + 1).attr('id'));
    }
};
///////// Helper functions
function LockSLUSection(id) {
    var mySection = $('#' + id);
    mySection.find('.SLUCtrl').each(function () {
        LockSLUCtrl($(this).attr('id'));
    });
    mySection.addClass('sectionB');
};
function UnLockSLUSection(id) {
    var mySection = $('#' + id);
    mySection.removeClass('sectionB');
    var i = 0;
    mySection.find('.SLUCtrl').each(function () {
        if (i == 0) { UnLockSLUCtrl($(this).attr('id')); }
        else { LockSLUCtrl($(this).attr('id')); }
        i += 1;
    });    
};
function IsValidSection(mySectionID) {
    var mySection = $('#' + mySectionID);
    if (mySection.hasClass('SLUStatement')) { //if section is a statement section
        MakeSLUSectionGreen(mySectionID);
    }
    if (mySection.hasClass('approvalForm')) { //if section is a approval/ratification section
        mySection.actApprovalSection();
    }
    return IsEnabledSection(mySection);
};
function GetNextSLUSectionID(currentSectionID) {
    var nextSectionID = "NA"
    alert(currentSectionID + ' - ' + nextSectionID);
    var mySection = $('#' + currentSectionID);
    var allSections = $('.SLUSection');
    var currentSecIndex = allSections.index(mySection);
    var x = 0;
    if (currentSecIndex < allSections.length - 1) {
        for (var i = currentSecIndex+1; i < allSections.length; i++) {
            if (allSections.eq(i).is(":visible")) { x = i; }
        }
        nextSectionID = allSections.eq(x).attr('id');
    }
    alert(currentSectionID+' - '+nextSectionID);
    return nextSectionID;
};
function IsEnabledSection(mySection) {
    var isvalid = false;
    var x = mySection.find('.is-invalid').length;
    var btns = mySection.find('.SLUBtn').length;
    var okbtns = mySection.find('.SLUBtnOK').length;
    if (x <= 0 && btns == okbtns) { isvalid = true; }
    return isvalid;
};
function LockSLUCtrl(id) {
    var myCtrl = $('#' + id);
    myCtrl.attr('disabled', 'disabled');
    myCtrl.addClass('nodrop');
    //For Multiselect
    var closestDiv = myCtrl.closest("div");
    closestDiv.find('.btn-default').each(function () {
        $(this).addClass('nodrop disabled bg-blue');
    });
};
function UnLockSLUCtrl(id) {
    var myCtrl = $('#' + id);
    myCtrl.removeAttr('disabled');
    myCtrl.removeClass('nodrop');
    //For Multiselect
    var closestDiv = myCtrl.closest("div");
    closestDiv.find('.btn-default').each(function () {
        $(this).removeClass('nodrop disabled bg-blue');
        $(this).removeAttr('disabled');
    });
};
///////////////////////////////////////////////////////////////////////////////



//function SLUSection(currentSectionID) {    
//    var mySection = $('#' + currentSectionID);
//    if (mySection.hasClass('approvalForm')) { mySection.actApprovalSection(); }
//    if (mySection.hasClass('SLUStatement')) { MakeSLUSectionGreen(currentSectionID); }
//    if (IsEnabledSLUSection(currentSectionID)) {
//        //alert(GetNextSLUSectionID(currentSectionID));
//        UnLockSLUSection(GetNextSLUSectionID(currentSectionID));
//    }
//    else {
//        LockSLUSection(GetNextSLUSectionID(currentSectionID));
//    }    
//};

//function SLUNextSection(mySectionID) {    
//    var mySection = $('#' + mySectionID);
//    var invalidcount = mySection.find('.is-invalid').length;
//    var btns = mySection.find('.SLUBtn').length;
//    var okbtns = mySection.find('.SLUBtnOK').length;
//    if (!mySection.is(":visible")) { invalidcount = 0; btns = okbtns; }
//    var allSections = $('.SLUSection');
//    var currentSecIndex = allSections.index(mySection);
//    if (invalidcount <= 0 && btns == okbtns) {        
//        if (currentSecIndex < allSections.length - 1) {
//            if (allSections.eq(currentSecIndex + 1).is(":visible")) {
//                UnLockSLUSection(allSections.eq(currentSecIndex + 1).attr('id'));
//                //if (allSections.eq(currentSecIndex + 1).hasClass('SLUStatement')) {
//                //    MakeSLUSectionGreen(allSections.eq(currentSecIndex + 1).attr('id'));
//                //}
//            }
//            else {
//                SLUNextSection(allSections.eq(currentSecIndex + 1).attr('id'));
//            }
//        }
//    }    
//};
$(document).ready(function () {
    $('.SLUCtrl').focus(function () {
        LockNextCtrlsInContainer($(this).attr('id'));
    });
    $('.ApproveCtrl').each(function () {
        var that = $(this);
        var mDiv = that.closest('.approvalForm');
        var app = that.val().toUpperCase();
        if (app == "YES" || app == "NO") {
            mDiv.isApprovalDone();
        } else {
            mDiv.isApprovalNotDone();
        }
    });
});

//function LockNextSLUSection(id) {
//    var mySection = $('#' + id);
//    var allSections = $('.SLUSection');
//    var currentSecIndex = allSections.index(mySection);
//    if (currentSecIndex < allSections.length - 1) {
//        if (allSections.eq(currentSecIndex + 1).is(":visible")) {
//            LockSLUSection(allSections.eq(currentSecIndex + 1).attr('id'));
//        }
//        LockNextSLUSection(allSections.eq(currentSecIndex + 1).attr('id'));
//    }
//};

//function UnLockSLUSection(id) {
//    var mySection = $('#' + id);
//    mySection.removeClass('sectionB');
//    var myCtrl = mySection.find('.SLUCtrl').eq(0);
//    myCtrl.focus();
//    LockNextSLUCtrls(myCtrl.attr('id'));
//    //mySection.find('.SLUCtrl').each(function () {
//    //    UnLockSLUCtrl($(this).attr('id'));
//    //});    
//};


$.fn.SLUEntrynDisabledForEntry = function () {
    var that = this;
    that.val('');
    that.attr('disabled', 'disabled')
        .addClass('bg-blue border-red nodrop is-invalid')
        .removeClass('is-valid');
};
$.fn.SLUEntrynEnableForEntry = function () {
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
$.fn.isApprovalNotDone = function () {
    var that = this;
    that.addClass('alert-danger').removeClass('alert-success');
};
$.fn.isApprovalDone = function () {
    var that = this;
    that.addClass('alert-success').removeClass('alert-danger');
};
$.fn.actApprovalSection = function () {
    var that = this;
    var y = that.find('.is-invalid').length;
    if (y <= 0) {
        var app = that.find('.ApproveCtrl').eq(0).val().toUpperCase();
        if (app == "YES" || app == "NO") { that.isApprovalDone(); } else { that.isApprovalNotDone();}
    }
    else { that.isApprovalNotDone(); }
};
$.fn.isInvalid = function () {
    var that = this;
    that.addClass('is-invalid valid').removeClass('is-valid');
    SLUInvalid(that.attr('id'));
};
$.fn.isValid = function () {
    var that = this;
    that.addClass('is-valid valid').removeClass('is-invalid');
    SLUValid(that.attr('id'));
};
function MakeSLUSectionGreen(divID) {    
    var mDiv = $('#' + divID);
    x = mDiv.find('.is-invalid').length;
    if (x > 0) { mDiv.isSLURed(); } else { mDiv.isSLUGreen(); }
};
function GetNextSLUSectionID(currentSectionID) {
    var nextSectionID = "NA"
    var mySection = $('#' + currentSectionID);
    var allSections = $('.SLUSection');
    var currentSecIndex = allSections.index(mySection);
    if (currentSecIndex < allSections.length - 1) {
        nextSectionID = allSections.eq(currentSecIndex + 1).attr('id');
    }
    return nextSectionID;
};
//function GetNextSLUCtrlID(currentCtrlID, currentContainerID) {
//    var nextCtrlID = "NA"
//    var myCtrl = $('#' + currentCtrlID);
//    var nextCtrl = myCtrl.next(".SLUCtrl");
//    if (nextCtrl.length > 0) {
//        if (nextCtrl.closest('.SLUContainer').attr('id') == currentContainerID) {
//            nextCtrlID = nextCtrl.attr('id');
//        }
//    }
//    return nextCtrlID;
//};
