var requestID;
var source;
var adapter;
var EmpArabicName = "موظف اختبارى 1";

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
        listCols: ['Created', 'Department', 'EmpArabicName'],
        queryFilter: 'ID eq ' + requestID
    })
        .then(function (arrData) {

            var date = new Date(arrData[0].Created);
            var formatDate = date.toLocaleString('en-GB', { year: "numeric", month: "numeric", day: "numeric" });
            $('#lbl_ReqDate').text(formatDate);
            $('#lblEmpName').text(arrData[0].EmpArabicName);
            $('#lblDept').text(arrData[0].Department);
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

}); // end of document.readyfunction BindArrayToGrid(data) {
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
        width: 600,
        height: 200,
        source: adapter,
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
                    if (value < 1) {
                        return { result: false, message: "لابد أن تكون الكمية من 1 إلى 99" };
                    }
                    return true;
                },
                createeditor: function (row, cellvalue, editor) {
                    editor.jqxNumberInput({ width: '60px', height: '30px', spinButtons: true, decimal: 1, digits: 2, decimalDigits: 0, min: 1, max: 99, promptChar: '' });
                }
            }, {
                text: 'ملاحظات',
                datafield: 'Notes',
                width: 200,
                align: 'right',
                cellsalign: 'right',
                cellclassname: 'GridCellStyle'
            }
        ]  // end of columns
    });

}