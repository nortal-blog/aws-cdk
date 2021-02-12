$(document).ready(function() {
    $.ajax({
        url: "api/hello"
    }).then(function(data) {
       $('.greeting-content').append(data.content);
    });
});