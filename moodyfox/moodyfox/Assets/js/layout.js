$(document).ready(function () {

    
   
    applyJQuery() 
    function applyJQuery() {
        if ($(window).width() <= 991) {
            $(".navbar-collapse").removeClass("wow animate__ animate__fadeInUp animate__animated animated")
        } 
    }
    var $navbar = $(".navbar-collapse").animate({ left: "-100%" }, 300)
    $(".mob-menu").click(function (e) {
        e.preventDefault();
        ;

        if ($navbar.is(":visible")) {
            $navbar.animate({ left: "-100%" }, 300, function () {
                $navbar.hide();
            });
        } else {
            $navbar.show().animate({ left: "0" }, 300);
        }
    });

    $(".contactmenu").click(function () {
        $('html, body').animate({
            scrollTop: $(".contect-form").offset().top
        }, 3000); 
    });


});