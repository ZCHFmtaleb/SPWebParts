function AddStationeryItemToGrid() {
    var row = {};

    row["Title"] = $("#ddlItem option:selected").text();
    row["Quantity"] = $('#txtQuantity').val();
    row["Notes"] = $('#txtNotes').val();

    if ($('#ddlItem').val()==="-1") {
        return;
    } else {
        var commit = $("#jqxgrid").jqxGrid('addrow', null, row);
    }
}