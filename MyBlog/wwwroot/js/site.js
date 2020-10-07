$("#btn-signup").click(function () {
    var formdata = $("#regform").serialize();
    $.ajax({
        method: "post",
        data: formdata,
        url: "/Client/User/Register",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            if (data.success == true) {
                $("#message-form").addClass("alert-success");
                setInterval(function () { location.reload(); }, 1000);
            }
            else {
                $("#message-form").addClass("alert-danger");
            }   
             $("#message-form").text(data.message);
        }
    })
   

})
$("#btn-login").click(function () {
    var formdata = $("#login-form").serialize();
    $.ajax({
        method: "post",
        data: formdata,
        url: "/Client/User/Login",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            if (data.success == true) {
                $("#message-login-form").addClass("alert-success").removeClass("alert-danger");
                setInterval(function () { location.reload(); }, 1000);
            }
            else {
                $("#message-login-form").addClass("alert-danger").removeClass("alert-success");
            }
            $("#message-login-form").text(data.message);
        }
    })


})
