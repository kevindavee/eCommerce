$(document).ready(function () {
    $('#btn-search').on('click', function () {
        var arr = GetFilterFieldValue();
        var url = "/Product/ProductIndex";
        $.get(url, { MaxHarga: arr[0].MaxHarga, MinHarga: arr[0].MinHarga, sort: arr[0].sort, brandId: arr[0].BrandId, CategoryId: arr[0].CategoryId }, function (data) {
            $('div#div-product-list').empty();
            $('div#div-product-list').append(data);
        })
    });
});


$(document).ready(function () {
    $('.btn-toggle-handphone').on('click', function () {
        var Index = parseInt($('#PageIndex').val());
        var PageCounts = parseInt($('#PageCounts').val());

        if ($(this).val() === "1") {
            PageCounts--;
            if (Index < PageCounts) {
                Index++;
            }
        }
        else {
            if (Index > 0) {
                Index--;
            }
        }

        var arr = GetFilterFieldValue();
        var url = "/Handphone/IndexList";
        arr[0].PageIndex = Index;

        $.get(url, { PageIndex: arr[0].PageIndex, BrandId: arr[0].BrandId, MaxHarga: arr[0].MaxHarga, MinHarga: arr[0].MinHarga, sort: arr[0].sort }, function (data) {
            $('div#div-handphone-items').empty();
            $('div#div-handphone-items').append(data);
        })
    });
});




function GetFilterFieldValue() {
    var sort = parseInt($('#dropdown-sort').val());
    var MinHarga = null;
    var MaxHarga = null;
    var brandId = parseInt($('#dropdown-brand').val());
    var categoryId = parseInt($('#hiddenCategoryIdBrandList').val());
    //var PageIndex = parseInt($('#PageIndex').val());
    var arr = [];

    if ($('#txt-min-harga').val() !== "") {
        MinHarga = parseInt($('#txt-min-harga').val());
    }

    if ($('#txt-max-harga').val() !== "") {
        MaxHarga = parseInt($('#txt-max-harga').val());
    }

    var items = {
        //PageIndex: PageIndex,
        MinHarga: MinHarga,
        MaxHarga: MaxHarga,
        sort: sort,
        BrandId: brandId,
        CategoryId : categoryId
    };

    arr.push(items);

    return arr;
}


function GetPriceByOptions(ProductId, Warna, Ukuran, ParentCategory) {
    alert("test");
    var url = "/Product/GetPriceByOptions";
    $.getJSON("/Product/GetPriceByOptions?ProductId=" + ProductId + "&optValueWarna=" + Warna + "&optValueUkuran=" + Ukuran + "&parentCategory=" + ParentCategory, function (data) {
        alert(data.toSource());
        //alert(data.price);
        //alert(data.idProductInstance);

        $('#lblPrice').empty();
        $('#lblPrice').append(data.price);
        $('#hiddenProductInstanceId').val(data.idProductInstance);
    })
}