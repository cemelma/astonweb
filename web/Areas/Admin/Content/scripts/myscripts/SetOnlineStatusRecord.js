function SetOnlineStatus(url, id) {
    $('#img_online_' + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");
    $.ajax({
        type: 'POST',
        url: url,
        data: "{id: '" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (result) {
            if (result == true ) {
                    $('#img_online_' + id).attr("src", "/Areas/Admin/Content/images/icons/online.png");
                $('#img_online_' + id).attr("title", "Offline Yap");
            }
            else {
                    $('#img_online_' + id).attr("src", "/Areas/Admin/Content/images/icons/offline.png");
                $('#img_online_' + id).attr("title", "Online Yap");
            }
        },
        error: function (result) {
            $('#img_online_' + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");
        }
    });
}