$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/insankaynaklari/pozisyonlar/" + lang;
    });

    SortOrder("/humanresourceposition/SortRecords");
});
