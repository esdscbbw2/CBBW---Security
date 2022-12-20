/// <reference path="../scripts/jquery-3.4.1.min.js" />
function getSelectedCheckBoxText(checkboxContainer) {
    var r = "";
    var chkboxes = $('#' + checkboxContainer + ' input[type="checkbox"]:checked');
    if (chkboxes.length > 0) {
        chkboxes.each(function () {
            r += $('#T' + $(this).attr("id")).attr("value") + ",";
        });
        r = r.slice(0, -1);
    }
    else {
        r = "No options selected";
    }
    return r;
}
function ShowSelectedCheckBoxText(checkboxContainer, resultContainer) {      
    $('#' + resultContainer).empty().append(getSelectedCheckBoxText(checkboxContainer));    
}


