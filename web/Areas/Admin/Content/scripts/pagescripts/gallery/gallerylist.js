﻿$(function () {
 
    $("#LanguageList").change(function () {
        var lang = $("#LanguageList option:selected").val();
        window.location.href = "/yonetim/galeriresimleri/" + lang;
    });

    SortOrder("/Gallery/SortRecords");
});
