<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == "getFurnitureType"){
	$furnitureID=$_POST['furnitureID'];

	$furnitureIDArray = explode(';', $furnitureID);
	$returnvalue ="";
	
	for ($x = 0; $x < count($furnitureIDArray); $x++) {
			$query = "SELECT * FROM furnituretype WHERE FurnitureTypeID  ='".$furnitureIDArray[$x]."';";

			$rows = array();

			$queryResult = $conn->query($query);
			if(mysqli_num_rows($queryResult)>0)
			{
				
		    while ($rows = mysqli_fetch_array($queryResult, MYSQLI_ASSOC)) {
			    
			    $string1 = $rows['FurnitureType'];
				
					$returnvalue .= "$string1;";
		  	}
			}

			//$queryResult = $conn->query($query);
			//$result = $queryResult->fetch_object(); 
			//$string1 = "$result->FurnitureType";
			//$returnvalue .= "$string1;";
	}

	echo $returnvalue;
}
?>