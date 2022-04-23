<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}
// Check if the called method is register or not
if($_POST['unity']=="register")
{
    // Getting user information from the user interface
    $firstname=$_POST['firstname'];
    $lastname=$_POST['lastname'];
    $gender=$_POST['gender'];
    $email=$_POST['email'];
    $password=$_POST['password'];
    // sha-256 is used to cryot the passwords
    $hash = hash('sha256',$password);

    // Check if there exists an email address in the database or not
    $emailcheckQuery="SELECT email FROM registereduser WHERE email = '".$email."';";
    $emailcheckQueryResult = mysqli_query($con,$emailcheckQuery) or die("2: Name check query failed");
    if(mysqli_num_rows($emailcheckQueryResult) == 1){
        echo "This email address is already registered.";
        exit();
    }

    // add the user into the database 
    $query="INSERT INTO registereduser (firstname, lastname, gender,email,hash) VALUES ('".$firstname."','".$lastname."','".$gender."','".$email."','".$hash."');";
    $queryResult = mysqli_query($con,$query);

    // Print this message if the register is successful
    if($queryResult){
        echo "Registration is successful";
    }else{
        echo "Failed to register";
    }
}
?>