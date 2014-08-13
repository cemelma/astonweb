$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/projeler/" + lang;
    });

    SortOrder("/Project/SortRecords");
});
