<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == "wallInformation"){
	$roomID=$_POST['roomID'];

	$query = "SELECT * FROM wall WHERE RoomID ='".$roomID."';";
	$queryResult = $conn->query($query);
	$result = $queryResult->fetch_object(); 
	$returnvalue = "$result->WallID;$result->WallName;$result->WallLength;$result->WallHeight;$result->RoomID";
	echo $returnvalue;
}
?>