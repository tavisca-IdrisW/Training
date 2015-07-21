"use strict";

window.bouncingBall = window.bouncingBall || {};

window.bouncingBall.containerView = function() {
  var maxXCoordinate = window.innerWidth-40;
  var maxYCoordinate = window.innerHeight-45;
  var minXCoordinate = 15;
  var minYCoordinate = 15;

  return {
    maxXCoordinate: maxXCoordinate,
    maxYCoordinate: maxYCoordinate,
    
    bondaryCheck: function(x, y) {
      var check = "";
      if(x > maxXCoordinate || x < minXCoordinate) {
        check = check + "changeX";
      }
        
      if(y > maxYCoordinate || y < minYCoordinate) {
        check = check + "changeY";
      }
      return check;
    }
  };
};