<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}
// Check if the called method is register or not
if($_POST['unity']=="room")
{
    // Getting room information from the user interface
    $userid=$_POST['userid'];

    // add the room into the database 
    $query="INSERT INTO room (UserID) VALUES ('".$userid."');";
    $queryResult = mysqli_query($con,$query);

    // Print this message if the insertion is successful
    if($queryResult){
        echo "RoomId creation is successful";
    }else{
        echo "Failed to insert";
    }
}
?>