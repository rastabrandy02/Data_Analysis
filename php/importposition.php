<?php

include_once 'settings.php';

$sql = "SELECT XPos, YPos, ZPos FROM Position";
$result = $conn->query($sql);

if ( isset($result->num_rows) && $result->num_rows >0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "" . $row["XPos"]. " " . $row["YPos"]. " " . $row["ZPos"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>