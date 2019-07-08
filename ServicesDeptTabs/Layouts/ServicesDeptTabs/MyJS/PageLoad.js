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
        columns: [
            {
                text: 'First Name',
                datafield: 'firstname',
                width: 200
            }, {
                text: 'Last Name',
                datafield: 'lastname',
                width: 200
            }, {
                text: 'Product',
                datafield: 'productname',
                width: 200
            }
        ]
    });

});

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
            console.log(fullURL);
            console.log(data.d.results.length);
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