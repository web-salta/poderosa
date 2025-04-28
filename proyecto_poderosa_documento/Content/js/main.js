 
$(document).ready(function(){
    $("#header_content").load("header.html");
});

$(document).ready(function () {
    $("#footer_content").load("footer.html");


    // Manejo de clic en los elementos con la clase 'menu-drow'
    $('.menu-drow').on('click', function (e) {
        //alert("uuu");
        e.preventDefault();
        //e.stopPropagation();
        // Selecciona el submenú asociado al elemento clicado
        var $submenu = $(this).next('.MultiLevelMenu');
        var $menuSecuVelo = $('.menuSecuVelo');

        // Oculta todos los submenús y punteros
        $('.MultiLevelMenu').not($submenu).removeClass('mostrar');
        $('.puntero').css('display', 'none');

        // Muestra el submenú y el puntero correspondiente
        if (!$submenu.hasClass('mostrar')) {
            $submenu.addClass('mostrar');
            $(this).find('.puntero').css('display', 'inherit');
            $menuSecuVelo.addClass('menuSecuVelo--activo');

            //oculta el scroll
            $("html, body").css("overflow-y", "hidden");

        } else {
            // Si el submenú ya está visible, lo oculta junto con el puntero
            $submenu.removeClass('mostrar');
            $(this).find('.puntero').css('display', 'none');
            $menuSecuVelo.removeClass('menuSecuVelo--activo');

            //muestra el scroll
            $("html, body").css("overflow-y", "inherit");
        }
    });

    // Manejo de clic en los elementos con la clase 'dropdown-toggle-custom'
    $('.dropdown-toggle-custom').on('click', function () {
        // Oculta la línea en todos los elementos antes de mostrarla en el elemento clicado
        $('.dropdown-linea').css('display', 'none');
        // Muestra la línea del elemento clicado con display inherit
        $(this).find('.dropdown-linea').css('display', 'inherit');
    });

    // Oculta el submenú, puntero y línea si se hace clic fuera del menú
    $(document).on('click', function (e) {
        if (!$(e.target).closest('.navbar_').length) {
            $('.MultiLevelMenu').removeClass('mostrar');
            $('.puntero').css('display', 'none');
            $('.dropdown-linea').css('display', 'none');
        }
    });

    // Oculta el submenú, puntero y velo si se hace clic fuera del menú
    $(document).on('click', function (e) { //alert("entre meu");
        // Verifica si el clic fue fuera del MultiLevelMenu y el velo
        if (!$(e.target).closest('.MultiLevelMenu, .menuSecuVelo, .menu-drow').length) {
            $('.MultiLevelMenu').removeClass('mostrar');
            $('.puntero').css('display', 'none');
            $('.dropdown-linea').css('display', 'none');
            $('.menuSecuVelo').removeClass('menuSecuVelo--activo');
            //muestra el scroll
            $("html, body").css("overflow-y", "inherit");
        }

    });

})


        function collapse() {
      // HEADER
      var position = $(window).scrollTop();

        $(window).scroll(function () {
          var scroll = $(window).scrollTop();
          if (scroll > position) {
            $('header').addClass('js-header-scrollDown');
          } else {
            $('header').removeClass('js-header-scrollDown');
          }
        position = scroll;
      });


        // vista movil: menu-sec debajo del menu-pri
        if (window.matchMedia("(max-width: 991px)").matches) {
            $('.js-nav-sec').appendTo('.js-navbar-collapse');
        $('.js-nav-sec').addClass('navbar-nav');
    }else{
            $('.js-nav-sec').prependTo('.js-header');
        $('.js-nav-sec').removeClass('navbar-nav');
    }

        // adecuacion de carrusel de noticias
        if (window.matchMedia("(min-width: 768px)").matches) {
            $('.js-owl-carousel-noticias-home .owl-stage').addClass('row w-auto');

        $('.js-owl-carousel-noticias-home .owl-item:nth-child(1)').addClass('col-lg-8 w-auto');
        $('.js-owl-carousel-noticias-home .owl-item:nth-child(2)').addClass('col-lg-4 w-auto d-md-flex d-lg-block');

        $('.js-owl-carousel-noticias-home .noticia-resumen-home--2').addClass('mr-md-3 mr-lg-0');
        $('.js-owl-carousel-noticias-home .noticia-resumen-home--3').addClass('ml-md-3 ml-lg-0');

        $('.js-owl-carousel-noticias-home .noticia-resumen-home--3').appendTo('.js-owl-carousel-noticias-home .owl-item:nth-child(2)');
    }

  }

        collapse();

        $(window).resize(function() {
            collapse();
  });

/*});*/

        const hamburgerButton = document.querySelector('.navbar-toggler'); // Selecciona el botón del menú

hamburgerButton.addEventListener('click', () => {
  const isExpanded = hamburgerButton.getAttribute('aria-expanded') === 'true'; // Verifica el estado
        console.log(`El menú está ${isExpanded ? 'expandido' : 'colapsado'}`);
});


/*
});*/

$(".menuSecuVelo").click(fnMenuSecuVelo__activo);

function fnMenuSecuVelo__activo() { //alert("ENTRE");
  $(".menuSecuVelo").toggleClass("menuSecuVelo--activo");
    //muestra el scroll
    $("html, body").css("overflow-y", "inherit");
}


function collapse() {
// HEADER
var position = $(window).scrollTop();

$(window).scroll(function () {
    var scroll = $(window).scrollTop();
    if (scroll > position) {
       // console.log('scrollDown');
        $('header').addClass('js-header-scrollDown');
    } else {
       // console.log('scrollUp');
        $('header').removeClass('js-header-scrollDown');
    }
    position = scroll;
});


    // vista movil: menu-sec debajo del menu-pri
    if (window.matchMedia("(max-width: 991px)").matches) {
      $('.js-nav-sec').appendTo('.js-navbar-collapse');
      $('.js-nav-sec').addClass('navbar-nav');
    }else{
      $('.js-nav-sec').prependTo('.js-header');
      $('.js-nav-sec').removeClass('navbar-nav');
    }

    // adecuacion de carrusel de noticias
    if (window.matchMedia("(min-width: 768px)").matches) {
      $('.js-owl-carousel-noticias-home .owl-stage').addClass('row w-auto');

      $('.js-owl-carousel-noticias-home .owl-item:nth-child(1)').addClass('col-lg-8 w-auto');
      $('.js-owl-carousel-noticias-home .owl-item:nth-child(2)').addClass('col-lg-4 w-auto d-md-flex d-lg-block');

      $('.js-owl-carousel-noticias-home .noticia-resumen-home--2').addClass('mr-md-3 mr-lg-0');
      $('.js-owl-carousel-noticias-home .noticia-resumen-home--3').addClass('ml-md-3 ml-lg-0');

      $('.js-owl-carousel-noticias-home .noticia-resumen-home--3').appendTo('.js-owl-carousel-noticias-home .owl-item:nth-child(2)');
    } 
    /*else{
      $('.js-owl-carousel-noticias-home .noticia-resumen-home--3').appendTo('.js-owl-carousel-noticias-home .owl-item:nth-child(3)');
    }*/
  }

  collapse();

  $(window).resize(function() {
    collapse();
  });
