var Backery = Backery || {};

// Auto-initialization of modules
jQuery(function () {
    for (var key in Backery) {
        if (typeof Backery[key]._initialize == 'function') {
            Backery[key]._initialize.call(Backery[key]);
        }
    }
});