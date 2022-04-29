<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}
// Check if the called method is register or not
if($_POST['unity']=="wall")
{
    // Getting room information from the user interface
    $roomID=$_POST['roomID'];
    $wallName=$_POST['wallName'];
    $wallLength=$_POST['wallLength'];
    $wallHeight=$_POST['wallHeight'];

    // add the room into the database 
    $query="INSERT INTO wall (WallName, WallLength,WallHeight,RoomID) VALUES ('".$wallName."','".$wallLength."','".$wallHeight."','".$roomID."');";
    $queryResult = mysqli_query($con,$query);

	echo "Success";
}
?>