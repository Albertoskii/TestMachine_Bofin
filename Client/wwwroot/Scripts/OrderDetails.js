//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
});
//Load Data function
function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.id + '</td>';
                html += '<td>' + item.date + '</td>';
                html += '<td>' + item.quantity + '</td>';
                html += '<td>' + item.prod_Id + '</td>';
                html += '<td><a href="#" onclick="getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delete(' + item.id + ')">Delete</a> | <a href="/Home/Invoices/' + item.id + '"> Invoice</a > </td>';
                 
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//GET By ID
function getbyID(id) {
    $('#OrderID').css('border-color', 'grey');
    $('#Date').css('border-color', 'lightgrey');
    $('#Quantity').css('border-color', 'lightgrey');
    $('#Prod_Id').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Home/GetbyID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#OrderID').val(result.id);
            $('#Date').val(result.date);
            $('#Quantity').val(result.quantity);
            $('#Prod_Id').val(result.prod_Id);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


//Add Data F
function Add() {

    var orderDetails = new Object(); 
    orderDetails.Id = 0;
    orderDetails.Date = $('#Date').val();
    orderDetails.Quantity = parseInt( $('#Quantity').val());
    orderDetails.Prod_Id = parseInt( $('#Prod_Id').val());
       
    
    $.ajax({
        type: "POST",
        url: "/Home/AddOrder/",
        data: {
            data: JSON.stringify(orderDetails)
        },
        dataType: 'json',
        statusCode: {
            200: function () {
                alert("Element Added");
                loadData();
                $('#myModal').modal('hide');

            },
        }
   });
}


//DELETE FUNCTION
function Delete(id) {
    var ans = confirm("Are you sure you want to delete this Order Detail?");
    if (ans) {
        $.ajax({
            url: "/Home/DeleteOrder/" + id,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            statusCode: {
                200: function () {
                    alert("Element Deleted");
                    loadData();
                }
            }
        });
    }
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#OrderID').val("");
    $('#Quantity').val("");
    $('#Date').val("");
    $('#Prod_Id').val("");
  
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Quantity').css('border-color', 'lightgrey');
    $('#Date').css('border-color', 'lightgrey');
    $('#Prod_Id').css('border-color', 'lightgrey');
}


function Update() {
    var orderDetails = new Object();
    orderDetails.Id = $('#OrderID').val();
    orderDetails.Date = $('#Date').val();
    orderDetails.Quantity = $('#Quantity').val();
    orderDetails.Prod_Id = $('#Prod_Id').val();
    $.ajax({
        type: "POST",
        url: "/Home/UpdateOrder/",
        data: {
            data: JSON.stringify(orderDetails)
        },
        dataType: 'json',
        statusCode: {
            200: function () {
                alert("Element Updated");
                loadData();
                $('#myModal').modal('hide');
            },
        }
    });
}