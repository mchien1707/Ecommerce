var listBill;
getAllBillPerPage();
let itemPerPage = 10;
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
        getAllBillPerPage()
    }
}
function nextPage() {
    if (currentPage < totalPage) {
        currentPage++;
        changeClassPagi()
        getCurrentPage()
        changeClassPagi()
        getAllBillPerPage()
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
function getAllBillPerPage() {
    axios.get(`${host}/list_bill`)
        .then((result) => {
            var list_bill = result.data.data;
            //console.log(list_bill);
            var tableBody = $('#listBillAdmin').empty();
            //console.log(list_bill[1].create_date);
            const content = list_bill.map((item, index) => {
                if (index >= start && index < end) {

                    let classBtn = ''

                    let textStatus = ''

                    if (item.status) {
                        classBtn = 'btn-success'
                        textStatus = 'Đã giao'
                    } else {
                        classBtn = 'btn-warning'
                        textStatus = 'Đang xử lý...'
                    }
                    document.getElementById('listBillAdmin').innerHTML += `
                                        <tr>
                                            <td>${++index}</td>
                                            <td>${(item._id).slice(-10)}</td>
                                            <td>${item.username}</td>
                                            <td>0${item.phone}</td>
                                            <td>${item.address}</td>
                                            <td>
                                                <button class="btn ${classBtn}" onclick=changeStatus('${item._id}')>${textStatus}</button>  
                                            </td>
                                            <td>${parseInt(item.total).toFixed(2)}$</td>
                                            <td>${(item.Create_date).slice(0, 10)}</td>
                                            <td><a class="btn btn-warning" type="button" href="./admin_billdetails.html" onclick="getIdBill('${item._id}')">Xem chi tiết</a> <button class="btn btn-danger" onclick="checkDelete('${item._id}')">Xóa</button></td>
                                        </tr>
                                        `;

                }


            });
        })
}
function changeStatus(id) {
    if (confirm("Bạn muốn thay đổi trạng thái đơn hàng ?")) {
        //console.log(id);
        axios.get(`${host}/get_bill_id/${id}`).then((result) => {
            let bill = result.data.data;
            let statusBill = {
                id: id,
                status: !bill.status

            }
            axios.put(`${host}/update_bill`, statusBill).then((result) => {
                //console.log('Update status thành cong');
                getAllBillPerPage()
            }).catch((error) => {
                //console.log('Cancel update');
            })
        })
    }
    else {
        //console.log("Không thay trạng thái đổi đơn hàng");
    }
}
function getIdBill(id) {
    localStorage.setItem('idBill', id)
}
function checkDelete(_id) {
    if (confirm("Bạn có muốn xoá bill này ?")) {
        deleteUser(_id)
    }
    else {
        //console.log("Chọn không xoá bill");
    }
}

function deleteUser(_id) {
    var id;
    listBill.map(item => {
        if (item._id === _id)
            id = _id
    })
    axios.delete(`${host}/delete_bill`, {
        data: {
            id: id
        }
    }).then((result) => {
        ////console.log(result.data)
        alert("Xoá bill thành công !")
        getAllBillPerPage()
    })
}



function renderListPage() {
    axios.get(`${host}/list_bill`).then((result) => {
        listBill = result.data.data;
        totalPage = Math.ceil(listBill.length / itemPerPage)


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
    getAllBillPerPage()
}


function changeClassPagi() {
    let btnPagination = document.querySelectorAll('#listPage li');
    $('#listPage li').removeClass('active');
    btnPagination[currentPage - 1].classList.add('active')
}
