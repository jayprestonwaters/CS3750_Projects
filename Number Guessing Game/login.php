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
  <form action="action.php" method="post">
    <div class="container card bg-dark mt-3 text-center" style="width: 30rem">
      <h2 class="card-header text-uppercase fw-bold">Log In</h2>
      <div class="card card-body bg-secondary my-2 d-flex align-items-center">
        <div class="form-floating mb-3">
          <input autocomplete="username" class="form-control" required id="id_username" type="text" name="username" placeholder="Username" style="width: 15rem">
          <label for="id_username">Username</label>
        </div>
        <div class="form-floating">
          <input autocomplete="current-password" class="form-control" required id="id_password" type="password" name="password" placeholder="Password" style="width: 15rem">
          <label for="id_password">Password</label>
        </div>
        <div class="col">
          <button class="btn btn-outline-info btn-lg fw-bold mt-4" type="submit">Log In</button>
        </div>
      </div>
    </div>
    <input type="hidden" name="formtype" value="login"/>
  </form>
</body>
</html>