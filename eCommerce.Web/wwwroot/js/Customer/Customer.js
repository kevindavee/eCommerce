function SaveProfile() {
    //alert("test");
    $.post("/Customer/SaveProfile/", $("#formCustomerProfile").serialize(), function (data) {
        if (data) {
            alert("Success");
        }
        else {
            alert("Error");
        }
    })
}


function CheckRbJob() {
    var rbVal = $("#divRbJob input[type='radio']:checked").val();
    //alert(rbVal);
    if (rbVal == "Lainnya") {
        $('#txtJobLainnya').removeClass("hide");
    }
    else {
        $('#txtJobLainnya').addClass("hide");
    }
}


function SaveAlamat() {
    //alert("test");
    $.post("/Customer/SaveCustomerAddress/", $("#formCustomerAddress").serialize(), function (data) {
        if (data) {
            alert("Success");
            window.location.href = '/Customer/AddressList';
        }
        else {
            alert("Error");
        }
    })
}


function DeleteCustomerAddress(AddressId) {
    //alert("test");
    $.post("/Customer/DeleteCustomerAddress?AddressId=" + AddressId, function (data) {
        if (data) {
            alert("Success");
            window.location.href = '/Customer/AddressList';
        }
        else {
            alert("Error");
        }
    })
}