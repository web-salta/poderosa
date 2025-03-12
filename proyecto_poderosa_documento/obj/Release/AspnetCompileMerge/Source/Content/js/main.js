$(document).ready(function () {

    /*
    document.getElementById("popup").addEventListener("click", popup_fn);
    function popup_fn() {
        alert("entre");
        window.open('https://www.poderosa.com.pe/Content/noticias/comunicado-para-realizar-negociaciones-via-factoring-o-confirming/comunicado-para-realizar-negociaciones-via-factoring-o-confirming.pdf')
        window.open('https://www.poderosa.com.pe/Content/noticias/cierre-de-informacion-contable-ejercicio-2022/comunicado-cierre-informacion-contable-ejercicio2022.pdf', '_blank');
    }*/

  // OWL CAROUSEL - INICIO  
  $('.js-owl-carousel-fotos').owlCarousel({
    items:  1,
    margin: 0,

    // dots: false,
    nav: true,

    loop: true,
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: true,
    smartSpeed: 750,
    // lazyLoad: true,
  });

  $('.js-owl-carousel-noticias-home').owlCarousel({
    // items:  1,
    margin: 0,

    // dots: false,
    nav: true,

    // loop: true,
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: true,
    smartSpeed: 750,
    // lazyLoad: true,

    // rewind: true,

    responsive: {
      0 : {
          items:  1,
          loop: true,
      },
      768 : {
          items:  3,
          loop: false,
      },
    }
  });
  // OWL CAROUSEL - FIN





  function collapse() {
      // HEADER
      var position = $(window).scrollTop();

      $(window).scroll(function () {
          var scroll = $(window).scrollTop();

          // if(scroll > 0){
          //   $("header").addClass("fixed-top");
          // } else {
          //   $("header").removeClass("fixed-top");
          // }

          if (scroll > position) {
              //console.log('scrollDown');
              $('header').addClass('js-header-scrollDown');
          } else {
              //console.log('scrollUp');
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

  // vista movil: fondo marron de cabecera al abrirse el menu
  // if('.js-navbar-collapse').hasClass('show'){
  //   $('.js-nav-pri')...
    // }



});
