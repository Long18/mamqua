
//@* auto convert name to title *@


var slide = {
    init: function () {
        slide.checkAll();
        slide.deleteProductDb();
        slide.DeleteMulDb();
        slide.ajaxStart();
        slide.registerEvent();
    },
    registerEvent: function () {
        $(".btn-active-status-product").off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Slide/ChangeStatus",
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        btn.removeClass('btn-danger');
                        btn.addClass('btn-default');
                        btn.removeClass('glyphicon-lock');
                        btn.addClass('glyphicon-ok');

                        alertify.success("Đã mở khóa");
                    } else {
                        btn.removeClass('btn-default');
                        btn.addClass('btn-danger');
                        btn.removeClass('glyphicon-ok');
                        btn.addClass('glyphicon-lock');
                        alertify.warning("Đã khóa");
                    }
                },
                failure: function (response) {
                    Lobibox.notify('warning', {
                        size: 'mini',
                        rounded: true,
                        delayIndicator: false,
                        msg: 'Đã có lỗi sảy ra'
                    });
                },
                error: function (response) {
                    Lobibox.notify('warning', {
                        size: 'mini',
                        rounded: true,
                        sound: true,
                        delayIndicator: false,
                        msg: 'Bạn không có quyền thực hiện chứ năng này'
                    });
                }
            }, function () { });
        });
    },
    checkAll: function () {
        $("#checkAll").change(function () { //"select all" change
            $(".checkbox").prop('checked', $(this).prop("checked")); //change all ".checkbox" checked status
        });

        //".checkbox" change
        $('.checkbox').change(function () {
            //uncheck "select all", if one of the listed checkbox item is unchecked
            if (false == $(this).prop("checked")) { //if this item is unchecked
                $("#checkAll").prop('checked', false); //change "select all" checked status to false
            }
            //check "select all" if all checkbox items are checked
            if ($('.checkbox:checked').length == $('.checkbox').length) {
                $("#checkAll").prop('checked', true);
            }
        });
    },
   deleteProductDb: function () {
        $('.btn-remove-delete-db').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);//gáng class thành btn
            var id = btn.data('id');//lấy giá trị từ btn và gáng vào id
            alertify.confirm("Xóa", "Bạn có chắc xóa", function () {//show thông báo nếu nhấn yes chạy hàm dưới
                $.ajax({//khởi tạo ajax
                    url: '/Admin/Slide/Delete',//url nhận data
                    data: {id :id },//truyền data 
                    dataType: 'json',//kiểu data
                    type: 'Post',//phương thức của data
                    success: function (respone) {// truyền thành công sẽ nhận giá trị trả về 
                        if (respone.status == true) {//kiểu tra giá trị trả về
                            $('#row_' + id + '').remove();//xóa dòng hiện tại
                            Lobibox.notify('success', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Thành công'
                            });//show thông báo
                        } else {
                            Lobibox.notify('error', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Thất bại'
                            });
                        }
                    },
                    failure: function (response) {
                        alertify.alert("Cảnh báo", 'Đã sảy ra lỗi');
                    },
                    error: function (response) {
                        Lobibox.notify('warning', {
                            size: 'mini',
                            rounded: true,
                            delayIndicator: false,
                            msg: 'Bạn không có quyền thực hiện chứ năng này'
                        });
                    }
                });
            }, function () { });


        });
    },
 
    DeleteMulDb: function () {
        $('#btn-remove-deleteAll-db').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];
            $('input[name="table_records"]:checked').each(function () {
                boxData.push($(this).val());
            });
            if (boxData == '') {
                alertify.alert('Lỗi', 'Không mục nào được chọn!');
            } else {
                alertify.confirm("Xóa", "Bạn có chắc muốn xóa", function () {
                    $.ajax({
                        url: '/Admin/Slide/DeleteSelectedDb',
                        dataType: 'json',
                        type: 'POST',
                        data: { ids: boxData.join(',') },
                        success: function (respone) {
                            if (respone.status == true) {
                                $.each(boxData, function (i, item) {
                                    $('#row_' + item).remove();
                                });
                                Lobibox.notify('success', {
                                    size: 'mini',
                                    rounded: true,
                                    delayIndicator: false,
                                    msg: 'Xóa thành công'
                                });
                            } else {
                                Lobibox.notify('error', {
                                    size: 'mini',
                                    rounded: true,
                                    delayIndicator: false,
                                    msg: 'Xóa thất bại'
                                });
                            }
                        },
                        failure: function (response) {
                            alertify.alert("Cảnh báo", 'Đã sảy ra lỗi');
                        },
                        error: function (response) {
                            Lobibox.notify('warning', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Bạn không có quyền thực hiện chứ năng này'
                            });
                        }
                    });
                });
            }


        });
    },
    ajaxStart : function() {
        $(document).ajaxStart(function() {
            $('.loadingModel').show();
        });
        $(document).ajaxStop(function () {
            $('.loadingModel').hide();
        });
    },
    loadImages:function() {
       
    }
}
slide.init();
