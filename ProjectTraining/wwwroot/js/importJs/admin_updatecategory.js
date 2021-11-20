function updateCategory(){
    let nameCategory = document.getElementById('nameCategory').value
    let desCategory = document.getElementById('desCategory').value
    


    let validName = document.getElementById('nameCategory').validity.valid
    let validDes = document.getElementById('desCategory').validity.valid
    


    
    let Category = {
        id : localStorage.getItem('idCategory'),
        name: nameCategory,
        des: desCategory,
    
    }
    //console.log(Category)
    if (validName && validDes) {
        axios.put(`${host}/update_category`, Category).then((result) => {
            //console.log("update category : ",result.data.result);
            
            if (result.data.result) {
                alert("Cập nhật loại sản phẩm thành công !")
                location.replace('./admin_category.html')
            
                
            }
            else {
                alert("Cập nhật loại sản phẩm thất bại !")
                location.replace('./admin_product.html')
            }
        }).catch(() => {
            //console.log("Cập nhật loại sản phẩm thất bại");
        })
    }else{
        alert("Vui lòng kiểm tra lại !")
    }
}