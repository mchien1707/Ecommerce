function handleSubmit() {
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPass = document.getElementById('repassword').value;

    //console.log(confirmPass)


    //Kiểm tra input true hay false
    var validEmail = document.getElementById('email').validity.valid;
    var validPassword = document.getElementById('password').validity.valid;
    var validConfirmPass = document.getElementById('repassword').validity.valid;
    
    
    if(password === confirmPass)
        validConfirmPass = true;
    else
        validConfirmPass = false;
    const register = {    
    
        email: email,
        password: password,
        role : 'admin'
    }
    
    
    //console.log(validEmail,validPassword,validConfirmPass);
    
    if(validEmail && validPassword && validConfirmPass){
        axios.post(`${host}/signup`, register)
        .then((result) => {
            //console.log(result.data.message);
            if (result.data.success) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Sign Up Success!',
                    showConfirmButton: false,
                    timer: 1200
                })
                setTimeout(function(){
                    location.replace('./admin_login.html');
                }, 1200)
            }
            else{
                Swal.fire({
                position: 'center',
                icon: 'warning',
                title: 'Registration Failed!!!',
                text : 'Account already exists!',
                showConfirmButton: false,
                timer: 1200
            })
            }
        }).catch(()=>{
            ////console.log(result.data.message);
        })
    }else{
        Swal.fire({
            position: 'center',
            icon: 'warning',
            title: 'Registration Failed!!!',
            text : 'Email or password is incorrect',
            showConfirmButton: false,
            timer: 1200
        })
    }
    
}