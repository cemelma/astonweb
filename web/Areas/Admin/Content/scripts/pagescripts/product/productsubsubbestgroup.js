$(document).ready(function () {
    var status = $("#ProcessMessage").val();

    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

    //  var countMember = parseInt($('#ModelDataCount').val());
    // if (countMember > 0) TableSorter("tblcategory", "0,2,4");
    var id = 0;

    //Sorting
    //SortOrder("/ProductSubGroup/SortRecords");
    SortOrderByCategory(id, "/ProductSubSubbestGroup/SortRecords");

    // $("#txtname").focus();
});
