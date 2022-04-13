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
    if($_POST["username"]){
      $username = $_POST["username"];
    } else {
      $username = $_SESSION["username"];
    }
    $_SESSION["username"] = $username;
    $password = $_POST["password"];
    $form = $_POST["formtype"];
    $servername = "sql204.epizy.com";
    $dbusername = "epiz_30809352";
    $dbpassword = "biVu1DogR1p";
    $dbname = "epiz_30809352_1";
    // Create connection
    
    $conn = new mysqli($servername, $dbusername, $dbpassword, $dbname);
    if ($conn->connect_error) {
      die("<br>Connection failed: " . $conn->connect_error);
    }
    
    if($form == "login"){
      //user login
      
      $GLOBALS['form'] = "none";
      $getsalt = "SELECT salt FROM Users WHERE username = '" . $username . "'";
      $saltquery = $conn->query($getsalt);
      if ($saltquery->num_rows > 0) {
        while ($row = $saltquery->fetch_assoc()) {
          $salt = $row["salt"];
        }
      }
      $gethash = "SELECT password FROM Users WHERE username = '" . $username . "'";
      $hashquery = $conn->query($gethash);
      if ($hashquery->num_rows > 0) {
        while ($row = $hashquery->fetch_assoc()) {
          $hash = $row["password"];
        }
      }
      $salted = $password . $salt;
      if(password_verify($salted, $hash)){
        
        $_SESSION["username"] = $username;
        $_SESSION["message"] = "<h2 class=\"text-center text-uppercase fw-bold\">Login successful!</h2><br>";
        
      } else {
          session_unset();
          session_destroy();
          $_SESSION["message"] = "<h2 class=\"text-center text-uppercase fw-bold\">Login failed.</h2><br>";

      }
    } else if($form == "register") {
        // user registration
        
        $GLOBALS['form'] = "none";
        $_SESSION["username"] = $username;
        $salt = uniqid();
        $hashed = password_hash($password . $salt, PASSWORD_DEFAULT);
        $sql = "INSERT INTO Users (username, salt, password) VALUES ('" . $username . "', '" . $salt . "', '" . $hashed . "')";
        if($conn->query($sql) === TRUE) {
          $_SESSION["message"] = "<h2 class=\"text-center text-uppercase fw-bold\">Registration successful!</h2><br>";
        } else {
            echo "Error: " . $sql . "<br><br>" . $conn->error;
        }
    } else {
        echo 
        "
        <h2 class=\"text-center\">Logged in as: " . $_SESSION["username"] . "</h2><br>
        ";
    }
    $conn->close();
  ?>
  <script>
      window.setTimeout(function () {
          location.href = "/";
      }, 1000); // refresh after one second
  </script>
  <div class="container card rounded bg-dark text-center mt-3 w-50" style="width: 30rem">
    <h2 class="card-header text-uppercase fw-bold text-light"> <?php echo $_SESSION["message"]; ?> </h2>
    <div><strong class="card-body spinner-border text-info bg-secondary" role="status"></strong></div>
  </div>
</body>
</html>