var business = {
    init: function () {
        business.deleteBernission();
        business.loadingImag();
        business.registerEvent();
    },
    registerEvent: function () {
        $('.btn-Edit').off('click').on('click', function () {

            $('#myModalNorm').modal('show');
            var id = $(this).data('id');
            business.viewDetail(id);
        });
        $('#btn-Add-data').off('click').on('click', function () {
            business.saveData();
        });
    },
    viewDetail: function (id) {
        $.ajax({
            url: '/Admin/Permission/ViewDetail/',
            data: { id: id },//set data truyền đi
            dataType: 'json',//set kiểu của giá trị truyền đi
            type: 'POST',//set kiểu phương th/ức
            success: function (respone) {

                if (respone.status == true) {//nếu thành công thì sẽ nhận giá trị trả về
                    
                    $('#txtID').val(respone.id);
                    $('#txtName').val(respone.name);
                    $('#txtDescription').val(respone.des);
                } else {
                    Lobibox.notify('error', {
                        size: 'mini',
                        rounded: true,
                        delayIndicator: false,
                        msg: 'Xóa thất bại'
                    });//show ra thông báo
                }
            }
        });
    },
    saveData: function () {
        var id = $('#txtID').val();
        var name = $('#txtName').val();
        var description = $('#txtDescription').val();
        var business = {
            ID: id,
            Name: name,
            Desciption: description
        }
        $.ajax({
            url: '/Admin/Permission/Edit',
            data: { data: JSON.stringify(business) },//chuyển một đối tượng sag chuỗi
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
                if (response.status == true) {
                    Lobibox.notify('success', {
                        size: 'mini',
                        rounded: true,
                        delayIndicator: false,
                        msg: 'Lưu thành công'
                    });
                    $('#myModalNorm').modal('hide');
                    location.reload();
                } else {
                    Lobibox.notify('error', {
                        size: 'mini',
                        rounded: true,
                        delayIndicator: false,
                        msg: 'Lưu Thất bại'
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
    deleteBernission: function () {
        $('.deletePermission').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            alertify.confirm('Xoá', 'Bạn sẽ xóa luôn các hành động của mục này', function () {
                $.ajax({
                    url: '/Admin/Permission/Delete/',
                    data: { id: id },//set data truyền đi
                    dataType: 'json',//set kiểu của giá trị truyền đi
                    type: 'POST',//set kiểu phương th/ức
                    success: function (respone) {
                        if (respone.status == true) {//nếu thành công thì sẽ nhận giá trị trả về
                            $('#row_' + id + '').remove();//kiểm tra giá trị trả về
                            Lobibox.notify('success', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Xóa thành công'
                            });//xóa dòng hiện tại trong table
                        } else {
                            Lobibox.notify('error', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Xóa thất bại'
                            });//show ra thông báo
                        }
                    },

                    error: function (response) {
                        Lobibox.notify('warning', {
                            size: 'mini',
                            rounded: true,
                            delayIndicator: false,
                            msg: 'bạn không có quyền thực hiện chức năng này'
                        });
                    }
                });
            }, function () { });

        });
    },
    loadingImag: function () {
        $(document).ajaxStart(function () {
            $('.loadingModel').show();
        });
        $(document).ajaxStop(function () {
            $('.loadingModel').hide();
        });
    }
}

business.init();