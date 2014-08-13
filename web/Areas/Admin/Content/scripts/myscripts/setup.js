$(function () {
    var screenheight = screen.height;
    $("#page").css("min-height", screenheight-200);
});

jQuery(function ($) {
    $.datepicker.regional['tr'] = {
        closeText: 'kapat',
        prevText: '&#x3c;geri',
        nextText: 'ileri&#x3e',
        currentText: 'bugÃ¼n',
        monthNames: ['Ocak', 'Şubat', 'Mart', 'Nisan', 'Mayıs', 'Haziran',
		'Temmuz', 'Ağustos', 'Eylül', 'Ekim', 'Kasım', 'Aralık'],
        monthNamesShort: ['Oca', 'Şub', 'Mar', 'Nis', 'May', 'Haz',
		'Tem', 'Ağu', 'Eyl', 'Eki', 'Kas', 'Ara'],
        dayNames: ['Pazar', 'Pazartesi', 'Salı', 'Çarşamba', 'Perşembe', 'Cuma', 'Cumartesi'],
        dayNamesShort: ['Pz', 'Pt', 'Sa', 'Ça', 'Pe', 'Cu', 'Ct'],
        dayNamesMin: ['Pz', 'Pt', 'Sa', 'Ça', 'Pe', 'Cu', 'Ct'],
        dateFormat: 'dd.mm.yy', firstDay: 1,
        isRTL: false
    };
    $.datepicker.setDefaults($.datepicker.regional['tr']);
});

$(document).ready(function () {
    smallBrowserSize(jQuery(window).width());
    $('.lbox-container').corners("5px bottom");
    $('.lbox h4').corners("5px top");
    $('ul.tab-menu li a').corners("5px top");
    
    $("#calendar").datepicker();/** jquery ui calendar/date picker - see jquery ui docs for help: http://jqueryui.com/demos/ **/
    var start = new Date;

    setInterval(function () {
        var now = new Date();
        //var timetext = now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds();

        var hour = now.getHours();
        var minutes = now.getMinutes();
        var seconds = now.getSeconds();

        var output = (hour < 10 ? '0' : '') + hour + ':' +
                     (minutes < 10 ? '0' : '') + minutes + ':' +
                     (seconds < 10 ? '0' : '') + seconds;

        $('.hour').text(output);

        //$('.date').text(output);

    }, 1000);
});


$(window).resize(function () {
    smallBrowserSize(jQuery(window).width());
});

function smallBrowserSize(e) {
    if (e <= 960) { $("#logo").hide(); } else { $("#logo").show(); }
}




/****************/