$(function () {
    var status = $("#ProcessMessage").val();
    var result = $("#hdsaveResult").val();
    var prId = $("#hdProductId").val();
    
    $("#imgloader").css("display", "none");
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

    $("#tabs").tabs();

    if (result == "False" || result == "false") {
        $("#tabs").tabs({ disabled: [1,2] });
    }
    else {
        $('#tabs').tabs({ selected: 1 });

    }
  
    if (status == "True" || status == "true") {


    }


    $("#btnclear").click(function () {
        $("#txtcode").val("");
        $("#txtmalzeme").val("");
        $("#txtebat").val("");
        $("#txtagirlik").val("");
        $("#txtton").val("");
        $("#txtfiyat").val("");
        $("#txtbirim").val("");
        //$("#txtrenk").css("background-color", "white");
        $("#txtrenk").val();

    });

    $("#btnsaveProp").click(function () {
        var code = $("#txtcode").val();
        var malzeme = $("#txtmalzeme").val();
        var ebat = $("#txtebat").val();
        var agirlik = $("#txtagirlik").val();
        var ton = $("#txtton").val();
        var fiyat = $("#txtfiyat").val();
        var birim = $("#txtbirim").val();
        var renk = $("#txtrenk").val();
        //$("#txtrenk").css("background-color", "white");

        if (code == "" || malzeme == "" || fiyat == "" || ebat=="" || agirlik=="" || ton=="" || birim=="" || renk=="" ) {
            alert("Kayıt eklemek için tüm alanları doldurmalısınız");
            return false;
        }
        $.blockUI({
            css: { backgroundColor: 'transparent',border:'none'},
            message: "<div id='circleG'><div id='circleG_1' class='circleG'></div><div id='circleG_2' class='circleG'></div><div id='circleG_3' class='circleG'></div></div> "
        });
         $.ajax({
            type: 'POST',
            url: '/Product/SaveDetail',
            data: '{code:"' + code + '",malzeme:"' + malzeme + '",birim:"' + birim + '",ebat:"' + ebat + '",agirlik:"' + agirlik + '",ton:"' + ton + '",fiyat:"' + fiyat + '",renk:"' + renk + '",prId:"' + prId + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: 'html',
            success: function (result) {
                $.unblockUI();
                $("#resultTable").html(result);

                //$("#ProductSubGroupId").empty().append($("<option></option>").val("").html("Ürün Alt Grubunu Seçiniz..."));
                //$("#g1").css("display", "block");
                //$.each(result, function (i, item) {
                //    $("#ProductSubGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                //});
                //$("#ProductSubGroupId").removeAttr("disabled");
                //$("#imgloader2").css("display", "none");
            },
            error: function () {
                $.unblockUI();
            }
        });

    });
   

   


    var selval = $("#Language option:selected").val();
    
    if (selval == "") {
        $("#ProductGroupId").attr("disabled", "disabled");
        $("#ProductGroupId").empty().append($("<option></option>").val("").html("Ürün Grubunu Seçiniz..."));
    }

   

    $("#ProductGroupId").change(function () {
        var val = $("#ProductGroupId option:selected").val();
        if (val == "") { $("#ProductSubGroupId").attr("disabled", true); }
        else {
            $("#imgloader2").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadSubGroup',
                data: '{id:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductSubGroupId").empty().append($("<option></option>").val("").html("Ürün Alt Grubunu Seçiniz..."));
                    $("#g1").css("display","block");
                    $.each(result, function (i, item) {
                        $("#ProductSubGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductSubGroupId").removeAttr("disabled");
                    $("#imgloader2").css("display", "none");
                },
                error: function () {

                }
            });
        }
    });

    $("#ProductSubGroupId").change(function () {
        var val = $("#ProductSubGroupId option:selected").val();
        if (val == "") { $("#ProductSubGroupId").attr("disabled", true); }
        else {
            $("#imgloader3").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadSubbestGroup',
                data: '{id:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductSubbestGroupId").empty().append($("<option></option>").val("37").html("Alt Grup Yok..."));
                    $("#g2").css("display", "block");

                    $.each(result, function (i, item) {
                        $("#ProductSubbestGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductSubbestGroupId").removeAttr("disabled");
                    $("#imgloader3").css("display", "none");
                },
                error: function () {

                }
            });
        }
    });

    $("#ProductSubbestGroupId").change(function () {
        var val = $("#ProductSubbestGroupId option:selected").val();
        if (val == "") { $("#ProductSubbestGroupId").attr("disabled", true); }
        else {
            $("#imgloader4").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadSubSubbestGroup',
                data: '{id:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductSubSubbestGroupId").empty().append($("<option></option>").val("5").html("Alt Grup Yok..."));
                    $("#g3").css("display", "block");

                    $.each(result, function (i, item) {
                        $("#ProductSubSubbestGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductSubSubbestGroupId").removeAttr("disabled");
                    $("#imgloader4").css("display", "none");
                },
                error: function () {

                }
            });
        }
    });

    
   
   
});
