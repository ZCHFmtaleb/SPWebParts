function AddStationeryItemToGrid() {
    var row = {};
    row["Title"] = $('#ddlItem').val();
    row["Quantity"] = $('#txtQuantity').val();
    row["Notes"] = $('#txtNotes').val();

    var commit = $("#jqxgrid").jqxGrid('addrow', null, row);
}