$(function () {
    $("#dllposition").change(function () {
        var positionId = $("#dllposition option:selected").val();
        if (positionId != 0)
            window.location.href = "/yonetim/insankaynaklari/cvListesi/" + positionId;
        else window.location.href = "/yonetim/insankaynaklari/cvListesi/";
    });
});
