<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == "roomStructuresLocationFetch"){
	$roomStructureID=$_POST['roomStructureID'];

	$query = "SELECT * FROM roomstructurelocation WHERE RoomStructureID ='".$roomStructureID."';";

    $queryResult = $conn->query($query); // table 
    $result = $queryResult->fetch_object(); // table is turned into an array and it can be used by column name 
    $returnvalue = "$result->LocationX;$result->LocationY;$result->LocationZ;$result->RotationX;$result->RotationY;$result->RotationZ;$result->RoomStructureID;$result->RoomStructureLocationID";

    echo $returnvalue;
}
?>