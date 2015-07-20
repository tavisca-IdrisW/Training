window.bouncingBall = window.bouncingBall || {
   minXCoordinate: 15,
   maxXCoordinate: window.innerWidth-40,
   minYCoordinate: 15,
   maxYCoordinate: window.innerHeight-45,
   addX: 20,
   addY: 15
};



 bouncingBall.start = function() {
  bouncingBall.ball = document.getElementById("ball");
  bouncingBall.ball.style.top = Math.random()*(bouncingBall.maxYCoordinate)+'px'; // Starting Position 
  bouncingBall.ball.style.left = Math.random()*(bouncingBall.maxXCoordinate)+'px'; // Starting Position 

  window.setInterval(bouncingBall.animateBall,100);
}

  bouncingBall.animateBall =function() {

    bouncingBall.xCooridinate = parseInt(ball.style.left);
    bouncingBall.yCoordinate = parseInt(ball.style.top); 
    bouncingBall.xCooridinate+= bouncingBall.addX;
    bouncingBall.yCoordinate+= bouncingBall.addY;
    bouncingBall.boundaryCondition();
    bouncingBall.ball.style.left =bouncingBall.xCooridinate + "px";
    bouncingBall.ball.style.top = bouncingBall.yCoordinate + "px";
}

window.onload = bouncingBall.start;