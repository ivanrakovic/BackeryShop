var Backery = Backery || {};

Backery.turnover = (function($) {

    var me = {};

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
                m.find('#' + route + '-container').html(data);
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
                    m.find('#' + route + '-container').html(data);
                }
            }
        });
    });
    return me;
}(jQuery));