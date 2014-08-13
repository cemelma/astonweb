$(function () {
    var status = $("#ProcessMessage").val();
    $("#imgloader").css("display", "none");
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");


    var selval = $("#Language option:selected").val();
    
    if (selval == "") {
        $("#ServiceGroupId").attr("disabled", "disabled");
        $("#ServiceGroupId").empty().append($("<option></option>").val("").html("Hizmet Grubu Seçiniz..."));
    }

    $("#Language").change(function () {

        var val = $("#Language option:selected").val();
        if (val == "") { $("#ServiceGroupId").attr("disabled", true); }
        else {
            $("#imgloader").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Service/LoadGroup',
                data: '{lang:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ServiceGroupId").empty().append($("<option></option>").val("").html("Hizmet Grubu Seçiniz..."));

                    $.each(result, function (i, item) {
                        $("#ServiceGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ServiceGroupId").removeAttr("disabled");
                    $("#imgloader").css("display", "none");
                },
                error: function () {

                }
            });


        }
             
      
    });

    
   
   
});
