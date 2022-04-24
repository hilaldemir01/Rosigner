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
    $wall1length=$_POST['wall1length'];
    $wall2length=$_POST['wall2length'];
    $wallheight=$_POST['wallheight'];

    // add the room into the database 
    $query="INSERT INTO room (wall1length, wall2length, wallheight) VALUES ('".$wall1length."','".$wall2length."','".$wallheight."');";
    $queryResult = mysqli_query($con,$query);

    // Print this message if the insertion is successful
    if($queryResult){
        echo "Room measurement is successful inserted";
    }else{
        echo "Failed to insert";
    }
}
?>