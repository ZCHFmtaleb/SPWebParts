// Save All Rows To Server
$("#btnSaveAllRowsToServer").on('click', function () {

    // #region var declarations

    var rowscount = $("#jqxgrid").jqxGrid('getdatainformation').rowscount;
    var webURL = _spPageContextInfo.webAbsoluteUrl;
    var api = "/_api/web/lists/";
    var query = "GetByTitle('StationeryRequests')/items";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);
    var MasterRecordId;
    var query2 = "GetByTitle('StationeryRequestDetails')/items";
    var fullURL2 = webURL + api + query2;
    var encfullURL2 = encodeURI(fullURL2);

    // #endregion

    // #region check : if no items in the grid

    if (rowscount === 0) {
        return;
    }

    // #endregion

    // #region Step 1 of 4 : Create MasterRecord and return the Id

    $.ajax({
        async: false,
        url: encfullURL,
        type: "POST",
        data: JSON.stringify({
            '__metadata': { 'type': 'SP.Data.StationeryRequestsListItem' },
            'EmpId': userId,
            'Status': 'New_StationeryRequest_Started',
            'EmpArabicName': EmpArabicName,
            'Department': Department,
            'EmpEmail': userEmail,
            'DM': DM
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-RequestDigest": $("#__REQUESTDIGEST").val()
        },
        success: onCreateMasterRecordSucceeded,
        error: onCreateMasterRecordFailed
    });
  

    // #endregion

    // #region Step 2 of 4 : Create a record for each item in "StationeryRequestDetails" list

    for (var i = 0; i < rowscount; i++) {
        var data = $('#jqxgrid').jqxGrid('getrowdata', i);
        $.ajax({
            async: false,
            url: encfullURL2,
            type: "POST",
            data: JSON.stringify({
                '__metadata': { 'type': 'SP.Data.StationeryRequestDetailsListItem' },
                'Title': data.Title,
                'Quantity': data.Quantity.toString(),
                'Notes': data.Notes,
                'MasterRecordId': parseInt(MasterRecordId),
                'Fulfilled': 'false'
            }),
            headers: {
                "accept": "application/json;odata=verbose",
                "content-type": "application/json;odata=verbose",
                "X-RequestDigest": $("#__REQUESTDIGEST").val()
            },
            success: onSaveAllRowsToServerSucceeded,
            error: onSaveAllRowsToServerFailed
        });
    } // End of for loop

    // #endregion

    // #region Step 3 of 4 : Show Success Popup

    ShowSuccessPopup();

    // #endregion

    // #region check : if ExtendedManagersLength is 0 or 1, skip DM approval 
    // send email directly to ServicesDivisionHead
    // then update Status to 'approved_by_DM'

    var to;
    if (ExtendedManagersLength <= 1) {
        to = ServicesDivisionHead_email;
        sprLib.list('StationeryRequests')
            .update({
                ID: requestID,
                Status: 'approved_by_DM'
            });
    }
    else {
        to = DM_Email;
    }

    // #endregion

    // #region Step 4 of 4 : Send email
   
    if (EmpArabicName === "") {
        EmpArabicName = userDisplayName;
    }
    var body = '<p dir=rtl>' +
        'السلام عليكم ورحمة الله وبركاته <br />' +
        ' تحية طيبة وبعد <br />' +
        'قام "' + EmpArabicName + '" بعمل طلب جديد من قسم الخدمات العامة <br />' +
        'الرجاء القيام بمراجعة الطلب واعتماده من خلال الرابط التالى: <br />' +
        '<a href='+webURL+'/Pages/StoresRequestView.aspx?srid=' + MasterRecordId+'>رابط الطلب</a>' +
        '</p >';
    var subject = 'تم عمل طلب جديد من قسم الخدمات العامة';
    sendEmail(to, body, subject);
    // #endregion
});


// #region Helper Methods

function onCreateMasterRecordSucceeded(data) {
    MasterRecordId = data.d.Id;
}
function onCreateMasterRecordFailed(error) {
    console.log(JSON.stringify(error));
    Swal.fire({
        text: 'حدث خطأ اثناء محاولة إرسال الطلب',
        type: 'error',
        confirmButtonText: 'تم'
    });
    throw new Error("Something went wrong");
}
function onSaveAllRowsToServerSucceeded(sender, args) {
}
function onSaveAllRowsToServerFailed(error) {
    console.log(JSON.stringify(error));
    Swal.fire({
        text: 'حدث خطأ اثناء محاولة إرسال الطلب',
        type: 'error',
        confirmButtonText: 'تم'
    });
    throw new Error("Something went wrong");
}
function ShowSuccessPopup(){
    // no errors happened means success
    Swal.fire({
        text: 'تم إرسال الطلب بنجاح',
        type: 'success',
        confirmButtonText: 'تم'
    });
}

// #endregion