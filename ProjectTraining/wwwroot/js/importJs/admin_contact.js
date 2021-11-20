var listUser;
getAllUserPerPage();
let itemPerPage = 4;
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
        getAllUserPerPage()
    }
}
function nextPage() {
    if (currentPage < totalPage) {
        currentPage++;
        changeClassPagi()
        getCurrentPage()
        changeClassPagi()
        getAllUserPerPage()
        //console.log('currenPage : ', currentPage);

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
function getAllUserPerPage() {
    axios.get(`${host}/get_user_contact`)
        .then((result) => {
            var list_user = result.data.data;
            //console.log(list_user);

            var tableBody = $('#listContactAdmin').empty();
            const content = list_user.map((item, index) => {
                if (index >= start && index < end) {

                    document.getElementById('listContactAdmin').innerHTML += `
                                <tr>
                                    <td>${++index}</td>
                                    <td>${item.name}</td>
                                    <td>${item.email}</td>
                                    <td>0${item.phone}</td>
                                    <td>${item.content}</td>
                                    <td><a class="btn btn-danger"  href="mailto:${item.email}">Mail</a> <a class="btn btn-success" href="tel:${item.phone}" >Call</a></td>
                                </tr>
                                `;
                }
            });
        })
}




function renderListPage() {
    axios.get(`${host}/get_user_contact`).then((result) => {
        listUser = result.data.data;
        totalPage = Math.ceil(listUser.length / itemPerPage)


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
    getAllUserPerPage()
}


function changeClassPagi() {
    let btnPagination = document.querySelectorAll('#listPage li');
    $('#listPage li').removeClass('active');
    btnPagination[currentPage - 1].classList.add('active')
}
