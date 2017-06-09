$(document).ready(function () {
    $('.btn-update-price').on('click', function () {
        var itemId = parseInt($(this).attr('id'));
        var textboxQuantity = '.txt-quantity#' + itemId;
        var quantity = parseInt($(textboxQuantity).val());

        if (quantity < 1) {
            alert("Insert minimal 1 item !");
        }
        else {
            $.ajax({
                type: "POST",
                url: '/Transaction/UpdateQuantity',
                data: { Quantity: quantity, TransactionDetailId: itemId },
                dataType: 'json',
                success: function (data) {
                    if (data.status === true) {
                        alert("Success");

                        var priceDOM = '#item-' + itemId + '-total-price';
                        $(priceDOM).html('Rp. ' + data.itemPrice);
                        $('#total-price').html('Rp.' + data.totalPrice);

                    }
                    else {
                        console.log(data.errorMsg);
                    }
                }
            });
        }
    });

    $('.btn-delete-item').on('click', function (e) {
        if (!confirm("Are you sure want to remove this item from your cart ?")) {
            e.preventDefault();
        }
    });
});