﻿var Backery = Backery || {};

Backery.turnover = (function ($) {

    var me = {};

    $('#turnover-date-input').datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: true,
        weekStart: 1,
        todayBtn: "linked",
        language: "sr-latin"
    });

    $('.js-decimal').inputmask({
        alias: "decimal",
        integerDigits: 5,
        digits: 2,
        digitsOptional: false,
        placeholder: "0",
        allowMinus: false
    });

    $('.js-turnover-submit').click(function (e) {
        e.preventDefault();
        var self = $(this);
        var selDdate = $('#turnover-date-input').datepicker('getDate');
        var shift = $("input:radio[name='shiftgrp']:checked").val();
        var bakeryId = $('#BackeryId').val();
        var data = {
            Date: selDdate.toDateString(),
            ShiftNo: shift,
            BackeryId: bakeryId
        }
        var rows = [];
        $('tr.js-turnover-detail').each(function (e) {
            var tr = $(this);
            var o = {};
            tr.find('input').each(function(evn) {
                var inpt = $(this);
                o[inpt[0].id.split('_')[0]] = inpt.val();
            });
            rows.push(o);
        });

        data["TurnoverDetails"] = rows;
        $.ajax({
            url: "/TurnoverData/InsertTurnover",
            type: 'POST',
            data: JSON.stringify(data) ,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response.success)
                    window.location.replace('/');

            },
            error: function () {
                alert("error");
            }
        });
        return false;
    });


    return me;
}(jQuery));