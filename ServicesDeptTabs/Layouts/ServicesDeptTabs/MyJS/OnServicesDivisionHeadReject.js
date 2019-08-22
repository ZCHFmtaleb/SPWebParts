$("#btnServicesDivisionHeadReject").on('click', function () {
    $("#btnDMapprove").fadeOut("slow");
    $("#btnServicesDivisionHeadApprove").fadeOut("slow");
    $("#divRejectReasons").fadeIn("slow");
});


$('#txtRejectReasons').bind('input propertychange', function () {
    var valid = true;

    if (!$("#txtRejectReasons").val()) {
        valid = false;
    }

    if (valid) {
        $("#btnServicesDivisionHeadRejectSubmit").attr("disabled", false);
        $("#reqStar").hide();
    }
    else {
        $("#btnServicesDivisionHeadRejectSubmit").attr("disabled", true);
        $("#reqStar").show();
    }
});


$("#btnServicesDivisionHeadRejectSubmit").on('click', function () {
    sprLib.list('StationeryRequests')
        .update({
            ID: requestID,
            Status: 'rejected_by_ServicesDivisionHead'
        })
        .then(function (objItem) {
            $("#btnDMReject").fadeOut("slow");
            $("#btnServicesDivisionHeadReject").fadeOut("slow");
            $("#divRejectReasons").fadeOut("slow");
            $("#btnServicesDivisionHeadRejectSubmit").fadeOut("slow");
            Swal.fire({
                text: 'تم إجراء رفض الطلب بنجاح ، مع إرسال تنويه بذلك لمقدم الطلب',
                type: 'success',
                confirmButtonText: 'تم'
            });


            var to = EmpEmail;
            var body = '<p dir=rtl>' +
                'السلام عليكم ورحمة الله وبركاته <br />' +
                ' تحية طيبة وبعد <br />' +
                ' نعتذر عن عدم قبول طلب قسم الخدمات العامة الخاص بك <br />' +
                'نظرا لعدم إعتماده من قبل قسم الخدمات العامة <br />' +
                'وذلك نظرا للأسباب التالية : <br />' +
                '<i>"' + $.trim($("#txtRejectReasons").val()) + '"</i>' + '<br />' +
                '<a href=' + _spPageContextInfo.webAbsoluteUrl + '/Pages/StoresRequestView.aspx?id=' + requestID + '>رابط الطلب</a>' +
                '</p >';
            var subject = 'نعتذر عن عدم قبول طلب قسم الخدمات العامة الخاص بك';
            sendEmail(to, body, subject);
        })
        .catch(function (strErr) { console.error(strErr); });
});
