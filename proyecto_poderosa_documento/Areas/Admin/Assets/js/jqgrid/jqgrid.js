/*$(function () {*/
$(function () {

    var $grid = $("#listGrid"),

    editingRowId,
    myEditParam = {
        keys: true,
        oneditfunc: function (id) {
            editingRowId = id;
        },
        afterrestorefunc: function (id) {
            editingRowId = undefined;
        }
    };


    /*-----------------*/
    var countries1 = { '0': 'Ninguno', '1': '1', '2': '2', '3': '3', '4': '4' };
    var myarray = [];

    $grid.jqGrid({
        url: "/noticia/GetTodoLists",
        datatype: 'json',
        mtype: 'GET',
        // loadonce: true,
        colNames: ['Id', 'Titulo', 'Foto', 'Fecha', 'Orden', 'Principal', '', ''],
        colModel: [
            { key: true, hidden: false, name: 'id_noticia', index: 'id_noticia', editable: false, align: "center", width: 5, sortable: true },
            { key: false, name: 'titulo_espanol', index: 'titulo_espanol', editable: false, width: 100, formatter: textoconcatenado, label: "nuevo" },
            {
                key: false, name: 'foto', index: 'foto', editable: false, width: 20, align: "center", formatter: function (cellvalue, options, rowObject) {
                    return "<img width='55px' height='55px' src='../../noticias/" + rowObject.foto + "' />";
                }
            },

            { key: false, name: 'fecha', index: 'fecha', align: "center", editable: false, width: 19 },
            {
                name: 'orden', index: 'orden', width: 10, align: 'center', editable: true, formatter: 'select',
                edittype: 'select',
                editoptions: {
                    value: countries1,
                    multiple: false,
                    defaultValue: '0',
                    // AM - Aqui defines los controladores de eventos para la lista desplegable
                    dataEvents: [
                      // AM - el controlador para el evento onchange
                      {
                          type: 'change',
                          fn: function (e) {
                              // AM - Obtén el id de la fila en la que se encuentra la lista desplegable
                              var rowId = $(e.target).closest('tr').attr('id');
                              // AM - Guarda los valores de la fila
                              $grid.saveRow(rowId);
                          }
                      }
                    ]
                }
            },

              {
                  name: 'principal', index: 'principal', width: 75, align: 'center', formatter: 'checkbox', width: 8,
                  edittype: 'checkbox', formatoptions: { disabled: false }, editable: true, editoptions: { value: '1:0', defaultValue: 'Yes' }
              },
             {
                 name: 'btneditar', index: 'btneditar', width: 8, sortable: false,
                 formatter: function (cellvalue, options, rowObject) {
                     return "<a href='/admin/noticia/editar/" + rowObject.id_noticia + "' class='btn btn-info active btn-xs' >Editar</a>";
                 }
             },
             {
                 name: 'btneliminar', index: 'btneliminar', width: 9, sortable: false,
                 formatter: function (cellvalue, options, rowObject) {
                     return "<a href='#'  class='btn btn-danger  active btn-xs' onclick='AbrirEliminarnoticia(" + rowObject.id_noticia + ")' >Eliminar</a>";
                 }
             }

        ],

        pager: '#pager',
        rowNum: 20,
        rowList: [30, 40, 50, 10000],
        sortname: 'id',
        //sortorder: 'asc',
        viewrecords: true,
        gridview: true,
        height: "100%",
        width: "1369",
        //autowidth: true,
        editurl: 'clientArray',
        onSelectRow: function (id) {
            if (editingRowId) {
                $(this).jqGrid('restoreRow', editingRowId, myEditParam);
            }
            $(this).jqGrid('editRow', id, myEditParam);
        },

        beforeSelectRow: function (rowid, e) {
            var $radio = $(e.target).closest('tr').find('input[type="radio"]');
            $radio.attr('checked', 'checked');
            return true; // allow row selection
        },        beforeShowForm: function (e) {
            var form = $(e[0]);
            form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
            style_edit_form(form);
        },
        //autowidth: true,
        multiselect: false,
        caption: "Lista de Noticias"
    });


    $.widget("ui.dialog", $.extend({}, $.ui.dialog.prototype, {
        _title: function (title) {
            var $title = this.options.title || '&nbsp;'
            if (("title_html" in this.options) && this.options.title_html == true)
                title.html($title);
            else title.text($title);
        }
    }));

})



