function updateProduct() {
    let nameProduct = document.getElementById('nameProduct').value
    let imageProduct = document.getElementById('imageProduct').value
    let priceProduct = document.getElementById('priceProduct').value
    let kindProduct = document.getElementById('kindProduct').value
    let amountProduct = document.getElementById('amountProduct').value
    let statusProduct = document.getElementById('statusProduct').value;
    let detailsProduct = document.getElementById('detailsProduct').value


    let validName = document.getElementById('nameProduct').validity.valid
    let validImage = document.getElementById('imageProduct').validity.valid
    let validPrice = document.getElementById('priceProduct').validity.valid
    let validKind = document.getElementById('kindProduct').validity.valid
    let validAmount = document.getElementById('amountProduct').validity.valid
    let validStatus = document.getElementById('statusProduct').validity.valid
    let validDetails = document.getElementById('detailsProduct').validity.valid


    
    let Product = {
        id : localStorage.getItem('idProduct'),
        name: nameProduct,
        kind: kindProduct,
        price: priceProduct,
        image_name: imageProduct,
        amount: amountProduct,
        status: statusProduct,
        details: detailsProduct
    }
    //console.log(Product)
    if (validName && validImage && validPrice && validKind && validAmount && validStatus && validDetails) {
        axios.put(`${host}/update_product`, Product).then((result) => {
            //console.log("update san pham : ",result.data.result);
            
            if (result.data.result) {
                alert("Cập nhật sản phẩm thành công !")
                location.replace('./admin_product.html')
            
                
            }
            else {
                alert("Cập nhật sản phẩm thất bại !")
                location.replace('./admin_product.html')
            }
        }).catch(() => {
            ////console.log("Cập nhật sản phẩm thất bại");
        })
    }else{
        alert("Vui lòng kiểm tra lại !")
    }

}
getCategoryProduct();
function getCategoryProduct(){
    axios.get(`${host}/list_category`)
    .then((result) => {
        var list_category = result.data.data;
        ////console.log(list_category);
        let kindProduct = document.getElementById('kindProduct').value
        //console.log("kind product : ",kindProduct);
        //var selectCategory = $('#kindProduct').empty();
        const content = list_category.map((item, index) => {
                if(item.name === kindProduct)
                    document.getElementById('kindProduct').innerHTML += ``
                else{
                    document.getElementById('kindProduct').innerHTML += `
                    <option>${item.name}</option>
                    `;
                }
                
            
        });
    })
}