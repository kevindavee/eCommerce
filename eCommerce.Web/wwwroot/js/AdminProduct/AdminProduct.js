var url = 'AdminProduct/';

function GetSubCategory(categoryId, subCategoryId) {
    var url = "/AdminProduct/GetSubCategory";
    var category = parseInt($(categoryId).val());
    $.getJSON(url, { CategoryId: category }, function (data) {
        $(subCategoryId).empty();
        $(subCategoryId).append('<option value="0">Choose Sub Category</option>');
        for (i = 0; i < data.subCategoryList.length; i++) {
            $(subCategoryId).append('<option value="' + data.subCategoryList[i].id + '">' + data.subCategoryList[i].nama + '</option>');
        }
    })
}

function GetBrandListByCategory(subCategoryId, brandId) {
    var url = "/AdminProduct/GetBrandListByCategory";
    var subCategory = $(subCategoryId).val();
    $.getJSON(url, { subCategoryId: subCategory }, function (data) {
        $(brandId).empty();
        $(brandId).append('<option value="0">Choose Brand</option>');
        for (i = 0; i < data.brandList.length; i++) {
            $(brandId).append('<option value="' + data.brandList[i].id + '">' + data.brandList[i].nama + '</option>');
        }
    })
}

function AddOptionWarna(tbId, productId) {
    if ($(tbId).val() === '')
    {
        alert("Harus diisi!");
    }
    else
    {
        $.post(url + "AddOptionWarna?warna=" + $(tbId).val() + "&productId=" + productId, function (data) {
            $('#tabProductDetails').html(data);
            $.getJSON(url + "ListCheckedOption?id=" + productId, function (data) {
                //lanjut disini
                if (data.checkedList[0] === true) {
                    $("#warnaHidden").removeClass("hide");
                }
                if (data.checkedList[1] === true) {
                    $("#sizeHidden").removeClass("hide");
                }
            })
            $('#details-tab').tab('show');
        })
    }
}

function AddOptionSize(tbId, productId) {
    if ($(tbId).val() === '') {
        alert("Harus diisi!");
    }
    else {
        $.post(url + "AddOptionSize?size=" + $(tbId).val() + "&productId=" + productId, function (data) {
            $('#tabProductDetails').html(data);
            $.getJSON(url + "ListCheckedOption?id=" + productId, function (data) {
                //lanjut disini
                if (data.checkedList[0] === true) {
                    $("#warnaHidden").removeClass("hide");
                }
                if (data.checkedList[1] === true) {
                    $("#sizeHidden").removeClass("hide");
                }
            })
            $('#details-tab').tab('show');
        })
    }
}

function DetailsProductClick(Id) {
    $.get(url + "ProductDetails?id=" + Id, function (data) {
        $('#tabProductDetails').html(data);
        $.getJSON(url + "ListCheckedOption?id=" + Id, function (data) {
                //lanjut disini
            if (data.checkedList[0] === true)
            {
                $("#warnaHidden").removeClass("hide");
            }
            if (data.checkedList[1] === true)
            {
                $("#sizeHidden").removeClass("hide");
            }
        })
        $('#details-tab').tab('show');
    })
}

function DeleteProduct(Id) {
    if (confirm("Are you sure?")) {
        $.post(url + "DeleteProduct?id=" + Id, function (data) {
            $('#tabManageProduct').html(data);
        })
    }
}

function ChangeOptions(productId, Id, OptionNama) {
    if (confirm("Are you sure?")) {
        $.post(url + "ChangeOptions?productId=" + productId + "&id=" + Id + "&check=" + document.getElementById(Id).checked, function (data) {
            $('#tabProductDetails').html(data);
            $.getJSON(url + "ListCheckedOption?id=" + productId, function (data) {
                //lanjut disini
                if (data.checkedList[0] === true) {
                    $("#warnaHidden").removeClass("hide");
                }
                if (data.checkedList[1] === true) {
                    $("#sizeHidden").removeClass("hide");
                }
            })
            $('#details-tab').tab('show');
        })
    }
}

function SubmitProduct() {
    if (confirm("Are you sure?")) {
        $.post(url + "SubmitProduct", $('#formDetailsProduct').serialize(), function (data) {
            $('#tabProductDetails').html(data);
            $('#details-tab').tab('show');
        })
    }
}

$(document).ready(function () {
    $('.btn-category').on('click', function () {
        var parentId = parseInt($(this).val());

        var displaySubCategoryTableId = '#table-subcategory-' + parentId;

        $('.table-subcategory').css('display', 'none');
        $(displaySubCategoryTableId).css('display', '');

    });

    $('#btn-submit-brand, #btn-submit-category').on('click', function myfunction(e) {
        var allFieldValid = true;

        $('.brand-field, .category-field').each(function () {
            if ($(this).val() === '') {
                allFieldValid = false;
            }
        });

        if (!allFieldValid) {
            e.preventDefault();
            alert("Please enter all required field !");
        }
    });

    $('#dropdown-child').on('change', function () {
        var childvalue = parseInt($('#dropdown-child').val());
        if (childvalue > 0 && !isNaN(childvalue)) {
            $('#btn-submit-brand-category').removeAttr('disabled');
            $('#CategoryId').val(childvalue);
        }
        else {
            $('#btn-submit-brand-category').attr('disabled', 'disabled');

        }
    });

    $('#dropdown-parent').on('change', function () {
        $('#btn-submit-brand-category').attr('disabled', 'disabled');
    });

    $('.btn-delete-brand-category').on('click', function () {
        if (!confirm("Are you sure ?")) {
            return false;
        }
    })
});
