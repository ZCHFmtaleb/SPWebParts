var requestID;
var source;
var adapter;
var EmpArabicName;
var EmpEmail;
var Status;
var DM;

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results === null) {
        return null;
    }
    else {
        return decodeURI(results[1]) || 0;
    }
};

$(document).ready(function () {

    requestID = $.urlParam('srid');

    //==============================================================

    sprLib.list('StationeryRequests').items({
        listCols: ['Created', 'Department', 'EmpArabicName', 'EmpEmail', 'Status', 'DM'],
        queryFilter: 'ID eq ' + requestID
    })
        .then(function (arrData) {

            var date = new Date(arrData[0].Created);
            var formatDate = date.toLocaleString('en-GB', { year: "numeric", month: "numeric", day: "numeric" });
            $('#lbl_ReqDate').text(formatDate);
            $('#lblEmpName').text(arrData[0].EmpArabicName);
            $('#lblDept').text(arrData[0].Department);
            EmpEmail = arrData[0].EmpEmail;
            Status = arrData[0].Status;
            DM = arrData[0].DM;

            hide_buttons_if_already_done_before();

        })
        .catch(function (errMsg) { console.error(errMsg); });

    //==============================================================

    sprLib.list('StationeryRequestDetails').items({
        listCols: ['ID', 'MasterRecordId', 'Title', 'Quantity', 'Notes', 'Fulfilled'],
        queryFilter: 'MasterRecordId eq ' + requestID,
        queryOrderby: 'ID'
    })
        .then(function (arrData) {
            BindArrayToGrid(arrData);
        })
        .catch(function (errMsg) { console.error(errMsg); });

    //==============================================================

    Read_ItemSpecificName_Categories();

}); // end of document.readyfunction hide_buttons_if_already_done_before() {    if (Status.toLowerCase() === "fulfilled".toLowerCase() || Status.toLowerCase() === "failed".toLowerCase()) {
        $("#btnAsif_Mark_as_Fulfilled").hide();
        $("#btnAsif_Mark_as_Failed").hide();
        $('#decision_div').text("عذرا تم إنهاء هذا الطلب سابقا بالفعل - This request has been processed before").css({ "text-align": "center", "font-size": "large" });
    }
}$("#ddlCat").on("change", Read_ItemSpecificNames);$("#btnAsif_Mark_as_Fulfilled").on("click", Mark_as_Fulfilled);$("#btnAsif_Mark_as_Failed").on("click", Mark_as_Failed);function BindArrayToGrid(data) {
    source = {
        localdata: data,
        datafields: [
         {
            name: 'ID',
            type: 'string'
            }, {
            name: 'InventoryID',
            type: 'string'
            }, {
            name: 'Title',
            type: 'string'
        }, {
            name: 'Quantity',
            type: 'string'
        }, {
            name: 'Notes',
            type: 'string'
            }, {
                name: 'Fulfilled',
                type: 'bool'
            }
        ],
        datatype: "array"
    };
    adapter = new $.jqx.dataAdapter(source);
    $("#jqxgrid").jqxGrid({
        rtl: true,
        width: 1500,
        height: 200,
        source: adapter,
        editable: true,
        selectionmode: 'singlerow',
        editmode: 'dblclick',
        columns: [
            {
                text: 'ID',
                datafield: 'ID',
                width: 50,
                editable: false
            },
            {
                text: 'InvID',
                datafield: 'InventoryID',
                width: 50,
                editable: false
            },
            {
                text: 'اسم الصنف',
                datafield: 'Title',
                width: 400,
                editable: false,
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle'
            }, {
                text: 'الكمية',
                datafield: 'Quantity',
                width: 100,
                columntype: 'numberinput',
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle',
                editable: false
            }, {
                text: 'ملاحظات',
                datafield: 'Notes',
                width: 400,
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle',
                editable: false
            }, {
                text: 'fullfilled - تم توفيره',
                datafield: 'Fulfilled',
                width: 150,
                columntype: 'checkbox',
                align: 'center',
                cellsalign: 'center',
                cellclassname: 'GridCellStyle',
                editable: true,
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxCheckBox({ checked: false, hasThreeStates: false });
                }
            }, {
                text: 'ItemSpecificName',
                datafield: 'ItemSpecificName',
                width: 250,
                align: 'center',
                cellsalign: 'center',
                editable: false
            }, {
                text: 'Select Item',
                width: 100,
                align: 'center',
                cellsalign: 'center',
                columntype: 'button',
                cellsrenderer: function () {
                    return "Edit";
                },
                buttonclick: function (row) {
                    // open the popup window when the user clicks a button.
                    editrow = row;
                    var offset = $("#jqxgrid").offset();
                    $("#popupWindow").jqxWindow({ isModal: true, modalOpacity: 0.5, position: { x: parseInt(offset.left) + 60, y: parseInt(offset.top) + 60 } });

                    // get the clicked row's data and initialize the input fields.
                    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
                    $("#hidden_ID").text(dataRecord.ID);
                    $("#hidden_Title").text(dataRecord.Title);
                    $("#hidden_Quantity").text(dataRecord.Quantity);
                    $("#hidden_Notes").text(dataRecord.Notes);

                    // show the popup window.
                    $("#popupWindow").jqxWindow('open');
                }
            }
        ]  // end of columns
    });
    // initialize the popup window and buttons.
    $("#popupWindow").jqxWindow({
        theme: 'energyblue', width: 450, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#Cancel"), modalOpacity: 0.5
    });

    $("#Cancel").jqxButton({ theme: 'energyblue' });
    $("#Save").jqxButton({ theme: 'energyblue' });

    // update the edited row when the user clicks the 'Save' button.
    $("#Save").click(function () {
        if ($("#ddlItem option:selected").text() === "(Please select category first)") {
            $("#item_not_selected_error").text("Please Select Item");
            return;
        } else {
            $("#item_not_selected_error").empty();
        }
        if (editrow >= 0) {
            var row = {
                ID: $("#hidden_ID").text(),
                Title: $("#hidden_Title").text(),
                Quantity: $("#hidden_Quantity").text(),
                Notes: $("#hidden_Notes").text(),
                ItemSpecificName: $("#ddlItem option:selected").text(),
                InventoryID: $("#ddlItem option:selected").val(),
                Fulfilled:true
            };
            var rowID = $('#jqxgrid').jqxGrid('getrowid', editrow);
            $('#jqxgrid').jqxGrid('updaterow', rowID, row);
            $("#popupWindow").jqxWindow('hide');
        }
    });

}