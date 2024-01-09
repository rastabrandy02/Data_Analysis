<?php

include_once 'settings.php';

if(true)
{    
   $Xpos = $_GET['Xpos']; 
   $Ypos = $_GET['Ypos'];
   $Zpos = $_GET['Zpos'];

   $Damager=$_GET['Damager'];

   $Amount=$_GET['Amount'];

   $XDmgSource=$_GET['XDmgSource'];
   $YDmgSource=$_GET['YDmgSource'];
   $ZDmgSource=$_GET['ZDmgSource'];

   $sql = "INSERT INTO Death (Xpos,Ypos,Zpos,Damager,Amount,XDmgSource,YDmgSource,ZDmgSource,)
   VALUES ('$Xpos','$Ypos','$Zpos','$Damager','$Amount','$XDmgSource','$YDmgSource','$ZDmgSource')";
     if (mysqli_query($conn, $sql)) {
        echo "New record has been added successfully! ";

        $last_id = $conn->insert_id;
        echo "New Death record created successfully.";
     } else {
        echo "Error: " . $sql . ":-" . mysqli_error($conn);
     }
     mysqli_close($conn);
}
?>