<?php
  session_start();
  $user = $_SESSION["username"];
  session_unset();
  $_SESSION["username"] = $user;
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
<div class="container card bg-dark mt-3 text-center" style="width: 30rem">
  <?php
    if(isset($_SESSION["username"])) {
      echo 
      "
      <h2 class=\"card-header text-uppercase fw-bold\">Logged in as: <div class=\"text-lowercase fw-normal\">" . $_SESSION["username"] . "</div></h2>
      <div class=\"card card-body bg-secondary my-2 d-flex align-items-center\">
        <div class=\"col\">
          <a href=\"game.php\" class=\"btn btn-outline-info bg-dark btn-lg fw-bold mt-4\">Play Game</a>
        </div>
        <div class=\"col\">
          <a href=\"logout.php\" class=\"btn btn-outline-info bg-dark fw-bold mt-4\">Log Out</a>
        </div>
      </div>
      ";
    } else {
        echo 
        "
        <div class=\"card card-body bg-secondary my-2 d-flex align-items-center\">
          <div class=\"col\">
            <a href=\"login.php\" class=\"btn btn-outline-info btn-lg bg-dark fw-bold mt-4 mb-2\">Log In</a>
          </div>
          <div class=\"text-uppercase fw-bold\">
            or
          </div>
          <div class=\"col\">
            <a href=\"register.php\" class=\"btn btn-outline-info bg-dark btn-lg fw-bold mt-2 mb-2\">Sign Up</a>
          </div>
          <div class=\"text-uppercase fw-bold\">
            to play!
          </div>
        </div>
        ";
    }
  ?>
</div>
</body>
</html>