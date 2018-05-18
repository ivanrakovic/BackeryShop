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

    $(document).on('click', '.js-edit-pricelist-detail', function (e) {
        e.preventDefault();
        var url = "/PriceListDetails/Edit"; 
        var recId = $(this).attr('data-id');
        $.ajax({
            url: url,
            type: 'GET',
            data: { id: recId },
            success: function (data) {
                var m = $('#pricelistdet');
                m.find('#pricelist-container').html(data);
                m.modal('show');
            }
        });
    });

    $(document).on('click', '.btn-default-edit-pricelist-detail', function (e) {
        e.preventDefault(); 
        var self = $(this);
        var priceListId = $('#PriceListId').val();
        $.ajax({
            url: '/PriceListDetails/Edit',
            type: 'POST',
            data: self.closest('form').serialize(),
            success: function (data) {
                var m = $('#pricelistdet');
                if (data.success == true) {
                    
                    $("a[data-pricelist-details='" + priceListId + "']").click();
                    m.modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                } else {
                    m.find('#pricelist-container').html(data);
                }
            }
        });
    });

    return me;
}(jQuery));