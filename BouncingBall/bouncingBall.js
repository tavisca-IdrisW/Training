
var ball,
   xCooridinate,
   yCoordinate,
   minXCoordinate = 15,
   maxXCoordinate = window.innerWidth-30,
   minYCoordinate = 15,
   maxYCoordinate = window.innerHeight-30,
   addX = 20,
   addY = 15;

function start()
{
  ball = document.getElementById("ball");
  ball.style.position = 'absolute';
  ball.style.top = Math.random()*(maxYCoordinate)+'px'; // Starting Position 
  ball.style.left = Math.random()*(maxXCoordinate)+'px'; // Starting Position 
  ball.style.width ='30px';
  ball.style.height ='30px';
  ball.style.borderRadius = '100%';
  ball.style.backgroundColor = '#bb0000';
  animateBall();
  window.setInterval(animateBall,100);

}

function animateBall()
{
    maxXCoordinate = window.innerWidth-30;
    maxYCoordinate = window.innerHeight-30;
    xCooridinate = parseInt(ball.style.left);
    yCoordinate = parseInt(ball.style.top); 
    xCooridinate+=addX;
    yCoordinate+=addY;
    boundaryCondition();
    ball.style.left = xCooridinate + "px";
    ball.style.top = yCoordinate + "px";
}

function boundaryCondition()
{
     
   if(xCooridinate > maxXCoordinate || xCooridinate < minXCoordinate){
    addX =- addX;
   }
        
    if(yCoordinate > maxYCoordinate || yCoordinate < minYCoordinate){
      addY=-addY;
    }
        
}

window.onload=start;