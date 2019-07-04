function GetCategories() {
    $.ajax({
        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('StoresCategories')/items?$select=Title",
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            for (var i = 0; i < data.d.results.length; i++) {
                var item = data.d.results[i];  
                $("#ddlCat").append(
                    $('<option></option>').val(item.Title).html(item.Title)
                );
            } 
        },
        error: function (data) {
            alert("Error: " + data);
        }
    });
}  
