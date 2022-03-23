<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}

if($_POST['unity']=="register")
{
    $firstname=$_POST['firstname'];
    $lastname=$_POST['lastname'];
    $gender=$_POST['gender'];
    $email=$_POST['email'];
    $password=$_POST['password'];
    $password1=$_POST['password1'];
    $hash = hash('sha256',$password);
    $emailcheckQuery="SELECT email FROM users WHERE email = '".$email."';";
    $emailcheckQueryResult = mysqli_query($con,$emailcheckQuery) or die("2: Name check query failed");
    if(mysqli_num_rows($emailcheckQueryResult) == 1){
        echo "This email address is already registered.";
        exit();
    }
    if($password != $password1){
        echo "Passwords do not match!";
        exit();
    }

    $query="INSERT INTO users (firstname, lastname, gender,email,hash) VALUES ('".$firstname."','".$lastname."','".$gender."','".$email."','".$hash."');";
    $queryResult = mysqli_query($con,$query);
    if($queryResult){
        echo "Registration is successful";
    }else{
        echo "Failed to register";
    }
}
?>