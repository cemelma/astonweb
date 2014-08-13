function DeleteRecord(url, id) {
    $.msgbox("Kaydı Silmek İstediğinize Emin misiniz?", {
        type: "confirm",
        buttons: [
          { type: "submit", value: "Evet" },
          { type: "cancel", value: "Hayır" }
          
        ]
    }, function (result) {
        if (result != false) {
            DeleteProcess(url,id);
        }
    
    });

 
    function DeleteProcess(url, id) {
        var cssbackgrnd = $("#listItem_" + id + " td").css("background-color");
        $("#DeleteImage_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");
        $.ajax({
            type: 'POST',
            url:  url,
            data: "{'id':'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (result) {
                $("#DeleteImage_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/delete.png");
                $('#listItem_' + id).fadeOut();
            },
            error: function () {
                $("#DeleteImage_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/delete.png");
                var odd = $("#listItem_" + id).attr("class");

                if (odd == "odd")
                    $("#listItem_" + id + " td").animate({ backgroundColor: "red" }, 'slow').delay(500).animate({ backgroundColor: "#F2F2F2" }, 'slow');
                else
                    $("#listItem_" + id + " td").animate({ backgroundColor: "red" }, 'slow').delay(500).animate({ backgroundColor: "#fff" }, 'slow');
            }
        });

    }

    
}