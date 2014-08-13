function SortOrder(url) {
    //Sorting
   
    var d;
    var listID = new Array();
    //var section = $('#section').val();
    $("#item-list").sortable({
        handle: '.handle',
        update: function () {
            var order = $('#item-list').sortable('serialize');
            $.each(order.split('&'), function (index, value) {
                d = value.split('=');
                listID[index] = d[1];
            });
            var json = JSON.stringify({ list: listID });

            $.ajax({
                type: 'POST',
                url: url,
                data: "{'list':'" + json + "'}",
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
    });

}