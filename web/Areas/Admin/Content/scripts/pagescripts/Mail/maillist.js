$(function () {
    var type = $("#selval").val();
    if (type != "") {
        $("#ddltype").val(type);
    }
    //    $("#ddltype").val(type);

    $("#ddltype").change(function () {
        var lang = $("#ddltype option:selected").val();
        $("#selval").val(lang);
   
        window.location.href = "/yonetim/mailkullanicilari/" + lang;
    });

});
