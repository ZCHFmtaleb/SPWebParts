// delete grid row
$("#deleterowbutton").on('click', function () {
    var selectedrowindex = $("#jqxgrid").jqxGrid('getselectedrowindex');
    var rowscount = $("#jqxgrid").jqxGrid('getdatainformation').rowscount;
    if (selectedrowindex >= 0 && selectedrowindex < rowscount) {
        var id = $("#jqxgrid").jqxGrid('getrowid', selectedrowindex);
        var commit = $("#jqxgrid").jqxGrid('deleterow', id);
    }
});