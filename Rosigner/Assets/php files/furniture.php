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
    $roomID=$_POST['roomID'];
    
    //furnitureıd döndürcez
    //www de çekcez
    

    $query2 = "SELECT * FROM room WHERE RoomID = '".$roomID."';";
    $queryResult2 = $con->query($query2); // table 
    $result2 = $queryResult2->fetch_object(); // table is turned into an array and it can be used by column name 
    $returnvalue = "$result2->RoomID";


    $searchQuery = "SELECT FurnitureTypeID FROM furnituretype WHERE FurnitureType='".$furnitureName."';";   
    $queryResult = $con->query($searchQuery);
    $result = $queryResult->fetch_object(); //result is an array, it can be used by column name
    $gettingFurnitureTypeID = "$result->FurnitureTypeID";

    $query="INSERT INTO furniture (YDimension, XDimension, ZDimension,FurnitureTypeID, RoomID) VALUES ('".$YDimension."','".$XDimension."','".$ZDimension."','".$gettingFurnitureTypeID."','".$returnvalue."');";
    $queryResult = mysqli_query($con,$query);

    $idQuery="SELECT FurnitureID FROM furniture WHERE RoomID = '".$roomID."';";
    $idQueryResult = $con->query($idQuery); // table 
    $result3 = $idQueryResult->fetch_object(); // table is turned into an array and it can be used by column name 
    $returnid = "$result3->FurnitureID";


    // Print this message if the measures saved successfully
    if($queryResult){
        echo  $returnid;
    }else{
        echo "Failed to save";
    }
}
?> 