$(document).ready(function () {
    $('#btn-add-to-cart').on('click', function () {
        var url = "/Transaction/AddToCart";
        var quantity = parseInt($('#txt-quantity').val());
        //product instance id nanti tolong di isi dengan hidden field yang nyimpen product instance id dari view
        var productInstanceId = 1;

        $.ajax({
            type: "POST",
            url: url,
            data: { ProductInstanceId: productInstanceId, Quantity: quantity },
            dataType: 'json',
            success: function (data) {
                if (data.status === true) {
                    alert("Item has been added to cart ! ");
                }
            }
        })
    });
});