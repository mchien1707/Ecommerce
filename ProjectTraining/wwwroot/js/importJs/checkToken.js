
var tokenAdmin = localStorage.getItem('tokenAdmin');
var roleAdmin = localStorage.getItem('roleAdmin');
//console.log(tokenAdmin);

function logout() {
                            
    localStorage.removeItem('tokenAdmin');
    localStorage.removeItem('idProduct');
    localStorage.removeItem('roleAdmin');
    //console.log("REMOVE")
    location.replace('./sadmin_login.html')
    setTimeout(1200);
    
}
$(document).ready(function(){
    if(!tokenAdmin){
        $("#adminContent").html('<h3 class="title_component">Vui lòng đăng nhập !</h3> <a class="login100-form-btn" href="./admin_login.html" style="width : 50% ; margin: 0 auto">Login</a>')
        location.replace('./admin_login.html')
    };
})
