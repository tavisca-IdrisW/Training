
  var block = [], 
      empty = "\xA0",
      score,
      moves,
      turn = "X",
      oldOnload,

  /*
   * Winable sums... in 2^cell# form.
   *     273                 84
   *        \               /
   *          1 |   2 |   4  = 7
   *       -----+-----+-----
   *          8 |  16 |  32  = 56
   *       -----+-----+-----
   *         64 | 128 | 256  = 448
   *       =================
   *         73   146   292
   *
   */

  wins = [7, 56, 448, 73, 146, 292, 273, 84];

  startNewGame = function () {
   var i;
    
    turn = "X";
    score = {"X": 0, "O": 0};
    moves = 0;

    for (i = 0; i < block.length; i += 1) {
      block[i].firstChild.nodeValue = empty;
    }
  },

  /*
   * Returns whether the given score is a winning score.
   */
  win = function (score) {
    var i;
    for (i = 0; i < wins.length; i += 1) {
      if ((wins[i] & score) === wins[i]) {
        return true;
      }
    }
    return false;
  },

  set = function () {

    if (this.firstChild.nodeValue !== empty) {
      return;
    }

    
    this.firstChild.nodeValue = turn;
    moves++;
    
    score[turn] += this.indicator;
    
    if (win(score[turn])) {
        alert(turn + " wins!");
        startNewGame();
    } else if (moves === 9) {
        alert("Draw!!");
        startNewGame();
    } else {
        turn = turn === "X" ? "O" : "X";
    }
  },

  /*
   * Can be done with CSS also but it seems better
   * for a smaller app.
   */
  play = function () {
    var row, 
        cell,
        parent;
        indicator = 1,
        board = document.createElement("table"),
        board.border = 1;

      for (var i = 1; i <= 3; i += 1) {  

        var row = document.createElement("tr");
        
        board.appendChild(row);
       
        for (var j = 1; j <= 3; j += 1) {

          var cell = document.createElement("td");
          
          cell.width = cell.height = 25;
          cell.align = cell.valign = 'center';
          cell.indicator = indicator;
          cell.onclick = this.set;
          cell.appendChild(document.createTextNode(""));
          row.appendChild(cell);
          block.push(cell);
          indicator += indicator;
        }
      }

      parent = document.getElementById("tictactoe") || document.body;
      parent.appendChild(board);
      startNewGame();
  };


  if (typeof window.onload === "function") {
      oldOnLoad = window.onload;
      window.onload = function () {
      oldOnLoad(); 
      play();
    };
  } else {
      window.onload = play;
  }
