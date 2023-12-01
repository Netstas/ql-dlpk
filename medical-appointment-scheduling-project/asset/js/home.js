$(document).ready(function () {
    $(".center").slick({
        centerMode: true,
        centerPadding: "0px",
        dots: true,
        autoplay: true,
        slidesToScroll: 1,
        slidesToShow: 1,
        autoplay: true,  
        autoplaySpeed: 3000,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: true,
                    centerMode: true,
                    centerPadding: "40px",
                    slidesToShow: 1,
                },
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: true,
                    centerMode: true,
                    centerPadding: "40px",
                    slidesToShow: 1,
                },
            },
        ],
    });

    $(".center-card").slick({
        centerMode: true,
        centerPadding: "0px",
        dots: true,
        autoplay: true,
        slidesToScroll: 1,
        slidesToShow: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        arrows: false,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: true,
                    centerMode: true,
                    centerPadding: "40px",
                    slidesToShow: 1,
                },
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: true,
                    centerMode: true,
                    centerPadding: "40px",
                    slidesToShow: 1,
                },
            },
        ],
    });


    $('.center-slider').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        centerMode: true,
        arrows: true,
        dots: true,
        speed: 300,
        centerPadding: '15px',
        infinite: true,
        //autoplaySpeed: 5000,
        //autoplay: true
    });
    $(".slider-single").slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        useTransform: true,
        asNavFor: ".slider-nav"
    });
    $(".slider-nav").slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        asNavFor: ".slider-single",
        dots: false,
        focusOnSelect: true
    });


});
