$(document).ready(function () {
    var _val = $("#NoteNumber").val();

    $.ajax({
        url: '/MaterialGatePass/GetcurentOutDetails',
        method: 'GET',
        data: { Notenumber: _val },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {

                if (index > 0) {
                    var cloneready = $('#tbody5').find('tr').clone();
                    cloneready.attr("id", index);
                    cloneready.find('td').find('#0_NotNos').attr('id', index + '_NotNos');
                    cloneready.find('td').find('#0_DrivernNo').attr('id', index + '_DrivernNo');//Display only
                    cloneready.find('td').find('#0_DriverName').attr('id', index + '_DriverName');
                    cloneready.find('td').find('#0_DriverNo').attr('id', index + '_DriverNo');
                    cloneready.find('td').find('#0_VehicleNo').attr('id', index + '_VehicleNo');
                    cloneready.find('td').find('#0_DesgCodeName').attr('id', index + '_DesgCodeName');//Display only
                    cloneready.find('td').find('#0_DesgCode').attr('id', index + '_DesgCode');
                    cloneready.find('td').find('#0_DesgName').attr('id', index + '_DesgName');
                    cloneready.find('td').find('#0_TripTC').attr('id', index + '_TripTC');//Display only
                    cloneready.find('td').find('#0_TripType').attr('id', index + '_TripType');
                    cloneready.find('td').find('#0_TripCode').attr('id', index + '_TripCode');
                    cloneready.find('td').find('#0_Toloc').attr('id', index + '_Toloc');//Display only
                    cloneready.find('td').find('#0_TolocName').attr('id', index + '_TolocName');
                    cloneready.find('td').find('#0_TolocCode').attr('id', index + '_TolocCode')
                    cloneready.find('td').find('#0_CarOut').attr('id', index + '_CarOut');//Display only
                    cloneready.find('td').find('#0_CarryOut').attr('id', index + '_CarryOut');
                    cloneready.find('td').find('#0_LoadMat').attr('id', index + '_LoadMat');
                    cloneready.find('td').find('#0_LoadM').attr('id', index + '_LoadM');  //Display only                           
                    cloneready.find('td').find('#0_RFIDs').attr('id', index + '_RFIDs');
                    cloneready.find('td').find('#0_SchTripDate').attr('id', index + '_SchTripDate');//Display only
                    cloneready.find('td').find('#0_SchTrip').attr('id', index + '_SchTrip');
                    cloneready.find('td').find('#0_SchTripdatetext').attr('id', index + '_SchTripdatetext');
                    cloneready.find('td').find('#0_ActualTripDates').attr('id', index + '_ActualTripDates');
                    
                    cloneready.find('td').find('#0_ActualTripTime').attr('id', index + '_ActualTripTime');
                    cloneready.find('td').find('#0_KM').attr('id', index + '_KM');//Display Only
                    cloneready.find('td').find('#0_KMOut').attr('id', index + '_KMOut');
                    cloneready.find('td').find('#0__OutRemarks').attr('id', index + '_OutRemarks');
                    $('#tbody6').append(cloneready);
                   
                }
                
                $("#" + index + "_NotNos").val(item.NoteNumber);
                $("#" + index + "_DrivernNo").html(item.Drivername + "/" + item.DriverNo);//Display Only
                $("#" + index + "_DriverName").val(item.Drivername);
                $("#" + index + "_DriverNo").val(item.DriverNo);
                $("#" + index + "_VehicleNo").val(item.VehicleNumber);
                $("#" + index + "_DesgCodeName").html(item.DesignationCode + "/" + item.DesignationName);//Display only          
                $("#" + index + "_DesgCode").val(item.DesignationCode);
                $("#" + index + "_DesgName").val(item.DesignationName);
                $("#" + index + "_TripTC").html(item.TripType + "/" + item.TripTypeStr);//Display only
                $("#" + index + "_TripType").val(item.TripType);
                $("#" + index + "_TripCode").val(item.TripTypeStr);
                $("#" + index + "_Toloc").html(item.ToLocationCodeName);//Display Only
                $("#" + index + "_TolocName").val(item.ToLocationCodeName);
                $("#" + index + "_TolocCode").val(item.ToLocationCode);
                if (item.CarryingOutMat == true) {
                    $("#" + index + "_CarOut").html("Yes");//Display only
                } else {
                    $("#" + index + "_CarOut").html("No");//Display only
                }

                $("#" + index + "_CarryOut").val(item.CarryingOutMat);
                $("#" + index + "_LoadM").html(item.LoadPercentage);//Display only
                $("#" + index + "_LoadMat").val(item.LoadPercentage);

                $("#" + index + "_RFIDs").val(item.RFIDCard).html('<select id="' + index + '_RFID" class="form-select pointer is-invalid" onchange="SelectedRFIDValid();ValidateControl()"  aria-label="Default select example"></select>');
                $("#" + index + "_SchTripDate").html(item.SchFromDatestr);//display only
                
                $("#" + index + "_SchTripdatetext").val(item.SchFromDatestr);
                $("#" + index + "_SchTrip").val(item.SchFromDate);
                $("#" + index + "_ActualTripDates").val(item.ActualTripOutDate).html('<input id="' + index + '_ActualTripDate" type="date" placeholder="11/11/2022" class="form-control pointer is-invalid" onchange="ValidateControl()">');
                $("#" + index + "_ActualTripTime").val(item.ActualTripOutTime);//.html('<input id="' + index + '_ActualTripTime"  type="text" class="form-control pointer is-invalid" onblur="ValidateControl()" placeholder="08:00PM">').addClass('timePicker');
                $("#" + index + "_KM").html(item.KMOUT);//Display Only
                $("#" + index + "_KMOut").val(item.KMOUT);
                $("#" + index + "_OutRemarks").val(item.OutRemarks).html('<input id="' + index + '_OutRemark" type="text" class="form-control">');
                $("#" + index + "_Action").html('<span class="actionBtn d-block"><button type="button" onclick="OnclickNewDCDetails(this)"  id="' + item.VehicleNumber + '" data-value="' + item.SchFromDatestr + '"   class="btn primaryLink" data-toggle="tooltip" data-placement="top" title="Details" data-placement="top" title="" data-bs-original-title="Pending"> <svg xmlns=http://www.w3.org/2000/svg width=24 height=24 viewBox="0 0 24 24" fill=none stroke=currentColor stroke-width=2 stroke-linecap=round stroke-linejoin=round class="feather feather-alert-triangle"><path d="M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z"></path><line x1=12 y1=9 x2=12 y2=13></line><line x1=12 y1=17 x2=12.01 y2=17></line></svg></button>');
               
            })
            GetRFIDCardNos();
        }
    });

});
function GetRFIDCardNos() {
    
    //var targets = ChangeRFID.caller.arguments[0].target;
    //var targetids = $(targets).attr('id');
    // targetid = targetid.slice(0, - 1);
    var FRIDData = $('#0_RFID');
   // alert(FRIDData);
    
    //var rfidlists = $('#' + targetids);

    $.ajax({
        url: '/MaterialGatePass/GetRFIdCards',
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            FRIDData.append($('<option/>', { value: "-1", text: "Select RFID" }));
            $(data).each(function (index, item) {
                FRIDData.append($('<option/>', { value: item.RFIDCardNo, text: item.RFIDCardNo }));
            });
            FRIDData.append($('<option/>', { value: "NA", text: "NA" }));
        }
    });

};


