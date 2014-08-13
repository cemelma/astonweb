/// <reference path="MessageBox.js" />
/// <reference path="MessageBox.js" />
// MessageBox
function MessageBox(message,typemsg)
{
    $.msgbox(
        message,
        {
            type: typemsg,
            buttons: [
                { type: "submit", value: "Tamam" }
            ]

        });
 
}