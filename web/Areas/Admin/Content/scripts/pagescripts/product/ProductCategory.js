$(document).ready(function () {

    var updateOutput = function (e) {
        var list = e.length ? e : $(e.target),
            output = list.data('output');
        //if (window.JSON) {
        //    $('#nestable-output').val(window.JSON.stringify(list.nestable('serialize')));//, null, 2));
        //    //SortOrder();
        //} else {
        //    $('#nestable-output').val('JSON browser support required for this demo.');
        //}
    };
    $('#nestable').nestable({group: 1}).on('change', updateOutput);
    $('.dd').nestable('collapseAll'); //Başlangıçta tüm grupları kapat

    $('#nestable-menu').on('click', function (e) {
        var target = $(e.target),
            action = target.data('action');
        if (action === 'expand-all') {
            $('.dd').nestable('expandAll');
        }
        if (action === 'collapse-all') {
            $('.dd').nestable('collapseAll');
        }
    });

});

function DeleteRecord(url, id) {
    $.msgbox("Kaydı Silmek İstediğinize Emin misiniz?", {
        type: "confirm",
        buttons: [
          { type: "submit", value: "Evet" },
          { type: "cancel", value: "Hayır" }
        ]
    }, function (result) {
        if (result != false) {
            DeleteProcess(url, id);
        }
    });


    function DeleteProcess(url, id) {
        $("#DeleteImage_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");
        $.ajax({
            type: 'POST',
            url: url,
            data: "{'id':'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (result) {
                $("#DeleteImage_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/delete.png");
                $('#DeleteImage_' + id).parent().parent().fadeOut();
            },
            error: function () {
                $("#DeleteImage_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/delete.png");
            }
        });
    }
}

function SetOnlineStatus(url, id) {
    //$('#img_online_' + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");
    $.ajax({
        type: 'POST',
        url: url,
        data: "{id: '" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (result) {
            if (result == true) {
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

function SortOrder() {
    var jsonlist = $("#nestable-output").val();
    $.ajax({
        type: 'POST',
        url: '/ProductGroup/SortRecords',
        data: "{'list':'" + jsonlist + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (result) {
            $('#info').empty();
            $('#info').append('<div class=\"notification success no-margin\"> <span class=\"strong\">Not:</span> Sıralama işlemi yapılmıştır.</div>');
        },
        error: function () {
            $('#info').empty();
            $('#info').append('<div class=\"notification error no-margin\"> <span class=\"strong\">Not:</span> Sıralama işlemi yapılamadı.</div>');
        }
    });
}