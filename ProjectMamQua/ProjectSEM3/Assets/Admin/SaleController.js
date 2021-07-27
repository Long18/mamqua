
var salePrice = {
    init: function () {
        salePrice.checkAll();
        salePrice.deleteProductDb();
        salePrice.ajaxStart();
        salePrice.deleteMul();
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
      $('.deleteProduct').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);//gáng class thành btn
            var id = btn.data('id');//lấy giá trị từ btn và gáng vào id
            alertify.confirm("Xóa", "Bạn có chắc xóa", function () {//show thông báo nếu nhấn yes chạy hàm dưới
                $.ajax({//khởi tạo ajax
                    url: '/Admin/Sale/Delete',//url nhận data
                    data: { id: id },//truyền data 
                    dataType: 'json',//kiểu data
                    type: 'Post',//phương thức của data
                    success: function (respone) {// truyền thành công sẽ nhận giá trị trả về 
                        if (respone.status == true) {//kiểu tra giá trị trả về
                            $('#row_' + id + '').remove();//xóa dòng hiện tại
                            Lobibox.notify('success', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Xóa thành công'
                            });//show thông báo
                        } else {
                            Lobibox.notify('error', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Xóa thất bại'
                            });
                        }
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
  
    //xóa nhiều mục được check
    deleteMul: function () {
        $('#btn-deleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];//khỏi tạo mảng rỗng 
            $('input[name="table_records"]:checked').each(function () {//lấy giá trị mỗi input được check
                boxData.push($(this).val());//thêm giá trị vào mảng 
            });
            if (boxData == '') {//kiểm tra nếu mảng rỗng thì show thông báo
                alertify.alert('Lỗi', 'Không mục nào được chọn!');
            } else {//mảng có giá trị thực hiện hàm bên dưới
                alertify.confirm("Xóa", "Mục này sẽ được đưa vào thùng rác", function () {
                    $.ajax({
                        url: '/Admin/Sale/DeleteSelectedDb',
                        dataType: 'json',
                        type: 'POST',
                        data: { ids: boxData.join(',') },//chuyển mảng thành một chuỗi và truyền đi
                        success: function (respone) {
                            if (respone.status == true) {
                                $.each(boxData, function (i, item) {//xóa các dòng đã được xóa
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
                                msg: 'Bạn không có quyền thực hiện chức năng này'
                            });
                        }
                    });
                }, function () { });

            }

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
salePrice.init();


