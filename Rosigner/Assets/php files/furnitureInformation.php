<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == 'furnitureInformation'){
	$furnitureID=$_POST['FurnitureID'];
	$roomID=$_POST['RoomID'];
	//$query = "SELECT * FROM furniture WHERE FurnitureID = '".$furnitureID."';";
	$queryFurniture = "SELECT * FROM furniture WHERE RoomID = '".$roomID."';";
 
	$returnvalue ="";
	$rows = array();

	$queryResult = $conn->query($queryFurniture);
	if(mysqli_num_rows($queryResult)>0)
	{
	    while ($rows = mysqli_fetch_array($queryResult, MYSQLI_ASSOC)) {
	    	
		    $string1 = $rows['FurnitureID'];
				$string2 = $rows['XDimension'];
				$string3 = $rows['YDimension'];
				$string4 = $rows['ZDimension'];
				$string5 = $rows['FurnitureTypeID'];
				$string6 = $rows['RoomID'];
				$returnvalue .= "$string1;$string2;$string3;$string4;$string5;$string6;";
	    }
	}
	echo $returnvalue;
	
}
?>