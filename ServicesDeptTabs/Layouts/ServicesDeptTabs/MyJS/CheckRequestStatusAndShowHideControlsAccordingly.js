function CheckRequestStatusAndShowHideControlsAccordingly() {

   var CurrentUserAccountName;

    
        
   sprLib.user().profile()
       .then(function (objProps) {           CurrentUserAccountName = objProps.AccountName;
            if (Status === "New_StationeryRequest_Started") {
                if (CurrentUserAccountName===DM) {

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

                    $('#decision_div').text("عذرا تم تقديم هذا الطلب سابقا").css({ "text-align": "center", "font-size": "large" });

                }
            }



        });
}