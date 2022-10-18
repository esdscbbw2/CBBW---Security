    $(document).ready(function () {
        $("#Back").click(function (e) {
            var $this = $(this);
            if ($this.on('click')) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation Message",
                    message: "Are You Sure Want To Go Back?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/GratuityRemitterEmailParam";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

    $(document).ready(function () {
        $("#NDCI").click(function (e) {
            var $this = $(this);
            if ($this.data('clicked', true)) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation",
                    message: "Are You Sure Want To Navigate No Due Certificate?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/NDC/Index";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

    $(document).ready(function () {
        $("#GR").click(function (e) {
            var $this = $(this);
            if ($this.data('clicked', true)) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation",
                    message: "Are You Sure Want To Navigate Gratuity Rule?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/GratuityRules/Index";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

    $(document).ready(function () {

        $("#GPS").click(function (e) {
            var $this = $(this);
            if ($this.data('clicked', true)) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation",
                    message: "Are You Sure Want To Navigate Gratuity Payment Schedule?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/GratuityPaymentSchedule/Index";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

    $(document).ready(function () {

        $("#GSD").click(function (e) {
            var $this = $(this);
            if ($this.data('clicked', true)) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation",
                    message: "Are You Sure Want To Navigate Gratuity Settelment Details?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/GratuitySettelmentDetails/Index";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

    $(document).ready(function () {

        $("#GPC").click(function (e) {
            var $this = $(this);
            if ($this.data('clicked', true)) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation",
                    message: "Are You Sure Want To Navigate Gratuity Paid Compliance?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/GratuityPaidCompliance/Index";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

    $(document).ready(function () {

        $("#NDCII").click(function (e) {
            var $this = $(this);
            if ($this.data('clicked', true)) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation",
                    message: "Are You Sure Want To Navigate No Due Certicifate PartII?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/NDCPartII/Index";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

    $(document).ready(function () {

        $("#GPDI").click(function (e) {
            var $this = $(this);
            if ($this.data('clicked', true)) {
                e.preventDefault();
                cuteAlert({
                    type: "question",
                    title: "Confirmation",
                    message: "Are You Sure Want To Navigate Gratuity Payment Details?",
                    confirmText: "Yes",
                    cancelText: "No"
                }).then((e) => {
                    if (e == ("confirm")) {
                        window.location = "/GratuityPaymentDetails/Index";
                    } else {
                        return false;
                    }
                })
            }
        });
    });

$(document).ready(function () {
    $("#GPDII").click(function (e) {
        var $this = $(this);
        if ($this.data('clicked', true)) {
            e.preventDefault();
            cuteAlert({
                type: "question",
                title: "Confirmation",
                message: "Are You Sure Want To Navigate Gratuity Payment DetailsII?",
                confirmText: "Yes",
                cancelText: "No"
            }).then((e) => {
                if (e == ("confirm")) {
                    window.location = "/GratuityPaymentDetailsII/Index";
                } else {
                    return false;
                }
            })
        }
    });
});
