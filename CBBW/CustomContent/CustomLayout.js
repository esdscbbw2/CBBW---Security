/// <reference path="../scripts/jquery-3.5.1.min.js" />

$(document).ready(function () {
    $(this).on('contextmenu', function (e) {
        e.preventDefault();
    });

    //Disable full page
    $(this).bind('cut copy paste', function (e) {
        e.preventDefault();
    });

    $('div').on('dragstart drop', function (e) {
        e.preventDefault();
        return false;
    });

});
   