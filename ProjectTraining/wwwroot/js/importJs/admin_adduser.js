function addUser() {
    var name = document.getElementById('username').value;
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    var confirmpw = document.getElementById('repassword').value;
    var phone = document.getElementById('phone').value;
    var address = document.getElementById('address').value;
    var role = document.getElementById('role').options[document.getElementById('role').selectedIndex].value;
    
    
    
    
    //Kiểm tra input true hay false
    var validName = document.getElementById('username').validity.valid;
    var validEmail = document.getElementById('email').validity.valid;
    var validPassword = document.getElementById('password').validity.valid;
    var validCfPassword = document.getElementById('repassword').validity.valid;
    var validPhone = document.getElementById('phone').validity.valid;
    var validAddress = document.getElementById('address').validity.valid;
    //console.log(validPhone);
    if(role === 'Khách hàng'){
        role = 'khachhang'
    }
    else{
        role = 'admin'
    }
    if(password === confirmpw)
        validCfPassword = true;
    else
        validCfPassword = false;
    if (isNaN(phone))
        validPhone = false;
    
    var User = {
        name: name,
        email: email,
        password: password,
        phone: phone,
        address: address,
        role: role,

    }
    if (validName && validEmail && validPassword && validPhone && validAddress) {
        axios.post(`${host}/signup`, User)
            .then((result) => {
                //console.log(result.data.message);
                if (result.data.success) {
                    alert("Thêm user thành công !")
                    
                    location.replace('./admin_user.html');
                    

                }
                else {
                    alert("Thêm user thất bại !")
                }
        }).catch(()=>{
            //console.log("KHONG GOI DUOC API");
            
        })
    }else{
        alert("Nhập sai thông tin ! Vui lòng kiểm tra lại")
    }

}