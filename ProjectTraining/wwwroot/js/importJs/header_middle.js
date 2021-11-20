document.getElementById('header-middle').innerHTML = `
<div class="header-middle">
        <div class="container2">
            <div class="header-middle-inner">
                <div class="row">
                    <div id="logo" class="col-md-3">
                        <div class="logo">
                            <a href="./home.html"><img style="
                                width: 240px;" src="images/logo.webp" class="img-responsive">
                            </a>
                            <i class="fas fa-align-justify icon-res" data-toggle="collapse" data-target="#menu-res"></i>
                        </div>
                        <ul id="menu-res" class="main-menu collapse">
                            <li class="main-menu-item">
                                <span class="hot">HOT</span>
                                <a class="menu-lv1" href="./home.html">HOME</a>
                            </li>

                            <li class="main-menu-item">
                                <span class="new">NEW</span>
                                <a class="menu-lv1" href="./collection.html"  onclick="removeLocalCategory()"   >SHOP</a>
                            </li>
                            <li class="main-menu-item">

                                <a class="menu-lv1" href="./support.html">SUPPORT</a>
                            </li>
                            <li class="main-menu-item">
                                <a class="menu-lv1" href="./support.html#contact">CONTACT US</a>
                            </li>
                        </ul>

                    </div>
                    <div id="menu-bar" class="col-md-6">
                        <div class="box-header-nav">
                            <ul class="main-menu">
                                <li class="main-menu-item">
                                    <span class="hot">HOT</span>
                                    <a class="menu-lv1" href="./home.html">HOME</a>
                                </li>

                                <li class="main-menu-item">
                                    <span class="new">NEW</span>
                                    <a class="menu-lv1" href="./collection.html" onclick="removeLocalCategory()">SHOP</a>
                                </li>
                                <li class="main-menu-item">

                                    <a class="menu-lv1" href="./support.html">SUPPORT</a>
                                </li>
                                <li class="main-menu-item">
                                    <a class="menu-lv1" href="./support.html#contact">CONTACT US</a>
                                </li>
                            </ul>
                            <div id="search-1" class="search__container">
                                <input class="search__input" type="text" placeholder="Search">
                            </div>
                        </div>
                    </div>
                    <div id="phone-number" class="col-md-3">
                        <div class="phone">
                            <i class="fas fa-phone-volume"></i><a href="tel:0909-090-999"
                                style="color: #000;">0909-090-999</a>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>`
    function removeLocalCategory(){
        localStorage.removeItem('menuItem')
    }