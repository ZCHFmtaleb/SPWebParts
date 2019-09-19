function Mark_as_Fulfilled() {
    //============Update "Fulfilled" in StationeryRequestDetails to true=======
    //============Deduct fulfilled items from Inventory "CurrentStock"======== 
    //============with checking for "MinStock" ==========================
    //requestID

    var rowscount = $("#jqxgrid").jqxGrid('getdatainformation').rowscount;
    for (var i = 0; i < rowscount; i++) {
        var data = $('#jqxgrid').jqxGrid('getrowdata', i);

       
        //var webURL = _spPageContextInfo.webAbsoluteUrl;
        //var api = "/_api/web/lists/";
        //var query2 = "GetByTitle('StationeryRequestDetails')/items(39)";
        //var fullURL2 = webURL + api + query2;
        //var encfullURL2 = encodeURI(fullURL2);


        sprLib.list('StationeryRequestDetails')
            .update({
                ID: data.ID,
                Fulfilled: data.Fulfilled,
                ItemSpecificName: data.ItemSpecificName
            })
            .then(function (objItem) {
                alert("suceess");
            })
            .catch(function (strErr) { console.error(strErr); });
     
    } // End of for loop

    function onSaveAllRowsToServerSucceeded(sender, args) {

    //===============Show Success Message ===========================

        Swal.fire({
            text: 'تم إرسال الطلب بنجاح',
            type: 'success',
            confirmButtonText: 'تم'
        });

    //===============Update WF Status to "Fulfilled"======================

    //===============Send Email to Original Requester Employee============

        var to = DM_Email;

        if (EmpArabicName === "") {
            EmpArabicName = userDisplayName;
        }

        var body = '<p dir=rtl>' +
            'السلام عليكم ورحمة الله وبركاته <br />' +
            ' تحية طيبة وبعد <br />' +
            'قام "' + EmpArabicName + '" بعمل طلب جديد من قسم الخدمات العامة <br />' +
            'الرجاء القيام بمراجعة الطلب واعتماده من خلال الرابط التالى: <br />' +
            '<a href=' + webURL + '/Pages/StoresRequestView.aspx?id=' + MasterRecordId + '>رابط الطلب</a>' +
            '</p >';
        var subject = 'تم عمل طلب جديد من قسم الخدمات العامة';
        sendEmail(to, body, subject);
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
}