$(document).ready(function () 
{
    write_Next_Year();
});

function write_Next_Year() 
{
    var next_year = new Date().getFullYear()+1;
    $(".Next_Year").text(next_year);
}
