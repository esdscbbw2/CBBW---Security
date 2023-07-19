function btn1Clicked() {
    PostDataInAjaxWithResponseHandleing('/Security/EHG/SetDummyData','',true,false);
};
function btn2Clicked() {
    var x = GetDataFromDivHavingNoTables('myDiv');
    PostDataInAjaxWithResponseHandleing('/Security/EHG/SetDummyData', x, false, '', true, function () {
        alert('ok');
    });
};
function btn3Clicked() {
    MyConfirmationAlert("From Btn3?", "");
};