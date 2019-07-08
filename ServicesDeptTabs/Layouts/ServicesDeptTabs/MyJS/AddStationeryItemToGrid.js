function AddStationeryItemToGrid() {

    var row2 = {};
    row2["firstname"] = $('#ddlCat').val();
    row2["lastname"] = $('#ddlItem').val();
    row2["productname"] = $('#txtQuantity').val();

    var commit = $("#jqxgrid").jqxGrid('addrow', null, row2);

}