//$("#pager").css({ "height": "0px", "border": "0px" });


function textoconcatenado(cellvalue, options, rowObject) {
    return "<b>Titulo Español:</b>&nbsp;" + rowObject.titulo_espanol + "\n" + "<b>Titulo Ingles: &nbsp;</b>" + rowObject.titulo_ingles;

}


function Editarnoticia(cellvalue) {
    return "<input type='button'  id=div" + cellvalue + "  class='button button-info' title='Eliminar Noticia' onclick=AbrirEditarnoticia('" + cellvalue + "'); value='Editar' />";
}

function AbrirEditarnoticia(valoreditcliente) {

    $.ajax({
        type: 'POST',
        dataType: 'text',
        //  url: 'admin/noticia/crud?id=' + valoreditcliente,
        url: "/admin/noticia/crud/" + valoreditcliente,
        beforeSend: function () {
        },
        success: function (resulteditprodu) {
            //$("#dialog-eliminareditprodu").dialog("close");
            Recargar_grilla()
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ' ' + thrownError);
        }
    });

}


function AbrirEliminarnoticia(valoreditcliente) {
    $("#dialog-eliminarenoticia").removeClass('hide').dialog({
        resizable: false,
        width: '320',
        modal: true,
        title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa fa-exclamation-triangle red'></i> Eliminar Noticia</h4></div>",
        title_html: true,
        buttons: [
            {
                html: "<i class='ace-icon fa fa-trash-o bigger-110'></i>&nbsp; Eliminar",
                "class": "btn btn-danger btn-minier",
                click: function () {
                    $.ajax({
                        type: 'post',
                        dataType: 'text',
                        url: '/noticia/eliminar/?id=' + valoreditcliente,


                        beforeSend: function () {
                        },
                        success: function (resulteditcliente) {

                            $("#dialog-eliminarenoticia").dialog("close");
                            Recargar_grilla();
                        },

                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status + ' ' + thrownError);
                        }
                    });

                }
            }
            ,
            {
                html: "<i class='ace-icon fa fa-times bigger-110'></i>&nbsp; Cancelar",
                "class": "btn btn-minier",
                click: function () {
                    $(this).dialog("close");
                }
            }
        ]
    });


}

function Recargar_grilla() {
    $("#listGrid").jqGrid({
        page: 1,
        url: 'admin/noticia',
        datatype: "json"
    }).trigger("reloadGrid");

    return [true, ''];
}


var message;
var newarray_bd = [];

var compareArray = new Array();
var array_rechazados = new Array();
var id = "";
var mensaje_total = "";

function verificaritemrepetidos(myarray) {
    id = "";
    compareArray.length = 0;
    array_rechazados.length = 0;


    if (myarray.length >= 1) {
        for (i = 0; i < myarray.length; i++) {

            if (compareArray.indexOf(myarray[i]) == -1) {
                compareArray.push(myarray[i]);
            } else {
                //ahora formamos el array del numeros repetidos
                //hacemos el if para formar un solo numero repetido 2,2,4,4,1
                //una vez que se filtre el array quedara asi 2,4,1
                if (array_rechazados.indexOf(myarray[i]) == -1) {
                    array_rechazados.push(myarray[i]);
                }
            }
        }

        if (array_rechazados.length >= 1) {
            mensaje_total = "El numero orden " + array_rechazados + " para noticias ya existe";
            newarray_bd.length = 0;


            $("#contenido_dialog").html(mensaje_total);
            /*--dialogo para campo principal--*/
            $("#dialog_orden_repetido").dialog({
                autoOpen: false
            });
            $("#dialog_orden_repetido").dialog("destroy");
            $("#dialog_orden_repetido")
              .removeClass('hide')
              .dialog(
                      {
                          resizable: false,
                          width: '320',
                          modal: true,
                          title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa  fa-check green'></i>Alerta</h4></div>",
                          title_html: true,
                          buttons: [

                                  {
                                      html: "<i class='ace-icon fa fa-times bigger-110 '></i>&nbsp; Aceptar",
                                      "class": "btn btn-minier",
                                      click: function () {
                                          $(this).dialog("close");
                                      }
                                  }]
                      });
            /*---- */
        }

    }
    return array_rechazados;
}


