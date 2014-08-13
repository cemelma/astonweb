﻿$(function () {
    var status = $("#ProcessMessage").val();
    $("#imgloader").css("display", "none");
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");


    var selval = $("#Language option:selected").val();
    
    if (selval == "") {
        $("#SectorGroupId").attr("disabled", "disabled");
        $("#SectorGroupId").empty().append($("<option></option>").val("").html("Sektör Grubu Seçiniz..."));
    }

    $("#Language").change(function () {

        var val = $("#Language option:selected").val();
        if (val == "") { $("#SectorGroupId").attr("disabled", true); }
        else {
            $("#imgloader").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Sector/LoadGroup',
                data: '{lang:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#SectorGroupId").empty().append($("<option></option>").val("").html("Sektör Grubu Seçiniz..."));

                    $.each(result, function (i, item) {
                        $("#SectorGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#SectorGroupId").removeAttr("disabled");
                    $("#imgloader").css("display", "none");
                },
                error: function () {

                }
            });


        }
             
      
    });

    
   
   
});
