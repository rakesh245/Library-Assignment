$(document).ajaxStart(function (a, b, c) {
    //run code when ajax call is started
    //console.log('ajax started');
    $('.div-loader').removeClass('hide');
});
$(document).ajaxComplete(function (a, b, c) {
    //run code when ajax call is completed
    //console.log('ajax ended');
    $('.div-loader').addClass('hide');
});
$(document).ajaxStop(function (a, b, c) {
    //run code when ajax call is stopped
    //console.log('ajax stopped');
    $('.div-loader').addClass('hide');
});
$(document).ajaxError(function (x, h) {
    //run code when ajax call encounters an error
    $('.div-loader').addClass('hide');
    alert('There was a problem. ' + x.statusText);
});