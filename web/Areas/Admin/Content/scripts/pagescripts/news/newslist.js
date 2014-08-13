$(function () {

    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/haberler/" + lang;
    });

    SortOrder("/News/SortRecords");

});
