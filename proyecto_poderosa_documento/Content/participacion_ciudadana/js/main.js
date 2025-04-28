function base_url(url) { return '@(Url.Content("~/"))' + url; }
/***************seccion********************************/
//var url = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/contador";
var url = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/contador";
var url2 = "http://localhost:50612/proyecto_poderosa_documento/participacion_ciudadana/lector";

$("button[name='count_click']").click(function () {
    $.ajax({
        type: 'post',
        dataType: 'text',
        url: url,
        beforeSend: function () {
            $('#count_click').prop('disabled', true);
        },
        success: function (result) {
            $("#count_click").text(result);
            $('#click').prop('disabled', false);
            window.open('https://www.poderosa.com.pe/Content/participacion_ciudadana/descargas/triptico-informativo-etapa-antes-2meia-i.pdf', '_blank');
        },

        error: function (xhr, ajaxOptions, thrownError) {
           alert(xhr.status + ' ' + thrownError);
        }
    });
})

$.ajax({
    type: 'post',
    dataType: 'text',
    url: url2,
    beforeSend: function () {
    },
    success: function (result) {
        $("#count_click").text(result);
    },

    error: function (xhr, ajaxOptions, thrownError) {
        alert(xhr.status + ' ' + thrownError);
    }
});