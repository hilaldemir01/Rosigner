<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == 'furnitureInformation'){
	$furnitureID=$_POST['furnitureID'];
	$query = "SELECT * FROM furniture WHERE FurnitureID = '".$furnitureID."';";
	$queryResult = $conn->query($query);
	$result = $queryResult->fetch_object(); 
	$returnvalue = "$result->FurnitureID;$result->XDimension;$result->YDimension;$result->ZDimension";
	echo $returnvalue;
}
?>