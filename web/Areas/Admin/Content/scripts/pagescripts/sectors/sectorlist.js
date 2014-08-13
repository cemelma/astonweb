$(function () {
    var id = 0;
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/hizmetler/" + lang;
    });

    $("#GroupList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        id = $("#GroupList option:selected").val();
        window.location.href = "/yonetim/sektorler/" + lang + "/" + id;
    });

    SortOrderByCategory(id, "/Sector/SortRecords");
});
