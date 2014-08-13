$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/sektorgruplari/" + lang;
    });

    SortOrder("/SectorGroup/SortRecords");
});
