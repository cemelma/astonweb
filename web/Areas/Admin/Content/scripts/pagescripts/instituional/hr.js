$(function () {
    var status = $("#ProcessMessage").val();

    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

    $("#ddllanguage").change(function () {
        var lang = $("#ddllanguage option:selected").val();
        window.location.href = "/yonetim/insankaynaklari/" + lang;
    });
});


