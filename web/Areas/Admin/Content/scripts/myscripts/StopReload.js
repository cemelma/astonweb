//function stopF5Key(evt) {
//    var evt = (evt) ? evt : ((event) ? event : null);
//    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
//    if (evt.keyCode == 116)
//    { return false; }
//}


document.onkeydown = function (e) {

    if (e.keyCode) keycode = e.keyCode
    else keycode = e.which;

    if (keycode == 116) {
        keycode = 505;
    }
    if (keycode == 505) {
        return false;
    }


}