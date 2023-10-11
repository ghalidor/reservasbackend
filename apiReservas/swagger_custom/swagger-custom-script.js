(function () {
    window.addEventListener("load", function () {
        setTimeout(function () {
            var logo = document.getElementsByClassName('link');
            logo[0].href = "https://wanvendor.pe/";
            logo[0].target = "_blank";

            logo[0].children[0].alt = "Wanvendor";
            logo[0].children[0].src = "https://wanvendor.pe/Template/img/logoWV1.png";
        });
    });
})();

