<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == "roomStructuresInformation"){
	$wallID=$_POST['wallID'];
	$query = "SELECT * FROM roomstructure WHERE WallID ='".$wallID."';";

	$returnvalue ="";
	$rows = array();

	$queryResult = $conn->query($query);
	if(mysqli_num_rows($queryResult)>0)
	{
	    while ($rows = mysqli_fetch_array($queryResult, MYSQLI_ASSOC)) {
	    	
	        $string1 = $rows['RoomStructureID'];
			$string2 = $rows['StrructureLength'];
			$string3 = $rows['StrructureWidth'];
			$string4 = $rows['RedDotDistance'];
			$string5 = $rows['GroundDistance'];
			$string6 = $rows['FurnitureTypeID'];
			$string7 = $rows['WallID'];
			$returnvalue .= "$string1;$string2;$string3;$string4;$string5;$string6;$string7;";
	    }
	}

	echo $returnvalue;
}
?>