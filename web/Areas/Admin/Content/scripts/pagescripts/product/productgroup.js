$(document).ready(function () {
    var status = $("#ProcessMessage").val();

    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

    $("#drplanguage").change(function () {
        var lang = $("#drplanguage option:selected").val();
        window.location.href = "/yonetim/urungruplari/" + lang;
    });

    //  var countMember = parseInt($('#ModelDataCount').val());
    // if (countMember > 0) TableSorter("tblcategory", "0,2,4");

    //Sorting
    SortOrder("/ProductGroup/SortRecords");
    // $("#txtname").focus();
});

function EditRecord(id) {

    $("#spanitem_" + id).css("display", "none");
    $("#textitem_" + id).css("display", "block");
    $("#textitem_" + id).val($("#spanitem_" + id).text());
    $("#btn_update_" + id).css("display", "none");
    $("#btn_save_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/save.png");
    $("#btn_save_" + id).css("display", "inline-block");
    $("#btn_cancel_" + id).css("display", "inline-block");
    $("#DeleteImage_" + id).css("display", "none");
    $("#textitem_" + id).focus();
}

function CancelRecord(id) {
    $("#spanitem_" + id).css("display", "block");
    $("#textitem_" + id).css("display", "none");
    //$("#textitem_" + id).val($("#spanitem_" + id).text());
    $("#btn_update_" + id).css("display", "inline-block");
    $("#btn_save_" + id).css("display", "none");
    $("#btn_cancel_" + id).css("display", "none");
    $("#DeleteImage_" + id).css("display", "inline-block");

}

function UpdateRecord(id) {
    var name = $("#textitem_" + id).val();
    if (name == "") {
        $.msgbox("Alana Bir Değer Giriniz!", {
            type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
        });
        return;
    }
    var clearname = ReplaceAll(name, "\'", "%47");
    var cssbackgrnd = $("#listItem_" + id + " td").css("background-color");
    $("#btn_save_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");

    $.ajax({
        type: 'POST',
        url: '/ProductGroup/UpdateRecord',
        data: '{id:"' + id + '",name:"' + name + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function () {

            $("#btn_save_" + id).css("display", "none");
            $("#btn_update_" + id).css("display", "inline-block");
            $("#btn_cancel_" + id).css("display", "none");
            $("#DeleteImage_" + id).css("display", "inline-block");

            $("#spanitem_" + id).css("display", "block");
            $("#spanitem_" + id).text($("#textitem_" + id).val());
            $("#textitem_" + id).css("display", "none");

            var odd = $("#listItem_" + id).attr("class");

            if (odd == "odd")
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#F2F2F2" }, 'slow');
            else
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#fff" }, 'slow');

        },
        error: function () {
            $.msgbox("İşlem Sırasında Bir Hata Oluştu.", {
                type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
            });

        }



    });

}

