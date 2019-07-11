$(document).ready(function () {

    ReadCategories();

    $("#txtQuantity").jqxNumberInput({
        width: '60px',
        height: '30px',
        spinButtons: true,
        decimal: 1,
        digits: 2,
        decimalDigits: 0,
        min: 1,
        max: 99,
        promptChar: ''
    });

    $("#btnAddStationeryItemToGrid").on('click', function () {
        AddStationeryItemToGrid();
    });

    var row = {};
    var source = {
        localdata: row,
        datafields: [{
            name: 'firstname',
            type: 'string'
        }, {
            name: 'lastname',
            type: 'string'
        }, {
            name: 'productname',
            type: 'string'
        }],
        datatype: "array"
    };
    var adapter = new $.jqx.dataAdapter(source);
    $("#jqxgrid").jqxGrid({
        width: 600,
        height: 200,
        source: adapter,
        editable: true,
        selectionmode: 'singlerow',
        editmode: 'dblclick',
        columns: [
            {
                text: 'First Name',
                datafield: 'firstname',
                width: 200,
                editable: false
            }, {
                text: 'Last Name',
                datafield: 'lastname',
                width: 200
            }, {
                text: 'Product',
                datafield: 'productname',
                width: 200,
                columntype: 'numberinput',
                validation: function (cell, value) {
                    if (value < 1 ) {
                        return { result: false, message: "لابد أن تكون الكمية من 1 إلى 99" };
                    }
                    return true;
                },
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ width: '60px', height: '30px', spinButtons: true, decimal: 1, digits: 2, decimalDigits: 0, min: 1, max: 99, promptChar: '' });
                }
            }
        ]  // end of columns
    });
}); // end of document.ready

function GetItemsOfSelectedCat() {

    var selected_cat = $("#ddlCat").val();
    var webURL = _spPageContextInfo.webAbsoluteUrl;
    var api = "/_api/web/lists/";
    var query = "GetByTitle('أصناف المخازن')/items?$filter=Category eq '" + selected_cat +"'&$select=Title";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);

    $.ajax({
        url: encfullURL,            
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            $("#ddlItem").empty();
            for (var i = 0; i < data.d.results.length; i++) {
                var item = data.d.results[i];
                $("#ddlItem").append(
                    $('<option></option>').val(item.Title).html(item.Title)
                );
            }
        },
        error: function (data) {
            alert("Error: " + data);
        }
    });
}

// delete grid row
$("#deleterowbutton").on('click', function () {
    var selectedrowindex = $("#jqxgrid").jqxGrid('getselectedrowindex');
    var rowscount = $("#jqxgrid").jqxGrid('getdatainformation').rowscount;
    if (selectedrowindex >= 0 && selectedrowindex < rowscount) {
        var id = $("#jqxgrid").jqxGrid('getrowid', selectedrowindex);
        var commit = $("#jqxgrid").jqxGrid('deleterow', id);
    }
});


// Save All Rows To Server
$("#btnSaveAllRowsToServer").on('click', function () {
    var rowscount = $("#jqxgrid").jqxGrid('getdatainformation').rowscount;

    var webURL = _spPageContextInfo.webAbsoluteUrl;
    var api = "/_api/web/lists/";
    var query = "GetByTitle('إختبار')/items";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);


    for (var i = 0; i < rowscount; i++) {
        var data = $('#jqxgrid').jqxGrid('getrowdata', i); 

        $.ajax({
            url: encfullURL,
            type: "POST",
            data: JSON.stringify({
                '__metadata': { 'type': 'SP.Data.ListListItem' },
                'Title': data.firstname,
                'LastName': data.lastname,
                'Product': data.productname.toString()
            }),
            headers: {
                "accept": "application/json;odata=verbose",
                "content-type": "application/json;odata=verbose",
                "X-RequestDigest": $("#__REQUESTDIGEST").val()
            },
            success: onSaveAllRowsToServerSucceeded,
            error: onSaveAllRowsToServerFailed
        });
    }
});

function onSaveAllRowsToServerSucceeded(sender, args) {
    Swal.fire({
        text: 'تم إرسال الطلب بنجاح',
        type: 'success',
        confirmButtonText: 'تم'
    });
}
function onSaveAllRowsToServerFailed(error) {
    console.log(JSON.stringify(error));
    Swal.fire({
        text: 'حدث خطأ اثناء محاولة إرسال الطلب',
        type: 'error',
        confirmButtonText: 'تم'
    });
}




