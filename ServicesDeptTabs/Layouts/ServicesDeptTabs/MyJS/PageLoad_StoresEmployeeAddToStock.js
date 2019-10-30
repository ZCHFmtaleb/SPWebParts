$(document).ready(function () {
    ReadCategories();


    $("#txtQuantity").jqxNumberInput({
        width: '60px',
        height: '30px',
        decimal: 1,
        digits: 5,
        decimalDigits: 0,
        min: 1,
        promptChar: '',
        groupSize: 0
    });


}); // end of document.ready

$("#btnAddToStock").on("click", AddToStock);