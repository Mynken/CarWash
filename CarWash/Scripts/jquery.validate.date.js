$(function () {
    $.validator.methods.date = function (value, element) {
        if ($.browser.webkit) {

            //ES - Chrome does not use the locale when new Date objects instantiated:
            var d = new Date();

            return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
        }
        else {

            return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
        }
    };
});
// custom validation for Chrome, because he thinks that format is mm/dd/yyyy;