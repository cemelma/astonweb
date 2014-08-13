$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/banner/" + lang;
    });

    SortOrder("/Banner/SortRecords");
});
