$(document).ready(function () {

  // OWL CAROUSEL - INICIO  
  $('.js-owl-carousel-fotos').owlCarousel({
    items:  1,
    margin: 0,

    dots: true,
    nav: true,

    loop: true,
    autoplay: true,
    autoplayTimeout: 3000,
    /*autoplayHoverPause: true,*/

    URLhashListener:true,
    autoplayHoverPause:true,
    startPosition: 'URLHash',

    smartSpeed: 750,
    // lazyLoad: true,
  });

    // OWL CAROUSEL - INICIO  
    $('.js-owl-carousel-slider').owlCarousel({
        items: 1,
        margin: 0,

        dots: true,
        nav: true,

        loop: true,
        autoplay: false,
        autoplayTimeout: 3000,
        /*autoplayHoverPause: true,*/


        autoplayHoverPause: true,

        smartSpeed: 750,
        // lazyLoad: true,
    });

});
