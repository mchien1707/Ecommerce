function updateUser() {
    var name = document.getElementById('username').value;
    var email = document.getElementById('email').value;
    var phone = document.getElementById('phone').value;
    var address = document.getElementById('address').value;
    var role = document.getElementById('role').options[document.getElementById('role').selectedIndex].value;

    //Kiểm tra input true hay false
    var validName = document.getElementById('username').validity.valid;
    var validEmail = document.getElementById('email').validity.valid;
    
    var validPhone = document.getElementById('phone').validity.valid;
    var validAddress = document.getElementById('address').validity.valid;
    
    if (role === 'Khách hàng') {
        role = 'khachhang'
    }
    else {
        role = 'admin'
    }
    
    if (isNaN(phone))
        validPhone = false;

    var User = {
        id : localStorage.getItem('idUser'),
        name: name,
        email: email,
        phone: phone,
        address: address,
        role: role,

    }
    if (validName && validEmail  && validPhone && validAddress) {
        axios.put(`${host}/update_user`, User)
            .then((result) => {
                
                if (result.data.result) {
                    alert("Cập nhật user thành công !")
                    location.replace('./admin_user.html');
                }
                else {
                    alert("Cập nhật user thất bại !")
                }
            }).catch(() => {
                //console.log("KHONG GOI DUOC API");

            })
    } else {
        alert("Nhập sai thông tin ! Vui lòng kiểm tra lại")
    }
}