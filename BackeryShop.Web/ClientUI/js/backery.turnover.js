var Backery = Backery || {};

Backery.turnover = (function ($) {

    var me = {};

    $('#turnover-date-input').datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: true,
        weekStart: 1,
        todayBtn: "linked",
        language: "sr-latin",
    }).change(dateChanged)
     .on('changeDate', dateChanged);

    function dateChanged(ev) {
        getTurnoverData();
    }

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

    $('body .js-turnover-filter').click(function (e) {
        getTurnoverData();
    });

    $('body .js-decimal').inputmask(ob);


    $(document).on('focusout', '.js-decimal', function (e) {
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
        caluculateTotal();
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

    getTurnoverData = function () {
        var selDdate = $('#turnover-date-input').datepicker('getDate');
        var shift = $("input:radio[name='shiftgrp']:checked").val();
        var bakeryId = $('#BackeryId').val();
        var lastTurnoverId = $('#LastTurnoverId').val();

        var data = {
            date: selDdate.toDateString(),
            shift: shift,
            backeryId: bakeryId,
            lastTurnoverId: lastTurnoverId
        };

        $.ajax({
            url: "/TurnoverData/GetTurnoverDataForDateShift",
            type: 'GET',
            data: data,
            success: function(data) {
                var m = $('#turnover-detail-data');
                m.html(data.view);
                $('#LastTurnoverId').val(data.lastTurnoverId);
                reCaluculateAll();
                caluculateTotal();
            }
        });

    };

    reCaluculateAll = function () {
        var inputs = $("input[id*='BakedNew']");
        inputs.each(function (e) {
            $(this).blur();
        });
    };

    caluculateTotal = function () {
        var total = 0;
        var inputs = $("input[id*='Total']");
        inputs.each(function (e) {
            var inpt = $(this);
            total = total + parseFloat(inpt.val()) || 0;
        });
        $('#totalsum').text(total.toFixed(2) + ' RSD');
        return false;
    };
    return me;
}(jQuery));