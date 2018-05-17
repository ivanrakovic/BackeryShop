var Backery = Backery || {};

Backery.turnover = (function ($) {

    var me = {};

    $('#turnover-date-input').datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: true,
        weekStart: 1,
        todayBtn: "linked",
        language: "rs-latin"
    });

    $('.js-decimal').inputmask({
        alias: "decimal",
        integerDigits: 5,
        digits: 2,
        digitsOptional: false,
        placeholder: "0",
        allowMinus: false
    });

    $('.js-pricelist-details').click(function () {
        $('#pricelistdetail').load(this.href);
        return false;
    });

    return me;
}(jQuery));