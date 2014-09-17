define(["jquery", "mustache"], function ($, Mustache) {
    return $.fn.loadTemplate = function (data) {
        var $this = $(this);

        // Gets the template structure
        var templateID = $this.attr('data-template');

        // Compiles the template
        var tepmplateCode = $("#" + templateID);
        var templateHtml = tepmplateCode.html();
        Mustache.parse(templateHtml);

        // Uses "data" to populate $this
        for (var i = 0, len = data.length; i < len; i++) {
            var rendered = Mustache.render(templateHtml, data[i]);
            $this.append(rendered);
        }

        return $this;
    }
});