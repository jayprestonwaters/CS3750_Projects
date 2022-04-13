<?php
  session_start();
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Document</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <link rel="stylesheet" href="style.css">
</head>
<body>
<nav class="navbar navbar-dark bg-dark">
  <div class="container-fluid border border-1 border-info rounded-pill pe-0">
    <a href="/" class="navbar-brand flex-grow-1">Number Guessing Game</a>
    <a href="/game.php" class="nav-item fw-bold text-decoration-none px-5 py-2">New Game</a>
  </div>
</nav>
  <?php
    $servername = "sql204.epizy.com";
    $dbusername = "epiz_30809352";
    $dbpassword = "biVu1DogR1p";
    $dbname = "epiz_30809352_1";
    // create connection
    
    $conn = new mysqli($servername, $dbusername, $dbpassword, $dbname);
    
    if ($conn->connect_error) {
      die("<br>Connection failed: " . $conn->connect_error);
    }
    if(!isset($_SESSION["gameid"])){
      // new game
      
      $_SESSION["target"] = rand(1, 100);
      $_SESSION["guess"] = 0;
      $_SESSION["score"] = 1;
      $sql = "INSERT INTO Games (player, target, score) VALUES ('" . $_SESSION["username"] . "', '" . $_SESSION["target"] . "', '" . $_SESSION["score"] . "')";
      $sqlquery = $conn->query($sql);
      echo
      "
      <form action=\"game.php\" method=\"post\">
        <div class=\"container card bg-dark text-center mt-3 pt-2\" style=\"width: 30rem\">
          <div class=\"card-header\">
            <h4 class=\"text-uppercase fw-bold\">Guess the Number</h4>
          </div>
          <div class=\"card card-body bg-secondary border border-2 border-info rounded my-2 py-4 d-flex align-items-center\">
            <div class=\"row mb-3\">
              <div class=\"col form-floating\">
                <label for=\"id_guess\" class=\"pt-0 ps-4\"><small>1 - 100</small></label>
                <input class=\"form-control pt-4 ps-4\" required id=\"id_guess\" type=\"number\" name=\"guess\" min=\"1\" max=\"100\" value=\"50\" placeholder=\"1\">
              </div>
              <div class=\"col\">
                <button class=\"btn btn-lg btn-outline-success bg-dark fw-bold mt-2\" type=\"submit\">Enter</button>
              </div>
            </div>
          </div>
        </div>
      </form>
      ";
      $getgameid = "SELECT MAX(id) AS id FROM Games";
      $getgameidquery = $conn->query($getgameid);
      while ($row = $getgameidquery->fetch_assoc()) {
        $_SESSION["gameid"] = $row["id"];
      }
    } else if($_POST["guess"] != $_SESSION["target"]) {
      // current game

      $_SESSION["guess"] = $_POST["guess"];
      $_SESSION["score"] = $_SESSION["score"] + 1;
      $updatescore = "UPDATE Games SET score = " . $_SESSION["score"] . " WHERE id = " . $_SESSION["gameid"];
      $updatescorequery = $conn->query($updatescore);
      if($_SESSION["target"] > $_SESSION["guess"]) {
        // higher
        
        $_SESSION["prompt"] = "Higher";
      } else if($_SESSION["target"] < $_SESSION["guess"]) {
        // lower
        
        $_SESSION["prompt"] = "Lower";
      }
      echo
      "
      <form action=\"game.php\" method=\"post\">
        <div class=\"container card bg-dark text-center mt-3 pt-2\" style=\"width: 30rem\">
          <div class=\"card-header\">
            <h4 class=\"text-uppercase fw-bold\">Guess the Number</h4>
          </div>
          <div class=\"card card-body bg-secondary border border-2 border-info rounded my-2 py-4 d-flex align-items-center\">
            <div class=\"row mb-3\">
              <div class=\"col form-floating\">
                <label for=\"id_guess\" class=\"pt-0 ps-4\"><small>" . $_SESSION["prompt"] . "</small></label>
                <input class=\"form-control pt-4 ps-4\" required id=\"id_guess\" type=\"number\" name=\"guess\" min=\"1\" max=\"100\" value=" . $_SESSION["guess"] . " placeholder=\"1\">
              </div>
              <div class=\"col\">
                <button class=\"btn btn-lg btn-outline-success bg-dark fw-bold mt-2\" type=\"submit\">Enter</button>
              </div>
            </div>
          </div>
        </div>
      </form>
      ";
//      unset($_POST["guess"]);
//      unset($_SESSION["guess"]);
//      unset($_SESSION["target"]);
    } else if($_POST["guess"] == $_SESSION["target"]) {
        //correct
        
      echo
      "
      <div class=\"container card bg-dark text-center mt-3 w-50\" style=\"min-width: 35rem; max-width: 60rem\">
        <div class=\"card-header\">
          <h2 class=\"text-uppercase fw-bold\">Correct!</h2>
        </div>
        <div class=\"card card-body bg-secondary border border-2 border-info rounded my-2\">
        <div class=\"card-text bg-secondary my-2\"><h5 class=\"text-uppercase fw-bold\">player: <b class=\"text-info\">" . $_SESSION["username"] . "</b><br>--<br>target: <b class=\"text-light\">" . $_SESSION["target"] . "</b> - score: <b class=\"text-warning\">" . $_SESSION["score"] . "</b></h5></div>
        <table class=\"border border-1 border-warning\">
          <tr><th class=\"text-center text-uppercase\" colspan=\"4\">Top 10 High Scores</th></tr>
          <tr>
            <th>#</th>
            <th> Player</th>
            <th> Target</th>
            <th> Guesses</th>
          </tr>
        </div>
      </div>
      ";
      $sqlscores = "SELECT * FROM (SELECT * FROM Games ORDER BY 4 LIMIT 10) t";
      $sqlscoresquery = $conn->query($sqlscores);
      if ($sqlscoresquery->num_rows > 0) {
        $place = 0;
        while ($row = $sqlscoresquery->fetch_assoc()) {
          $place = $place + 1;
          $hsplayer = $row["player"];
          $hstarget = $row["target"];
          $hsscore = $row["score"];
          if($hsplayer == $_SESSION["username"] && $hstarget == $_SESSION["target"] && $hsscore == $_SESSION["score"]){
            echo
            "
            <tr class=\"border border-2 border-success bg-dark\">
              <td>$place</td>
              <td>$hsplayer</td>
              <td>$hstarget</td>
              <td>$hsscore</td>
            </tr>
            ";
          } else {
            echo
            "
            <tr>
              <td>$place</td>
              <td>$hsplayer</td>
              <td>$hstarget</td>
              <td>$hsscore</td>
            </tr>
            ";
          }
        }
        echo
        "
        </table>
        <div class=\"col\">
          <a href=\"game.php\" class=\"btn btn-outline-info btn-lg fw-bold mt-4\">Play Again</a>
        </div>
        ";
      }
      $user = $_SESSION["username"];
      $score = $_SESSION["score"];
      session_unset();
      $_SESSION["username"] = $user;
      $_SESSION["score"] = $score;
    } else {
        echo
        "
        <script>
        window.setTimeout(function () {
            location.href = \"/\";
        }, 1000); // refresh after one second
        </script>
        <div class=\"container card rounded bg-dark text-center mt-3 w-50\" style=\"width: 30rem\">
        <h2 class=\"card-header text-uppercase fw-bold text-light\">Error: Redirecting</h2>
        <div><strong class=\"card-body spinner-border text-info bg-secondary\" role=\"status\"></strong></div>
        </div>
        ";
    }
    $sqldelete = "DELETE FROM Games WHERE `score` = 0 OR `player` = ''";
    $sqldeletequery = $conn->query($sqldelete);
    $conn->close();
  ?>
</body>
</html>