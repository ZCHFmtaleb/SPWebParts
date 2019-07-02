function createListItem() {
    //Fetch the values from the input elements  
    //var eName = $('#txtempname').val();
    //var eDesg = $('#txtdesignation').val();
    //var eEmail = $('#txtemail').val();
    //var eMobile = $('#txtmobile').val();
    //var eBloodGroup = $('#txtbloodgrp').val();
    //var eComAddress = $('#txtaddress').val();
    //var eEmergency = $('#txtemergency').val();

    $.ajax({
        async: true, // Async by default is set to “true” load the script asynchronously  
        // URL to post data into sharepoint list  
        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('StationeryRequests')/items",
        method: "POST", //Specifies the operation to create the list item  
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.StationeryRequestsListItem' // it defines the ListEnitityTypeName  
            },
            //Pass the parameters
            'Title': 'Test1'
            //'Designation': eDesg,
            //'Email': eEmail,
            //'Mobile': eMobile,
            //'BloodGroup': eBloodGroup,
            //'CommunicationAddress': eComAddress,
            //'EmergencyContact': eEmergency
        }),
        headers: {
            "accept": "application/json;odata=verbose", //It defines the Data format   
            "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
            "X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value   
        },
        success: function (data) {
            sweetAlert("نجاح", "تمت إضافة الطلب بنجاح", "success");
    },
        error: function (error) {
            console.log(JSON.stringify(error));
        }

    });

}  
