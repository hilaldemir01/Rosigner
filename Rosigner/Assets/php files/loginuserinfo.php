<?php
$conn = new mysqli("localhost","root","","rosigner");

// Check connection
if ($conn -> connect_errno) {
  echo "Failed to connect to database: " . $conn -> connect_error;
}
if($_POST['unity'] == 'loginuserinfo'){
	$email=$_POST['email'];
	$query = "SELECT * FROM registereduser WHERE email = '".$email."';";
	$queryResult = $conn->query($query); // table 
	$result = $queryResult->fetch_object(); // table is turned into an array and it can be used by column name 
	$returnvalue = "$result->FirstName;$result->LastName;$result->Gender";
	echo $returnvalue;
}
?>