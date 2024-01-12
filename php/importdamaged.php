<?php

include_once 'settings.php';

$sql = "SELECT Receiver, XPos, YPos, ZPos, Amount, Damager, XDmgSource, YDmgSource, ZDmgSource FROM Damaged";
$result = $conn->query($sql);

if ( isset($result->num_rows) && $result->num_rows >0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "" . $row["Receiver"]. " " . $row["XPos"]. " " . $row["YPos"]. " " . $row["ZPos"]. " " . $row["Amount"]. " " . $row["Damager"]. " " . $row["XDmgSource"]. " " . $row["YDmgSource"]. " " . $row["ZDmgSource"]. "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>