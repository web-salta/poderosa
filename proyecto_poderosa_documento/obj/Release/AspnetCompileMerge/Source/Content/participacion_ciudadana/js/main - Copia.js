function base_url(url) { return '@(Url.Content("~/"))' + url; }
/***************seccion********************************/
//var url = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/contador";
var url = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/contador";
var url2 = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/lector";
var url3 = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/redirect";
$(document).ready(function () {

    $.ajax({
        type: 'post',
        dataType: 'text',
        url: url2,
        beforeSend: function () {
        },
        success: function (result) {
            $("p#count_click").text(result);
           // window.open('https://www.poderosa.com.pe/Content/descargas/libros/historia-de-la-quina-y-la-quinina.pdf', '_blank');
        },

        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ' ' + thrownError);
        }
    });
   /* $("form").submit(function () {
        var response = grecaptcha.getResponse();
        if (response.length == 0) {
            alert('Por favor chequear la Captcha');
            return false;
        } else {
            $.ajax({
                type: 'post',
                dataType: 'text',
                url: url,
                beforeSend: function () {
                },
                success: function (result) {
                    alert("1");
                    $("p#count_click").text(result);
                   // window.open('https://www.poderosa.com.pe/Content/descargas/libros/historia-de-la-quina-y-la-quinina.pdf', '_blank');
                    $.redirect("https://www.poderosa.com.pe/Content/descargas/libros/historia-de-la-quina-y-la-quinina.pdf", {}, "POST", "_blank");
                },

                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status + ' ' + thrownError);
                }
            });
        }
    });*/


});
$("button[name='count_click']").click(function () {
   // alert("uuu");
    $.ajax({
        type: 'post',
        dataType: 'text',
        url: url,
        beforeSend: function () {
        },
        success: function (result) {
            $("p#count_click").text(result);
            window.open('https://www.poderosa.com.pe/Content/descargas/libros/historia-de-la-quina-y-la-quinina.pdf', '_blank');
        },

        error: function (xhr, ajaxOptions, thrownError) {
           alert(xhr.status + ' ' + thrownError);
        }
    });
})

