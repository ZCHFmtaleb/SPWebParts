var webURL = _spPageContextInfo.webAbsoluteUrl;
var api = "/_api/web/lists/";

function Read_ItemSpecificName_Categories() {
    var query = "GetByTitle('StoresCategories')/items?$select=Title";
    var fullURL = webURL + api + query;

    $.ajax({
        url: fullURL,
        method: "GET",
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose"
        },
        success: onQuerySucceeded,
        error: onQueryFailed
    });
}

function onQuerySucceeded(data) {
    $.each(data.d.results, function (key, value) {
        $("#ddlCat").append(
            $('<option></option>').val(value.Title).html(value.Title)
        );
    });
}

function onQueryFailed(data) {
    alert("Error: " + data);
}