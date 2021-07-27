var cart = {
    init: function () {
        cart.registerEven();
    },
    registerEven: function () {
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = '/';
        });
        $('#btnUpdate').off('click').on('click', function (e) {
            e.preventDefault();
            var listProduct = $('.quantity');//lấy danh sách sản phẩm trong card
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),//lấy giá trị hiện tại của textbox đó
                    ProductModel: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update/',
                data: { cartModel: JSON.stringify(cartList) }, //chuyển mảng thành một chuỗi,  
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        window.location.href = '/Cart/Index';
                    }
                }
            });

        });
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Cart/DeleteAll/',
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        window.location.href = '/Home/';
                    }
                }
            });
        });

        $('.quantity').on('change', function () {
            var CartItem = [
                {
                    Quantity: $(this).val(), //lấy giá trị hiện tại của textbox đó
                    ProductModel: {
                        ID: $(this).data('id')
                    }
                }
            ];
            if ($(this).val() > 5 || $(this).val() < 1) {
                alert('Bạn không đươc đặt quá 5 sản phẩm');
                return;
            } else {
                $.ajax({
                    url: '/Cart/Update/',
                    dataType: 'json',
                    data: { cartModel: JSON.stringify(CartItem) }, //chuyển mảng thành một chuỗi,  
                    type: 'POST',
                    success: function (response) {
                        if (response.status == true) {
                            window.location.href = '/Cart/';
                        }
                    }
                });
            }

        });

        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/Delete/',
                dataType: 'json',
                data: { id: id },
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        window.location.href = '/Cart/';
                    }
                }
            });
        });
    }
}
cart.init();