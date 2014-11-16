$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/katalogresimleri/" + lang;
    });

    SortOrder("/Catalog/SortRecords");
});
