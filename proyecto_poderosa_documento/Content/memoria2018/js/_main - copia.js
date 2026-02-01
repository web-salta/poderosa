$(document).ready(function(){


  // vista movil: menu-sec debajo del menu-pri
  function collapse() {
    if (window.matchMedia("(max-width: 991px)").matches) {
      $('.js-nav-sec').appendTo('.js-navbar-collapse');
      $('.js-nav-sec').addClass('navbar-nav');
    }else{
      $('.js-nav-sec').prependTo('.js-header');
      $('.js-nav-sec').removeClass('navbar-nav');
    }



    /*if (window.matchMedia("(min-width: 768px)").matches) {
      $('.js-owl-carousel--noticias .owl-stage').addClass('row');
      $('.js-owl-carousel--noticias .owl-stage').addClass('w-auto');

      $('.js-owl-carousel--noticias .owl-item:first-child').addClass('col-lg-8 w-auto');
      $('.js-owl-carousel--noticias .owl-item:nth-child(2)').addClass('col-lg-4 w-auto row');

      $('.js-owl-carousel--noticias .noticia-resumen--3').appendTo('.js-owl-carousel--noticias .owl-item:nth-child(2)');

      $('.js-owl-carousel--noticias .noticia-resumen--2').wrap('<div class="col-md-6"></div>');
      $('.js-owl-carousel--noticias .noticia-resumen--3').wrap('<div class="col-md-6"></div>');
    }*/ 
    /*else if (window.matchMedia("(min-width: 992px)").matches) {
      $('.js-owl-carousel--noticias .owl-stage').addClass('row');
      $('.js-owl-carousel--noticias .owl-stage').addClass('w-auto');

      $('.js-owl-carousel--noticias .owl-item:first-child').addClass('col-md-12 col-lg-8');
      $('.js-owl-carousel--noticias .owl-item:nth-child(2)').addClass('col-md-12 col-lg-4');

      $('.js-owl-carousel--noticias .noticia-resumen--3').appendTo('.js-owl-carousel--noticias .owl-item:nth-child(2)');
    }*/
    /*else{
      $('.js-owl-carousel--noticias .owl-stage').removeClass('row');
      $('.js-owl-carousel--noticias .owl-stage').removeClass('w-auto');

      $('.js-owl-carousel--noticias .owl-item:first-child').removeClass('col-lg-8');
      $('.js-owl-carousel--noticias .owl-item:nth-child(2)').removeClass('col-lg-4');

      $('.js-owl-carousel--noticias .noticia-resumen--3').appendTo('.js-owl-carousel--noticias .owl-item:nth-child(3)');
    }*/

    if (window.matchMedia("(min-width: 768px)").matches) {
      /*var maqueta = [
        '<div class="row">',
          '<div class="col-lg-8     js-noticia-1-container">',
            //noticia-1
          '</div>',

          '<div class="col-lg-4">',
            '<div class="row">',
              '<div class="col-md-6 col-lg-12    js-noticia-2-container">',
                //noticia-2
              '</div>',

              '<div class="col-md-6 col-lg-12    js-noticia-3-container">',
                //noticia-3
              '</div>',
            '</div>',
          '</div>',
        '</div>'
      ].join("\n");

      $("#js-noticias-container").append(maqueta);*/

      $('.noticia-resumen--1').appendTo('.js-noticia-1-container');
      $('.noticia-resumen--2').appendTo('.js-noticia-2-container');
      $('.noticia-resumen--3').appendTo('.js-noticia-3-container');
    } else {
      /*var maqueta_2 = [
        '<div class="owl-carousel    js-owl-carousel--noticias   owl-carousel--no-nav owl-carousel--no-dots-md">',
          //notas
        '</div>'
      ].join("\n");

      $("#js-owl-carousel--noticias").append(maqueta_2);*/

      $('.noticia-resumen--1').appendTo('.js-owl-carousel--noticias');
      $('.noticia-resumen--2').appendTo('.js-owl-carousel--noticias');
      $('.noticia-resumen--3').appendTo('.js-owl-carousel--noticias');
    }
  }

  collapse();

  $(window).resize(function() {
    collapse();
  });

  // vista movil: fondo marron de cabecera al abrirse el menu
  // if('.js-navbar-collapse').hasClass('show'){
  //   $('.js-nav-pri')...
  // }



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

  $('.js-owl-carousel--noticias').owlCarousel({
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




});
