var user = {
    init: function () {
        user.registerEvent();
        user.deleteUser();
        user.deleteMul();
        user.checkAll();
        user.removeDeleteUser();
        user.removeDeleteMul();
        user.deleteUserDb();
        user.ajaxStart();
        user.removeDeleteMulDb();
    },
    registerEvent: function () {
        $(".btn-active-status-product").off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeActive",
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
    removeDeleteUser: function () {
        $('.btn-remove-delete-user').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            alertify.confirm("Hủy xóa tạm", "Bạn có chắc muốn hủy xóa tạm tài khoản", function () {
                $.ajax({
                    url: "/Admin/User/ChangeStatus",
                    data: { id: id },
                    dataType: 'json',
                    type: 'POST',
                    success: function (respone) {
                        if (respone.status == true) {
                            $('#row_' + id + '').remove();
                            alertify.success('Hủy xóa tài tạm khoản thành công');
                        } else {
                            alertify.error('Hủy xóa tạm tài khoản thất bại');
                        }
                    },
                    failure: function (response) {
                        alertify.alert("Cảnh báo", 'Đã sảy ra lỗi');
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
                });
            }, function () { });


        });
    },
    deleteUser: function () {
        $('.btn-delete-user').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            alertify.confirm("Xóa", "Mục này sẽ được đưa vào thùng rác", function () {
                $.ajax({
                    url: "/Admin/User/ChangeStatus",
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
                                msg: 'Đã đưa vào mục xóa tạm'
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
                            sound: true,
                            delayIndicator: false,
                            msg: 'Bạn không có quyền thực hiện chứ năng này'
                        });
                    }
                });
            }, function () { });


        });
    },
    deleteUserDb: function () {
        $('.btn-remove-delete-db').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            alertify.confirm("Xóa", "Bạn có chắc xóa", function () {
                $.ajax({
                    url: "/Admin/User/Delete",
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
                            sound: true,
                            delayIndicator: false,
                            msg: 'Bạn không có quyền thực hiện chứ năng này'
                        });
                    }
                });
            }, function () { });


        });
    },

    deleteMul: function () {
        $('#btn-deleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];
            $('input[name="table_records"]:checked').each(function () {
                boxData.push($(this).val());
            });
            if (boxData == '') {
                alertify.alert('Lỗi', 'Bạn không thể xóa khi không có mục nào được chọn!');
            } else {
                alertify.confirm("Xóa", "Mục này sẽ được đưa vào thùng rác", function () {
                    $.ajax({
                        url: '/Admin/User/DeleteSelected',
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
                }, function () { });

            }

        });
    },
    removeDeleteMul: function () {
        $('#btn-remove-deleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];
            $('input[name="table_records"]:checked').each(function () {
                boxData.push($(this).val());
            });
            if (boxData == '') {
                alertify.alert('Lỗi', 'Bạn không thể xóa khi không có mục nào được chọn!');
            } else {
                alertify.confirm("Hủy xóa", "Mục này sẽ được lấy lại", function () {
                    $.ajax({
                        url: '/Admin/User/DeleteSelectedRecycelBin',
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
                                    msg: 'Đã đưa vào mục xóa tạm'
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
                                sound: true,
                                delayIndicator: false,
                                msg: 'Bạn không có quyền thực hiện chứ năng này'
                            });
                        }
                    });
                }, function () { });
            }


        });
    },
    removeDeleteMulDb: function () {
        $('#btn-remove-deleteAll-db').off('click').on('click', function (e) {
            e.preventDefault();
            var boxData = [];
            $('input[name="table_records"]:checked').each(function () {
                boxData.push($(this).val());
            });
            if (boxData == '') {
                alertify.alert('Lỗi', 'Bạn không thể xóa khi không có mục nào được chọn!');
            } else {
                alertify.confirm("Xóa", "Mục này sẽ được xóa", function () {
                    $.ajax({
                        url: '/Admin/User/DeleteSelectedDb',
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
                }, function () { });

            }

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