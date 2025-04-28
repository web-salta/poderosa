
function base_url(url) { return '@(Url.Content("~/"))' + url; }
/***************seccion********************************/
var url2 = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/contador";

$(document).ready(function () {
       $("form").submit(function () {
            var response = grecaptcha.getResponse();
            if (response.length == 0) {
                alert('Por favor chequear la Captcha');
                return false;
            } else {
              //  alert('todo bien');*/
               // $("button[name='count_click']").click(function () {
                    $.ajax({
                        type: 'post',
                        dataType: 'text',
                        url: url2,
                        beforeSend: function () {
                        },
                        success: function (result) {
                            alert("1");
                            $("p#count_click").text(result);
                            window.open('https://www.poderosa.com.pe/Content/descargas/libros/historia-de-la-quina-y-la-quinina.pdf', '_blank');
                        },

                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status + ' ' + thrownError);
                        }
                    });
             //});
            }
      });
 });
/*
var onloadCallback = function () {
    grecaptcha.render('html_element', {
        'sitekey': '6Lc_EJUmAAAAAFcJGGYkmWcou8AueS4OTcxVW1E_'
    });
};*/

   /* $("button[name='count_click']").click(function () {

        $.ajax({
            type: 'post',
            dataType: 'text',
            url: url2,
            beforeSend: function () {
            },
            success: function (result) {
                $("p#count_click").text(result);
            },

            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ' ' + thrownError);
            }
        });
    });*/

