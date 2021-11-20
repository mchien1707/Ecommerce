function addProduct() {
    var nameProduct = document.getElementById('nameProduct').value
    var imageProduct = document.getElementById('imageProduct').value
    var priceProduct = document.getElementById('priceProduct').value
    var kindProduct = document.getElementById('kindProduct').value;
    var amountProduct = document.getElementById('amountProduct').value
    var statusProduct = document.getElementById('statusProduct').value;
    var detailsProduct = document.getElementById('detailsProduct').value



    var validName = document.getElementById('nameProduct').validity.valid
    var validImage = document.getElementById('imageProduct').validity.valid
    var validPrice = document.getElementById('priceProduct').validity.valid
    var validKind = document.getElementById('kindProduct').validity.valid
    var validAmount = document.getElementById('amountProduct').validity.valid
    var validStatus = document.getElementById('statusProduct').validity.valid
    var validDetails = document.getElementById('detailsProduct').validity.valid



    var Product = {
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
        axios.post(`${host}/insert_product`, Product).then((result) => {
            //console.log("them san pham : ", result.data.success);

            if (result.data.success) {
                alert("Thêm sản phẩm thành công !")
                location.replace('./admin_addproduct.html')
            }
            else {
                alert("Thêm sản phẩm thất bại !")
                location.replace('./admin_addproduct.html')
            }
        }).catch(() => {
            //console.log(result.data.success);
        })
    }

}
getCategoryProduct();
function getCategoryProduct() {
    axios.get(`${host}/list_category`)
        .then((result) => {
            var list_category = result.data.data;
            //console.log(list_category);

            var selectCategory = $('#kindProduct').empty();
            const content = list_category.map((item, index) => {
                document.getElementById('kindProduct').innerHTML += `
                        <option>${item.name}</option>
                `;
            });
        })
}