'use strict';

window.bouncingBall = window.bouncingBall || {};

window.bouncingBall.bBall = function(elementID, x, y) {
  
  var ball = document.getElementById(elementID),
      xCooridinate =  x,
      yCoordinate = y,
      addX = 15,
      addY = 20;

  return {
    animateBall: function() {

      // var containerObj = window.bouncingBall.containerView();
      var changeCoordinate = window.bouncingBall.containerObj.bondaryCheck(xCooridinate, yCoordinate);

      //To check if X-limit(width) has been reached....
      if (changeCoordinate.indexOf('changeX') > -1){
        addX = -addX;
      }

      //To check if Y-limit(height) has been reached....
      if (changeCoordinate.indexOf('changeY') > -1){
        addY = -addY;
      }

      xCooridinate+= addX;
      yCoordinate+= addY;

      ball.style.left = xCooridinate + 'px';
      ball.style.top = yCoordinate + 'px';
    }
  };
};

  /**
  * Adding an event manager. Will manage events.
  */
  var addEvent = function(object, type, callback) {
    if (object === null || typeof(object) == 'undefined') return;
    if (object.addEventListener) {
        object.addEventListener(type, callback, false);
    } else if (object.attachEvent) {
        object.attachEvent('on' + type, callback);
    } else {
        object['on'+type] = callback;
    }
  };

  addEvent(window, 'resize', function(){
    window.bouncingBall.containerObj.resetBoundary();
  });

 var start = function() {
  window.bouncingBall.containerObj = window.bouncingBall.containerView();
  var newBall = window.bouncingBall.bBall('ball',
    Math.random()*(window.bouncingBall.containerObj.maxXCoordinate),
    Math.random()*(window.bouncingBall.containerObj.maxYCoordinate));

  window.setInterval(newBall.animateBall,100);
};

window.onload = start;