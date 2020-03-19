$("#btnDMapprove").on('click', function () {
    var to = "";
    var newStatus = "";
    var body = "";
    // if DM is same person as ServicesDivisionHead, we send email directly to stores employee, and update Status directly to "approved_by_ServicesDivisionHead"
    if (DMemail === ServicesDivisionHead_email) {
        to = EmpFor_StoresRequests_email;
        newStatus = "approved_by_ServicesDivisionHead";
        body = '<p dir=rtl>' +
            'السلام عليكم ورحمة الله وبركاته <br />' +
            ' تحية طيبة وبعد <br />' +
            'قام "' + EmpArabicName + '" بعمل طلب جديد من قسم الخدمات العامة <br />' +
            'وتم إعتماد الطلب بواسطة المدير المباشر <br />' +
            'الرجاء القيام بتوفير الطلب من خلال الرابط التالى: <br />' +
            '<a href=' + _spPageContextInfo.webAbsoluteUrl + '/Pages/StoresEmployeeRequestFulfillingScreen.aspx?srid=' + requestID + '>رابط الطلب</a>' +
            '</p >';
    } else {
        to = ServicesDivisionHead_email;
        newStatus = "approved_by_DM";
        body = '<p dir=rtl>' +
            'السلام عليكم ورحمة الله وبركاته <br />' +
            ' تحية طيبة وبعد <br />' +
            'قام "' + EmpArabicName + '" بعمل طلب جديد من قسم الخدمات العامة <br />' +
            'وتم إعتماد الطلب بواسطة المدير المباشر <br />' +
            'الرجاء القيام بمراجعة الطلب واعتماده من خلال الرابط التالى: <br />' +
            '<a href=' + _spPageContextInfo.webAbsoluteUrl + '/Pages/StoresRequestView.aspx?srid=' + requestID + '>رابط الطلب</a>' +
            '</p >';
    }

    sprLib.list('StationeryRequests')
        .update({
            ID: requestID,
            Status: newStatus
        })
        .then(function (objItem) {
            $("#btnDMapprove").fadeOut("slow");
            $("#btnDMReject").fadeOut("slow");
            $("#CheckMark").fadeIn("slow");
            $("#successText").fadeIn("slow");

            
            var subject = 'تم عمل طلب جديد من قسم الخدمات العامة';
            sendEmail(to, body, subject);
        })
        .catch(function (strErr) { console.error(strErr); });
});




















   