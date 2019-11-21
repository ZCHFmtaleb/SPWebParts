function Read_ItemSpecificNames() {
    $("#ddlItem").empty();
    var selected_cat = $("#ddlCat").val();
    var qF = "Category eq '" + selected_cat + "'";
    var encqF = encodeURI(qF);

    if (selected_cat === "Select Category") {
        $("#ddlItem").append(
            $('<option></option>').val("(Please select category first)").html("(Please select category first)")
        );
    }
    else {
        sprLib.list('Inventory').items({
            listCols: ['ID', 'Title'],
            queryFilter: encqF,
            queryOrderby: 'ID'
        })
            .then(function (arrData) {
                var SortedItems = arrData.sort(function (a, b) {
                    return a.Title.localeCompare(b.Title);
                });
                for (var i = 0; i < SortedItems.length; i++) {
                    var item = SortedItems[i];
                    $("#ddlItem").append(
                        $('<option></option>').val(item.ID).html(item.Title)
                    );
                }
            })
            .catch(function (errMsg) { console.error(errMsg); });
    }
}