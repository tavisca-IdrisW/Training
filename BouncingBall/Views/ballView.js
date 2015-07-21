"use strict";

window.bouncingBall = window.bouncingBall || {};

window.bouncingBall.bBall = function(elementID, x, y) {
  
  var ball = document.getElementById(elementID),
      xCooridinate =  x,
      yCoordinate = y,
      addX = 15,
      addY = 20;
  
  return {
    ball: ball,
    xCooridinate: xCooridinate,
    yCoordinate: yCoordinate,
    animateBall: function() {

      var containerObj = window.bouncingBall.containerView();
      var changeCoordinate = containerObj.bondaryCheck(xCooridinate, yCoordinate);

      //To check if X-limit(width) has been reached....
      if (changeCoordinate.indexOf("changeX") > -1){
        addX = -addX;
      }

      //To check if Y-limit(height) has been reached....
      if (changeCoordinate.indexOf("changeY") > -1){
        addY = -addY;
      }

      xCooridinate+= addX;
      yCoordinate+= addY;

      ball.style.left = xCooridinate + "px";
      ball.style.top = yCoordinate + "px";
    }
  };
};

 var start = function() {
  var initialContainer = window.bouncingBall.containerView();
  var newBall = window.bouncingBall.bBall("ball", 
    Math.random()*(initialContainer.maxXCoordinate),
    Math.random()*(initialContainer.maxYCoordinate));
  
  window.setInterval(newBall.animateBall,100);
};

window.onload = start;