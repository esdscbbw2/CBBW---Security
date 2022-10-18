$(document).ready(function () {
    $("#loginbtn").click(function () {
        var usr = $("#UserName").val();
        var pwd = $("#Password").val();
        Session["UserName"] = usr;
        Session["Password"] = pwd;
        $("#UserName")(a => a.UserName);
        if (usr.length == 0) {
            $("#UserName").css({ "border-color": "#ff0000", "border-size": "1px" });
            $("#usrerror").css({ "display": "block" });
        }
        else {
            $("#usrerror").css({ "display": "none" });
            $("#usrerror").focus();
        }
        if (pwd.length == 0) {
            $("#pwderror").css({ "display": "block" });
        }
        else {
            $("#pwderror").css({ "display": "none" });
        }
    });
    $('#UserName').on("change keyup paste", function (evt) {
        if ($('#UserName').val() != '' && $("#Password").val() != '') {
            $('#loginbtn').prop('disabled', false);
            $('#loginbtn').css({ "background-color": "#10253f" });
            $('#loginbtn').css({ "opacity": "1" });
        }
        else {
            $('#loginbtn').prop('disabled', true);
            $('#loginbtn').css({ "background-color": "#1ffff" });
            $('#loginbtn').css({ "opacity": ".3" });
        }

        if ($('#UserName').val() == '') {
            $('#UserName').css({ "border-color": "red" });
            $("#usermsg").css({ 'visibility': 'visible' });
        }
        else {
            $('#UserName').css({ "border-color": "#48D1CC" });
            $("#usermsg").css({ 'visibility': 'hidden' });
        }
    });
    $('#Password').on("change keyup paste", function (evt) {
        if ($('#UserName').val() != '' && $("#Password").val() != '') {
            $('#loginbtn').prop('disabled', false);
            $('#loginbtn').css({ "background-color": "#10253f" });
            $('#loginbtn').css({ "opacity": "1" });
        }
        else {
            $('#loginbtn').prop('disabled', true);
            $('#loginbtn').css({ "background-color": "#1ffff" });
            $('#loginbtn').css({ "opacity": ".3" });
        }
        if ($('#Password').val() == '') {
            $('#Password').css({ "border-color": "red" });
            $("#pwdmsg").css({ 'visibility': 'visible' });
        }
        else {
            $('#Password').css({ "border-color": "#48D1CC" });
            $("#pwdmsg").css({ 'visibility': 'hidden' });
        }
    });
});