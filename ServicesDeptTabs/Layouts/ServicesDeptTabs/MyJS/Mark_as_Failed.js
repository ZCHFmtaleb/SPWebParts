function Mark_as_Failed() {
    $("#btnAsif_Mark_as_Fulfilled").fadeOut("slow");
    $("#divRejectReasons").fadeIn("slow");
}

$('#txtRejectReasons').bind('input propertychange', function () {
    var valid = true;

    if (!$("#txtRejectReasons").val()) {
        valid = false;
    }

    if (valid) {
        $("#btnAsif_submit_fail_reasons").attr("disabled", false);
        $("#reqStar").hide();
    }
    else {
        $("#btnAsif_submit_fail_reasons").attr("disabled", true);
        $("#reqStar").show();
    }
});

$("#btnAsif_submit_fail_reasons").on('click', function () {
    sprLib.list('StationeryRequests')
        .update({
            ID: requestID,
            Status: 'failed'
        })
        .then(function (objItem) {
            $("#btnAsif_Mark_as_Failed").fadeOut("slow");
            $("#divRejectReasons").fadeOut("slow");
            $("#btnAsif_submit_fail_reasons").fadeOut("slow");
            Swal.fire({
                text: 'تم إجراء رفض الطلب بنجاح ، مع إرسال تنويه بذلك لمقدم الطلب',
                type: 'success',
                confirmButtonText: 'تم'
            });


            var to = EmpEmail;
            var body = '<p dir=rtl>' +
                'السلام عليكم ورحمة الله وبركاته <br />' +
                ' تحية طيبة وبعد <br />' +
                ' نعتذر عن عدم إمكانية توفير طلب قسم الخدمات العامة الخاص بك (قرطاسية ومخازن) رقم "' + requestID + '" <br />' +
                'وذلك نظرا للأسباب التالية : <br />' +
                '<i>"' + $.trim($("#txtRejectReasons").val()) + '"</i>' + '<br />' +
                '<a href=' + _spPageContextInfo.webAbsoluteUrl + '/Pages/StoresRequestView.aspx?id=' + requestID + '>رابط الطلب</a>' +
                '</p >';
            var subject = 'نعتذر عن عدم إمكانية توفير طلب قسم الخدمات العامة الخاص بك (قرطاسية ومخازن)';
            sendEmail(to, body, subject);
        })
        .catch(function (strErr) { console.error(strErr); });
});