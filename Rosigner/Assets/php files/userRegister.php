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
    $hash = hash('sha256',$password);

    $query="INSERT INTO users (firstname, lastname, gender,email,hash) VALUES ('".$firstname."','".$lastname."','".$gender."','".$email."','".$hash."');";
    $queryResult = mysqli_query($con,$query);
    if($queryResult){
        echo "Registration is successful";
    }else{
        echo "Failed to register";
    }
}
?>