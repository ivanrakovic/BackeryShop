var Backery = Backery || {};

Backery.localization = (function ($) {

    var me = {};

    me.msg = {
        required: "Polje mora biti popunjeno.",
        number: "Morate uneti broj.",
        range: "Unesite broj izmedju 10 i 100.",
        minlength: jQuery.format("Morate uneti minimum {0} karaktera"),
        maxlength: jQuery.format("Morate uneti maksimum {0} karaktera"),
    };

    return me;
}(jQuery));