$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/projereferanslari/" + lang;
    });

    $("#GroupList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        id = $("#GroupList option:selected").val();
        window.location.href = "/yonetim/projereferanslari/" + lang + "/" + id;
    });

    SortOrder("/ProjectReference/SortRecords");
});
