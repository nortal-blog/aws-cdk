$(document).ready(function() {
    $.ajax({
        url: "api/hello/"
    }).then(function(data) {
        console.log(data);
       $('.hello-content').append(data);
    });

    $.ajax({
        url: "api/world/"
    }).then(function(data) {
        console.log(data);
       $('.world-content').append(data);
    });
});