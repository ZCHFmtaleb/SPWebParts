// Save All Rows To Server
$("#btnSaveAllRowsToServer").on('click', function () {
    var rowscount = $("#jqxgrid").jqxGrid('getdatainformation').rowscount;
    var webURL = _spPageContextInfo.webAbsoluteUrl;
    var api = "/_api/web/lists/";
    var query = "GetByTitle('StationeryRequests')/items";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);
    var MasterRecordId;

    // Create MasterRecord and return the Id
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

    function onCreateMasterRecordSucceeded(data) {
        MasterRecordId = data.d.Id;
        console.log("MasterRecordId is " + MasterRecordId);
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

    var query2 = "GetByTitle('StationeryRequestDetails')/items";
    var fullURL2 = webURL + api + query2;
    var encfullURL2 = encodeURI(fullURL2);

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

    // no errors happened means success
    Swal.fire({
        text: 'تم إرسال الطلب بنجاح',
        type: 'success',
        confirmButtonText: 'تم'
    });

    var to = DM_Email;

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
});
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
