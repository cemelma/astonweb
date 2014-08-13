$(function () {

    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/ekipmanlar/" + lang;
    });

    SortOrder("/Equipment/SortRecords");

});