function grabar() {
    var batch = new Array();
    //Get ids for all current rows
    //var dataIds = $('#listGrid').jqGrid('getDataIDs');
    var dataIds = $("#listGrid").jqGrid('getDataIDs');
    //alert(dataIds);
    /*----------listar arrays---------*/
    compareArray.length = 0;
    newarray_bd.length = 0;
    id = "";

    var myIDs = $("#listGrid").jqGrid('getDataIDs');
    for (var i = 0; i < myIDs.length; i++) {
        var myRow = $('#listGrid').jqGrid('getRowData', myIDs[i]);
        //obteniendo los valores del las noticias español e ingles (3)
        //valor_select_orden = document.getElementById(myRow.id_noticia + "_orden").value;
        valor_select_orden = myRow.orden;
        if (valor_select_orden != 0) {
            newarray_bd.push(parseInt(valor_select_orden, 10));
        }
    }
    var flag_grabar = verificaritemrepetidos(newarray_bd);
    /*---------------------------------*/

    for (var i = 0; i < dataIds.length; i++) {
        try {
            //Save row only to the grid 
            var d = $('#listGrid').jqGrid('saveRow', dataIds[i], false, 'clientArray');

            //Get row data
            var data = $('#listGrid').jqGrid('getRowData', dataIds[i]);

            //Data doesn't contains actual id
            // data.Id = dataIds[i];
            // 
            //Add data to the batch
            delete data["btneditar"];
            delete data["btneliminar"];
            // debugger
            batch.push(data);
        }
        catch (ex) {
            //If you are using editRules it might end up with exception
            $('#listGrid').jqGrid('restoreRow', dataIds[i]);
        }
    }

    var lista_ = batch;
    var count = 0;
    $.each(lista_, function (i, item) {
        if (item.principal == 1) {
            count++
        }
    });

    if (count >= 2) {
        //  alert("Por favor, solamente seleccione una noticia principal, gracias");
        /*--dialogo para campo principal--*/
        $("#dialog_principal").dialog({
            autoOpen: false
        });
        $("#dialog_principal").dialog("destroy");
        $("#dialog_principal")
          .removeClass('hide')
          .dialog(
                  {
                      resizable: false,
                      width: '320',
                      modal: true,
                      title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa  fa-check green'></i>Alerta</h4></div>",
                      title_html: true,
                      buttons: [

                              {
                                  html: "<i class='ace-icon fa fa-times bigger-110 '></i>&nbsp; Aceptar",
                                  "class": "btn btn-minier",
                                  click: function () {
                                      $(this).dialog("close");
                                  }
                              }]
                  });
        /*---- */



        $('#listGrid').trigger('reloadGrid');
    } else {
        if (flag_grabar.length < 1) {
            $.ajax({
                contentType: 'application/json',
                type: 'POST',
                //url: @Url.Action("actualizar", "noticia"),
                url: "/noticia/actualizar",
                dataType: 'json',
                data: JSON.stringify(batch),
                //data: JSON.stringify( lista_),
                success: function (result) {
                    $('#listGrid').trigger('reloadGrid');
                    /**/
                    /*message = '<div class="alert alert-success alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button> La grabación ha sido sastifactoria, gracias.</div>';

                   $('div#mensaje').html(message);
                   $("div#mensaje").fadeTo(2000, 500).slideUp(500, function () {
                       $("div#mensaje").alert('close');
                   });*/
                    //  alert("La grabación ha sido sastifactoria, gracias.");
                    /*--dialogo para la grabación--*/
                    $("#dialog_grabar").dialog({
                        autoOpen: false
                    });
                    $("#dialog_grabar").dialog("destroy");
                    $("#dialog_grabar")
                      .removeClass('hide')
                      .dialog(
                              {
                                  resizable: false,
                                  width: '320',
                                  modal: true,
                                  title: "<div class='widget-header'><h4 class='smaller'><i class='ace-icon fa  fa-check green'></i> Grabación Sastifactoria</h4></div>",
                                  title_html: true,
                                  buttons: [

                                          {
                                              html: "<i class='ace-icon fa fa-times bigger-110 '></i>&nbsp; Aceptar",
                                              "class": "btn btn-minier",
                                              click: function () {
                                                  $(this).dialog("close");
                                              }
                                          }]
                              });
                    /*---- */
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // $('#listGrid').trigger('reloadGrid');
                    //alert('Error aaa: ' + errorThrown);
                }
            });

        } else {
            $('#listGrid').trigger('reloadGrid');
        }
    }
}
