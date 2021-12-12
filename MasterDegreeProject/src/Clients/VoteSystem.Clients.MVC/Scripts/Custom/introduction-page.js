'use strict';

$(document).ready(function () {
    var windowWidth = $(window).width();

    scaleVideoContainer();

    initBannerVideoSize('.video-container .poster img');    
    initBannerVideoSize('.video-container video');
   
    $(window).on('resize', function () {
        scaleVideoContainer();
        scaleBannerVideoSize('.video-container .poster img');        
        scaleBannerVideoSize('.video-container video');       
    });

    $('video').on('ended', function () {
        this.load();
        this.play();
    });

    /* affix the navbar after scroll below header */
    $('#nav').affix({
        offset: {
            top: $('header').height() - $('#nav').height()
        }
    });

    /* highlight the top nav as scrolling occurs */
    $('body').scrollspy({
        target: '#nav'
    })

    /* smooth scrolling for scroll to top */
    $('.scroll-top').click(function () {
        $('body').animate({
            scrollTop: 0
        }, 500);
    })

    /* smooth scrolling for nav sections */
    $('#nav .navbar-nav li>a').click(function () {       
        var link = $(this).attr('href');
        var posi = $(link).offset().top;

        $('body').animate({
            scrollTop: posi
        }, 500);
    });

    $('#float-menu-btn').on('click', function () {
        $('#navigation-menu-btn').removeClass('pull-right').addClass('text-center');
        $('#title-menu-btn').removeClass('pull-left').addClass('text-center');
    });    
   
    $('.navbar-header')
        .width(windowWidth)
        .css({
            'margin-left': '0',
            'margin-right': '0'
        });
    
    $('footer')
        .width(windowWidth - 40)
        .css({
            'margin-left': '0',
            'margin-right': '0px'
        });
   
    $(window).resize(function () {
        windowWidth = $(window).width();
        $('.navbar-header')
           .width(windowWidth)
           .css({
               'margin-left': '0',
               'margin-right': '0'
           });

        $('footer')
         .width(windowWidth - 40)
         .css({
             'margin-left': '0',
             'margin-right': '0'
         });

        /* smooth scrolling for nav sections */
        $('#nav .navbar-nav li>a').click(function () {
            var link = $(this).attr('href');
            var posi = $(link).offset().top;

            $('body').animate({
                scrollTop: posi
            }, 500);
        });
    });

     var navMain = $("#nav-main");
     navMain.on("click", "a", null, function () {
         navMain.collapse('hide');
     });
});

function scaleVideoContainer() {
    var height = $(window).height();
    var unitHeight = parseInt(height) + 'px';
    $('.homepage-hero-module').css('height', unitHeight);
}

function initBannerVideoSize(element) {    
    $(element).each(function () {
        $(this).data('height', $(this).height());
        $(this).data('width', $(this).width());
    });

    scaleBannerVideoSize(element);
}

function scaleBannerVideoSize(element) {
    var windowWidth = $(window).width(),
        windowHeight = $(window).height(),
        videoWidth,
        videoHeight;    

    $(element).each(function () {
        var videoAspectRatio = ($(this).data('height')  + 125) / $(this).data('width'),
            windowAspectRatio = windowHeight / windowWidth;        
        if (videoAspectRatio > windowAspectRatio) {
            videoWidth = windowWidth;
            videoHeight = videoWidth * videoAspectRatio;
            $(this).css({
                'top': -(videoHeight - windowHeight) / 2 + 'px',
                'margin-left': 0
            });
        } else {
            videoHeight = windowHeight;
            videoWidth = videoHeight / videoAspectRatio;
            $(this).css({
                'margin-top': 0,
                'margin-left': -(videoWidth - windowWidth) / 2 + 'px'
            });
        }

        $(this).width(videoWidth).height(videoHeight);

        $('.homepage-hero-module .video-container video').addClass('fadeIn animated');
    });
}