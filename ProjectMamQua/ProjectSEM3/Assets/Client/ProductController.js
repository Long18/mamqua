product = {
    init:function() {
       
        product.registerEvent();
    },
    registerEvent:function() {
        
    }
    //loadImages:function() {
    //    $.ajax({
    //        url: '/Product/LoadImages',
    //        data: { id: $('.jcarousel-list-vertical').data('id') },
    //        dataType: 'json',
    //        type: 'GET',
    //        success : function(response) {
    //            var data = response.data;
    //            var html = '';
    //            if (data != null) {
    //                $.each(data, function (i, item) {
    //                    html += '<li><img class=\'cloudzoom-gallery\' src="'+item+'" data-cloudzoom="useZoom: \'#CloudZoom\', image: \''+item+'\', zoomImage: \''+item+'\'"></li>';
    //                });
    //           //  $('.jcarousel-list-vertical').html(html);

    //            }
    //        }
    //});
    //}
}
product.init();