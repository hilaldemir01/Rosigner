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

    $query2 = "SELECT * FROM room WHERE UserID = '".$userid."';";

    $queryResult2 = $con->query($query2); // table 
    $result = $queryResult2->fetch_object(); // table is turned into an array and it can be used by column name 
    $returnvalue = "$result->RoomID";
    echo $returnvalue;
}
?>