function CheckRequestStatusAndShowHideControlsAccordingly() {

   var CurrentUserAccountName;

    
        
   sprLib.user().profile()
       .then(function (objProps) {           CurrentUserAccountName = objProps.AccountName;
           if (Status.toLowerCase() === "New_StationeryRequest_Started".toLowerCase()) {
                if (CurrentUserAccountName.toLowerCase() === DM.toLowerCase()) {

                    $('#btnDMapprove').show();
                    $('#btnDMReject').show();

                    $('#btnServicesDivisionHeadApprove').hide();
                    $('#btnServicesDivisionHeadReject').hide();
                    

                }
                else {

                    $('#btnDMapprove').hide();
                    $('#btnDMReject').hide();

                    $('#btnServicesDivisionHeadApprove').hide();
                    $('#btnServicesDivisionHeadReject').hide();

                    $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : بإنتظار إعتماد المدير المباشر").css({ "text-align": "center", "font-size": "large" });

                }
           }
           else if (Status.toLowerCase() === "approved_by_DM".toLowerCase()) {
               if (CurrentUserAccountName.toLowerCase() === "ZAYED\\test_manager_2".toLowerCase()) {

                    $('#btnDMapprove').hide();
                    $('#btnDMReject').hide();

                    $('#btnServicesDivisionHeadApprove').show();
                    $('#btnServicesDivisionHeadReject').show();


                }
                else {

                    $('#btnDMapprove').hide();
                    $('#btnDMReject').hide();

                    $('#btnServicesDivisionHeadApprove').hide();
                    $('#btnServicesDivisionHeadReject').hide();

                    $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : بإنتظار إعتماد قسم الخدمات العامة").css({ "text-align": "center", "font-size": "large" });

                }

            }
           else if (Status.toLowerCase() === "approved_by_ServicesDivisionHead".toLowerCase()) {
                   $('#btnDMapprove').hide();
                   $('#btnDMReject').hide();
                   $('#btnServicesDivisionHeadApprove').hide();
                   $('#btnServicesDivisionHeadReject').hide();
                   $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : جارى تنفيذ الطلب").css({ "text-align": "center", "font-size": "large" });
           }
           else if (Status.toLowerCase() === "rejected_by_DM".toLowerCase()) {
               $('#btnDMapprove').hide();
               $('#btnDMReject').hide();
               $('#btnServicesDivisionHeadApprove').hide();
               $('#btnServicesDivisionHeadReject').hide();
               $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : تم رفض الطلب من قبل المدير المباشر").css({ "text-align": "center", "font-size": "large" });
           }
           else if (Status.toLowerCase() === "rejected_by_ServicesDivisionHead".toLowerCase()) {
               $('#btnDMapprove').hide();
               $('#btnDMReject').hide();
               $('#btnServicesDivisionHeadApprove').hide();
               $('#btnServicesDivisionHeadReject').hide();
               $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : تم رفض الطلب من قبل قسم الخدمات العامة").css({ "text-align": "center", "font-size": "large" });
           }


        });
}