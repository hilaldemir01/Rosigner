<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}

if($_POST['unity']=="roomstructure")
{
    // Getting room information from the user interface
    $StrructureLength=$_POST['StrructureLength'];
    $StrructureWidth=$_POST['StrructureWidth'];
    $RedDotDistance=$_POST['RedDotDistance'];
    $GroundDistance=$_POST['GroundDistance'];
    $StructureName=$_POST['StructureName'];
    $wallName=$_POST['wallName'];
    $roomID=$_POST['roomID'];
    $LocationX = $_POST['LocationX'];
    $LocationY = $_POST['LocationY'];
    $LocationZ = $_POST['LocationZ'];
    $RotationX = $_POST['RotationX'];
    $RotationY = $_POST['RotationY'];
    $RotationZ = $_POST['RotationZ'];

    $FurnitureTypeQuery = "SELECT FurnitureTypeID FROM furnituretype WHERE FurnitureType='".$StructureName."';";   
    $FurnitureTypeResult = $con->query($FurnitureTypeQuery);
    $result = $FurnitureTypeResult->fetch_object(); //result is an array, it can be used by column name
    $FurnitureTypeID = "$result->FurnitureTypeID";


    $WallQuery = "SELECT WallID FROM wall WHERE WallName='".$wallName."' AND RoomID ='".$roomID."';";   
    $WallResult = $con->query($WallQuery);
    $result2 = $WallResult->fetch_object(); //result is an array, it can be used by column name
    $WallID = "$result2->WallID";

// add the room structures into the database 
    $query="INSERT INTO roomstructure (StrructureLength,StrructureWidth,RedDotDistance,GroundDistance,FurnitureTypeID,WallID) VALUES ('".$StrructureLength."','".$StrructureWidth."','".$RedDotDistance."','".$GroundDistance."','".$FurnitureTypeID."','".$WallID."');";
    $queryResult = mysqli_query($con,$query);


    $query2 = "SELECT * FROM roomstructure WHERE RoomStructureID = (SELECT LAST_INSERT_ID());";
    $queryResult2 = $con->query($query2); // table 
    $result2 = $queryResult2->fetch_object(); // table is turned into an array and it can be used by column name 
    $returnvalue = "$result2->RoomStructureID";

    $query3 = "INSERT INTO roomstructurelocation (LocationX,LocationY,LocationZ,RotationX,RotationY,RotationZ,RoomStructureID) VALUES ('".$LocationX."','".$LocationY."','".$LocationZ."','".$RotationX."','".$RotationY."','".$RotationZ."','".$returnvalue."');";

    $queryResult3 = $con->query($query3); // table 

    if($queryResult3 == true){
        echo "Success";
    }else{
        echo "Fail";

    }
}
?>