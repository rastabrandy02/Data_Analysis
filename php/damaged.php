<?php

include_once 'settings.php';

if(true)
{    
     $Xpos = $_POST['XPos']; 
     $Ypos = $_POST['YPos'];
     $Zpos = $_POST['ZPos'];

     $Damager = $_POST['Damager'];
     $Receiver = $_POST['Receiver'];

     $Amount = $_POST['Amount'];

     $XDmgSource = $_POST['XDmgSource'];
     $YDmgSource = $_POST['YDmgSource'];
     $ZDmgSource = $_POST['ZDmgSource'];

     $sql = "INSERT INTO `Damaged` (`Receiver`,`XPos`,`YPos`,`ZPos`,`Amount`,`Damager`,`XDmgSource`,`YDmgSource`,`ZDmgSource`)
     VALUES ('$Receiver','$Xpos','$Ypos','$Zpos','$Amount','$Damager','$XDmgSource','$YDmgSource','$ZDmgSource')";
     if (mysqli_query($conn, $sql)) {
        echo "New record has been added successfully! ";

        $last_id = $conn->insert_id;
        echo "New Damaged record created successfully.";
     } else {
        echo "Error: " . $sql . ":-" . mysqli_error($conn);
     }
     mysqli_close($conn);
     
}
?>