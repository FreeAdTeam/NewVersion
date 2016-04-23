function selectCategory(e) {
    var id = $(e).attr("data-id");
    $("#SearchTwo").val(id);
    $("#categoryName").text($(e).attr("data-name"));
}

$(document).ready(function () {
    $('#status').fadeOut(); // will first fade out the loading animation
    $('#preloader').delay(350).fadeOut('slow'); // will fade out the white DIV that covers the website.
    $('body').delay(350).css({ 'overflow': 'visible' });
    //$(".datecontrol").datepicker();
    $('[data-toggle="tooltip"]').tooltip({ html: true })
    //new WOW().init();
    $("[data-header]").on("click", function (e) {
        e.preventDefault();
        var orderValue = $(this).attr("data-header-val");
        var orderDirection = $("#OrderDirection").val();
        if (orderDirection == "asc") {
            $("#OrderDirection").val("desc");
        } else {
            $("#OrderDirection").val("asc");
        }
        $("#ChangeOrderDirection").val(true)
        $("#orderBy").val(orderValue);
        var form = $("#searchform");
        //alert(orderValue);
        form.submit();
    });
    $(window).bind('scroll', function () {
        var navHeight = $(window).height() - 400;
        if ($(window).scrollTop() > navHeight) {
            $('.navbar-default').addClass('on');
        } else {
            $('.navbar-default').removeClass('on');
        }
    });

    $('body').scrollspy({
        target: '.navbar-default',
        offset: 80
    })
});
function showTableDetail(e) {
    $(e).parent().parent().next().slideToggle('fast');
}

function resetSortDirectionAndSubmit() {
    $("#ChangeOrderDirection").val(false)
    $("#searchform").submit();
}

$.ajaxSetup({ cache: false });

function openModal() {
    $("body").css("overflow", "hidden");
    $("#fullModal").modal('show');
}
function dismiss() {
    $("body").css("overflow", "visible");
}