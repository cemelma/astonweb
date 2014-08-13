$(function () {
    var id = 0;
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/hizmetler/" + lang;
    });

    $("#GroupList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        id = $("#GroupList option:selected").val();
        window.location.href = "/yonetim/hizmetler/" + lang + "/" + id;
    });

    SortOrderByCategory(id, "/Service/SortRecords");
});
