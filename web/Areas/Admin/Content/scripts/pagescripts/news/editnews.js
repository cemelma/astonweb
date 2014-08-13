$(function () {
    var status = $("#ProcessMessage").val();

    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

    var spotlen = $("#Spot").val().length;
    var left = 200 - parseInt(spotlen);
  
    $('#wordcounter').text('Kalan Karakter: ' + left);
    
    $("#Spot").keyup(function () {
        var count = 200;
        var wordlen = $("#Spot").val().length;
        var leftword = count - wordlen;
        if (leftword < 0) {
            var word = $("#Spot").val();
            $("#Spot").val(word.substring(0, count));
        } else {
            $(this).attr('disabled', false);
            $('#wordcounter').text('Kalan Karakter: ' + leftword);
        }



    });

    $("#txtdate").datepicker({ dateFormat: 'dd.mm.yy' });
    $("#Header").focus();
});

