function cevir(sayi, separator) {
    sayarr = sayi.split(separator);
    var str = "";
    var items = [
        ["", ""],
        ["BIR", "ON"],
        ["IKI", "YIRMI"],
        ["UC", "OTUZ"],
        ["DORT", "KIRK"],
        ["BES", "ELLI"],
        ["ALTI", "ALTMIS"],
        ["YEDI", "YETMIS"],
        ["SEKIZ", "SEKSEN"],
        ["DOKUZ", "DOKSAN"]
    ];


    for (eleman = 0; eleman < sayarr.length; eleman++) {


        for (basamak = 1; basamak <= sayarr[eleman].length; basamak++) {
            basamakd = 1 + (sayarr[eleman].length - basamak);

            try {

                switch (basamakd) {

                    case 6:

                        str = str + " " + items[sayarr[eleman].charAt(basamak - 1)][0] + " YUZ";

                        break;
                    case 5:
                        str = str + " " + items[sayarr[eleman].charAt(basamak - 1)][1];

                        break;
                    case 4:
                        if (items[sayarr[eleman].charAt(basamak - 1)][0] != "BIR")
                            str = str + " " + items[sayarr[eleman].charAt(basamak - 1)][0] + " BIN";
                        else str = str + " BIN";
                        break;
                    case 3:
                        if (items[sayarr[eleman].charAt(basamak - 1)][0] != "BIR") str = str + " " + items[sayarr[eleman].charAt(basamak - 1)][0] + " YUZ";
                        else str = str + " YUZ";

                        break;
                    case 2:
                        str = str + " " + items[sayarr[eleman].charAt(basamak - 1)][1];

                        break;
                    default:

                        str = str + " " + items[sayarr[eleman].charAt(basamak - 1)][0];
                        break;


                }
            } catch (err) {
                alert(err.description);
                alert("eleman" + basamak);
                break;
            }


        }
        if (eleman < 1) str = str + " TL";
        else {
            if (sayarr[1] != "00") str = str + " KRS";

        }
    }
    return str
}