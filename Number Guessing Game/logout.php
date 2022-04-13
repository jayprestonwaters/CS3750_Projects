<?php
  session_start();
  
  session_unset();
  
  session_destroy();
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
  <script>
      window.setTimeout(function () {
          location.href = "/";
      }, 500); // refresh after half second
  </script>
  <div class="container card rounded bg-dark text-center mt-3 w-50" style="width: 30rem">
    <h2 class="card-header text-uppercase fw-bold text-light">Logging Out</h2>
    <div><strong class="card-body spinner-border text-info bg-secondary" role="status"></strong></div>
  </div>
</body>
</html>