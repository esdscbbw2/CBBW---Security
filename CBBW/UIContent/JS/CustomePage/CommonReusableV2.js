function MyAlert(MessageType, MessageText) {
    //MessageType 0-Information Alert, 1-Success Alert,2-Confirmation Alert,
    //MessageType  3- Eror Alert. 4- Validation Failed Alert,5-Warning Alert,
   
    var IsOK = false;
    switch (MessageType) {
        case 1:
            IsOK = MySuccessAlert(MessageText);
            break;
        case 2:
            IsOK = MyConfirmationAlert(MessageText);
            break;
        case 3:
            IsOK = MyErrorAlert(MessageText);
            break;
        case 4:
            IsOK = MyValidationFailedAlert(MessageText);
            break;
        case 5:
            IsOK = MyWarningAlert(MessageText);
            break;
        default:
            IsOK = MyInformationAlert(MessageText);
    }
    return IsOK;
};
function MyAlertWithRedirection(MessageType, MessageText, RedirectUrl) {
    //MessageType 0-Information Alert, 1-Success Alert,2-Confirmation Alert,
    //MessageType  3- Eror Alert. 4- Validation Failed Alert,5-Warning Alert,

    switch (MessageType) {
        case 1:
            MySuccessAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 2:
            MyConfirmationAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 3:
            MyErrorAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 4:
            MyValidationFailedAlertWithRedirection(MessageText, RedirectUrl);
            break;
        case 5:
            MyWarningAlertWithRedirection(MessageText, RedirectUrl);
            break;
        default:
            MyInformationAlertWithRedirection(MessageText, RedirectUrl);
    }
};

function MySuccessAlert(MessageText) {
    var IsOK = false;
    Swal.fire({
        title: 'Success',
        text: MessageText,
        icon: 'success',
        customClass: 'swal-wide my-success',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            IsOK=true;
        }
    }
    return IsOK;
};
function MyInformationAlert(MessageText) {
    var IsOK = false;
    Swal.fire({
        title: 'Information',
        text: MessageText,
        icon: 'info',
        customClass: 'swal-wide my-info',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            IsOK = true;
        }
    }
    return IsOK;
};
function MyValidationFailedAlert(MessageText) {
    var IsOK = false;
    Swal.fire({
        title: 'Validation Failed',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            IsOK = true;
        }
    }
    return IsOK;
};
function MyErrorAlert(MessageText) {
    var IsOK = false;
    Swal.fire({
        title: 'Error Occurred',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            IsOK = true;
        }
    }
    return IsOK;
};
function MyConfirmationAlert(MessageText) {
    var IsOK = false;
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(callback);
    function callback(result) {
        if (result.value) {
            IsOK = true;
        }
    }
    return IsOK;
};
function MyWarningAlert(MessageText) {
    var IsOK = false;
    Swal.fire({
        title: 'Warning',
        text: MessageText,
        icon: 'warning',
        customClass: 'swal-wide my-warning',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            IsOK = true;
        }
    }
    return IsOK;
};
function MySuccessAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Success',
        text: MessageText,
        icon: 'success',
        customClass: 'swal-wide my-success',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyInformationAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Information',
        text: MessageText,
        icon: 'info',
        customClass: 'swal-wide my-info',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyValidationFailedAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Validation Failed',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyErrorAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Error Occurred',
        text: MessageText,
        icon: 'error',
        customClass: 'swal-wide my-error',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyConfirmationAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Confirmation',
        text: MessageText,
        icon: 'question',
        customClass: 'swal-wide',
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        cancelButtonClass: 'btn-cancel',
        confirmButtonColor: '#2527a2',
        showCancelButton: true,
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};
function MyWarningAlertWithRedirection(MessageText, RedirectUrl) {
    Swal.fire({
        title: 'Warning',
        text: MessageText,
        icon: 'warning',
        customClass: 'swal-wide my-warning',
        buttons: {
            confirm: 'Ok'
        },
        confirmButtonColor: '#2527a2',
    }).then(callback);
    function callback(result) {
        if (result.value) {
            window.location.href = RedirectUrl;
        }
    }
};

