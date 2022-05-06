<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == "roomStructuresInformation"){
	
	$wallID = $_POST['wallID'];

	$query = "SELECT * FROM roomstructure WHERE WallID = '".$wallID."';";
	$queryResult = $conn->query($query);
	$result = $queryResult->fetch_object(); 
	$returnvalue = "$result->RoomStructureID;$result->FurnitureTypeID;$result->StrructureLength;$result->RedDotDistance;$result->GroundDistance;$result->StrructureWidth;$result->WallID";
	echo $returnvalue;
}
?>