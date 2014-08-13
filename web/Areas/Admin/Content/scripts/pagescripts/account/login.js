$(function () {
    var status = $("#ProcessMessage").val();

    if (status == "false" || status == "False") {

        $("#notification_div").css("display", "block");
    }
   
});

