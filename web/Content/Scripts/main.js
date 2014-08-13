function equalHeight(group) {
    var tallest = 0;
    group.each(function () {
        var thisHeight = $(this).height();
        if (thisHeight > tallest) { tallest = thisHeight; }
    });
    group.height(tallest);
}
function equalWidth(group) {
    var largest = 0;
    group.each(function () {
        var thisWidth = $(this).width();
        if (thisWidth > largest) { largest = thisWidth; }
    });
    group.width(largest);
}
$(document).ready(function () {
    $('header .menu ul.cssdropdown li ul').hover(
        function () { $(this).siblings('a').addClass('selected'); },
        function () { $(this).siblings('a').removeClass('selected'); }
    );
    Cufon.replace('h1,h2,h3,h4,h5', { fontFamily: 'Helvetica Neue LT Pro', hover: { color: '#000000' } });
    Cufon.replace('.news_content h2', { fontFamily: 'MyriadPro-Condensed', hover: { color: '#000000' } });
    $('#ebso_haberler .cycle').cycle({ fx: 'fade', timeout: 5500, pager: '#ebso_haberler .slide-controls', cleartype: true, cleartypeNoBg: true });
    $('#ebso_toolbar .ebso_tool li:last').addClass('last');
    $('.ajanda_panel p:gt(1)').addClass('last');

    /* -- Form Input Elements Actions -- */
    //$('input,textarea')
    //    .focus(function () { $(this).val(''); $(this).addClass('input_active'); })
    //    .blur(function () { $(this).removeClass('input_active'); });
    $('#header_search').blur(function () { if ($(this).val() == '') { $(this).val('EBSO\'da ara'); } });
    $('#login_email').blur(function () { if ($(this).val() == '') { $(this).val('E-posta adresi'); } });
    $("#login_sifre")
        .focus(function () { this.type = 'password'; })
        .blur(function () { if ($(this).val() == '') { $(this).val('Şifre'); this.type = 'text'; } });
    /* -- End -- */
    //setTimeout(function () { equalHeight($(".main_equal")); }, 800);
    //jQuery('.clients-carousel').jcarousel();
});
