
window.onload = function () {
    var wtf = $('#user_content');
    var height = wtf[0].scrollHeight;
    wtf.scrollTop(height);
};




document.getElementById('user_content').onkeydown = function (event) {
    if (event.keyCode == 13) {
        alert('5');
    }
}
function DoAjaxPostAndMore(btnClicked) {
    var $form = $(btnClicked).parents('form');

    $.ajax({
        type: "POST",
        url: $form.attr('Teacher'),
        data: {
            message: $("#MSG").val(),
        },
        error: function (xhr, status, error) {
            //do something about the error

        },

        success: function () {
            //do something with response
            var url = '/Chat/ReturnStudentNewMessages/';
            $('#user_content').load(url);
            $('#MSG').val("");
            $.ajax({
                success: function () {
                    setTimeout(function () {
                        var wtf = $('#user_content');
                        height = wtf[0].scrollHeight;
                        wtf.scrollTop(height);
                    }, 1000);
                }
            })
        }
    });


    return false;// if it's a link to prevent post
}

setInterval(function () {
    $(function () {
        var url = "http://localhost:58776/chat/newmessagestatus";
        $.ajax(url,
{
    statusCode: {
        200: function () {
            var url = '/Chat/ReturnStudentNewMessages/';
            $('#user_content').load(url);
            $.ajax({
                success: function () {
                    setTimeout(function () {
                        var wtf = $('#user_content');
                        height = wtf[0].scrollHeight;
                        wtf.scrollTop(height);
                    }, 1000);
                }
            })
        }
    },
});
    });
}, 4000);

