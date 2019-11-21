function GetItemsOfSelectedCat(listName) {    var selected_cat = $("#ddlCat").val();

    if (selected_cat === "-1") {
        $("#ddlItem").empty();
        $("#ddlItem").append(
            $('<option></option>').val('-1').html('اختر الصنف المطلوب')
        );
        return;
    }


    var webURL = _spPageContextInfo.webAbsoluteUrl;
    var api = "/_api/web/lists/";
    var query = "GetByTitle('" + listName + "')/items?$filter=Category eq '" + selected_cat + "'&$select=ID,Title";
    var fullURL = webURL + api + query;
    var encfullURL = encodeURI(fullURL);

    $.ajax({
        url: encfullURL,
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            $("#ddlItem").empty();
            var SortedItems = data.d.results.sort(function (a, b) {
                return a.Title.localeCompare(b.Title);
            });
            for (var i = 0; i < SortedItems.length; i++) {
                var item = SortedItems[i];
                $("#ddlItem").append(
                    $('<option></option>').val(item.ID).html(item.Title)
                );
            }
        },
        error: function (data) {
            alert("Error: " + data.responseText);
        }
    });
}