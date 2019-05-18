var Backery = Backery || {};

Backery.validation = $(function () {

    var me = {};

    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form.js-valid").validate({
        // Specify validation rules
        rules: {
            
            Value: {
                required: true,
                number: true,
                range: [10, 100]
            },
            Description: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            ManufacturerId: {
                required: true
            },
            Name: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            DisplayName: {
                required: true,
                minlength: 2,
                maxlength: 50
            }
        },
        // Specify validation error messages
        messages: {
            Value: {
                required: Backery.localization.msg.required,
                number: Backery.localization.msg.number,
                range: Backery.localization.msg.range
            },
            Description: {
                required: Backery.localization.msg.required,
                minlength: Backery.localization.msg.minlength,
                maxlength: Backery.localization.msg.maxlength
            },
            ManufacturerId: {
                required: Backery.localization.msg.required
            },
            Name: {
                required: Backery.localization.msg.required,
                minlength: Backery.localization.msg.minlength,
                maxlength: Backery.localization.msg.maxlength
            },
            DisplayName: {
                required: Backery.localization.msg.required,
                minlength: Backery.localization.msg.minlength,
                maxlength: Backery.localization.msg.maxlength
            }
            
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    return me;
}(jQuery));
