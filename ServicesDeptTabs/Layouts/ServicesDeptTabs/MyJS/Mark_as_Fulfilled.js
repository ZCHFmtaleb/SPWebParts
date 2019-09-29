var resultsArray;

function Mark_as_Fulfilled() {
    //============Update "Fulfilled" in StationeryRequestDetails to true=======
    //============Deduct fulfilled items from Inventory "CurrentStock"======== 
    //============with checking for "MinStock" ==========================

    var rowscount = $("#jqxgrid").jqxGrid('getdatainformation').rowscount;
    resultsArray = [];
   
    for (var i = 0; i < rowscount; i++) {
        var data = $('#jqxgrid').jqxGrid('getrowdata', i);

        if (data.Fulfilled === true) {
            update_to_fulfilled_in_StationeryRequestDetails(data.ID, data.ItemSpecificName,data);
        }
    } // End of for loop

    var allSuccess = true;
    for (n = 0; n < resultsArray.length; n++) {
        if (resultsArray[n] === false) {
            allSuccess = false;
        }
    }
    if (allSuccess === true) {
        Swal.fire({
            html: 'Fulfilled quantities deducted successfully for all items <br> تم خصم الكميات المنصرفة لجميع الأصناف بنجاح',
            type: 'success',
            confirmButtonText: 'OK &nbsp; &nbsp; &nbsp; تم'
        });

        sprLib.list('StationeryRequests')
            .update({
                ID: requestID,
                Status: 'fulfilled'
            })
            .then(function (objItem) {
                $("#btnAsif_Mark_as_Fulfilled").fadeOut("slow");
                $("#btnAsif_Mark_as_Failed").fadeOut("slow");

                var to = EmpEmail;
                var body = '<p dir=rtl>' +
                    'السلام عليكم ورحمة الله وبركاته <br />' +
                    ' تحية طيبة وبعد <br />' +
                    ' تم بنجاح تنفيذ الطلب الخاص بكم من قسم الخدمات العامة (قرطاسية ومخازن) رقم "' + requestID + '" <br />' +
                    '<a href=' + _spPageContextInfo.webAbsoluteUrl + '/Pages/StoresRequestView.aspx?id=' + requestID + '>رابط الطلب</a><br /><br />' +
                    'فى حالة وجود خطأ يرجى التواصل مع قسم الخدمات العامة<br />' +
                    'وشكرا جزيلا لحسن تعاونكم<br />' +
                    '</p >';
                var subject = 'تم بنجاح تنفيذ الطلب الخاص بكم من قسم الخدمات العامة';
                sendEmail(to, body, subject);
            })
            .catch(function (strErr) { alert(JSON.stringify(error)); });

    }
    else if (allSuccess === false) {
        Swal.fire({
            html: 'An error occured with some items <br> حدث خطأ أثناء محاولة خصم الكميات المنصرفة لبعض الأصناف',
            type: 'error',
            confirmButtonText: 'OK &nbsp; &nbsp; &nbsp; تم'
        });
    }
}


function update_to_fulfilled_in_StationeryRequestDetails(pDetailsRowID, pItemSpecificName,pData) {
    $.ajax({
        async: false,
        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('StationeryRequestDetails')/items(" + pDetailsRowID + ")",
        type: "POST",
        headers: {
            "accept": "application/json;odata=verbose",
            "X-RequestDigest": $("#__REQUESTDIGEST").val(),
            "content-Type": "application/json;odata=verbose",
            "IF-MATCH": "*",
            "X-HTTP-Method": "MERGE"
        },
        data: "{__metadata:{'type':'SP.Data.StationeryRequestDetailsListItem'},'Fulfilled':true,'ItemSpecificName': '" + pItemSpecificName + "'}",
        success: function (data) {
            get_Inventory_amounts_then_update_it(pData);
        },
        error: function (error) {
            resultsArray.push(false);
            alert(JSON.stringify(error));
        }
    });
}			


function get_Inventory_amounts_then_update_it(kData) {

    var webURL = _spPageContextInfo.webAbsoluteUrl;
    var api = "/_api/web/lists/";
    var query = "GetByTitle('Inventory')/items?$filter=ID eq '" + kData.InventoryID + "'&$select=CurrentStock,MinStock";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);

    $.ajax({
        async: false,
        url: encfullURL,
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (T) {
            var CS = T.d.results[0].CurrentStock;
            var MS = T.d.results[0].MinStock;
            var q = kData.Quantity;
            var NS = CS - q;
            console.log(kData.InventoryID + "  " + CS + " and " + MS);
            console.log(kData.Fulfilled);
            update_to_new_stock(kData.InventoryID, NS, MS, kData.ItemSpecificName);
        },
        error: function (errorData) {
            resultsArray.push(false);
            alert("Error: " + errorData);
        }
    });
}


function update_to_new_stock(pInvID, pNewStock, pMinStock, pItemSpecificName) {
    $.ajax({
        async: false,
        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('Inventory')/items("+ pInvID +")",
        type: "POST",
        headers: {
            "accept": "application/json;odata=verbose",
            "X-RequestDigest": $("#__REQUESTDIGEST").val(),
            "content-Type": "application/json;odata=verbose",
            "IF-MATCH": "*",
            "X-HTTP-Method": "MERGE"
        },
        data: "{__metadata:{'type':'SP.Data.InventoryListItem'},CurrentStock: " + pNewStock+"} ",
        success: function (data) {
            $("#results_summary").append('<li><span style="color:LimeGreen;font-weight:bold;">&#x2714; </span>تم خصم الكمية المنصرفة بنجاح من مخزون الصنف  "' + pItemSpecificName + '" - المخزون الحالى ' + '<span class=bb>' + pNewStock + '</span>' + ' - الحد الأدنى ' + '<span class=bb>' + pMinStock + '</span>' +'</li>');
            if (pNewStock < pMinStock) {
                $("#results_summary").append('<li style="color:OrangeRed;font-weight:bold;">تنبيه : المخزون الحالى أقل من الحد الأدنى  Warning: Current Stock is less than minimum</li>');
            }
            resultsArray.push(true);
        },
        error: function (error) {
            resultsArray.push(false);
            alert(JSON.stringify(error));
        }
    });  	
}