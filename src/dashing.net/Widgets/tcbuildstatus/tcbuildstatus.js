(function() {
  var __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  Dashing.Tcbuildstatus = (function(_super) {
    __extends(Tcbuildstatus, _super);

    Tcbuildstatus.accessor('percentage', Dashing.AnimatedValue);

    function Tcbuildstatus() {
      Tcbuildstatus.__super__.constructor.apply(this, arguments);
      this.observe('percentage', function(value) {
        return $(this.node).find(".meter").val(value).trigger('change');
      });
    }

    Tcbuildstatus.prototype.ready = function() {
      var meter;
      meter = $(this.node).find(".meter");
      meter.attr("data-bgcolor", meter.css("background-color"));
      meter.attr("data-fgcolor", meter.css("color"));
      meter.knob();
      return this.meter = meter;
    };

    Tcbuildstatus.prototype.setStatus = function(element, status) {
      return 1;
    };

    Tcbuildstatus.prototype.onData = function(data) {
      var elem, i, status, _i, _len, _ref;
      if (data.status) {
        $(this.get('node')).attr('class', function(i, c) {
          return c.replace(/\bstatus-\S+/g, '');
        });
        $(this.get('node')).addClass("status-" + data.status);
      }
      if (data.state === 'running') {
        $(this.node).find(".progress").css('visibility', 'visible');
      } else {
        $(this.node).find(".progress").css('visibility', 'hidden');
      }
      _ref = data.historicStatus;
      for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
        status = _ref[i];
        elem = $(this.get('node')).find('.hist' + i);
        elem.attr('class', function(j, c) {
          return c.replace(/\bstatus-\S+/g, '');
        });
        elem.addClass("status-" + status);
      }
      return 0;
    };

    return Tcbuildstatus;

  })(Dashing.Widget);

}).call(this);

//# sourceMappingURL=tcbuildstatus.js.map
