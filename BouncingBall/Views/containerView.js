window.bouncingBall = window.bouncingBall || {};

bouncingBall.boundaryCondition = function()
{
  bouncingBall.maxXCoordinate = window.innerWidth-40;
  bouncingBall.maxYCoordinate = window.innerHeight-45;
   if(bouncingBall.xCooridinate > bouncingBall.maxXCoordinate || 
    bouncingBall.xCooridinate < bouncingBall.minXCoordinate) {
    bouncingBall.addX =- bouncingBall.addX;
   }
    
  if(bouncingBall.yCoordinate > bouncingBall.maxYCoordinate || 
    bouncingBall.yCoordinate < bouncingBall.minYCoordinate) {
    bouncingBall.addY =- bouncingBall.addY;
  }
}