<?php

include_once 'settings.php';

if(true)
{    
     $Xpos = $_GET['Xpos']; 
     $Ypos = $_GET['Ypos'];
     $Zpos = $_GET['Zpos'];

     $sql = "INSERT INTO enKilled (Xpos,Ypos,Zpos)
     VALUES ('$Xpos' ,'$Ypos','$Zpos')";
     if (mysqli_query($conn, $sql)) {
        echo "New record has been added successfully! ";

        $last_id = $conn->insert_id;
        echo "New enemyKilled record created successfully.";
     } else {
        echo "Error: " . $sql . ":-" . mysqli_error($conn);
     }
     mysqli_close($conn);
}
?>