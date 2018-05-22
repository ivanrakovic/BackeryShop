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

    $('.js-master').click(function (e) {
        e.preventDefault();
        var self = $(this);
        var route = self.attr('data-route');
        var action = self.attr('data-action');
        var recId = self.attr('data-id');        
        var url = route + "/" + action;

        $.ajax({
            url: url,
            type: 'GET',
            data: { id: recId },
            success: function (data) {
                var m = $('.js-detail-data');
                m.html(data);
            }
        });
        return false;
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
               
               
            },
            error: function () {
                alert("error");
            }
        });
        return false;
    });
    $(document).on('click', '.js-detail', function (e) {
        e.preventDefault();
        var self = $(this);
        var route = self.attr('data-route');
        var action = self.attr('data-action');
        var recId = self.attr('data-id');  
        var url = route + "/" + action; 
        
        $.ajax({
            url: url,
            type: 'GET',
            data: { id: recId },
            success: function (data) {
                var m = $('#' + route);
                m.find('#'+route+'-container').html(data);
                m.modal('show');
            }
        });
    });

    $(document).on('click', '.btn-default-detail', function (e) {
        e.preventDefault(); 
        var self = $(this);
        var action = self.attr('data-action');
        var route = self.attr('data-route');
        var masterId = self.attr('data-master-id');

        var url = route + "/" + action; 
        
        $.ajax({
            url: url,
            type: 'POST',
            data: self.closest('form').serialize(),
            success: function (data) {
                var m = $('#' + route);
                if (data.success === true) {
                    $(".js-master[data-id='" + masterId + "']").click();
                    m.modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                } else {
                    m.find('#' + route+'-container').html(data);
                }
            }
        });
    });

    return me;
}(jQuery));