/// <reference path="C:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\TEMPLATE\LAYOUTS\MicrosoftAjax.js" />/// <reference path="C:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\TEMPLATE\LAYOUTS\SP.debug.js" />
var results;
    function Test3() {
        sprLib.list('طلبات إدارة الخدمات - مخازن').items({
            listCols: {
                ID: { dataName: 'ID' },
                Status: { dataName: 'Status' }
            },
            queryFilter: 'RequestBatchGuid eq "55bb5316-6e79-405c-b016-d175acd9daf1"'
        })
            .then(function (arrData) {
                var i;
                for (i = 0; i < arrData.length; i++) {
                    sprLib.list('طلبات إدارة الخدمات - مخازن').update({
                        ID: arrData[i].ID,
                        Status: "نهائى"
                    });
                } 
               
            });
    }


    function Update() {
        var url = "/_api/Web/Lists/GetByTitle('طلبات إدارة الخدمات - مخازن')/getItemById('32')";
        var data = {
        __metadata: {'type': 'SP.Data.PurchasingStoresRequestsListItem' },
                Status: 'تم اعتماد قسم الخدمات'
            };

    updateItem(url,data);

}


    function Test() {
        $.ajax({
            url: "/_api/web/lists/getbytitle('طلبات إدارة الخدمات - مخازن')/Items?$select=ID,Status",
            type: "GET",
            headers: { "Accept": "application/json;odata=verbose" },
            cache: false,
            success: function (data) {
                console.log(data);
                var items = [];
                $(data.d.results).each(function () {
                    items.push('<ul id="' + 'listUL' + '">' + '<li id="' + 'listLI' + '">' + this.ID + ' , ' + this.Status + '</li>' + '</ul>');
                });
                items.push("</div>");
                $("#listResult").html(items.join(''))
            }
        });
    }



            function updateItem(url, updateData) {
        $.ajax({
            url: _spPageContextInfo.webAbsoluteUrl + url,
            type: "PATCH",
            headers: {
                "accept": "application/json;odata=verbose",
                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                "content-Type": "application/json;odata=verbose",
                "X-Http-Method": "PATCH",
                "If-Match": "*"
            },
            data: JSON.stringify(updateData),
            success: function (data) {
                console.log(data);
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });
    }

function GetQueryStringParams(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] === sParam) {
            return sParameterName[1];
        }
    }
}
function updateDMapprove() {    var g = GetQueryStringParams('rid');
    getListData(g);
}


function getListData(g) {
    var clientContext = new SP.ClientContext();
    var oList = clientContext.get_web().get_lists().getByTitle('طلبات إدارة الخدمات - مخازن');

    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml(
        '<View><Query><Where><Eq><FieldRef Name=\'RequestBatchGuid\'/>' +
        '<Value Type=\'Text\'>'+g+'</Value></Eq></Where></Query>' +
        '<RowLimit>10</RowLimit></View>'
    );

    this.collListItem = oList.getItems(camlQuery);
    clientContext.load(collListItem);

    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onQuerySucceeded),
        Function.createDelegate(this, this.onQueryFailed)
    );
}
function onQuerySucceeded() {
    var listItemInfo = '';
    var listItemEnumerator = collListItem.getEnumerator();
    while (listItemEnumerator.moveNext()) {
        var oListItem = listItemEnumerator.get_current();
        var n = oListItem.get_id();
        updateListItem(n);
    }
    $("#data").html(listItemInfo);
}
function onQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() +
        '\n' + args.get_stackTrace());
}


function updateListItem(id) {
    var clientContext = new SP.ClientContext();
    var oList = clientContext.get_web().get_lists().getByTitle('طلبات إدارة الخدمات - مخازن');

    this.oListItem = oList.getItemById(id);
    oListItem.set_item('Status', 'تم اعتماد المدير المباشر');
    oListItem.update();

    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onUpdateSucceeded),
        Function.createDelegate(this, this.onUpdateFailed)
    );
}
function onUpdateSucceeded() {
    alert('Item updated!');
}
function onUpdateFailed(sender, args) {
    alert('Request failed. ' + args.get_message() +
        '\n' + args.get_stackTrace());
}
