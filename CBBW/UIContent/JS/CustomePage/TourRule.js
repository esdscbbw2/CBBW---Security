function FillServiceType(effDate) {
    var multiselectCtrl = $('#ServiceTypeDD');
    $.ajax({
        url: '/Security/TourRule/getServiceTypeList?EffectiveDate='+effDate,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            multiselectCtrl.empty();
            multiselectCtrl.multiselect('destroy');
            //ToLT.append($('<option/>', { value: "-1", text: "Select location type" }));
            $(data).each(function (index, item) {
                if (item.IsSelected) {
                    multiselectCtrl.append('<option value="' + item.ID + '" disabled>' + item.DisplayText + '</option>');
                }
                else { multiselectCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText })); }
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
$(document).ready(function () {
    $('#mEffectiveDate').change(function () {
        that = $(this);
        that.CustomDateFormatCloneRow();        
        if (that.val() != '') {
            $('#ServiceTypeDiv').removeClass('sectionB');
            $('#ServiceTypeDD').makeEnabled();
            FillServiceType(that.val());
            that.isValid();
        } else { that.isInvalid(); }
    });
    $('#ServiceTypeDD').change(function () {
        that = $(this);
        if (that.val() != '') {
            $('#BodyDiv').removeClass('sectionB');
            $('.myctrl').each(function () {
                $(this).makeEnabled();
            });

            that.isValid();
        } else { that.isInvalid(); }
    });
});