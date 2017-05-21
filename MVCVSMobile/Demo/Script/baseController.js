var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $("#txtTimkiem").autocomplete({
            minLength: 0,
            source: function( request, response ) {
                $.ajax( {
                    url: "/Dssanpham/ListName",
                    dataType: "jsonp",
                    data: {
                        term: request.term
                    },
                    success: function( response ) {
                        response( response.data );
                    }
                } );
            },
            focus: function (event, ui) {
                $("#txtTimkiem").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtTimkiem").val(ui.item.label);
                return false;
            }
        })
   .autocomplete("instance")._renderItem = function (ul, item) {
       return $("<li>")
         .append("<div>" + item.label + "<br>" + item.desc + "</div>")
         .appendTo(ul);
   };
    }
}
common.init();