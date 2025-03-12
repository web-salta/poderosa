$(document).ready(function () {

    $.validator.addMethod("emailmetodo", function (value, element) {
        // allow any non-whitespace characters as the host part
        return this.optional(element) || /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);
    }, 'Por favor, escribe un correo v&aacute;lido');


    jQuery.validator.messages.required = "";

    var validate = $('#formulario-contacto').validate({
        rules: {
            telefono: {
                digits: true
            },
            edad: {
                digits: true
            },
            email: {
                emailmetodo: true
            },

        },
        invalidHandler: function (e, validator) {
            var errors = validator.numberOfInvalids();
            if (errors) {
                var message = errors == 1
					? 'You missed 1 field. It has been highlighted below'
					: 'You missed ' + errors + ' fields.  They have been highlighted below';
                $("div.error span").html(message);
                $("div.error").show();
            } else {
                $("div.error").hide();
            }
        },
        onkeyup: false,
        submitHandler: function () {
            $("div.error").hide();
           // alert("submit! use link below to go to the other step");
            enviaremail();

        },
        messages: {
            solicito: {
                required: "( Seleccione alguna opci&oacuten )"
            },
            password2: {
                required: " ",
                equalTo: "Please enter the same password as above"
            },
            edad: {
                required: " ",
                edad: "Solo digitos"
            }

        },
        errorPlacement: function (error, element) {
            //alert(element);
            if (element.attr("name") == "solicito") {
                error.insertAfter('#radio');
            } else {
                error.insertAfter(element);
            }

        }
    });



    /*$('input[name="solicito"]').rules("add", "required");*/


    $("#cc_type").change(
      function () {
          switch ($(this).val()) {
              case 'amex':
                  creditcard.unmask().mask("9999 999999 99999");
                  break;
              default:
                  creditcard.unmask().mask("9999 9999 9999 9999");
                  break;
          }
      }
    );



    jQuery.extend(jQuery.validator.messages, {
        remote: "Por favor, rellena este campo.",
        // email: "Por favor, escribe una direcci&oacute;n de correo v&aacute;lida",
        number: "Por favor, escribe un número entero válido.",
        digits: "S&oacute;lo d&iacute;gitos.",

        maxlength: jQuery.validator.format("Por favor, no escribas más de {0} caracteres."),
        minlength: jQuery.validator.format("Por favor, no escribas menos de {0} caracteres."),
        rangelength: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1} caracteres."),
        range: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1}."),
        max: jQuery.validator.format("Por favor, escribe un valor menor o igual a {0}."),
        min: jQuery.validator.format("Por favor, escribe un valor mayor o igual a {0}.")
    });


});



//var form = $(this).closest("form");


$('#limpiar').on('click', function (e) {


    var validator = $("#formulario-contacto").validate();
    validator.resetForm();

    $("#nombre").val("");
    $("#apellidos").val("");
    $("#distrito").val("");
    $("#email").val("");
    $("#universidad").val("");
    $("#especialidad").val("");
    $("#departamento").val("");
});

