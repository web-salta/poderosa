$(document).ready(function () {

    /***********************************ACORDEON***********************************/
    /******************************************************************************/
    //Horizontal Tab
    $('#parentHorizontalTab2').easyResponsiveTabs({
        type: 'default', //Types: default, vertical, accordion
        width: 'auto', //auto or any width like 600px
        fit: true, // 100% fit in a container
        tabidentify: 'hor_1', // The tab groups identifier
        color: '#ffff',
        //bg_color: '#ffffff66',
        bg_color: '#e8c866',
        activetab_bg: '#494941',
        inactive_bg: '#e8c866',



        activate: function (event) { // Callback function if tab is switched
            //alert("aaa");
            $('#ver_1_tab_item-0').show();
            console.log($(this));
            var $tab = $(this);
            var $info = $('#nested-tabInfo');
            var $name = $('span', $info);
            $name.text($tab.text());
            $info.show();
        }
    });

    // Child Tab
    var cantidad = $("#cantidad_ano").val();
    if (cantidad > 0) {
        for (var i = 1; i <= cantidad; i++) {
            $('#ChildVerticalTab2_' + i).easyResponsiveTabs({
                type: 'vertical',
                width: 'auto',
                fit: true,
                tabidentify: 'ver_' + i, // The tab groups identifier
                // activetab_bg: '#fff', // background color for active tabs in this group
                //inactive_bg: 'transparent', // background color for inactive tabs in this group
                //active_border_color: '#fff', // border color for active tabs heads in this group
                //active_content_border_color: '#5AB1D0' // border color for active tabs contect in this group so that it matches the tab head border

                activetab_bg: '#caa326', // background color for active tabs in this group
                inactive_bg: 'transparent', // background color for inactive tabs in this group
                active_border_color: '#fff', // border color for active tabs heads in this group
                border: '1px solid #fff',
                color: '#ffff',

            });
        }
    }


    /***********************************FN ACORDEON***********************************/
    /******************************************************************************/

});
