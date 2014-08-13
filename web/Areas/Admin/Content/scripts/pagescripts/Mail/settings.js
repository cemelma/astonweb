$(function () {
    var type = $("#nullsetting").val();
    if (type == "true") {
        $("#txtpassword").addClass("required");
    }
  
    var status = $("#ProcessMessage").val();

    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");


});
