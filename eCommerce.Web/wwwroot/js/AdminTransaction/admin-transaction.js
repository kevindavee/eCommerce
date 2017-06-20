$(document).ready(function () {
    $('.textbox-tracking').on('keyup', function () {
        var value = $(this).val();
        var buttonSubmit = '#btn-submit-' + $(this).attr('id');
        if (value != '') {
            $(buttonSubmit).removeAttr('disabled');
        }
        else {
            $(buttonSubmit).attr('disabled', 'disabled');
        }
    });

    $('#btn-submit-bank, #btn-submit-shipper').on('click', function myfunction(e) {
        var allFieldValid = true;

        $('.bank-field, .shipper-field').each(function () {
            if ($(this).val() === '') {
                allFieldValid = false;
            }
        });

        if (!allFieldValid) {
            e.preventDefault();
            alert("Please enter all required field !");
        }
    })
});