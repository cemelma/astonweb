function ReplaceAll(Source, stringToFind, stringToReplace) {
    var temp = Source;
    var index = temp.indexOf(stringToFind);
    while (index != -1) {
        temp = temp.replace(stringToFind, stringToReplace);
        index = temp.indexOf(stringToFind);
    }
    return temp;
}

function wordcounter(event,control) {
    if (event) {
        var wordlen = $(control).val().length;
         var leftword = 200 - wordlen;
         if (leftword < 0) {
            var word = $(control).val();
            $(control).val( word.substring(0, 200)) ; 
         } else {
            $(this).attr('disabled', false);
            $('#wordcounter').text('Kalan Karakter: ' + leftword);
        }
    }
}

function capWords(control,event) {
    var keycode;
    if(window.event) {
        keycode = window.event.keyCode;
    } else if (e) {
        keycode = e.which;
    }
   // alert(keycode);
    if (keycode != 36 && keycode != 37) {
        var words = $(control).val().split(" ");
        // alert(words);
        for (var i = 0 ; i < words.length ; i++) {
            var testwd = words[i];
            var firsLetter = testwd.substr(0, 1);
            if (firsLetter == 'i') firsLetter = 'İ';
            var rest = testwd.substr(1, testwd.length - 1)
            words[i] = firsLetter.toUpperCase() + rest;
            words[i].replace(',', ' ');
            //alert(words[i]);
        }
        $(control).val(words.join(" "));
    }
   
}

function SetPageSlug(word)
{
    word = ReplaceAll(word, " ", '-').toLowerCase();
    word = ReplaceAll(word, '?', '-').toLowerCase();
    word = ReplaceAll(word, '*', '-').toLowerCase();
    word = ReplaceAll(word, '/', '-').toLowerCase();
    word = ReplaceAll(word, "\'", '').toLowerCase();
    word = ReplaceAll(word, '\"', '-').toLowerCase();
    word = ReplaceAll(word, ',', '').toLowerCase();
    word = ReplaceAll(word, '.', '').toLowerCase();
    word = ReplaceAll(word, ':', '').toLowerCase();
    word = ReplaceAll(word, ';', '').toLowerCase();
    word = ReplaceAll(word, '&', '').toLowerCase();
    word = ReplaceAll(word, '%', '').toLowerCase();
    word = ReplaceAll(word, '+', '').toLowerCase();
    word = ReplaceAll(word, '#;', '').toLowerCase();
    word = ReplaceAll(word, '!', '').toLowerCase();
    word = ReplaceAll(word, 'ş', 's').toLowerCase();
    word = ReplaceAll(word, 'ç', 'c').toLowerCase();
    word = ReplaceAll(word, 'ö', 'o').toLowerCase();
    word = ReplaceAll(word, 'ğ', 'g').toLowerCase();
    word = ReplaceAll(word, 'ü', 'u').toLowerCase();
    word = ReplaceAll(word, 'Ü', 'u').toLowerCase();
    word = ReplaceAll(word, 'Ş', 's').toLowerCase();
    word = ReplaceAll(word, 'Ç', 'c').toLowerCase();
    word = ReplaceAll(word, 'Ö', 'o').toLowerCase();
    word = ReplaceAll(word, 'Ğ', 'g').toLowerCase();

    return word;
}
