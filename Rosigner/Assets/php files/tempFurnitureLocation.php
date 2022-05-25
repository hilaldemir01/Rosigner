<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}
// Check if the called method is register or not
if($_POST['unity']=="tempFurnitureLocation")
{
    // Getting room information from the user interface
    $FurnitureID=$_POST['FurnitureID'];
    $StartX=$_POST['StartX'];
    $FinishX=$_POST['FinishX'];
    $CenterX=$_POST['CenterX'];
    $StartY=$_POST['StartY'];
    $FinishY=$_POST['FinishY'];
    $CenterY=$_POST['CenterY'];
    $FitnessScore=$_POST['FitnessScore'];
    $CloseWallName=$_POST['CloseWallName'];
    $Degree = $_POST['Degree'];

    $FurnitureIDArray = explode (";", $FurnitureID); 
    $StartXArray = explode (";", $StartX); 
    $FinishXArray = explode (";", $FinishX); 
    $CenterXArray = explode (";", $CenterX); 
    $StartYArray = explode (";", $StartY);     
    $FinishYArray = explode (";", $FinishY); 
    $CenterYArray = explode (";", $CenterY); 
    $FitnessScoreArray = explode (";", $FitnessScore);
    $CloseWallNameArray = explode(";",$CloseWallName);
    $DegreeArray = explode(";",$Degree);

  #  $query = "";
    for ($x = 0; $x < count($FurnitureIDArray)-1; $x++) {
        $query="INSERT INTO furnituregeneticlocation (FurnitureID,StartX,FinishX,CenterX,StartY,FinishY,CenterY,FitnessScore,CloseWallName,Degree) VALUES ('".intval($FurnitureIDArray[$x])."','".intval($StartXArray[$x])."','".intval($FinishXArray[$x])."','".intval($CenterXArray[$x])."','".intval($StartYArray[$x])."','".intval($FinishYArray[$x])."','".intval($CenterYArray[$x])."','".intval($FitnessScoreArray[$x])."','".$CloseWallNameArray[$x]."','".intval($DegreeArray)."');";
        $queryResult = mysqli_query($con,$query);

    }
    echo "Success";
}
?>