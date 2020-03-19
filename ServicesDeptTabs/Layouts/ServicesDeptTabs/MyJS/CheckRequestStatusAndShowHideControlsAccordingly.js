function CheckRequestStatusAndShowHideControlsAccordingly() {

   var CurrentUserAccountName;
   var CurrentUserAccountEmail;
    
        
   sprLib.user().profile()
       .then(function (objProps) {           CurrentUserAccountName = objProps.AccountName;           CurrentUserAccountEmail = objProps.Email;
           if (Status.toLowerCase() === "New_StationeryRequest_Started".toLowerCase()) {
               if (CurrentUserAccountName.toLowerCase() === DM.toLowerCase()) {

                   $('#btnDMapprove').show();
                   $('#btnDMReject').show();

                   $('#btnServicesDivisionHeadApprove').hide();
                   $('#btnServicesDivisionHeadReject').hide();
                   $('#btnServicesDivisionHeadRejectSubmit').remove();
               }
               else if (1 == 1) {
                   // if Requester Employee has a "SpecialRouting", and current user is the approver as per "SpecialRouting" list, show approve buttons
                   var IsCurrentUserApprover_InSpecialRouting = false;
                   $.ajax({
                       async: false,
                       url: encodeURI(_spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/" + "GetByTitle('SpecialRouting')/items?$filter=ApproverEmail eq '" + CurrentUserAccountEmail + "'&$select=EmployeeEmail"),
                       method: "GET",
                       headers: { "Accept": "application/json; odata=verbose" },
                       success: function (T) {
                           if (T === undefined || T.d.results.length == 0) {
                           } else {
                               IsCurrentUserApprover_InSpecialRouting = true;
                           }
                       },
                       error: function (errorData) {
                           alert("Error: " + errorData);
                       }
                   });
                   if (IsCurrentUserApprover_InSpecialRouting===true) {
                       $('#btnDMapprove').show();
                       $('#btnDMReject').show();

                       $('#btnServicesDivisionHeadApprove').hide();
                       $('#btnServicesDivisionHeadReject').hide();
                       $('#btnServicesDivisionHeadRejectSubmit').remove();
                   }
                }
                else {

                    $('#btnDMapprove').hide();
                    $('#btnDMReject').hide();

                    $('#btnServicesDivisionHeadApprove').hide();
                    $('#btnServicesDivisionHeadReject').hide();

                    $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : بإنتظار إعتماد المدير المباشر").css({ "text-align": "center", "font-size": "large" });

               }
               return;
           }
           else if (Status.toLowerCase() === "approved_by_DM".toLowerCase()) {
               if (CurrentUserAccountEmail === ServicesDivisionHead_email) {

                   $('#btnDMapprove').hide();
                   $('#btnDMReject').hide();
                   $('#btnDMRejectSubmit').remove();
                   

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
               return;
            }
           else if (Status.toLowerCase() === "approved_by_ServicesDivisionHead".toLowerCase()) {
                   $('#btnDMapprove').hide();
                   $('#btnDMReject').hide();
                   $('#btnServicesDivisionHeadApprove').hide();
                   $('#btnServicesDivisionHeadReject').hide();
                   $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : جارى تنفيذ الطلب").css({ "text-align": "center", "font-size": "large" });
           }

           else if (Status.toLowerCase() === "fulfilled".toLowerCase()) {
               $('#btnDMapprove').hide();
               $('#btnDMReject').hide();
               $('#btnServicesDivisionHeadApprove').hide();
               $('#btnServicesDivisionHeadReject').hide();
               $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : تم توفير الطلب").css({ "text-align": "center", "font-size": "large" });
           }
           else if (Status.toLowerCase() === "failed".toLowerCase()) {
               $('#btnDMapprove').hide();
               $('#btnDMReject').hide();
               $('#btnServicesDivisionHeadApprove').hide();
               $('#btnServicesDivisionHeadReject').hide();
               $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا - حالة الطلب : لا توجد إمكانية لتوفير الطلب").css({ "text-align": "center", "font-size": "large" });
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