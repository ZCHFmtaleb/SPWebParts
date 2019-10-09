function sendEmail(to, body, subject) {
    var siteurl = _spPageContextInfo.webServerRelativeUrl;
    var urlTemplate = siteurl + "/_api/SP.Utilities.Utility.SendEmail";

    $.ajax({
        contentType: 'application/json',
        url: urlTemplate,
        type: "POST",
        data: JSON.stringify({
            'properties': {
                '__metadata': {
                    'type': 'SP.Utilities.EmailProperties'
                },
               'To': {
                    'results': [to]
                },
                'CC': {
                    'results': ['test@zayed.org.ae']
                },
                'Body': body,
                'Subject': subject
            }
        }),
        headers: {
            "Accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-RequestDigest": jQuery("#__REQUESTDIGEST").val()
        },
        success: function (data) {
            console.log("an email is sent now successfully to " + to);
        },
        error: function (err) {
            console.log('Error in sending Email: ' + JSON.stringify(err));
        }
    });
}