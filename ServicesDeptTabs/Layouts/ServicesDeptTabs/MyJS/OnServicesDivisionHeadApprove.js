$("#btnServicesDivisionHeadApprove").on('click', function () {
    sprLib.list('StationeryRequests')
        .update({
            ID: requestID,
            Status: 'approved_by_ServicesDivisionHead'
        })
        .then(function (objItem) {
            $("#btnServicesDivisionHeadApprove").fadeOut("slow");
            $("#btnDMapprove").fadeOut("slow");
            $("#btnDMReject").fadeOut("slow");
            $("#CheckMark").fadeIn("slow");
            $("#successText").fadeIn("slow");


            var to = 'test_spuser_2@zayed.org.ae';
            var body = '<p dir=rtl>' +
                'السلام عليكم ورحمة الله وبركاته <br />' +
                ' تحية طيبة وبعد <br />' +
                'قام "' + EmpArabicName + '" بعمل طلب جديد من قسم الخدمات <br />' +
                'وتم إعتماد الطلب بواسطة المدير المباشر ورئيس قسم الخدمات العامة <br />' +
                'الرجاء القيام بمراجعة الطلب والبدء بتوفيره من خلال الرابط التالى: <br />' +
                '<a href=' + _spPageContextInfo.webAbsoluteUrl + '/Pages/StoresEmployeeRequestFulfillingScreen.aspx?id=' + requestID + '>رابط الطلب</a>' +
                '</p >';
            var subject = 'تم إعتماد طلب جديد من قسم الخدمات';
            sendEmail(to, body, subject);
        })
        .catch(function (strErr) { console.error(strErr); });
});


