$(document).ready(function () {
    $("#SaleDate").datepicker({
        autoclose: true
    });

    $("#AddButton").click(function () {

        CreateRowForSale();
        $("#ItemName").val("");
        $("#Quantity").val("");
        $("#ItemPrice").val("");
        $("#Stock").val("");

    });

    $("#ItemName").on("change", function () {

        var salesQuantity = 0;
        $("#SaleDetailsTable tr").each(function (index, value) {
            var Quantity = parseInt($(this).find(".quantity").html());
            salesQuantity = salesQuantity + Quantity;
        });

        var id = $("#ItemName").val();
        $.ajax({
            url: "/InventorySales/GetItemSalesPrice",
            type: "post",
            data: { id: id },
            success: function (response) {
                $("#ItemPrice").val(response);
            }
        });
        $.ajax({
            url: "/InventorySales/GetItemStock",
            type: "post",
            data: { id: id },
            success: function (response) {
                var stock = parseInt(response);
                var availabeStock = stock - salesQuantity;
                if (availabeStock <= 0) {
                    $("#Stock").val(0);
                }
                else {
                    $("#Stock").val(availabeStock);
                }
            }
        });
    });

    //$("#Quantity").blur(function () {
    //    var QuantityValue = parseFloat($("#Quantity").val());
    //    if (QuantityValue == "" || QuantityValue == undefined) {
    //        $("#Quantity").val(0)
    //    }
    //    var StockValue = parseFloat($("#Stock").val());
    //    var NewStockValue = StockValue - QuantityValue;
    //    if (NewStockValue < 0) {
    //        $("#Stock").val("Out Of Stock");
    //    }
    //    else {
    //        $("#Stock").val(NewStockValue);
    //    }
    //});

    $("#VAT").blur(function () {
        var value = $(this).val();
        if (value == "" || value == undefined) {
            $(this).val(0);
        }
        TotalAmount();
    });
    $("#DueAmount").blur(function () {
        var value = $(this).val();
        if (value == "" || value == undefined) {
            $(this).val(0);
        }
    });
    $("#Discount").blur(function () {
        var value = $(this).val();
        if (value == "" || value == undefined) {
            $(this).val(0);
        }
        TotalAmount();
    });
});


function CreateRowForSale() {
    var selectedItem = GetSelectedItem();
    var index = $("#SaleDetailsTable").children("tr").length;
    var sl = index;

    var serialCell = "<td>" + (++sl) + "</td>";
    var itemCell = "<td class='item' id='" + selectedItem.ItemName + "'>" + selectedItem.itemText + "</td>";
    var priceCell = "<td class='price'>" + selectedItem.ItemPrice + "</td>";
    var quantityCell = "<td class='quantity'>" + selectedItem.Quantity + "</td>";
    var totalCell = "<td class='total'>" + selectedItem.Total + "</td>";
    var actionCell = "<td>" + "<input type='button' class='btn btn-danger' value='Remove' onclick='GetDeleteId(" + index + ")' id='" + index + "'/>" + "</td>";
    $("#SaleDetailsTable").append("<tr id='DelRow_" + index + "'>" + serialCell + itemCell + quantityCell + priceCell + totalCell + actionCell + "</tr>");
    SubTotalAmount();
    TotalAmount();
}



function GetSelectedItem() {

    var ItemName = $("#ItemName").val();
    var Quantity = $("#Quantity").val();
    var ItemPrice = $("#ItemPrice").val();
    var ItemStockQuantity = $("#Stock").val();
    var itemText = $("#ItemName option:selected").text();
    var Total = Quantity * ItemPrice;

    var Item = {
        "ItemName": ItemName,
        "Quantity": Quantity,
        "ItemPrice": ItemPrice,
        "ItemStockQuantity": ItemStockQuantity,
        "itemText": itemText,
        "Total": Total
    };
    return Item;
}

$("#btnSubmit").click(function () {
    console.log(GetSelectedItem());
    var index = $("#SaleDetailsTable").children("tr").length;
    $("#SaleDetailsTable tr ").each(function (index, value) {

        var ItemId = ($(this).find(".item").attr("id"));
        var Quantity = ($(this).find(".quantity").html());
        var Price = ($(this).find(".price").html());


        var bindexCell = "<input type='hidden' id='Index" + index + "' name='InventorySalesDetails.index' value='" + index + "'/>";
        var bitem = "<input type='hidden' id='InventoryId" + index + "' name='InventorySalesDetails[" + index + "].InventoryId' value='" + ItemId + "'/>";
        var bQuantity = "<input type='hidden' id='Quantity" + index + "' name='InventorySalesDetails[" + index + "].Quantity' value='" + Quantity + "'/>";
        var bPrice = "<input type='hidden' id='Price" + index + "' name='InventorySalesDetails[" + index + "].Price' value='" + Price + "'/>";

        $("#bindValue").append(bindexCell, bitem, bQuantity, bPrice);
    });
});


var GetDeleteId = function (Id) {
    $("#DelRow_" + Id).remove();
    SubTotalAmount();
    TotalAmount();
};

function SubTotalAmount() {
    var sumOfTotal = 0;
    if ($("#SaleDetailsTable").children("tr").length == 0) {
        $("#SubTotal").val(0);
    }
    else {
        $("#SaleDetailsTable tr ").each(function (index, value) {
            var Total = parseFloat(($(this).find(".total").html()));
            sumOfTotal = Math.floor(sumOfTotal + Total);
            $("#SubTotal").val(sumOfTotal);
        });
    }

}
function TotalAmount() {
   // console.log("Hello World");
    var subTotal = $("#SubTotal").val();
    var discount = $("#Discount").val();
    var VAT = $("#VAT").val();

    if (subTotal == "" || subTotal == undefined) {
        subTotal = 0;
    }
    if (discount == "" || discount == undefined) {
        discount = 0;
    }
    if (VAT == "" || VAT == undefined) {
        VAT = 0;
    }
    discount = Math.floor(parseFloat(discount));
    VAT = Math.floor(parseFloat(VAT));
    subTotal = Math.floor(parseFloat(Math.floor(subTotal)));

    if (subTotal != null && discount != null && VAT != null) {
        var grandTotal = Math.floor(subTotal + subTotal * (VAT / 100) - discount);
       // alert(grandTotal);
        $("#Total").val(grandTotal);
    }
}