//for new Dc details
function OnclickNewDCDetails(ctrl) {
    $('#tbody2').empty();
    var targetid = $(ctrl).attr("id");
    var targetval = $(ctrl).attr('data-value');

    $.ajax({
        url: '/Security/MaterialGatePass/GetReferenceDCDetails?VehicleNo=' + targetid + '&FromDT=' + targetval,
        method: 'GET',
        // data: { VehicleNo: targetid, FromDT: targetval },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {
                if (index > 0) {
                    var cloneready = $('#tbody1').find('tr').clone();
                    cloneready.attr("id", index);
                    cloneready.find('td').find('#0_NotNo').attr('id', index + '_NotNo');
                    cloneready.find('td').find('#0_Date').attr('id', index + '_Date');
                    cloneready.find('td').find('#0_Frmloc').attr('id', index + '_Frmloc');
                    cloneready.find('td').find('#0_Tolocation').attr('id', index + '_Tolocation');
                    cloneready.find('td').find('#0_fok').attr('id', index + '_fok');
                    cloneready.find('td').find('#0_Actions').attr('id', index + '_Actions');
                    $('#tbody2').append(cloneready);
                }
                $("#" + index + "_NotNo").val(item.NoteNumber);
                $("#" + index + "_Date").val(item.NoteDatestr);
                $("#" + index + "_Frmloc").val(item.FromLocationText);
                $("#" + index + "_Tolocation").val(item.ToLocationText);
                $("#" + index + "_fok").val(item.FindOk).html('<option value="NA">-</option><option value="Yes">Yes</option><option value="No">No</option>');

                //$("#" + index + "_Action").html('<button type="button" onclick="Detailsclick()"  id="' + index + '_btn" value="' + item.NoteNumber + '"  class="btn primaryLink" data-toggle="tooltip" data-placement="top"title="Details">Details</button>');
                $("#" + index + "_Actions").html('<button type="button" onclick="Detailsclick(this)"  id="' + index + '_btn" data-value="' + item.NoteNumber + '"   class="btn primaryLink" data-toggle="tooltip" data-placement="top" title="Details"> <svg xmlns=http://www.w3.org/2000/svg width=24 height=24 viewBox="0 0 24 24" fill=none stroke=currentColor stroke-width=2 stroke-linecap=round stroke-linejoin=round class="feather feather-eye"><path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"></path><circle cx=12 cy=12 r=3></circle></svg></button>');
            })
        }
    });
}
//for Existing Dc details
function OnclickHistoryDCDetails(ctrl) {
    var target = OnclickHistoryDCDetails.caller.arguments[0].target;
    var targetval = $(ctrl).attr('data-value');


    $.ajax({
        url: '/MaterialGatePass/GetHistoryDCDetails',
        method: 'GET',
        data: { ID: targetval },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, item) {

                if (index > 0) {
                    var cloneready = $('#tbody1').find('tr').clone();
                    cloneready.find('td').find('#0_NotNo').attr('id', index + '_NotNo');
                    cloneready.find('td').find('#0_Date').attr('id', index + '_Date');
                    cloneready.find('td').find('#0_Frmloc').attr('id', index + '_Frmloc');
                    cloneready.find('td').find('#0_Toloc').attr('id', index + '_Toloc');
                    cloneready.find('td').find('#0_fok').attr('id', index + '_fok');
                    cloneready.find('td').find('#0_Action').attr('id', index + '_Action');
                    $('#tbody2').append(cloneready);
                }
                $("#" + index + "_NotNo").val(item.NoteNumber);
                $("#" + index + "_Date").val(item.NoteDatestr);
                $("#" + index + "_Frmloc").val(item.FromLocationText);
                $("#" + index + "_Toloc").val(item.ToLocationText);
                $("#" + index + "_fok").val(item.CheckFound);

                //$("#" + index + "_Action").html('<button type="button" onclick="Detailsclick()"  id="' + index + '_btn" value="' + item.NoteNumber + '"  class="btn primaryLink" data-toggle="tooltip" data-placement="top"title="Details">Details</button>');
                $("#" + index + "_Action").html('<button type="button" onclick="Detailsclick()"  id="' + index + '_btn" value="' + item.NoteNumber + '"   class="btn primaryLink" data-toggle="tooltip" data-placement="top" title="Details"><i data-feather="eye"></i></button>');
            })
        }
    });

}
//for ItemWise Data display
function Detailsclick(ctrl) {
    var target = Detailsclick.caller.arguments[0].target;
    //var targetval = $(target).attr("value");
    var targetval = $(ctrl).attr('data-value');
    $.ajax({
        url: '/MaterialGatePass/GetItemWiseDetails',
        method: 'GET',
        data: { Notenumber: targetval },
        dataType: 'json',
        success: function (data) {
            $('#tbody4').empty();
            $(data).each(function (index, item) {

                if (index > 0) {
                    var cloneready = $('#tbody3').find('tr').clone();
                    cloneready.find('#0_itemtype').attr('id', index + '_itemtype');
                    cloneready.find('#0_itemcodename').attr('id', index + '_itemcodename');
                    cloneready.find('#0_uom').attr('id', index + '_uom');
                    cloneready.find('#0_bags').attr('id', index + '_bags');
                    cloneready.find('#0_kgs').attr('id', index + '_kgs');
                    $('#tbody4').append(cloneready);
                }
                $("#" + index + "_itemtype").html(item.ItemTypeText);
                $("#" + index + "_itemcodename").html(item.ItemCode + "/" + item.ItemText);
                $("#" + index + "_uom").html(item.UOMText);
                $("#" + index + "_bags").html(item.QuantityBag);
                $("#" + index + "_kgs").html(item.QuantityKg);
            })
        }
    });

}
//for Add new record in Dc details
function getDCRecords() {
    var schrecords = [];
    $('.add-rows').each(function () {
        var rowid = $(this).attr('id');
        schrecords.push({
            'NoteNumber': $('#' + rowid + '_NotNo').val(),
            'NoteDate': $('#' + rowid + '_Date').val(),
            'FromLocationText': $('#' + rowid + '_Frmloc').val(),
            'ToLocationText': $('#' + rowid + '_Tolocation').val(),
            'FindOk': $('#' + rowid + '_fok').val()
        });
    });
    return schrecords;
};
//get current out record
function getVechCurrentOutRecords() {
    var Currrecords = [];
    $('.add-row').each(function () {
        var rowid = $(this).attr('id');
        if (rowid >= 0) {
            Currrecords.push({
                'NoteNumber': $('#' + rowid + '_NotNos').val(),
                'DriverNo': $('#' + rowid + '_DriverNo').val(),
                'Drivername': $('#' + rowid + '_DriverName').val(),
                'DesignationCode': $('#' + rowid + '_DesgCode').val(),
                'DesignationName': $('#' + rowid + '_DesgName').val(),
                'TripType': $('#' + rowid + '_TripType').val(),
                'TripTypeStr': $('#' + rowid + '_TripCode').val(),
                'ToLocationCodeName': $('#' + rowid + '_TolocName').val(),
                'CarryingOutMat': $('#' + rowid + '_CarryOut').val(),
                'LoadPercentage': $('#' + rowid + '_LoadMat').val(),
                
                'SchFromDate': $('#' + rowid + '_SchTripdatetext').val(),
                'KMOUT': $('#' + rowid + '_KMOut').val(),
                'VehicleNumber': $('#' + rowid + '_VehicleNo').val(),
                'RFIDCard': $('#' + rowid + '_RFID').val(),
                'ActualTripOutDate': $('#' + rowid + '_ActualTripDate').val(),
                'ActualTripOutTime': $('#' + rowid + '_ActualTripTime').val().toLocaleString(),
                'OutRemarks': $('#' + rowid + '_OutRemark').val()

            });
        }
    });
    return Currrecords;
};
//Get DC details on save click
function SaveData() {
    
     var dcrecords = getDCRecords();
    var currentOutrecord = getVechCurrentOutRecords();
   //alert(JSON.stringify(currentOutrecord));
    // alert(JSON.stringify(dcrecords));
    $.ajax({
        method: 'POST',
        url: '/MaterialGatePass/SaveVehicleMaterialOutDCDetails',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            ListCurrentOutData: currentOutrecord,
           ListofMGPReferenceDCData: dcrecords
        }),
        success: function (data) {
            $(data).each(function (index, item) {
               // alert(item.bResponseBool);
                if (item.bResponseBool == true) {
                    var url = "/Security/MaterialGatePass/Create";
                    window.location.href = url;
                   
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Data saved successfully.',
                        icon: 'success',
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    }).then(callback);
                    function callback(result) {
                        if (result.value) {
                            var url = "/Security/MaterialGatePass/Create";
                            window.location.href = url;
                        }
                    }
                } else {
                    Swal.fire({
                        title: 'Confirmation',
                        text: 'Failed to save data.',
                        icon: 'question',
                        customClass: 'swal-wide',
                        buttons: {
                            confirm: 'Ok'
                        },
                        confirmButtonColor: '#2527a2',
                    });
                }
            });
        },
    });
};

function SelectedRFIDValid() {
    var target = SelectedRFIDValid.caller.arguments[0].target;
    var rowid = $(target.closest('.add-row')).attr("id");
    var _val = $('#' + rowid + '_RFID option:selected').val();
    //alert(_val);
   
};

function activateSubmitBtn() {

    var btnSubmit = $('#btnSubmit');
    if ($('.is-invalid').length > 0) {
        btnSubmit.attr('disabled', 'disabled');
    } else {
        btnSubmit.removeAttr('disabled');
    }
};
function ValidateControl() {
    var target = ValidateControl.caller.arguments[0].target;
    var targetid = $(target).attr('id');
    //alert(targetid);
    var isvalid = true;//validatectrl(targetid, $(target).val());
   
    if (isvalid) {
        $(target).removeClass('is-invalid').addClass('is-valid');
    } else {
        $(target).removeClass('is-valid').addClass('is-invalid');
    }
    activateSubmitBtn();

};



