order = {
    init: function () {
        order.registerEvent();
        order.checkAll();
        order.updateOrder();
        order.ajaxStart();
        order.deleteMulOrder();
        order.deleteOrder();
    },
    registerEvent: function () {

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
    }, updateOrder: function () {
        $('#btn-updateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];//khỏi tạo mảng rỗng 
            var status = $('#status').val();
            var payment = $('#payment').val();
            $('input[name="table_records"]:checked').each(function () {//lấy giá trị mỗi input được check
                boxData.push($(this).val());//thêm giá trị vào mảng 
            });
            if (boxData == '') {//kiểm tra nếu mảng rỗng thì show thông báo
                alertify.alert('Lỗi', 'Bạn phải chọn đơn hàng để cập nhập');
            }else if (payment == '' && status == '') {
                alertify.alert('Lỗi', 'Bạn phải tình trạng thanh toán hoặc tình trạng đơn hàng!');
            }
            else {//mảng có giá trị thực hiện hàm bên dưới
                    $.ajax({
                        url: '/Admin/Order/UpdateOrder',
                        dataType: 'json',
                        type: 'POST',
                        data: { ids: boxData.join(','), status: status, payment: payment},//chuyển mảng thành một chuỗi và truyền đi
                        success: function (respone) {
                            if (respone.status == true) {
                                location.reload();
                            } else {
                                Lobibox.notify('error', {
                                    size: 'mini',
                                    rounded: true,
                                    delayIndicator: false,
                                    msg: 'Cập nhập thất bại'
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
               

            }

        });
    },
    deleteMulOrder:function() {
        $('#btn-deleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];
            $('input[name="table_records"]:checked').each(function () {
                boxData.push($(this).val());
            });
            if (boxData == '') {
                alertify.alert('Lỗi', 'Không mục nào được chọn!');
            } else {
                alertify.confirm("Hủy xóa", "Mục này sẽ được lấy lại", function () {
                    $.ajax({
                        url: '/Admin/Order/Delete',
                        dataType: 'json',
                        type: 'Post',
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
                }, function () { });
            }


        });
    },
    deleteOrder:function() {
        $('.deleteOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);//lay class tren gang vao btn
            var id = btn.data('id');//lay gia tri data-id 
            alertify.confirm('Xoá', 'Mục này sẽ được chuyển vào thùng rác', function () {
                $.ajax({
                    url: '/Admin/Order/Delete/',
                    data: { ids: id },//set data truyền đi
                    dataType: 'json',//set kiểu của giá trị truyền đi
                    type: 'POST',//set kiểu phương th/ức
                    success: function (respone) {
                        if (respone.status == true) {//nếu thành công thì sẽ nhận giá trị trả về
                            $('#row_' + id + '').remove();//kiểm tra giá trị trả về
                            Lobibox.notify('success', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Xóa đơn hàng thành công'
                            });//xóa dòng hiện tại trong table
                        } else {
                            Lobibox.notify('error', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Xóa đơn hàng thất bại'
                            });//show ra thông báo
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
    ajaxStart: function () {
        $(document).ajaxStart(function () {
            $('.loadingModel').show();
        });
        $(document).ajaxStop(function () {
            $('.loadingModel').hide();
        });
    }
}
order.init();
