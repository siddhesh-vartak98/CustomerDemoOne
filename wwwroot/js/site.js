$(document).ready(function () {
    setTimeout(function () {
        $('.alert-success').alert('close');
    }, 7000);

    $(".isActiveModel").click(function () {

        alert();

        $("#statusID").val($(this).attr("data-id"));

        var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), {
            keyboard: false
        })

        myModal.show()
        
    });
});