
var user = {
    init: function () {
        user.deleteProduct();
        user.checkAll();
        user.deleteMul();
        user.deleteProduct();
        user.deleteProductDb();
        user.removeDeleteMul();
        user.DeleteMulDb();
        user.removeDeleteUser();
        user.registerEvent();
        user.ajaxStart();
    },
    saveData: function () {
        var name = $('#txtName').val();
        var url = $('#title').val();
        var id = $('#txtID').val();
        var contentCategory = {
            ID : id,
            Name: name,
            MetaTitle: url
        }
        $.ajax({
            url: '/Admin/ContentCategory/Create',
            data: { data: JSON.stringify(contentCategory)},//chuyển một đối tượng sag chuỗi
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
                if (response.status == true) {
                    Lobibox.notify('success', {
                        size: 'mini',
                        //rounded: true,
                        delayIndicator: false,
                        msg: 'Thêm mới thành công'
                    });
                    $('#myModalNorm').modal('hide');
                    location.reload();                  
                } else {
                    Lobibox.notify('error', {
                        size: 'mini',
                        //rounded: true,
                        delayIndicator: false,
                        msg: 'Tên đã tồn tại'
                    });
                }
            },
            error: function (err) {
                Lobibox.notify('warning', {
                    size: 'mini',
                   // rounded: true,
                    delayIndicator: false,
                    msg: 'Bạn không có quyền thực hiện chức năng này '
                });
                $('#myModalNorm').modal('hide');
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
    viewDetail:function(id) {
        
        $.ajax({
            url: '/Admin/ContentCategory/ViewDetail',
            data: { id: id },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
               
                if (response.status == true) {
                    $('#txtID').val(response.id);
                    $('#txtName').val(response.name);
                    $('#title').val(response.url);
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
    resetForm: function () {
        $('#txtID').val('0');
        $('#txtName').val('');
        $('#title').val('');
    },
    deleteProduct: function () {
        $('.deleteContentCategory').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);//lay class tren gang vao btn
            var id = btn.data('id');//lay gia tri data-id 
            alertify.confirm('Xoá', 'Mục này sẽ được chuyển vào thùng rác', function () {
                $.ajax({
                    url: '/Admin/ContentCategory/ChangeStatus/',
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
                                msg: 'Đã đưa vào mục xóa tạm'
                            });//xóa dòng hiện tại trong table
                        } else {
                            Lobibox.notify('error', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Xóa thất bại'
                            });;//show ra thông báo
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
    }, deleteProductDb: function () {
        $('.btn-remove-delete-db').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);//gáng class thành btn
            var id = btn.data('id');//lấy giá trị từ btn và gáng vào id
            alertify.confirm("Xóa", "Bạn có chắc xóa", function () {//show thông báo nếu nhấn yes chạy hàm dưới
                $.ajax({//khởi tạo ajax
                    url: '/Admin/ContentCategory/Delete',//url nhận data
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
    //cập nhập lại trạng thái status = true
    removeDeleteUser: function () {
        $('.btn-remove-delete-contentCaegory').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            alertify.confirm("Hủy xóa tạm", "Bạn có chắc muốn hủy xóa tạm tài khoản", function () {
                $.ajax({
                    url: "/Admin/ContentCategory/ChangeStatus",
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
                                msg: 'Đã hủy xóa tạm'
                            });
                        } else {
                            Lobibox.notify('error', {
                                size: 'mini',
                                rounded: true,
                                delayIndicator: false,
                                msg: 'Thất bại'
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
                        url: '/Admin/ContentCategory/DeleteSelected',
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
                            alertify.alert("Cảnh báo", 'Bạn không có quyền thực hiện chứ năng này');
                        }
                    });
                }, function () { });

            }

        });
    },
    //cập nhập lại trạng thái status = true
    removeDeleteMul: function () {
        $('#btn-remove-deleteAll').off('click').on('click', function (e) {
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
                        url: '/Admin/ContentCategory/DeleteSelectedRecycelBin',
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
                                    msg: 'Đã hủy xóa tạm'
                                });
                            } else {
                                Lobibox.notify('error', {
                                    size: 'mini',
                                    rounded: true,
                                    delayIndicator: false,
                                    msg: 'Hủy xóa thất bại'
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
                        url: '/Admin/ContentCategory/DeleteSelectedDb',
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
user.init();




//Auto convert name to title
var slug = function (str) {
    str = str.replace(/^\s+|\s+$/g, ''); // trim
    str = str.toLowerCase();

    // remove accents, swap ñ for n, etc
    var from = "ÁÄÂÀÃÅẬẮẰẶẴẲẤẨẦẬẪČÇĆĎÉĚËÈÊẼĔȆỂẾỀỆỄÍÌÎÏŇÑÓÖÒÔÕỐỒỘỖỔØŘŔŠŤÚŮÜÙÛÝŸŽáäâàãåắằẳặẵấầậẫẩăčçćďéěëèêẽĕȇếềệễểíìîïňñóöòôõốồỗổộøðơờớởợỡřŕšťúůüùữựừửữûýÿžþÞĐđßÆa·/_,:;";
    var to = "AAAAAAAAAAAAAAAAACCCDEEEEEEEEEEEEEIIIINNOOOOOOOOOOORRSTUUUUUYYZaaaaaaaaaaaaaaaaacccdeeeeeeeeeeeeeiiiinnoooooooooooooooooorrstuuuuuuuuuuyyzbBDdBAa------";

    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
    }

    str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
        .replace(/\s+/g, '-') // collapse whitespace and replace by -
        .replace(/-+/g, '-'); // collapse dashes
    $('#title').val(str);
    return str;
}