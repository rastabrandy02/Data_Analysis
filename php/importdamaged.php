<?php

include_once 'settings.php';

$sql = "SELECT posX, Ypos, Zpos FROM Damaged";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "" . $row["Xpos"]. " " . $row["Ypos"]. " " . $row["Zpos"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>