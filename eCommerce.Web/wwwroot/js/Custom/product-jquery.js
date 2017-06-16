$(document).ready(function () {
    $('#btn-add-to-cart').on('click', function () {
        var url = "/Transaction/AddToCart";
        var quantity = parseInt($('#txt-quantity').val());
        //product instance id nanti tolong di isi dengan hidden field yang nyimpen product instance id dari view
        var productInstanceId = parseInt($('#hiddenProductInstanceId').val());

        if (isNaN(productInstanceId) || productInstanceId === 0) {
            alert('Fill options first !');
        }
        else {
            $.ajax({
                type: "POST",
                url: url,
                data: { ProductInstanceId: productInstanceId, Quantity: quantity },
                dataType: 'json',
                success: function (data) {
                    if (data.status === true) {
                        alert("Item has been added to cart ! ");
                    }
                    else {
                        if (data.emptyStock != "") {
                            alert(data.emptyStock);
                        }
                        else {
                            console.log(data.errorMsg);
                        }
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (jqXHR.status == 401) {
                        window.location.href = '/Account/Login';
                    }
                }
            })

        }

    });
});