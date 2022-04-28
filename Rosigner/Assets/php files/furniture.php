<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}
// Check if the called method is register or not
if($_POST['unity']=="furniture")
{
    // Getting user information from the user interface
    $YDimension = $_POST['height'];
    $XDimension = $_POST['width'];
    $ZDimension = $_POST['length'];
    $furnitureName = $_POST['furnitureName'];

    
    $searchQuery = "SELECT FurnitureTypeID FROM furnituretype WHERE FurnitureType='".$furnitureName."';";   
    $queryResult = $con->query($searchQuery);
    $result = $queryResult->fetch_object(); //result is an array, it can be used by column name
    $gettingFurnitureTypeID = "$result->FurnitureTypeID";

    $query="INSERT INTO furniture (YDimension, XDimension, ZDimension,FurnitureTypeID) VALUES ('".$YDimension."','".$XDimension."','".$ZDimension."','".$gettingFurnitureTypeID."');";
    $queryResult = mysqli_query($con,$query);

    // Print this message if the measures saved successfully
    if($queryResult){
        echo "Measures saved successfully";
    }else{
        echo "Failed to save";
    }
}
?>