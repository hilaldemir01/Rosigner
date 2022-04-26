<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}
// Check if the called method is register or not
if($_POST['unity']=="furnitureNameInsertion")
{
    // to check if the database table is empty
    $isEmptyCheck = "SELECT COUNT(*) FROM furnituretype;";
    $isEmptyCheckResult = mysqli_query($con,$isEmptyCheck);
    if($isEmptyCheckResult){
            // Getting room information from the user interface
        $allFurnitureNames=$_POST['allFurnitureNames'];
        $returnvalue = 0;
        $furnitureArray = explode (";", $allFurnitureNames); 

        foreach($furnitureArray as $value){
            // add the room into the database 
            $query="INSERT INTO furnituretype (FurnitureType) VALUES ('".$value."');";
            $queryResult = mysqli_query($con,$query);
                // Print this message if the insertion is successful
            if($queryResult){
                $returnvalue = 0;

            }else{
                $returnvalue = 1;
            }      
        } 
        if($returnvalue == 0){
            echo "Insertion is successful";
        }else{
            echo "Insertion failed";
        }
    }else{
        echo "Database is full";
    }
}
?>