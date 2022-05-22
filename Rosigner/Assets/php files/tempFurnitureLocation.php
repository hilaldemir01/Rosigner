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
    $XPositionStartX=$_POST['XPositionStartX'];
    $XPositionFinishX=$_POST['XPositionFinishX'];
    $XPositionStartY=$_POST['XPositionStartY'];
    $XPositionFinishY=$_POST['XPositionFinishY'];
    $YPositionStartX=$_POST['YPositionStartX'];
    $YPositionFinishX=$_POST['YPositionFinishX'];
    $YPositionStartY=$_POST['YPositionStartY'];
    $YPositionFinishY=$_POST['YPositionFinishY'];

    $FurnitureIDArray = explode (";", $FurnitureID); 
    $StartXArray = explode (";", $StartX); 
    $FinishXArray = explode (";", $FinishX); 
    $CenterXArray = explode (";", $CenterX); 
    $StartYArray = explode (";", $StartY);     
    $FinishYArray = explode (";", $FinishY); 
    $CenterYArray = explode (";", $CenterY); 
    $FitnessScoreArray = explode (";", $FitnessScore);
    $CloseWallNameArray = explode(";",$CloseWallName);
    $XPositionStartXArray = explode (";", $XPositionStartX); 
    $XPositionFinishXArray = explode (";", $XPositionFinishX); 
    $XPositionStartYArray = explode (";", $XPositionStartY); 
    $XPositionFinishYArray = explode (";", $XPositionFinishY); 
    $YPositionStartXArray = explode (";", $YPositionStartX); 
    $YPositionFinishXArray = explode (";", $YPositionFinishX); 
    $YPositionStartYArray = explode (";", $YPositionStartY); 
    $YPositionFinishYArray = explode (";", $YPositionFinishY); 

  #  $query = "";
    for ($x = 0; $x < count($FurnitureIDArray)-1; $x++) {
        $query="INSERT INTO furnituregeneticlocation (FurnitureID,StartX,FinishX,CenterX,StartY,FinishY,CenterY,FitnessScore,CloseWallName,XPositionStartX,XPositionFinishX,XPositionStartY,XPositionFinishY,YPositionStartX,YPositionFinishX,YPositionStartY,YPositionFinishY) VALUES ('".intval($FurnitureIDArray[$x])."','".intval($StartXArray[$x])."','".intval($FinishXArray[$x])."','".intval($CenterXArray[$x])."','".intval($StartYArray[$x])."','".intval($FinishYArray[$x])."','".intval($CenterYArray[$x])."','".intval($FitnessScoreArray[$x])."','".$CloseWallNameArray[$x]."','".intval($XPositionStartXArray[$x])."','".intval($XPositionFinishXArray[$x])."','".intval($XPositionStartYArray[$x])."','".intval($XPositionFinishYArray[$x])."','".intval($YPositionStartXArray[$x])."','".$YPositionFinishXArray[$x]."','".$YPositionStartYArray[$x]."','".$YPositionFinishYArray[$x]."');";
        $queryResult = mysqli_query($con,$query);

    }
    echo "Success";
}
?>