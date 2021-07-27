var user = {
    init: function () {
      
        user.deleteGroupUserDb();
        user.ajaxStart();
        user.registerEvent();
       
    },
    registerEvent: function () {
        $('#btn-Add').off('click').on('click', function (e) {
            e.preventDefault();
            $('#myModalNorm').modal('show');
            user.resetForm();
        });
        $('#btn-Add-data').off('click').on('click', function (e) {
            e.preventDefault();
            user.saveData();
        });
        $('.btn-Edit').off('click').on('click', function () {

            $('#myModalNorm').modal('show');
            var id = $(this).data('id');
            user.viewDetail(id);
        });
    },
    saveData: function () {
        var name = $('#txtName').val();
        var des = $('#txtDescription').val();
        var id = $('#txtID').val();
        var groupUser = {
            ID : id,
            Name: name,
            Desciption: des
        }
        $.ajax({
            url: '/Admin/GroupUser/Create',
            data: { data: JSON.stringify(groupUser) },//chuyển một đối tượng sag chuỗi
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
                if (response.status == true) {
                    Lobibox.notify('success', {
                        size: 'mini',
                        rounded: true,
                        delayIndicator: false,
                        msg: 'Thêm mới thành công'
                    });
                    $('#myModalNorm').modal('hide');
                    location.reload();                  
                } else {
                    Lobibox.notify('error', {
                        size: 'mini',
                        rounded: true,
                        delayIndicator: false,
                        msg: 'Thêm mới thất bại'
                    });
                }
            },
            error: function (err) {
                Lobibox.notify('warning', {
                    size: 'mini',
                    rounded: true,
                    delayIndicator: false,
                    msg: 'Bạn không có quyền thực hiện chức năng này '
                });
                $('#myModalNorm').modal('hide');
            }
        });

    },
    viewDetail: function (id) {

        $.ajax({
            url: '/Admin/GroupUser/ViewDetail',
            data: { id: id },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {

                if (response.status == true) {
                    $('#txtID').val(response.id);
                    $('#txtName').val(response.name);
                    $('#txtDescription').val(response.des);
                } else {
                    alertify.alert('Lỗi', 'Không có giữ liệu trả về');
                }
            },
            error: function (err) {
                Lobibox.notify('warning', {
                    size: 'mini',
                    rounded: true,
                    delayIndicator: false,
                    msg: 'Bạn không có quyền thực hiện chức năng này '
                });
            }
        });

    },
    deleteGroupUserDb: function () {
        $('.deleteGroupUserID').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            alertify.confirm("Xóa", "Bạn có chắc muốn xóa", function () {
                $.ajax({
                    url: "/Admin/GroupUser/Delete",
                    data: { id: id },
                    dataType: 'json',
                    type: 'POST',
                    success: function (respone) {
                        if (respone.status == true) {
                            $('#row_' + id + '').remove();
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


        });
    
    }, ajaxStart: function () {
        $(document).ajaxStart(function () {
            $('.loadingModel').show();
        });
        $(document).ajaxStop(function () {
            $('.loadingModel').hide();
        });
    }
}
user.init();