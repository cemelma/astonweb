$(function () {
    var id = 0;
    
    //$("#GroupList").change(function () {
    //    var lang = $("#LanguageList option:selected").val();
    //    id = $("#GroupList option:selected").val();
    //    window.location.href = "/yonetim/urunlistesi/" + lang+"/"+id;
    //});
    SortOrderByCategory(id, "/Product/SortRecords");
});

