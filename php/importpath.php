<?php

include_once 'settings.php';

$sql = "SELECT Xpos, Ypos, Zpos, pathN FROM Paths";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "" . $row["Xpos"]. " " . $row["Ypos"]. " " . $row["Zpos"]. " " . $row["pathN"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>