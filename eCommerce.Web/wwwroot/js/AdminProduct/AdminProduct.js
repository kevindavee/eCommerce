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
        $('#details-tab').tab('show');
    })
}