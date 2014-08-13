$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/cozumortaklari/" + lang;
    });

    SortOrder("/SolutionPartner/SortRecords");
});
