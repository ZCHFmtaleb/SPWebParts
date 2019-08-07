$("#btnDMReject").on('click', function () {
            $("#btnDMapprove").fadeOut("slow");
            $("#divRejectReasons").fadeIn("slow");
});


$('#txtRejectReasons').bind('input propertychange', function () {
    var valid = true;

    if (!$("#txtRejectReasons").val()) {
        valid = false;
    }

    if (valid) {
        $("#btnDMRejectSubmit").attr("disabled", false);
        $("#reqStar").hide();
    }
    else {
        $("#btnDMRejectSubmit").attr("disabled", true);
        $("#reqStar").show();
    }
});


$("#btnDMRejectSubmit").on('click', function () {
    
});
