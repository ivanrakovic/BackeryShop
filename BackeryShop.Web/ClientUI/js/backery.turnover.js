var Backery = Backery || {};

Backery.turnover = (function ($) {

    var me = {};

    $('#turnover-date-input').datepicker({
        format: "dd/mm/yyyy",
        weekStart: 1,
        todayBtn: "linked",
        language: "rs-latin"
    });


    return me;
}(jQuery));