var Backery = Backery || {};

Backery.turnover = (function ($) {

    var me = {};

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

   
    $('#turnover-date-input').datepicker({
        format: "dd/mm/yyyy",
        todayHighlight: true,
        weekStart: 1,
        todayBtn: "linked",
        language: "sr-latin",
        daysOfWeekDisabled: "6,0"
    }).change(dateChanged)
        .on('changeDate', dateChanged);

    function dateChanged() {
        getTurnoverData();
    }

    $('body .js-turnover-filter').click(function (e) {
        getTurnoverData();
    });

    $('body .js-decimal').inputmask(ob);
    $('body .js-decimal').keydown(function (e) {
        var self = $(this);
        var elementName = self[0].id;
        var startPos = self[0].selectionStart;
        switch (e.keyCode) {
            case 37:
                changeHorizontalElement(elementName, startPos, -1);
                break;
            case 38:
                changeVerticalElement(elementName, +1);
                break;
            case 39:
                changeHorizontalElement(elementName, startPos, +1);
                break;
            case 40:
                changeVerticalElement(elementName, -1);
                break;
        }
    });

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
        var turnoverId = $('#TurnoverId').val();

        var data = {
            Date: selDdate.toDateString(),
            ShiftNo: shift,
            BackeryId: bakeryId,
            LastTurnoverId: lastTurnoverId,
            Id: turnoverId
        };

        var rows = [];
        $('tr.js-turnover-detail').each(function (ef) {
            var tr = $(this);
            var o = {};
            tr.find('input').each(function (evn) {
                var inpt = $(this);
                o[inpt[0].id.split('_')[0]] = inpt.val();
            });
            rows.push(o);
        });

        data["TurnoverDetails"] = rows;


        var route = $('#IsEditMode').val() == 'False' ? 'Insert' : 'Update';


        $.ajax({
            url: '/TurnoverData/' + route + 'DataTurnover',
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
        var shiftElement = $("input:radio[name='shiftgrp']:checked");
        var shift = shiftElement.val();
        var bakeryId = $('#BackeryId').val();
        var lastTurnoverId = $('#LastTurnoverId').val();
        var shiftText = shiftElement.parent().find('span').text();
        var formatedDate = selDdate.toLocaleDateString('nb-NO');

        var turnData = {
            date: selDdate.toDateString(),
            shift: shift,
            backeryId: bakeryId,
            lastTurnoverId: lastTurnoverId
        };

        $.ajax({
            url: "/TurnoverData/GetTurnoverDataForDateShift",
            type: 'GET',
            data: turnData,
            success: function (data) {
                var m = $('#turnover-detail-data');
                m.html(data);
                reCaluculateAll();
                caluculateTotal();

                $('.js-decimal').inputmask(ob);

                $('#actionDate').text(formatedDate + ' - ' + shiftText);
                $('#actionType').text($('#IsEditMode').val() == 'False' ? 'Unos' : 'Izmena');
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

    changeHorizontalElement = function (name, inputPos, direction) {

        var selectedEl = $('#' + name);
        var rowPos = selectedEl.data('row-pos');
        if (rowPos + direction == 0 || rowPos + direction > 3) return false;

        var value = $('#' + name).val();
        if (direction == -1 && inputPos > 0) return false;
        if (direction == 1 && inputPos + 1 <= value.length) return false;
        var nextId = rowPos + direction;
        $('#' + name).closest('tr').find("[data-row-pos='" + nextId + "']").focus(); 
        
    };

    changeVerticalElement = function (name, rowPos) {

        var elNameArr = name.split('_');
        var targetId = elNameArr[0] + '_' + (parseInt(elNameArr[1]) - rowPos).toString();
        $('#' + targetId).focus(); 

    };
    return me;
}(jQuery));