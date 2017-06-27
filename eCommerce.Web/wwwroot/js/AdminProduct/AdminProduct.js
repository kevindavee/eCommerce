var url = 'AdminProduct/';

function GetSubCategory(categoryId, subCategoryId) {
    var category = $(categoryId).val();
    $.getJSON(url + "GetSubCategory?CategoryId=" + category, function (data) {
        $(subCategoryId).empty();
        $(subCategoryId).append('<option value="0">Pilih Sub Category</option>');
        for (i = 0; i < data.subCategoryList.length; i++) {
            $(subCategoryId).append('<option value="' + data.subCategoryList[i].id + '">' + data.subCategoryList[i].nama + '</option>');
        }
    })
}

function GetBrandListByCategory(subCategoryId, brandId) {
    var subCategory = $(subCategoryId).val();
    $.getJSON(url + "GetBrandListByCategory?subCategoryId=" + subCategory, function (data) {
        $(brandId).empty();
        $(brandId).append('<option value="0">Pilih Brand</option>');
        for (i = 0; i < data.brandList.length; i++) {
            $(brandId).append('<option value="' + data.brandList[i].id + '">' + data.brandList[i].nama + '</option>');
        }
    })
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
            if (data === "1")
            {
                
                if (OptionNama === 'Warna')
                {
                    if (document.getElementById(Id).checked === true) {
                        $("#warnaHidden").removeClass("hide");
                    }
                    else
                    {
                        $("#warnaHidden").addClass("hide");
                    }
                }
                else
                {
                    if (document.getElementById(Id).checked === true) {
                        $("#sizeHidden").removeClass("hide");
                    }
                    else {
                        $("#sizeHidden").addClass("hide");
                    }
                }
            }
            else
            {
                alert(data);
            }
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
    })
});
