<?php

$XPos = $_POST["XPos"];
$YPos = $_POST["YPos"];
$ZPos = $_POST["ZPos"];


$servername = "localhost:3306";
$username = "sofial";
$password = "bQVHqZ7v9wjQ";
$database = "sofial";

$connection = new mysqli($servername, $username, $password, $database);

if($connection->connect_error)
{
    die("Connection failed: " . $connection->connect_error);
}

$sql = "INSERT INTO `Damaged_Player`(`XPos`, `YPos`, `ZPos`) VALUES ('$XPos','$YPos','$ZPos')";
if ($connection->query($sql) == TRUE) {
    echo $XPos;
    echo $YPos;
    echo $ZPos;
  }
  
  $connection->close();

?>
