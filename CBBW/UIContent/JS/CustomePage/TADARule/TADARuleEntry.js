function MakeBodyDisable() {
    $('#Div1').addClass('sectionB');
    $('#Div2').addClass('sectionB');
    $('#Div3').addClass('sectionB');
    $('#Div4').addClass('sectionB');
    $('.myctrl').each(function () {
        $(this).val('').isInvalid();
        $(this).makeDisable();
    });
};
function EnableDiv(divid) {
    if (divid == 'Div1') {
        $('#Div1').removeClass('sectionB');        
    } else if (divid == 'Div2') {
        $('#Div1').removeClass('sectionB');
        $('#Div2').removeClass('sectionB');
    } else if (divid == 'Div3') {
        $('#Div1').removeClass('sectionB');
        $('#Div2').removeClass('sectionB');
        $('#Div3').removeClass('sectionB');
    } else if (divid == 'Div4') {
        $('#Div1').removeClass('sectionB');
        $('#Div2').removeClass('sectionB');
        $('#Div3').removeClass('sectionB');
        $('#Div4').removeClass('sectionB');
    }
    $('#' + divid).find('.myctrl').each(function () {
        $(this).makeEnabled();
    });
};
function ChangeEffectiveDate() {
    var that = $('#mEffectiveDate');
    that.CustomDateFormatCloneRow();
    var effDate = that.val();
    if (effDate != '') {
        FillCategoryIDs(that.val());
        EnableDiv('Div1');
        $('#TADARule_EffectiveDate').val(effDate);
        that.isValid();
        $.ajax({
            url: '/Security/TADARules/GetRuleData?EffectiveDate=' + effDate,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                $(data).each(function (index, item) {
                    $('#TADARule_MinHoursForHalfDA').val(item.MinHoursForHalfDA);
                    $('#TADARule_MinKmsForDA').val(item.MinKmsForDA);
                    $('#TADARule_LodgingExpOnCompAcco').val(item.LodgingExpOnCompAcco.toString());
                    $('#TADARule_LocalConvEligibility').val(item.LocalConvEligibility.toString());
                    $('#TADARule_DepuStaffDAEligibility').val(item.DepuStaffDAEligibility.toString());
                    $('#TADARule_ExtraDAApplicability').val(item.ExtraDAApplicability.toString());
                    
                });                
            }
        });
    } else { that.isInvalid(); }
};
function FillCategoryIDs(effDate) {
    var multiselectCtrl = $('#CategoryDD');
    $.ajax({
        url: '/Security/TADARules/GetCategories?EffectiveDate=' + effDate,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            var aitemsCount = 0;
            var btnsubmitCtrl = $('#btnSubmit');
            multiselectCtrl.empty();
            multiselectCtrl.multiselect('destroy');
            $(data).each(function (index, item) {
                if (item.IsSelected) {
                    multiselectCtrl.append('<option value="' + item.ID + '" disabled>' + item.DisplayText + '</option>');
                }
                else {
                    aitemsCount = aitemsCount + 1;
                    multiselectCtrl.append($('<option/>', { value: item.ID, text: item.DisplayText }));
                }
            });
            multiselectCtrl.attr('multiple', 'multiple');
            multiselectCtrl.multiselect({
                templates: {
                    button: '<button id="B0" type="button" class="multiselect dropdown-toggle btn btn-primary w-100 selectBox" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                },
            });
            multiselectCtrl.multiselect('clearSelection');
            multiselectCtrl.multiselect('refresh');
            if (aitemsCount > 0) { btnsubmitCtrl.makeDisable(); } else { btnsubmitCtrl.makeEnabled(); }
        }
    });
};
$(document).ready(function () {
    MakeBodyDisable();
    $('#mEffectiveDate').change(function () {
        ChangeEffectiveDate();

    });
});