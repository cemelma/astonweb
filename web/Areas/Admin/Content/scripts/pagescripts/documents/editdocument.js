$(function () {
    var status = $("#ProcessMessage").val();
    $("#imgloader").css("display", "none");
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");


    var selval = $("#Language option:selected").val();
    
    if (selval == "") {
        $("#DocumentGroupId").attr("disabled", "disabled");
        $("#DocumentGroupId").empty().append($("<option></option>").val("").html("Dökümanı Seçiniz..."));
    }

    $("#Language").change(function () {

        var val = $("#Language option:selected").val();
        if (val == "") { $("#DocumentGroupId").attr("disabled", true); }
        else {
            $("#imgloader").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Documents/LoadGroup',
                data: '{lang:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#DocumentGroupId").empty().append($("<option></option>").val("").html("Dökümanı Seçiniz..."));

                    $.each(result, function (i, item) {
                        $("#DocumentGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#DocumentGroupId").removeAttr("disabled");
                    $("#imgloader").css("display", "none");
                },
                error: function () {

                }
            });


        }
             
      
    });

    
   
   
});
