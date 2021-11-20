document.getElementById('header-nav').innerHTML = `
<div class="header-nav">
        <div class="container2">
            <div class="row">
                <div class="col-md-4">
                    <div class="all-department">
                        <a href="#">
                            <span class="text-title">All Departments</span>

                        </a>
                        <i class="fas fa-align-justify" data-toggle="collapse" data-target="#category"></i>
                    </div>
                    <div id="category" class="category collapse">
                        <ul>
                            <li class="menu-category">
                                <a href="./collection.html">
                                                    IPHONE
                                                    <span class="fas fa-angle-right"></span>
                                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div id="search-2" class="col-md-4">
                    <div class="search__container">
                        <input class="search__input" type="text" placeholder="Search" onkeyup="search(event)">
                    </div>
                </div>
                <div id="block-link" class="col-md-4">
                    <div class="shop-cart">
                        <a href="./cart.html" class="block-link">
                            <i class="fas fa-shopping-cart"></i>
                            <span class="count" id=countproduct></span>   
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>`
var itemMenu = document.getElementsByClassName('menu-category');
for (let i = 0; i < itemMenu.length; i++) {
    var item = itemMenu[i];
    item.addEventListener('click', function () {
        var itemClick = event.target;
        localStorage.setItem('menuItem', itemClick.innerText);
    });
}
var product = JSON.parse(localStorage.getItem('productCart'));
if (product) {
    document.getElementById('countproduct').innerHTML = product.length;
}
else {
    document.getElementById('countproduct').innerHTML = 0;
}
axios.get(`${host}/list_category`)
    .then((result) => {
        var list_category = result.data.data;
        list_category.map((item) => {
            document.getElementsByClassName('menu-category')[0].innerHTML += `
                                                <a href="./collection.html">
                                                    ${item.name.toUpperCase()}
                                                    <span class="fas fa-angle-right"></span>
                                                </a> `
        })
    }).catch((error) => {
        //console.log(error);
    });
