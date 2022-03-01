<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}

if($_POST['unity']=="login")
{
    $email=$_POST['email'];
    $password=$_POST['password'];

    $query="SELECT email,hash FROM users WHERE email = '".$email."';";
    $queryResult = mysqli_query($con,$query) or die("2: Name check query failed");
    if(mysqli_num_rows($queryResult) != 1){
        echo "This email address is not registered.";
        exit();
    }
    $existinginfo = mysqli_fetch_assoc($queryResult);
    $hash = $existinginfo["hash"];
   
    $loginhash = hash('sha256',$password);


    if($hash != $loginhash){
        echo "Incorrect password";
        exit();
    }
    echo "Login success!";

    
    exit();
}

?>