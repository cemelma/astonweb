$(function () {
    var status = $("#ProcessMessage").val();
    $("#txtcevaptarihi").datepicker({ dateFormat: 'dd.mm.yy' });
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

});

function EditRecord(id) {

    $("#spantextfiyat_" + id).css("display", "none");
    $("#textfiyat_" + id).css("display", "block");
    $("#textfiyat_" + id).val($("#spantextfiyat_" + id).text());
    $("#moneytype_" + id).css("display", "none");
    $("#moneytype2_" + id).css("display", "none");
    
    $("#spantextdonanim_" + id).css("display", "none");
    $("#textdonanim_" + id).css("display", "block");
    $("#textdonanim_" + id).val($("#spantextdonanim_" + id).text());

    $("#spantextadet_" + id).css("display", "none");
    $("#textadet_" + id).css("display", "block");
    $("#textadet_" + id).val($("#spantextadet_" + id).text());

   
    $("#btn_update_" + id).css("display", "none");
    $("#btn_save_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/save.png");
    $("#btn_save_" + id).css("display", "inline-block");
    $("#btn_cancel_" + id).css("display", "inline-block");
    $("#DeleteImage_" + id).css("display", "none");
    $("#textitem_" + id).focus();
}

function CancelRecord(id) {
    $("#spantextfiyat_" + id).css("display", "inline-block");
    $("#textfiyat_" + id).css("display", "none");
  
    $("#spantextdonanim_" + id).css("display", "inline-block");
    $("#textdonanim_" + id).css("display", "none");
 
    $("#spantextadet_" + id).css("display", "block");
    $("#textadet_" + id).css("display", "none");
  
    $("#btn_update_" + id).css("display", "inline-block");
    $("#btn_save_" + id).css("display", "none");
    $("#btn_cancel_" + id).css("display", "none");
    $("#DeleteImage_" + id).css("display", "inline-block");
    $("#moneytype_" + id).css("display", "inline-block");
    $("#moneytype2_" + id).css("display", "inline-block");
}

function UpdateRecord(id) {
   
    
    var hrd = $("#hdndonanim_" + id).val();
  
    var fiyat = $("#textfiyat_" + id).val();
    if (fiyat == "") {
        $.msgbox("Fiyat Alanına Bir Değer Giriniz!", {
            type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
        });
        return;
    }

    var fiyatclear = ReplaceAll(fiyat, "TL", "");
    var donanimclear = 0;
    if(hrd=="true" || hrd=="True")
    {
        var donanim = $("#textdonanim_" + id).val();
        if (donanim == "") {
            $.msgbox("Donanım Fiyatı Alanına Bir Değer Giriniz!", {
                type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
            });
            return;
        }

        donanimclear = ReplaceAll(donanim, "TL", "");
    }

  
    var adet = $("#textadet_" + id).val();
    if (adet == "") {
        $.msgbox("Adet Alanına Bir Değer Giriniz!", {
            type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
        });
        return;
    }


    var teklifid = $("#teklifid").val();
   
    var cssbackgrnd = $("#listItem_" + id + " td").css("background-color");
    $("#btn_save_" + id).attr("src", "/Areas/Admin/Content/images/icons/16/loader.gif");

    $.ajax({
        type: 'POST',
        url: '/Teklif/UpdateRecord',
        data: '{id:"' + id + '",fiyat:"' + fiyatclear + '",adet:"'+adet+'",donanim:"'+donanimclear+'",teklifid:"'+teklifid+'"}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (msg) {

            $("#spantextfiyat_" + id).css("display", "inline-block");
            $("#textfiyat_" + id).css("display", "none");
            $("#spantextfiyat_" + id).text(fiyat);
            $("#spantextdonanim_" + id).css("display", "block");
            $("#textdonanim_" + id).css("display", "none");
            $("#spantextdonanim_" + id).text(donanimclear);
            $("#spantextadet_" + id).css("display", "block");
            $("#textadet_" + id).css("display", "none");
            $("#spantextadet_" + id).text(adet);


            $("#btn_update_" + id).css("display", "inline-block");
            $("#btn_save_" + id).css("display", "none");
            $("#btn_cancel_" + id).css("display", "none");
            $("#DeleteImage_" + id).css("display", "inline-block");

            var odd = $("#listItem_" + id).attr("class");

            if (odd == "odd")
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#F2F2F2" }, 'slow');
            else
                $("#listItem_" + id + " td").animate({ backgroundColor: "#66ff66" }, 'slow').delay(500).animate({ backgroundColor: "#fff" }, 'slow');


            
            $("#moneytype_" + id).css("display", "inline-block");
            $("#moneytype2_" + id).css("display", "inline-block");
            var topl = parseFloat(fiyatclear) * parseInt(adet) + parseFloat(donanimclear) * parseInt(adet);
            $("#spanitemtoplam_" + id).text(topl+" TL");
            $("#toplamtutar").text(msg[0] +" TL");
            $("#kdvtutar").text(msg[2]+" TL");

        },
        error: function () {
            $.msgbox("İşlem Sırasında Bir Hata Oluştu.", {
                type: "alert", buttons: [{ type: "submit", value: "Tamam" }]
            });

        }



    });

}

