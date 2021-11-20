var list;
getAllProductPerPage();
let itemPerPage = 6;
let currentPage = 1;
let start = 0;
let end = itemPerPage;
let totalPage;
renderListPage();

getCurrentPage();
// setTimeout(function () {
//     changePage();
// }, 300);

function prePage() {
    if (currentPage >= 2) {
        currentPage--;
        changeClassPagi()
        getCurrentPage()
        changeClassPagi()
        getAllProductPerPage()
    }
}
function nextPage() {
    if (currentPage < totalPage) {
        currentPage++;
        changeClassPagi()
        getCurrentPage()
        changeClassPagi()
        getAllProductPerPage()
        //console.log('currentPage : ', currentPage);

    }
}

function getCurrentPage() {
    start = (currentPage - 1) * itemPerPage;
    end = currentPage * itemPerPage;
    if (currentPage === totalPage) {
        $('#liNext').addClass('disabled')
    }
    else {
        $('#liNext').removeClass('disabled')
    }
    if (currentPage > 1) {
        $('#liPre').removeClass("disabled")
    } else {
        $('#liPre').addClass("disabled")
    }

}


function getAllProductPerPage() {
    axios.get(`${host}/list_product`)
        .then((result) => {
            var list_product = result.data.data;
            //console.log(list_product);

            var tableBody = $('#listProductAdmin').empty();
            const content = list_product.map((item, index) => {
                if (index >= start && index < end) {


                    var tempprice = item.price.toLocaleString(
                        'en-US', { minimumFractionDigits: 0 }
                    );
                    if (item.amount <= 0)
                        item.status = 'unavailable'
                    document.getElementById('listProductAdmin').innerHTML += `
                                <tr>
                                    <td>${++index}</td>
                                    <td><img src="${item.image_name}" alt="Phu kien dien thoai"></td>
                                    <td>${item.name}</td>
                                    <td>${item.kind}</td>
                                    <td>${tempprice}$</td>
                                    <td>${item.amount}</td>
                                    <td>${item.status}</td>
                                    <td><a class="btn btn-warning" type="button" href="./admin_updateproduct.html" onclick="getIdProduct('${item._id}')">Chỉnh sửa</a> <button class="btn btn-danger" onclick="checkDelete('${item._id}')">Xóa</button></td>
                                </tr>
                                `;
                }
            });
        })
}
function getIdProduct(id) {
    localStorage.setItem('idProduct', id)
}
function checkDelete(_id) {
    if (confirm("Bạn có muốn xoá sản phẩm này ?")) {
        deleteProduct(_id)
    }
    else {
        //console.log("Chọn không xoá sản phẩm");
    }
}

function deleteProduct(_id) {
    var id;
    list.map(item => {
        if (item._id === _id)
            id = _id
    })
    axios.delete(`${host}/delete_product`, {
        data: {
            id: id
        }
    }).then((result) => {
        //console.log(result.data)
        alert("Xoá sản phẩm thành công !")
        getAllProductPerPage()

    })
}



function renderListPage() {
    axios.get(`${host}/list_product`).then((result) => {
        list = result.data.data;
        totalPage = Math.ceil(list.length / itemPerPage)
        var htmlListPage = `<li class="page-item  active"><a class="page-link" onclick="otherPage(1)">${1}</a></li>`
        for (let i = 2; i <= totalPage; i++) {

            htmlListPage += `<li class="page-item"><a class="page-link" onclick="otherPage(${i})">${i}</a></li>`
        }
        document.getElementById('listPage').innerHTML = htmlListPage;
        //console.log('Total page :' + totalPage);
    })
}
function otherPage(i) {
    currentPage = i;
    changeClassPagi()
    getCurrentPage()
    getAllProductPerPage()
}


function changeClassPagi() {
    let btnPagination = document.querySelectorAll('#listPage li');
    $('#listPage li').removeClass('active');
    btnPagination[currentPage - 1].classList.add('active')
}