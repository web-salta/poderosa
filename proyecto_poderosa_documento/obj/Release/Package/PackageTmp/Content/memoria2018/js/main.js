$(document).ready(function () {
    //FUNCTIONS
    function f_offset_top() {

        if (window.matchMedia('(min-width: 768px)').matches) {
            var offset_top = 0;
        } else {
            var offset_top = 95;
        }
        return offset_top;
    }

    function f_scrollspy() {
        var offset_top = f_offset_top();
        $('body').scrollspy('dispose');
        $('body').scrollspy({
            target: ".nav-pri",
            offset: offset_top
        });
    }

    function f_toggle_nav() {
        $('.nav-pri').toggleClass('nav-pri--open');
        $(".nav-pri__toggler").toggleClass("is-active");
    }

    function f_nav_spy() {
        var activo = $('.nav-pri__a.active .nav-pri__a-txt').text();
        var title = $('.nav-pri__title');
        title.text(activo);

        title.removeClass('nav-pri__title--0 nav-pri__title--1 nav-pri__title--2 nav-pri__title--3 nav-pri__title--4 nav-pri__title--5');

        if ($('.nav-pri__a--0').hasClass('active')) {
            title.addClass('nav-pri__title--0');
        }
        if ($('.nav-pri__a--1').hasClass('active')) {
            title.addClass('nav-pri__title--1');
        }
        if ($('.nav-pri__a--2').hasClass('active')) {
            title.addClass('nav-pri__title--2');
        }
        if ($('.nav-pri__a--3').hasClass('active')) {
            title.addClass('nav-pri__title--3');
        }
        if ($('.nav-pri__a--4').hasClass('active')) {
            title.addClass('nav-pri__title--4');
        }
        if ($('.nav-pri__a--5').hasClass('active')) {
            title.addClass('nav-pri__title--5');
        }
    }

    //EVENTS
    $('.nav-pri__toggler, .nav-pri__velo').click(function (event) {
        f_toggle_nav();
    });

    $('.nav-pri__a, .nav-pri__brand-body-a, .sec-foot__a').click(function (event) {
        if (this.hash !== "") {
            event.preventDefault();

            if ($('.nav-pri').hasClass('nav-pri--open')) {
                f_toggle_nav();
            }

            var hash = this.hash;
            var offset_top = f_offset_top();
            $('html, body').animate({
                scrollTop: $(hash).offset().top - offset_top + 1
            }, 500, function () {
                // window.location.hash = hash;
                //MOBILE
                if (!(window.matchMedia('(min-width: 768px)').matches)) {
                    // f_nav_spy();
                    setTimeout(f_nav_spy, 20);
                }
            });
        }
    });


    //ALL
    f_scrollspy();
    //MOBILE
    if (!(window.matchMedia('(min-width: 768px)').matches)) {
        f_nav_spy();
    }


    $(window).scroll(function () {
        //MOBILE
        if (!(window.matchMedia('(min-width: 768px)').matches)) {
            f_nav_spy();
        }
    });

    $(window).resize(function () {
        //ALL
        f_scrollspy();
        //MOBILE
        if (!(window.matchMedia('(min-width: 768px)').matches)) {
            f_nav_spy();
        }
    });



    // slick - INICIO
    $('.js-sec-banner__carrusel').slick({
        autoplay: true,
        arrows: false,
        dots: false,
        speed: 1500,
        fade: true,
        cssEase: 'linear',
        autoplaySpeed: 1000,
    });

    $('.js-slider').slick({
        autoplay: true,
        arrows: false,
        dots: true,
        speed: 900,
    });
    // slick - FIN

});
