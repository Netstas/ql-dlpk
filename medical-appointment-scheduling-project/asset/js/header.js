$(document).ready(function () {
    var headerTop = $('.header-top');
    var headerCenter = $('.header-center');
    var headerFooter = $('.header-footer');

    $(window).scroll(function () {
        var scroll = $(window).scrollTop();

        if (scroll > 0) {
            headerTop.addClass('hidden');
            headerCenter.addClass('hidden');
            headerFooter.addClass('sticky');
        } else {
            headerTop.removeClass('hidden');
            headerCenter.removeClass('hidden');
            headerFooter.removeClass('sticky');
        }
    });
    const hiddenDiv = $('#hidden');
    const envelopeIcon = hiddenDiv.find('.fa-regular.fa-envelope');
    const minusIcon = hiddenDiv.find('.fa-solid.fa-minus.minus');

    const form = $('.form');

    hiddenDiv.click(function () {
        if (form.hasClass('active')) {
            form.removeClass('active');
            envelopeIcon.show();
            minusIcon.hide();
        } else {
            form.addClass('active');
            envelopeIcon.hide();
            minusIcon.show();
        }
    });
});

