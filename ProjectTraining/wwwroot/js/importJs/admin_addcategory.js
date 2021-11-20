function addCategory() {
    var nameCategory = document.getElementById('nameCategory').value
    var desCategory = document.getElementById('desCategory').value
    
    var validNameCategory = document.getElementById('nameCategory').validity.valid
    //console.log(desCategory);
    
    var Category = {
        name: nameCategory,
        des: desCategory,
    }
    
    if (validNameCategory) {
        axios.post(`${host}/insert_category`, Category).then((result) => {
            //console.log("them san category: ", result.data.result);

            if (result.data.result) {
                alert("Thêm thành công !")
                location.replace('./admin_addcategory.html')
            }
            else {
                alert("Thêm thất bại !")
                location.replace('./admin_addcategory.html')
            }
        }).catch(() => {
            //console.log(result.data.result);
        })
    }
}