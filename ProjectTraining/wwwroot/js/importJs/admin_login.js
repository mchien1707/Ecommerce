function handleLogin() {
    const host = 'https://accessories1.herokuapp.com';
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    var validEmail = document.getElementById('email').validity.valid;
    var validPass = document.getElementById('password').validity.valid;

    var login = {
        email: email,
        password: password,

    };

    if (validEmail && validPass) {
        axios.post(`${host}/login`, login).then((result) => {
            // console.log(result.data.success)
            // console.log(typeof(result.data.message.user.role[0]))
            // console.log(result.data.message.user.role[0] === 'admin')
            if (result.data.success && result.data.message.user.role[0] === 'admin') {
                localStorage.setItem("tokenAdmin", result.data.message.token);
                localStorage.setItem("roleAdmin", result.data.message.user.role);
                // Swal.fire({
                //     position: 'center',
                //     icon: 'success',
                //     title: 'Login successfully!',
                //     showConfirmButton: false,
                //     timer: 1200,
                // })
                
                location.replace('./admin_bill.html');
                
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'warning',
                    title: 'Login failed!',
                    text: 'Wrong Email or Password !',
                    showConfirmButton: false,
                    timer: 2000
                })
                setTimeout(function () {

                }, 1200)
            }

        });
    }
}