/// <reference path="~/Theme/bower_components/jquery/dist/jquery.min.js" />
$(document).ready(function () {
    $("#OrderDate").datepicker({
        autoclose: true
    });

    $("#AddButton").click(function () {

        CreateRowForOrder();
        TotalAmount()
    });
});

function CreateRowForOrder() {
    var SelectedItem = GetSelectedItem();

    var index = $("#OrderDetailsTable").children("tr").length;
    var sl = index;

    var indexCell = "<td style='display:none'> <input type='hidden' id='index" + index + "' name='OrderDetail.index' value='" + index + "'/> </td>"
    var serialCell = "<td>" + (++sl) + "</td>";

    var itemNameCell = "<td> <input type='hidden' id='ItemName" + index + "' name='OrderDetail[" + index + "].ItemId' value='" + SelectedItem.ItemName + "' />" + SelectedItem.itemText + " </td>";
    var itemQuantityCell = "<td> <input type='hidden' id='ItemQuantity" + index + "' name='OrderDetail[" + index + "].Quantity' value='" + SelectedItem.ItemQuantity + "' />" + SelectedItem.ItemQuantity + " </td>";
    var itemPriceCell = "<td> <input type='hidden' id='ItemPrice" + index + "' name='OrderDetail[" + index + "].Price' value='" + SelectedItem.ItemPrice + "' />" + SelectedItem.ItemPrice + " </td>";
    var LineTotalCell = "<td class='total'>" + SelectedItem.ItemQuantity * SelectedItem.ItemPrice + " </td>";
    var actionCell = "<td>" + "<input type='button' class='btn btn-danger btn-sm' value='Remove' onclick='GetDeleteId(" + index + ")' id='" + index + "'/>" + "</td>";
    var createNewRow = "<tr id='DelRow_" + index + "'> " + indexCell + serialCell + itemNameCell + itemQuantityCell + itemPriceCell + LineTotalCell + actionCell + " </tr>";

    $("#OrderDetailsTable").append(createNewRow);
    $("#ItemName").val("");
    $("#ItemQuantity").val("");
    $("#ItemPrice").val("");

}

var GetDeleteId = function (Id) {
    $("#DelRow_" + Id).remove();
    TotalAmount();

}

function GetSelectedItem() {

    var ItemName = $("#ItemName").val();
    var ItemQuantity = $("#ItemQuantity").val();
    var ItemPrice = $("#ItemPrice").val();
    var itemText = $("#ItemName option:selected").text();
    var Item = {
        "ItemName": ItemName,
        "ItemQuantity": ItemQuantity,
        "ItemPrice": ItemPrice,
        "itemText": itemText
    }
    return Item;
}

function TotalAmount() {
    var sumOfTotal = 0;
    if ($("#OrderDetailsTable").children("tr").length == 0) {
        $("#Total").val(0);
    }
    else {
        $("#OrderDetailsTable tr ").each(function (index, value) {
            var Total = parseFloat(($(this).find(".total").html()));
            sumOfTotal = sumOfTotal + Total;
            $("#Total").val(sumOfTotal);
        });
    }
}
