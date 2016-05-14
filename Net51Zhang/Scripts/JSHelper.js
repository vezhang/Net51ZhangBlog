var JSHelper = (function() {
    var pub = {};
    pub.getTicket = function() {
        var current = this;
        var tick = new Date();
        current.ticket = tick;
        return {
            isValid: function() {
                return current.ticket === tick;
            }
        };
    };

    return pub;
})();