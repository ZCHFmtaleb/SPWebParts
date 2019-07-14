var user = null;var userTitle = "";var userId = "";var loginName="";var userEmail="";
$(document).ready(function () {
    ExecuteOrDelayUntilScriptLoaded(GetCurrentUser, "sp.js");    GetCurrentUser();
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
            name: 'Title',
            type: 'string'
        }, {
                name: 'Quantity',
            type: 'string'
        }, {
                name: 'Notes',
            type: 'string'
        }],
        datatype: "array"
    };
    var adapter = new $.jqx.dataAdapter(source);
    $("#jqxgrid").jqxGrid({
        rtl: true,
        width: 600,
        height: 200,
        source: adapter,
        editable: true,
        selectionmode: 'singlerow',
        editmode: 'dblclick',
        columns: [
            {
                text: 'اسم الصنف',
                datafield: 'Title',
                width: 200,
                editable: false,
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle'
            }, {
                text: 'الكمية',
                datafield: 'Quantity',
                width: 200,
                columntype: 'numberinput',
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle',
                validation: function (cell, value) {
                    if (value < 1 ) {
                        return { result: false, message: "لابد أن تكون الكمية من 1 إلى 99" };
                    }
                    return true;
                },
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ width: '60px', height: '30px', spinButtons: true, decimal: 1, digits: 2, decimalDigits: 0, min: 1, max: 99, promptChar: '' });
                }
            },{
                text: 'ملاحظات',
                datafield: 'Notes',
                width: 200,
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle'
            }
        ]  // end of columns
    });
}); // end of document.ready


function GetCurrentUser() {
    var context = new SP.ClientContext.get_current();
    var web = context.get_web();
    user = web.get_currentUser(); //must load this to access info.
    context.load(user);
    context.executeQueryAsync(onGetCurrentUserSucceeded, onGetCurrentUserFailed);}
function onGetCurrentUserSucceeded() {    userTitle = user.get_title();    userId = user.get_id();    loginName = user.get_loginName();    userEmail = user.get_email();    console.log("current user data are:  " + userTitle + "," + userId + "," + loginName + "," + userEmail);}function onGetCurrentUserFailed(sender, args) {    console.log('request failed ' + args.get_message() + '\n' + args.get_stackTrace());}

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
    var query = "GetByTitle('StationeryRequests')/items";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);
    var guid = uuidv1();

    for (var i = 0; i < rowscount; i++) {
        var data = $('#jqxgrid').jqxGrid('getrowdata', i); 

        $.ajax({
            url: encfullURL,
            type: "POST",
            data: JSON.stringify({
                '__metadata': { 'type': 'SP.Data.StationeryRequestsListItem' },
                'Title': data.Title,
                'Quantity': data.Quantity.toString(),
                'Notes': data.Notes,
                'EmpId': userId,
                'Status': 'New_StationeryRequest_Started',
                'RequestBatchGuid': guid
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




