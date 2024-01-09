<?php
    $servername = "localhost:3306";
    $username = "sofial";
    $password = "bQVHqZ7v9wjQ";
    $database = "sofial";
    $conn=mysqli_connect($servername,$username,$password,"$database");
      if(!$conn){
          die('Could not Connect MySql Server:' .mysql_error());
        }
      if($conn){
            echo('Success connecting to database! <br><br>');
        }
?>