var gl_Objs_Avg = 0;
var gl_Skills_Avg = 0;


$(document).ready(function () 
{
    write_Next_Year();
    Calc_Avg_of_ObjRates();
    Calc_Avg_of_SkillRates();
    Calc_Final_Result();
});

function write_Next_Year() 
{
    var next_year = new Date().getFullYear()+1;
    $(".Next_Year").text(next_year);
}

$(".ObjRate").on('change', function () {
    Calc_Avg_of_ObjRates();
    Calc_Final_Result();
});

$(".SkillRate").on('change', function () {
    Calc_Avg_of_SkillRates();
    Calc_Final_Result();
});


function Calc_Avg_of_ObjRates() {
    var sum = 0;
    var Number_of_Objs = $('.ObjRate').length;
    $('.ObjRate :selected').each(function () {
            sum += Number($(this).val());
    });

    Avg = Number(sum / Number_of_Objs).toFixed(2);         
    $("#Avg_of_ObjRates").html(Avg);
    gl_Objs_Avg = Avg;
}

function Calc_Avg_of_SkillRates() {
    var sum2 = 0;
    var Number_of_Skills = $('.SkillRate').length;
    $('.SkillRate :selected').each(function () {
        sum2 += Number($(this).val());
    });

    AvgSkills = Number(sum2 / Number_of_Skills).toFixed(2);
    $("#Avg_of_SkillRates").html(AvgSkills);
    gl_Skills_Avg = AvgSkills;
}

function Calc_Final_Result() {
    var Final_Result = 0;
    Final_Result = (Number(gl_Objs_Avg) +Number(gl_Skills_Avg)) / 2.00;
    $("#Final_Result").html(Final_Result.toFixed(2));

    Refresh_Commentary(Final_Result.toFixed(0));

}


function Refresh_Commentary(r) {
    switch (Number(r)) {
        case 1:
            $("#Commentary").html("بحاجة إلى التحسين العاجل/ ضعيف");
            break;
        case 2:
            $("#Commentary").html("أقل من المتوقع/ مقبول");
            break;
        case 3:
            $("#Commentary").html("حسب المتوقع/ جيد");
            break;
        case 4:
            $("#Commentary").html("فوق المتوقع/ جيد جداً");
            break;
        case 5:
            $("#Commentary").html("متميز/ممتاز");
            break;
        default:
            $("#Commentary").html("");
    }
}


