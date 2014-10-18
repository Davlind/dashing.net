(function() {
  var __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  Dashing.Number = (function(_super) {
    __extends(Number, _super);

    function Number() {
      return Number.__super__.constructor.apply(this, arguments);
    }

    Number.accessor('current', Dashing.AnimatedValue);

    Number.accessor('difference', function() {
      var current, diff, last;
      if (this.get('last')) {
        last = parseInt(this.get('last'));
        current = parseInt(this.get('current'));
        if (last !== 0) {
          diff = Math.abs(Math.round((current - last) / last * 100));
          return "" + diff + "%";
        }
      } else {
        return "";
      }
    });

    Number.accessor('arrow', function() {
      var arrow_direction;
      if (this.get('last')) {
        arrow_direction = 'right';
        if (parseInt(this.get('current')) > parseInt(this.get('last'))) {
          arrow_direction = 'up';
        } else if (parseInt(this.get('current')) < parseInt(this.get('last'))) {
          arrow_direction = 'down';
        }
        return 'fa fa-arrow-' + arrow_direction;
      }
    });

    Number.prototype.onData = function(data) {
      if (data.status) {
        $(this.get('node')).attr('class', function(i, c) {
          return c.replace(/\bstatus-\S+/g, '');
        });
        return $(this.get('node')).addClass("status-" + data.status);
      }
    };

    return Number;

  })(Dashing.Widget);

}).call(this);

//# sourceMappingURL=number.js.map
