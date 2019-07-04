$(document).ready(function () {

    GetCategories();
    $("#txtQuantity").jqxNumberInput({
        width: '60px',
        height: '30px',
        spinButtons: true,
        decimal: 1,
        digits: 2,
        decimalDigits: 0,
        min: 1,
        max: 99,
        promptChar: ''
    });

    //var url = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('StoresCategories')/items?$select=Title";
    //var source =
    //{
    //    datatype: "json",
    //    datafields: [
    //        { Title: 'Title' }
    //    ],
    //    url: url,
    //    async: true
    //};

    //source = GetCategories();
    //var dataAdapter = new $.jqx.dataAdapter(source);
    //$("#ddlCat").jqxDropDownList({
    //    source: dataAdapter, displayMember: "Title", valueMember: "Title", width: 250, height: 30, placeHolder: "اختر مجموعة الصنف"
    //});
});


function GetItemsOfSelectedCat(selected_cat){
    $.ajax({
        url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('أصناف المخازن')/items?$filter=Category eq '" + selected_cat+"'&$select=Title",            
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            $("#ddlItem").empty();
            for (var i = 0; i < data.d.results.length; i++) {
                var item = data.d.results[i];
                $("#ddlItem").append(
                    $('<option></option>').val(item.Title).html(item.Title)
                );
            }
        },
        error: function (data) {
            alert("Error: " + data);
        }
    });
}