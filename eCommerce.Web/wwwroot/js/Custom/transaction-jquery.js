$(document).ready(function () {
    //Cart Page
    $('.btn-update-price').on('click', function () {
        var itemId = parseInt($(this).attr('id'));
        var textboxQuantity = '.txt-quantity#' + itemId;
        var quantity = parseInt($(textboxQuantity).val().split(".", 1));

        if (quantity < 1) {
            alert("Insert minimum 1 item !");
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
                        var quantityDOM = '.txt-quantity#' + itemId;
                        $(quantityDOM).val(quantity);
                        $('#total-price').html('Rp.' + ThousandSeparator(data.totalPrice));

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

    $('#dropdown-alamat').on('change', function () {
        var alamatId = parseInt($(this).val());
        var url = "/Transaction/GetAlamatDetail";

        if (alamatId == 0) {
            $('#ShippingDetail_AlamatPengiriman').val('');
            $('#ShippingDetail_Kota').val('');
            $('#ShippingDetail_Provinsi').val('');
            $('#ShippingDetail_KodePos').val('');
        }
        else {
            $.ajax({
                type: "POST",
                url: url,
                data: { AlamatId: alamatId },
                dataType: 'json',
                success: function (data) {
                    $('#ShippingDetail_AlamatPengiriman').val(data.theAlamat);
                    $('#ShippingDetail_Kota').val(data.kota);
                    $('#ShippingDetail_Provinsi').val(data.provinsi);
                    $('#ShippingDetail_KodePos').val(data.kodePos);
                }
            });
        }
    })

    //Page Checkout
    $('#ShippingDetail_NamaPenerima').on('keyup', function () {
        EnableSubmitButton('#btn-submit-order');
    });

    $('.required-dropdown').on('change', function () {
        EnableSubmitButton('#btn-submit-order');
    });

    //Page Payment Confirmation
    $('#BankId').on('change', function () {
        EnableSubmitButton('#btn-submit-confirmation');
    });

    $('.required-field').on('keyup', function () {
        EnableSubmitButton('#btn-submit-confirmation');
    });

    function EnableSubmitButton(buttonName) {
        var disabled = CheckRequiredField();

        if (!disabled) {
            $(buttonName).removeAttr('disabled');
        }
        else {
            $(buttonName).attr('disabled', 'disabled');
        }
    }

    function CheckRequiredField() {
        var required = false;
        $('.required-field').each(function () {
            var value = $(this).val();
            if (value === '0' || value === '') {
                required = true;
            }
        });
        return required;
    }
});