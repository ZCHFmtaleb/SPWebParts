var InventoryID;
var Quantity;

function AddToStock() {
        InventoryID = $("#ddlItem option:selected").val();

        if (InventoryID === "-1") {
            Swal.fire({
                text: 'No Item selected - لم يتم إختيار صنف محدد',
                type: 'error',
                confirmButtonText: 'OK'
            });
            return;
        }     

        Quantity=$('#txtQuantity').val();
        get_Inventory_amounts_then_update_it();
}

function get_Inventory_amounts_then_update_it() {

    var webURL = _spPageContextInfo.webAbsoluteUrl;
    var api = "/_api/web/lists/";
    var query = "GetByTitle('Inventory')/items?$filter=ID eq '" + InventoryID + "'&$select=CurrentStock";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);

    $.ajax({
        async: false,
        url: encfullURL,
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (T) {
            /*  CS Current Stock
             *  q    Quantity
             *  NS New Stock
             * */
            var CS = T.d.results[0].CurrentStock;
            var q = Quantity;
            var NS = CS + q;
            update_to_new_stock(NS);
        },
        error: function (errorData) {
            alert("Error: " + errorData);
        }
    });
}


function update_to_new_stock(pNewStock) {
    $.ajax({
        async: false,
        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('Inventory')/items(" + InventoryID + ")",
        type: "POST",
        headers: {
            "accept": "application/json;odata=verbose",
            "X-RequestDigest": $("#__REQUESTDIGEST").val(),
            "content-Type": "application/json;odata=verbose",
            "IF-MATCH": "*",
            "X-HTTP-Method": "MERGE"
        },
        data: "{__metadata:{'type':'SP.Data.InventoryListItem'},CurrentStock: " + pNewStock + "} ",
        success: function (data) {
            var message = ' تم إضافة الكمية بنجاح إلى مخزون الصنف - المخزون الحالى ' + pNewStock + '<br> Quantity is added successfully to stock - current stock ' + pNewStock;
            Swal.fire({
                html: message,
                type: 'success',
                confirmButtonText: 'OK'
            });
        },
        error: function (error) {
            alert(JSON.stringify(error));
        }
    });
}