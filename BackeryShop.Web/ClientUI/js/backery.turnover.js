var Backery = Backery || {};

Backery.turnover = (function ($) {

    var me = {};

    $('#turnover-date-input').datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: true,
        weekStart: 1,
        todayBtn: "linked",
        language: "sr-latin"
    });

    var ob = {
        alias: "decimal",
        integerDigits: 5,
        digits: 2,
        digitsOptional: false,
        allowZero: true,

        placeholder: "0",
        allowMinus: true,
        autoclear: false
    };
    $('.js-decimal').inputmask(ob);

    $('.js-decimal').focusout(function (e) {
        var self = $(this);

        var id = self.closest('tr').attr('data-row-id');
        var prevbal = parseFloat($('#PreviousBalance_' + id).val()) || 0;
        var bakedNew = parseFloat($('#BakedNew_' + id).val()) || 0;
        var scrap = parseFloat($('#Scrap_' + id).val()) || 0;
        var sold = parseFloat($('#Sold_' + id).val()) || 0;
        var price = parseFloat($('#Price_' + id).val()) || 0;
        var newbal = prevbal + bakedNew - sold - scrap;
        var newBalInput = $('#NewBalance_' + id);
        if (newbal < 0) {
            if (!newBalInput.hasClassnewBalInput) {
                newBalInput.addClass('js-decimal-invalid');
            }
        } else
            newBalInput.removeClass('js-decimal-invalid');

        newBalInput.val(newbal).inputmask(ob);

        $('#Total_' + id).val(sold * price).inputmask(ob);
        if (self.val() == '') {
            self.val('0').inputmask(ob);
        }
    });
    

    $('.js-turnover-submit').click(function (e) {
        e.preventDefault();
        var self = $(this);
        var selDdate = $('#turnover-date-input').datepicker('getDate');
        var shift = $("input:radio[name='shiftgrp']:checked").val();
        var bakeryId = $('#BackeryId').val();
        var lastTurnoverId = $('#LastTurnoverId').val();

        var data = {
            Date: selDdate.toDateString(),
            ShiftNo: shift,
            BackeryId: bakeryId,
            LastTurnoverId: lastTurnoverId
        };

        var rows = [];
        $('tr.js-turnover-detail').each(function (e) {
            var tr = $(this);
            var o = {};
            tr.find('input').each(function (evn) {
                var inpt = $(this);
                o[inpt[0].id.split('_')[0]] = inpt.val();
            });
            rows.push(o);
        });

        data["TurnoverDetails"] = rows;
        $.ajax({
            url: "/TurnoverData/InsertTurnover",
            type: 'POST',
            data: JSON.stringify(data),
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