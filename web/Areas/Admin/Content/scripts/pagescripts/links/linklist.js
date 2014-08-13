$(function () {

    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/linkler/" + lang;
    });

    SortOrder("/Link/SortRecords");

});