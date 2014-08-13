$(function () {
    $("#TimeCreated").datepicker({ dateFormat: 'dd.mm.yy' });

  //  $("#TownId").attr('disabled', true);
  //  $("#DistrictId").attr('disabled', true);
 

    $("#CountryId").change(function () {
        var id = $("#CountryId :selected").val();

        if (id == "") {
            $("#TownId").attr('disabled', true);
            $("#DistrictId").attr('disabled', true);

        }
        else{

            $.ajax({
                type: 'POST',
                url: '/Estate/GetTowns',
                data: "{'id':'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {

                    var items = '<option>İlçe Seçiniz</option>';
                    $.each(result, function (i, district) {
                        items += "<option value='" + district.Value + "'>" + district.Text + "</option>";
                    });
                    $('#TownId').html(items);
                    $("#TownId").attr('disabled', false);
                },
                error: function () {
                    alert("error");
                }
            });
        }
    });



    $("#TownId").change(function () {
        var id = $("#TownId :selected").val();
        if (id == "") {
            $("#DistrictId").attr('disabled', true);

        }
        else{
            $.ajax({
                type: 'POST',
                url: '/Estate/GetDistricts',
                data: "{'id':'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {

                    var items = '<option>Semti Seçiniz</option>';
                    $.each(result, function (i, district) {
                        items += "<option value='" + district.Value + "'>" + district.Text + "</option>";
                    });
                    $('#DistrictId').html(items);
                    $("#DistrictId").attr('disabled', false);
                },
                error: function () {
                    alert("error");
                }
            });
        }
        });
    


});