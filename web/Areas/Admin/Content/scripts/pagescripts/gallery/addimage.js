$(function () {
    var status = $("#ProcessMessage").val();
    $("#imgloader").css("display", "none");
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");


    var selval = $("#Language option:selected").val();

    if (selval == "") {
        $("#GalleryGroupId").attr("disabled", "disabled");
        $("#GalleryGroupId").empty().append($("<option></option>").val("").html("Galeri Seçiniz..."));
    }

    $("#Language").change(function () {

        var val = $("#Language option:selected").val();
        if (val == "") { $("#GalleryGroupId").attr("disabled", true); }
        else {
            $("#imgloader").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Gallery/LoadGroup',
                data: '{lang:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#GalleryGroupId").empty().append($("<option></option>").val("").html("Galeri Seçiniz..."));

                    $.each(result, function (i, item) {
                        $("#GalleryGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#GalleryGroupId").removeAttr("disabled");
                    $("#imgloader").css("display", "none");
                },
                error: function () {

                }
            });


        }


    });




});
