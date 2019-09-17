var requestID;
var source;
var adapter;
var EmpArabicName = "موظف اختبارى 1";
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

    requestID = $.urlParam('id');

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

        })
        .catch(function (errMsg) { console.error(errMsg); });

    //==============================================================

    sprLib.list('StationeryRequestDetails').items({
        listCols: ['ID', 'MasterRecordId', 'Title', 'Quantity', 'Notes'],
        queryFilter: 'MasterRecordId eq ' + requestID,
        queryOrderby: 'ID'
    })
        .then(function (arrData) {
            BindArrayToGrid(arrData);
        })
        .catch(function (errMsg) { console.error(errMsg); });

    //==============================================================

    Read_ItemSpecificName_Categories();

}); // end of document.ready$("#ddlCat").on("change", Read_ItemSpecificNames);function BindArrayToGrid(data) {
    source = {
        localdata: data,
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
    adapter = new $.jqx.dataAdapter(source);
    $("#jqxgrid").jqxGrid({
        rtl: true,
        width: 1000,
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
                editable: false
            }, {
                text: 'ملاحظات',
                datafield: 'Notes',
                width: 200,
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle',
                editable: false
            }, {
                text: 'fullfilled - تم توفيره',
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
                width: 250,
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
        if (editrow >= 0) {
            //var row = {
            //    firstname: $("#firstName").val(), lastname: $("#lastName").val(), productname: $("#product").val(),
            //    quantity: parseInt($("#quantity").jqxNumberInput('decimal')), price: parseFloat($("#price").jqxNumberInput('decimal'))
            //};
            var rowID = $('#jqxgrid').jqxGrid('getrowid', editrow);
            $('#jqxgrid').jqxGrid('updaterow', rowID, row);
            $("#popupWindow").jqxWindow('hide');
        }
    });


}