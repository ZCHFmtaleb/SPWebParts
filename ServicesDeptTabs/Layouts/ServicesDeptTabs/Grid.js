$(document).ready(function () {
    // prepare the data
    var data = generatedata(200);

    var source =
    {
        localdata: data,
        datatype: "array",
        updaterow: function (rowid, rowdata, commit) {
            // synchronize with the server - send update command
            // call commit with parameter true if the synchronization with the server is successful 
            // and with parameter false if the synchronization failder.
            commit(true);
        },
        datafields:
            [
                { name: 'firstname', type: 'string' },
                { name: 'lastname', type: 'string' },
                { name: 'productname', type: 'string' },
                { name: 'available', type: 'bool' },
                { name: 'quantity', type: 'number' },
                { name: 'price', type: 'number' },
                { name: 'date', type: 'date' }
            ]
    };

    var dataAdapter = new $.jqx.dataAdapter(source);

    // initialize jqxGrid
    $("#grid").jqxGrid(
        {
            width: getWidth('Grid'),
            source: dataAdapter,
            editable: true,
            enabletooltips: true,
            selectionmode: 'multiplecellsadvanced',
            columns: [
                { text: 'اسم الصنف العام', columntype: 'textbox', datafield: 'firstname', width: 120 },
                { text: 'Last Name', datafield: 'lastname', columntype: 'textbox', width: 120 },
                { text: 'الكمية', columntype: 'dropdownlist', datafield: 'productname', width: 195 },
                { text: 'Available', datafield: 'available', columntype: 'checkbox', width: 67 },
                {
                    text: 'Ship Date', datafield: 'date', columntype: 'datetimeinput', width: 110, align: 'right', cellsalign: 'right', cellsformat: 'd',
                    validation: function (cell, value) {
                        if (value == "")
                            return true;

                        var year = value.getFullYear();
                        if (year >= 2020) {
                            return { result: false, message: "Ship Date should be before 1/1/2020" };
                        }
                        return true;
                    }
                },
                {
                    text: 'Quantity', datafield: 'quantity', width: 70, align: 'right', cellsalign: 'right', columntype: 'numberinput',
                    validation: function (cell, value) {
                        if (value < 0 || value > 150) {
                            return { result: false, message: "Quantity should be in the 0-150 interval" };
                        }
                        return true;
                    },
                    createeditor: function (row, cellvalue, editor) {
                        editor.jqxNumberInput({ decimalDigits: 0, digits: 3 });
                    }
                },
                {
                    text: 'Price', datafield: 'price', align: 'right', cellsalign: 'right', cellsformat: 'c2', columntype: 'numberinput',
                    validation: function (cell, value) {
                        if (value < 0 || value > 15) {
                            return { result: false, message: "Price should be in the 0-15 interval" };
                        }
                        return true;
                    },
                    createeditor: function (row, cellvalue, editor) {
                        editor.jqxNumberInput({ digits: 3 });
                    }

                }
            ]
        });

    // events
    $("#grid").on('cellbeginedit', function (event) {
        var args = event.args;
        $("#cellbegineditevent").text("Event Type: cellbeginedit, Column: " + args.datafield + ", Row: " + (1 + args.rowindex) + ", Value: " + args.value);
    });

    $("#grid").on('cellendedit', function (event) {
        var args = event.args;
        $("#cellendeditevent").text("Event Type: cellendedit, Column: " + args.datafield + ", Row: " + (1 + args.rowindex) + ", Value: " + args.value);
    });
});
