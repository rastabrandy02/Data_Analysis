<?php
    $servername = "localhost:3306";
    $username = "ogylandyy";
    $password = "H57nt8Z4Kusc";
    $database = "ogylandyy";
    $conn=mysqli_connect($servername,$username,$password,"$database");
      if(!$conn){
          die('Could not Connect MySql Server:' .mysql_error());
        }
      if($conn){
            echo('Success connecting to database! <br><br>');
        }
?>