<?php

include_once 'settings.php';

if(true)
{    
     $Xpos = $_POST['XPos']; 
     $Ypos = $_POST['YPos'];
     $Zpos = $_POST['ZPos'];

     $sql = "INSERT INTO Jump (XPos,YPos,ZPos)
     VALUES ('$Xpos' ,'$Ypos','$Zpos')";
     if (mysqli_query($conn, $sql)) {
        echo "New record has been added successfully! ";

        $last_id = $conn->insert_id;
        echo "New Jumped record created successfully.";
     } else {
        echo "Error: " . $sql . ":-" . mysqli_error($conn);
     }
     mysqli_close($conn);
}
?>