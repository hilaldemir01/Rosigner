<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == "furnitureLocationInformation"){
	
	$furnitureID=$_POST['furnitureID'];

	$furnitureIDArray = explode(';', $furnitureID);
	$returnvalue ="";
	
  
	for ($x = 0; $x < count($furnitureIDArray); $x++) {
  
		$query = "SELECT * FROM furnituregeneticlocation WHERE FurnitureID ='".$furnitureIDArray[$x]."';";

		$rows = array();

		$queryResult = $conn->query($query);
		if(mysqli_num_rows($queryResult)>0)
		{
				
	    while ($rows = mysqli_fetch_array($queryResult, MYSQLI_ASSOC)) {
		    
		    $string1 = $rows['GeneticLocationID'];
		    $string2 = $rows['FurnitureID'];
				$string3 = $rows['StartX'];
				$string4 = $rows['FinishX'];
				$string5 = $rows['CenterX'];
				$string6 = $rows['StartY'];
				$string7 = $rows['FinishY'];
				$string8 = $rows['CenterY'];
				$returnvalue .= "$string1;$string2;$string3;$string4;$string5;$string6;$string7;$string8;";
	  	}
		}
	
	}	
	

	echo $returnvalue;
}
?>