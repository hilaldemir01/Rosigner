<?php
$con = new mysqli("localhost","root","","rosigner");

// Check connection
if ($con -> connect_errno) {
  echo "Failed to connect to database: " . $con -> connect_error;
}
// Check if the called method is login or not
if($_POST['unity']=="login")
{
     // Getting user information from the user interface
    $email=$_POST['email'];
    $password=$_POST['password'];
    // Fetching user information using user emails
    $query="SELECT email,hash FROM users WHERE email = '".$email."';";
    $queryResult = mysqli_query($con,$query) or die("2: Name check query failed");
    if(mysqli_num_rows($queryResult) != 1){
        echo "This email address is not registered.";
        exit();
    }
    // get login info from query
    $existinginfo = mysqli_fetch_assoc($queryResult);
    $hash = $existinginfo["hash"];

    // crypt the inputted password and check if matches in the database
    $loginhash = hash('sha256',$password);
    if($hash != $loginhash){
        echo "Incorrect password";
        exit();
    }
    // Print this message if the login is successful
    echo "Login success!";
    
    exit();
}

?>