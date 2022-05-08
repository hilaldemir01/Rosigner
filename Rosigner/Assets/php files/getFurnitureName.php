<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == "getFurnitureType"){
	$furnitureID=$_POST['furnitureID'];
	$query = "SELECT * FROM furnituretype WHERE FurnitureTypeID  ='".$furnitureID."';";
	$queryResult = $conn->query($query);
	$result = $queryResult->fetch_object(); 
	$returnvalue = "$result->FurnitureType";
	echo $returnvalue;
}
?>