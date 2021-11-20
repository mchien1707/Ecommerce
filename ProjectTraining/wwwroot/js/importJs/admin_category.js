var listCategory;
getAllCategoryPerPage();
let itemPerPage = 6;
let currentPage = 1;
let start = 0;
let end = itemPerPage;
let totalPage;
renderListPage();

getCurrentPage();


function prePage() {
    if (currentPage >= 2) {
        currentPage--;
        changeClassPagi()
        getCurrentPage()
        changeClassPagi()
        getAllCategoryPerPage()
    }
}
function nextPage() {
    if (currentPage < totalPage) {
        currentPage++;
        changeClassPagi()
        getCurrentPage()
        changeClassPagi()
        getAllCategoryPerPage()
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
function getAllCategoryPerPage() {
    axios.get(`${host}/list_category`)
        .then((result) => {
            var list_category = result.data.data;
            //console.log(list_category);

            var tableBody = $('#listCategoryAdmin').empty();
            const content = list_category.map((item, index) => {
                if (index >= start && index < end) {

                    document.getElementById('listCategoryAdmin').innerHTML += `
                                <tr>
                                    <td>${++index}</td>
                                    <td>${item.name}</td>
                                    <td>${item.des}</td>
                                   
                                    <td><a class="btn btn-warning" type="button" href="./admin_updatecategory.html" onclick="getIdCategory('${item._id}')">Chỉnh sửa</a> <button class="btn btn-danger" onclick="checkDelete('${item._id}')">Xóa</button></td>
                                </tr>
                                `;
                }
            });
        })
}
function getIdCategory(id) {
    localStorage.setItem('idCategory', id)
}
function checkDelete(_id) {
    if (confirm("Bạn có muốn xoá mục sản phẩm này ?")) {
        deleteUser(_id)
    }
    else {
        ////console.log("Chọn không xoá category");
    }
}

function deleteUser(_id) {
    var id;
    listCategory.map(item => {
        if (item._id === _id)
            id = _id
    })
    axios.delete(`${host}/delete_category`, {
        data: {
            id: id
        }
    }).then((result) => {
        //console.log(result.data)
        alert("Xoá sản category thành công !")
        getAllCategoryPerPage()
    })
}



function renderListPage() {
    axios.get(`${host}/list_category`).then((result) => {
        listCategory = result.data.data;
        totalPage = Math.ceil(listCategory.length / itemPerPage)


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
    getAllCategoryPerPage()
}


function changeClassPagi() {
    let btnPagination = document.querySelectorAll('#listPage li');
    $('#listPage li').removeClass('active');
    btnPagination[currentPage - 1].classList.add('active')
}